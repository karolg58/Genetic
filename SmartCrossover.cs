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
        public static List<int> serverFreeMemoryBase = DataModel.servers.Select(x => x.capacity).ToList();

        public SmartCrossover()
            : base(2, 1)
        {
            IsOrdered = false;
        }

        protected override IList<IChromosome> PerformCross(IList<IChromosome> parents)
        {
            var parent1 = parents[0] as Chromosome;
            var parent2 = parents[1] as Chromosome;

            parent1.CurrentEnergy -= parent1.DefaultEnergy/2;
            parent2.CurrentEnergy -= parent2.DefaultEnergy/2;

            var leftVideoAssignments = parent1.VideoAssignments;
            var rightVideAssignments = parent2.VideoAssignments;
            var distinctVideoAssignmnets = leftVideoAssignments.Concat(rightVideAssignments).Distinct();

            var childGenotype = GetChildGenotype(distinctVideoAssignmnets);

            Chromosome chromosome = new Chromosome(childGenotype.Count, parent1.DefaultEnergy);
            chromosome.ReplaceGenes(childGenotype);
            var newChromosomes = new List<IChromosome>() { chromosome };
            return newChromosomes;
        }

        private static List<VideoAssignment> GetChildGenotype(IEnumerable<VideoAssignment> distinctVideoAssignmnets)
        {
            var childVideoAssignments = new List<VideoAssignment>(distinctVideoAssignmnets);
            var fittingChildVideoAssignments = new List<VideoAssignment>();
            var serverFreeMemory = new List<int>(serverFreeMemoryBase);
            while (childVideoAssignments.Any())
            {
                var videoAssignment = childVideoAssignments[RandomizationProvider.Current.GetInt(0, childVideoAssignments.Count)];
                if (serverFreeMemory[videoAssignment.server.id] >= videoAssignment.video.size)
                {
                    serverFreeMemory[videoAssignment.server.id] -= videoAssignment.video.size;
                    fittingChildVideoAssignments.Add(videoAssignment);
                    childVideoAssignments.Remove(videoAssignment);
                }
                else
                {
                    childVideoAssignments.Remove(videoAssignment);
                }
            }

            return fittingChildVideoAssignments;
        }
    }
}