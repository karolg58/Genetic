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
        public static TimeSpan fitnessGenerationTimeStart {get;set;}
        public static TimeSpan fitnessGenerationTimeMiddle {get;set;}
        public static TimeSpan fitnessGenerationTimeEnd {get;set;}
        public static TimeSpan T11 {get;set;}
        public static TimeSpan fitnessGenerationTime {get;set;} = new TimeSpan(0);
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
            var population = new Population(100, 100, chromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new FitnessStagnationTermination(1000);
            int i=0;
            DateTime t1 = DateTime.Now;
            ga.GenerationRan += (_,__) => {
                DateTime t2 = DateTime.Now;
                Console.WriteLine($"Generation: {i++}, Fitness: {ga.BestChromosome.Fitness}, Best genotype lenght: {ga.BestChromosome.Length}, Counting time = {(t2 - t1).Ticks}, Fitness time = {fitnessGenerationTime.Ticks}, Percentage: {((float)fitnessGenerationTime.Ticks/(float)(t2 - t1).Ticks)}");
                // Console.WriteLine($"Generation: {i++}, Fitness: {ga.BestChromosome.Fitness}, Best genotype lenght: {ga.BestChromosome.Length},  PercentageMiddle: {((float)fitnessGenerationTimeMiddle.Ticks/(float)(t2 - t1).Ticks)}, PercentageFitness: {((float)T11.Ticks/(float)(t2 - t1).Ticks)}");
                fitnessGenerationTime = new TimeSpan(0);
                fitnessGenerationTimeStart = new TimeSpan(0);
                fitnessGenerationTimeMiddle = new TimeSpan(0);
                fitnessGenerationTimeEnd = new TimeSpan(0);
                T11 = new TimeSpan(0);
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
