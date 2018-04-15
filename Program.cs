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
        public static TimeSpan fitnessGenerationTime1 {get;set;}
        public static TimeSpan fitnessGenerationTime2 {get;set;}
        public static TimeSpan fitnessGenerationTime3 {get;set;}
        public static TimeSpan fitnessGenerationTime {get;set;} = new TimeSpan(0);
        static void Main(string[] args)
        {
            DataModel data_model = new DataModel();
            InputReader input_reader = new InputReader();
            input_reader.ReadDataFromFile("data/videos_worth_spreading.in");

            var selection = new EliteSelection();
            var crossover = new CutAndSpliceCrossover();
            var mutation = new Mutation();
            
            var fitness = new Fitness();
            var chromosome = new Chromosome(100);
            var population = new Population(100, 100, chromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new FitnessStagnationTermination(1000);
            int i=0;
            DateTime t1 = DateTime.Now;
            ga.GenerationRan += (_,__) => {
                DateTime t2 = DateTime.Now;
                Console.WriteLine($"Generation: {i++}, Fitness: {ga.BestChromosome.Fitness}, Best genotype lenght: {ga.BestChromosome.Length}, Counting time = {(t2 - t1).Ticks}, Fitness time = {fitnessGenerationTime.Ticks}, Percentage: {((float)fitnessGenerationTime.Ticks/(float)(t2 - t1).Ticks)}");
                fitnessGenerationTime = new TimeSpan(0);
                t1 = t2;
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
