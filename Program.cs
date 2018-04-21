using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public static Stopwatch ReadWatch { get; set; } = new Stopwatch();
        public static string version = "2";//code version (should be the same for muliple running the same code)

        static void Main(string[] args)
        {
            var timeEvolvingTermination = new TimeEvolvingTermination(new TimeSpan(0, 1, 0));
            //var generationsNumberTermination = new GenerationNumberTermination(500);
            const float mutationPropability = 1f;
            const float crossoverPropability = 1f;

            var fileNames = new List<string> { "me_at_the_zoo", "videos_worth_spreading", "trending_today", "kittens" };
            var repeatingCounter = 5;

            for (int i = 0; i < repeatingCounter; i++)
            {
                foreach (var fileName in fileNames)
                {
                    try
                    {
                        DataModel.Reset();//static fields reset
                        DataModel data_model = new DataModel();
                        InputReader input_reader = new InputReader();                     
                        ReadWatch.Start();
                        input_reader.ReadDataFromFile("data/input/" + fileName + ".in");
                        Fitness.Reset();//static fields reset, has to be after read data
                        ReadWatch.Stop();
                        Console.WriteLine($"{ReadWatch.ElapsedMilliseconds}");

                        Runner.emasGA(
                            new Population(2000, 100000, new Chromosome(3000)),
                            new Fitness(),
                            new SwapMutation(10),
                            timeEvolvingTermination,
                            mutationPropability,
                            crossoverPropability,
                            fileName + "_" + version);

                        Runner.defaultGA(
                            new Population(2000, 100000, new Chromosome(3000)),
                            new Fitness(),
                            new SwapMutation(10),
                            timeEvolvingTermination,
                            mutationPropability,
                            crossoverPropability,
                            fileName + "_" + version);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        i--;
                    }
                }
            }

            Console.WriteLine("Done");
        }
    }
}
