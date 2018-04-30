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
    public static class Runner
    {
        public static Stopwatch WholeFitnessWatch {get;set;} = new Stopwatch();
        public static Stopwatch WholeCrossoverWatch {get;set;} = new Stopwatch();
        public static Stopwatch Crossover1Watch {get;set;} = new Stopwatch();
        public static Stopwatch Crossover2Watch {get;set;} = new Stopwatch();
        public static Stopwatch GenerationWatch {get;set;} = new Stopwatch();
        public static Stopwatch ForPlotsWatch {get;set;} = new Stopwatch();
        public static void defaultGA(Population population, Fitness fitness, MutationBase mutation, TerminationBase termination, float mutationPropability, float crossoverPropability, string version){
            string plotsFilePath = SaveToFile.initFilesForPlots(version + "_default", DateTime.Now.Ticks.ToString());//delete if exists
            int i = 0;           
            var ga = new GeneticAlgorithm(population, fitness, new EliteSelection(), new CutAndSpliceCrossover(), mutation);
            ga.MutationProbability = mutationPropability;
            // ga.CrossoverProbability = crossoverPropability;
            ga.Termination = termination;
            ga.GenerationRan += (_, __) => i = PrintGenerationData(i, ga, plotsFilePath);
            ga.TerminationReached += (_, __) => PrintEndData(ga);

            Console.WriteLine(version + "_default GA running...");
            GenerationWatch.Start();
            ga.Start();
        }

        public static void emasGA(Population population, Fitness fitness, MutationBase mutation, TerminationBase termination, float mutationPropability, float crossoverPropability, string version){
            string plotsFilePath = SaveToFile.initFilesForPlots(version + "_emas", DateTime.Now.Ticks.ToString());//delete if exists
            int i = 0;
            var emasAlgorithm = new GeneticAlgorithm(population, fitness, new EmasSelection(), new SmartCrossover(), mutation);
            emasAlgorithm.Reinsertion = new EmasReinsertion();
            emasAlgorithm.MutationProbability = mutationPropability;
            emasAlgorithm.CrossoverProbability = crossoverPropability;
            emasAlgorithm.Termination = termination;
            emasAlgorithm.GenerationRan += (_,__) => i = PrintGenerationData(i, emasAlgorithm, plotsFilePath);
            emasAlgorithm.TerminationReached += (_,__) => PrintEndData(emasAlgorithm);

            Console.WriteLine(version + "_emas GA running...");
            GenerationWatch.Start();
            emasAlgorithm.Start();
        }

        private static void PrintEndData(GeneticAlgorithm ga)
        {
            Console.WriteLine($"GA done in {ga.GenerationsNumber} generations.");

            var bestChromosome = ga.BestChromosome as Chromosome;
            Console.WriteLine($"Best solution found has fitness: {bestChromosome.Fitness}.");
        }

        private static int PrintGenerationData(int i, GeneticAlgorithm ga, string plotsFilePath)
        {
            var bestC = ga.BestChromosome as Chromosome;
            //SaveToFile.Save(bestC, @"data\output");
            if(i == 0) ForPlotsWatch.Start();
            SaveToFile.PlotsData(i, bestC.Fitness, plotsFilePath, ForPlotsWatch);
            Console.WriteLine($@"G.num.: {i++}, Pop. Count: {ga.Population.CurrentGeneration.Chromosomes.Count}, Energy Sum: {ga.Population.CurrentGeneration.Chromosomes.Sum(x => (x as Chromosome).CurrentEnergy)} Fitness: {ga.BestChromosome.Fitness}, Lenght: {ga.BestChromosome.Length}, generation time = {GenerationWatch.ElapsedMilliseconds}, Crossover time = {WholeCrossoverWatch.ElapsedMilliseconds}, cross1: {Crossover1Watch.ElapsedMilliseconds}, cross2: {Crossover2Watch.ElapsedMilliseconds}");

            GenerationWatch.Restart();
            WholeFitnessWatch.Reset();
            Crossover1Watch.Reset();
            Crossover2Watch.Reset();
            WholeCrossoverWatch.Reset();
            return i;
        }
    }
}