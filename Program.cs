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
            var crossover = new UniformCrossover();
            var mutation = new FlipBitMutation();
            
            var chromosome = new BinaryChromosome(DataModel.possibleVideoAssignments.Count);
            var population = new Population(200, 200, chromosome);
            var fitness = new BinaryFitness();

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            int i=0;
            ga.GenerationRan += (x,y) => Console.WriteLine($"Generation: {i++}, Fitness: {ga.BestChromosome.Fitness}, Best length: {ga.BestChromosome.Length}");
            ga.Termination = new FitnessStagnationTermination(10000);

            Console.WriteLine("GA running...");
            ga.Start();
            Console.WriteLine($"GA done in {ga.GenerationsNumber} generations.");

            var bestChromosome = ga.BestChromosome;
            Console.WriteLine($"Best solution found has fitness: {bestChromosome.Fitness} {bestChromosome.Length}.");


            Console.WriteLine("Done");
        }
    }
}
