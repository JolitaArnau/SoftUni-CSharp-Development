﻿namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;

    public class ExitCommand : ICommand
    {
        public string Execute(string command, string[] data)
        {
            Environment.Exit(0);
            return "Bye-bye!";
        }
    }
}