using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AutoMapper;
using Newtonsoft.Json;
using PetClinic.DataProcessor.Dto.Import;
using PetClinic.Models;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace PetClinic.DataProcessor
{
    using System;
    using PetClinic.Data;

    public class Deserializer
    {
        private const string FailureMessage = "Error: Invalid data.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var animalAids = JsonConvert.DeserializeObject<AnimalAid[]>(jsonString);

            var validEntries = new List<AnimalAid>();

            var sb = new StringBuilder();

            foreach (var animalAid in animalAids)
            {
                var animalAidExists = validEntries.Any(a => a.Name == animalAid.Name);

                if (!IsValid(animalAid) || animalAidExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                validEntries.Add(animalAid);
                sb.AppendLine(String.Format(SuccessMessage, animalAid.Name));
            }

            context.AddRange(validEntries);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var animals = JsonConvert.DeserializeObject<AnimalDto[]>(jsonString);

            var sb = new StringBuilder();

            var validEntries = new List<Animal>();

            foreach (var dto in animals)
            {
                var animal = Mapper.Map<Animal>(dto);

                var passportIsValid = IsValid(animal.Passport);

                var alreadyExists = validEntries.Any(a => a.Passport.SerialNumber == animal.Passport.SerialNumber);

                if (!IsValid(animal) || !passportIsValid || alreadyExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                validEntries.Add(animal);
                sb.AppendLine(
                    String.Format(SuccessMessage, $"{animal.Name} Passport №: {animal.Passport.SerialNumber}"));
            }

            context.Animals.AddRange(validEntries);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);
            var elements = xDoc.Root.Elements();

            var validEntries = new List<Vet>();

            var sb = new StringBuilder();

            foreach (var vetEntry in elements)
            {
                var name = vetEntry.Element("Name")?.Value;
                var profession = vetEntry.Element("Profession")?.Value;
                var ageString = vetEntry.Element("Age")?.Value;
                var phoneNumber = vetEntry.Element("PhoneNumber")?.Value;


                var age = 0;

                if (ageString != null)
                {
                    age = int.Parse(ageString);
                }

                Vet vet = new Vet()
                {
                    Name = name,
                    Profession = profession,
                    Age = age,
                    PhoneNumber = phoneNumber,
                };

                var phoneNumberExists = validEntries.Any(v => v.PhoneNumber == vet.PhoneNumber);

                if (!IsValid(vet) || phoneNumberExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                validEntries.Add(vet);
                sb.AppendLine(String.Format(SuccessMessage, vet.Name));
            }

            context.Vets.AddRange(validEntries);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);
            var elements = xDoc.Root.Elements();

            var sb = new StringBuilder();
            var validEntries = new List<Procedure>();

            foreach (var el in elements)
            {
                var vetName = el.Element("Vet")?.Value;
                var passportId = el.Element("Animal")?.Value;
                var dateTimeString = el.Element("DateTime")?.Value;

                var vetId = context.Vets.SingleOrDefault(v => v.Name == vetName)?.Id;
                var passportExists = context.Passports.Any(p => p.SerialNumber == passportId);

                var dateIsValid = DateTime
                    .TryParseExact(dateTimeString, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out DateTime dateTime);

                var animalAidElements = el.Element("AnimalAids")?.Elements();

                if (vetId == null || !passportExists || animalAidElements == null || !dateIsValid)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var animalAidIds = new List<int>();
                var allAidsExist = true;

                foreach (var aid in animalAidElements)
                {
                    var aidName = aid.Element("Name")?.Value;

                    var aidId = context.AnimalAids.SingleOrDefault(a => a.Name == aidName)?.Id;

                    if (aidId == null || animalAidIds.Any(id => id == aidId))
                    {
                        allAidsExist = false;
                        break;
                    }

                    animalAidIds.Add(aidId.Value);
                }

                if (!allAidsExist)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var procedure = new Procedure()
                {
                    VetId = vetId.Value,
                    AnimalId = context.Animals.Single(a => a.PassportSerialNumber == passportId).Id,
                    DateTime = dateTime,
                };

                foreach (var id in animalAidIds)
                {
                    var mapping = new ProcedureAnimalAid()
                    {
                        Procedure = procedure,
                        AnimalAidId = id
                    };

                    procedure.ProcedureAnimalAids.Add(mapping);
                }

                var isValid = IsValid(procedure);

                if (!isValid)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                validEntries.Add(procedure);
                sb.AppendLine("Record successfully imported.");
            }

            context.Procedures.AddRange(validEntries);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static Animal FindAnimal(PetClinicContext context, string animalSerialNumber)
        {
            var animal = context.Animals.SingleOrDefault(a => a.PassportSerialNumber == animalSerialNumber);

            return animal;
        }

        private static AnimalAid FindAnimalAid(PetClinicContext context, string animalAidName)
        {
            var animalAid = context.AnimalAids.SingleOrDefault(ai => ai.Name == animalAidName);

            return animalAid;
        }

        private static Vet FindVet(PetClinicContext context, string vetName)
        {
            var vet = context.Vets.SingleOrDefault(v => v.Name == vetName);

            return vet;
        }


        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}