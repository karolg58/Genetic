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
            var mutation = new Mutation();
            
            var fitness = new Fitness();
            var chromosome = new Chromosome(100);
            var population = new Population(1000, 1000, chromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new FitnessStagnationTermination(1000);
            int i=0;
            DateTime t1 = DateTime.Now;
            ga.GenerationRan += (_,__) => {
                DateTime t2 = DateTime.Now;
                Console.WriteLine($"Generation: {i++}, Fitness: {ga.BestChromosome.Fitness}, Best genotype lenght: {ga.BestChromosome.Length}, Counting time = {(t2 - t1).TotalMilliseconds}ms");
                t1 = t2;
                var bestC = ga.BestChromosome as Chromosome;
                SaveToFile.Save(bestC, @"data\output");
            };

            Console.WriteLine("GA running...");
            ga.Start();
            Console.WriteLine($"GA done in {ga.GenerationsNumber} generations.");

            var bestChromosome = ga.BestChromosome as Chromosome;
            Console.WriteLine($"Best solution found has fitness: {bestChromosome.Fitness}.");

            Console.WriteLine("Done");
        }
    }
}
