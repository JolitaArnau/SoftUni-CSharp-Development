using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using PetClinic.Models;

namespace PetClinic.DataProcessor
{
    using System;
    using PetClinic.Data;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context.Animals
                .Where(a => a.Passport.OwnerPhoneNumber.Equals(phoneNumber))
                .Select(e => new
                {
                    OwnerName = e.Passport.OwnerName,
                    AnimalName = e.Name,
                    Age = e.Age,
                    SerialNumber = e.Passport.SerialNumber,
                    RegisteredOn = e.Passport.RegistrationDate
                })
                .OrderBy(a => a.Age)
                .ThenBy(a => a.SerialNumber)
                .ToArray();

            return JsonConvert.SerializeObject(animals, Formatting.Indented,
                new JsonSerializerSettings() {DateFormatString = "dd-MM-yyyy"});
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var procedures = context.Procedures.Select(p => new
                {
                    Passport = p.Animal.PassportSerialNumber,
                    OwnerNumber = p.Animal.Passport.OwnerPhoneNumber,
                    DateTime = p.DateTime,
                    AnimalAids = p.ProcedureAnimalAids.Select(pa => new
                    {
                        Name = pa.AnimalAid.Name,
                        Price = pa.AnimalAid.Price,
                    }),
                    TotalPrice = p.ProcedureAnimalAids.Select(paa => paa.AnimalAid.Price).Sum()
                })
                .OrderBy(d => d.DateTime)
                .ThenBy(ps => ps.Passport)
                .ToArray();

            var xDoc = new XDocument(new XElement("Procedures"));

            foreach (var p in procedures)
            {
                xDoc.Root.Add
                (
                    new XElement("Procedure", 
                        new XElement("Passport", p.Passport),
                        new XElement("OwnerNumber", p.OwnerNumber),
                        new XElement("DateTime", p.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)),
                        new XElement("AnimalAids", p.AnimalAids.Select(aa => 
                            new XElement("AnimalAid", 
                                new XElement("Name", aa.Name),
                                new XElement("Price", aa.Price)))),
                        new XElement("TotalPrice", p.TotalPrice))
                );
            }

            return xDoc.ToString();
        }
    }
}