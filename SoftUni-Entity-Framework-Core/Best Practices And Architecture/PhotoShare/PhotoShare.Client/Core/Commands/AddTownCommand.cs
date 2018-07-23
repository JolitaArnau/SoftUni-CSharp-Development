namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;

    public class AddTownCommand : ICommand
    {
        private readonly ITownService townService;

        public AddTownCommand(ITownService townService)
        {
            this.townService = townService;
        }

        public string Execute(string command, string[] data)
        {
            if (data.Length != 2)
            {
                throw new InvalidOperationException($"Command {command} not valid!");
            }

            var townName = data[0];
            var countryName = data[1];

            this.townService.Create(townName, countryName);
            
            return $"Town {townName} was added successfully!";
        }
    }
}