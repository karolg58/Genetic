using System;
using System.Diagnostics;
using System.Linq;
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
            input_reader.ReadDataFromFile("data/me_at_the_zoo.in");
            // input_reader.ReadDataFromFile("data/videos_worth_spreading.in");
            // input_reader.ReadDataFromFile("data/trending_today.in");
            // input_reader.ReadDataFromFile("data/kittens.in");
            ReadWatch.Stop();
            Console.WriteLine($"{ReadWatch.ElapsedMilliseconds}");
            var mutation = new SwapMutation(1);
            var reinsertion = new EmasResinsertion();

            var fitness = new Fitness();
            var chromosome = new Chromosome(3000);
            var population = new Population(200, 100000, chromosome);
            var timeEvolvingTermination = new TimeEvolvingTermination(new TimeSpan(0, 0, 15));

            var emasAlgorithm = new GeneticAlgorithm(population, fitness, new EmasSelection(), new CutAndSpliceEmasCrossover(), mutation);
            emasAlgorithm.Reinsertion = reinsertion;
            emasAlgorithm.MutationProbability = 0.25f;
            emasAlgorithm.CrossoverProbability = 1f;
            emasAlgorithm.Termination = timeEvolvingTermination;
            int i = 0;
            emasAlgorithm.GenerationRan += (_,__) =>
            {
                // i = PrintGenerationData(i, emasAlgorithm);
            };
            emasAlgorithm.TerminationReached += (_,__) => PrintEndData(emasAlgorithm);

            var ga = new GeneticAlgorithm(population, fitness, new EliteSelection(), new CutAndSpliceCrossover(), mutation);
            ga.MutationProbability = 0.25f;
            ga.CrossoverProbability = 1f;
            ga.Termination = timeEvolvingTermination;
            i = 0;
            ga.GenerationRan += (_, __) =>
            {
                i = PrintGenerationData(i, ga);
            };
            ga.TerminationReached += (_, __) => PrintEndData(ga);

            Console.WriteLine("GA running...");
            GenerationWatch.Start();
            ga.Start();
            emasAlgorithm.Start();

            Console.WriteLine("Done");
        }

        private static void PrintEndData(GeneticAlgorithm ga)
        {
            Console.WriteLine($"GA done in {ga.GenerationsNumber} generations.");

            var bestChromosome = ga.BestChromosome as Chromosome;
            Console.WriteLine($"Best solution found has fitness: {bestChromosome.Fitness}.");
        }

        private static int PrintGenerationData(int i, GeneticAlgorithm ga)
        {
            var bestC = ga.BestChromosome as Chromosome;
            SaveToFile.Save(bestC, @"data\output");
            Console.WriteLine($@"G.num.: {i++}, Population Count: {ga.Population.CurrentGeneration.Chromosomes.Count}, Energy Sum: {ga.Population.CurrentGeneration.Chromosomes.Sum(x => (x as Chromosome).CurrentEnergy)} Fitness: {ga.BestChromosome.Fitness}, Lenght: {ga.BestChromosome.Length}, generation time = {GenerationWatch.ElapsedMilliseconds}, Fitness time = {WholeFitnessWatch.ElapsedMilliseconds}, Fitness percentage: {((float)WholeFitnessWatch.ElapsedMilliseconds / (float)GenerationWatch.ElapsedMilliseconds)}");

            GenerationWatch.Restart();
            WholeFitnessWatch.Reset();
            return i;
        }
    }
}
