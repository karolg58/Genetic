using System;
using System.Diagnostics;
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
        public static Stopwatch ReadWatch {get;set;} = new Stopwatch();
        public static Stopwatch WholeFitnessWatch {get;set;} = new Stopwatch();
        public static Stopwatch GenerationWatch {get;set;} = new Stopwatch();

        static void Main(string[] args)
        {
            DataModel data_model = new DataModel();
            InputReader input_reader = new InputReader();
            ReadWatch.Start();
            // input_reader.ReadDataFromFile("data/me_at_the_zoo.in");
            input_reader.ReadDataFromFile("data/videos_worth_spreading.in");
            // input_reader.ReadDataFromFile("data/trending_today.in");
            // input_reader.ReadDataFromFile("data/kittens.in");
            ReadWatch.Stop();
            Console.WriteLine($"{ReadWatch.ElapsedMilliseconds}");
            var selection = new EliteSelection();
            var crossover = new CutAndSpliceCrossover();
            var mutation = new Mutation(30);
            
            var fitness = new Fitness();
            var chromosome = new Chromosome(3000);
            var population = new Population(1000, 1000, chromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.MutationProbability = 0.25f;
            ga.Termination = new GenerationNumberTermination(250);
            int i=0;
            ga.GenerationRan += (_,__) => {
                var bestC = ga.BestChromosome as Chromosome;
                SaveToFile.Save(bestC, @"data\output");
                Console.WriteLine($@"G.num.: {i++}, Fitness: {ga.BestChromosome.Fitness}, Lenght: {ga.BestChromosome.Length}, generation time = {GenerationWatch.ElapsedMilliseconds}, Fitness time = {WholeFitnessWatch.ElapsedMilliseconds}, Fitness percentage: {((float)WholeFitnessWatch.ElapsedMilliseconds/(float)GenerationWatch.ElapsedMilliseconds)}");

                GenerationWatch.Restart();
                WholeFitnessWatch.Reset();
            };

            Console.WriteLine("GA running...");
            GenerationWatch.Start();
            ga.Start();
            Console.WriteLine($"GA done in {ga.GenerationsNumber} generations.");

            var bestChromosome = ga.BestChromosome as Chromosome;
            Console.WriteLine($"Best solution found has fitness: {bestChromosome.Fitness}.");

            Console.WriteLine("Done");
        }
    }
}
