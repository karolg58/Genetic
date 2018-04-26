using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using genetic;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;

namespace GeneticSharp.Domain.Crossovers
{
    /// <summary>
    /// Cut and Splice crossover.
    /// <remarks>
    /// Results in a change in length of the children strings. The reason for this difference is that each parent string has a separate choice of crossover point.
    /// <see href="http://en.wikipedia.org/wiki/Crossover_(genetic_algorithm)#.22Cut_and_splice.22">Wikipedia</see>
    /// </remarks>
    /// </summary>
    [DisplayName("Cut and Splice")]
    public class SmartCrossover : CrossoverBase
    {
        public static List<int> serverFreeMemoryBase = DataModel.servers.Select(x => x.capacity).ToList();
        
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartCrossover"/> class.
        /// </summary>
        public SmartCrossover()
            : base(2, 2)
        {
            IsOrdered = false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Performs the cross with specified parents generating the children.
        /// </summary>
        /// <param name="parents">The parents chromosomes.</param>
        /// <returns>
        /// The offspring (children) of the parents.
        /// </returns>
        protected override IList<IChromosome> PerformCross(IList<IChromosome> parents)
        {
            var parent1 = parents[0] as Chromosome;
            var parent2 = parents[1] as Chromosome;

            parent1.CurrentEnergy -= parent1.DefaultEnergy;
            parent2.CurrentEnergy -= parent2.DefaultEnergy;

            var genes1 = parent1.GetGenes().ToList();
            var genes2 = parent2.GetGenes().ToList();

            var newChromosomes = new List<IChromosome>();

            for(int i = 0; i < 2; i++){
                Console.WriteLine("Start chromosome ", i);
                var genes = new Dictionary<Gene, int>();

                var serverFreeMemory = new List<int>(serverFreeMemoryBase);

                while(genes1.Count > 0 || genes2.Count > 0){
                    Console.WriteLine("Start gene");
                    if(genes1.Count > 0){
                        Console.WriteLine("Start genes1 ");
                        int idx = RandomizationProvider.Current.GetInt(0, genes1.Count());
                        var gene = genes1[idx];
                        if(!genes.ContainsKey(gene)){
                            Console.WriteLine("genes1 is absent");
                            var a = gene.Value as VideoAssignment;
                            if(serverFreeMemory[a.server.id] >= a.video.size){
                                Console.WriteLine("loading genes1");
                                serverFreeMemory[a.server.id] -= a.video.size;
                                Console.WriteLine("memory decremented");
                                genes.Add(gene, 1);
                                Console.WriteLine("genes1 is loaded");
                            }
                        }
                        Console.WriteLine("delete genes1");
                        genes1.Remove(gene);
                        Console.WriteLine("genes1 deleted");
                    }   
                }
                var smartChromosome = new Chromosome(genes.Count);
                int id = 0;
                foreach(var gene in genes.Keys){
                    Console.WriteLine("genes1 is inserted");
                    smartChromosome.ReplaceGene(id, gene);
                    id++;
                }
                newChromosomes.Add(smartChromosome);
            }
            Console.WriteLine("End crossover");
            return newChromosomes;
        }
        #endregion
    }
}