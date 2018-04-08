using System;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;

namespace genetic
{
    class Program
    {
        static void Main(string[] args)
        {
            DataModel data_model = new DataModel();
            InputReader input_reader = new InputReader();
            input_reader.ReadDataFromFile("data/me_at_the_zoo.in");

            var selection = new EliteSelection();
            var crossover = new CutAndSpliceCrossover();
            var mutation = new UniformMutation(true);
            
            var fitness = new Fitness();
            var chromosome = new Chromosome(1000);
            var population = new Population(50, 50, chromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new GenerationNumberTermination(100);

            Console.WriteLine("GA running...");
            ga.Start();
            Console.WriteLine($"GA done in {ga.GenerationsNumber} generations.");

            var bestChromosome = ga.BestChromosome as Chromosome;
            Console.WriteLine($"Best solution found has fitness: {bestChromosome.Fitness}.");
		    Console.ReadKey();


            Console.WriteLine("Done");
        }
    }
}
