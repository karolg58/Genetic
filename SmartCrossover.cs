using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using genetic;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;

namespace GeneticSharp.Domain.Crossovers
{
    public class SmartCrossover : CrossoverBase
    {
        public SmartCrossover()
            : base(2, 1)
        {
            IsOrdered = false;
        }

        protected override IList<IChromosome> PerformCross(IList<IChromosome> parents)
        {
            Runner.WholeCrossoverWatch.Start();
            var parent1 = parents[0] as Chromosome;
            var parent2 = parents[1] as Chromosome;

            parent1.CurrentEnergy -= parent1.DefaultEnergy/2;
            parent2.CurrentEnergy -= parent2.DefaultEnergy/2;

            var leftVideoAssignments = parent1.VideoAssignments;
            var rightVideAssignments = parent2.VideoAssignments;
            var distinctVideoAssignmnets = leftVideoAssignments.Concat(rightVideAssignments).Distinct();

            Runner.Crossover1Watch.Start();
            var childGenotype = GetChildGenotype(distinctVideoAssignmnets);
            Runner.Crossover1Watch.Stop();

            Runner.Crossover2Watch.Start();
            Chromosome chromosome = new Chromosome(childGenotype.Count, parent1.DefaultEnergy);
            chromosome.ReplaceGenes(childGenotype);
            var newChromosomes = new List<IChromosome>() { chromosome };
            Runner.Crossover2Watch.Stop();
            Runner.WholeCrossoverWatch.Stop();
            return newChromosomes;
        }

        private static List<VideoAssignment> GetChildGenotype(IEnumerable<VideoAssignment> distinctVideoAssignmnets)
        {
            var count = distinctVideoAssignmnets.Count();
            var childVideoAssignments = new List<VideoAssignment>(distinctVideoAssignmnets.OrderBy(x => RandomizationProvider.Current.GetInt(0,count*32)));
            var fittingChildVideoAssignments = new List<VideoAssignment>();
            var serverFreeMemory = DataModel.servers.Select(x => x.capacity).ToList();
            foreach (var videoAssignment in childVideoAssignments)
            {
                if (serverFreeMemory[videoAssignment.server.id] >= videoAssignment.video.size)
                {
                    serverFreeMemory[videoAssignment.server.id] -= videoAssignment.video.size;
                    fittingChildVideoAssignments.Add(videoAssignment);
                }

            }
            return fittingChildVideoAssignments;
        }
    }
}