using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Reinsertions;

public class EmasResinsertion : ReinsertionBase
{
    public EmasResinsertion() : base(true, true)
    {
    }

    protected override IList<IChromosome> PerformSelectChromosomes(IPopulation population, IList<IChromosome> offspring, IList<IChromosome> parents)
    {
        var c = population.CurrentGeneration.Chromosomes.Select(x => x as Chromosome).ToList();
        var newPopulation = c.Concat(offspring.Select(x => x as Chromosome)).ToList();
        PerformMeetings(newPopulation);
        var list = newPopulation.Where(x => x.CurrentEnergy > 0).Select(x => x as IChromosome).ToList();
        return list;
    }

    private void PerformMeetings(IList<Chromosome> chromosomes)
    {
        foreach (var chromosome in chromosomes)
        {
            chromosome.DoMeeteing(chromosomes[GeneticSharp.Domain.Randomizations.RandomizationProvider.Current.GetInt(0, chromosomes.Count)]);
        }
    }
}