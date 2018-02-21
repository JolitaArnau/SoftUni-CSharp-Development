using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var line = Console.ReadLine();

        var trainers = new List<Trainer>();

        while (!line.Equals("Tournament"))
        {
            var trainerAndPokemon = line.Split();

            ParseTrainersAndPokemons(trainerAndPokemon, trainers);

            line = Console.ReadLine();
        }

        var command = Console.ReadLine();

        while (!command.Equals("End"))
        {
            var element = command;

            FilterResult(trainers, element);

            command = Console.ReadLine();
        }

        foreach (var trainer in trainers.OrderByDescending(b => b.NumberOfBadges))
        {
            Console.WriteLine($"{trainer.Name} {trainer.NumberOfBadges} {trainer.Pokemons.Count}");
        }
    }

    private static void FilterResult(List<Trainer> trainers, string element)
    {
        foreach (var trainer in trainers)
        {
            if (trainer.Pokemons.Any(e => e.Element.Equals(element)))
            {
                trainer.NumberOfBadges++;
            }
            else
            {
                foreach (var pokemon in trainer.Pokemons)
                {
                    pokemon.Health -= 10;
                }
            }

            trainer.Pokemons.Where(h => h.Health > 0).ToList();
        }
    }

    private static void ParseTrainersAndPokemons(string[] trainerAndPokemon, List<Trainer> trainers)
    {
        var trainerName = trainerAndPokemon[0];

        var pokemonName = trainerAndPokemon[1];
        var pokemonElement = trainerAndPokemon[2];
        var pokemonHealth = int.Parse(trainerAndPokemon[3]);

        if (trainers.All(t => t.Name != trainerName))
        {
            trainers.Add(new Trainer(trainerName));
        }


        var trainer = trainers.First(t => t.Name == trainerName);
        trainer.Pokemons.Add(new Pokemon(pokemonName, pokemonElement, pokemonHealth));
    }
}