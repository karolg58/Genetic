using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;

public class EmasSelection : SelectionBase
{
    public int ReproductionThreshhold {get;private set;} = 75;
    public EmasSelection(int reproductionThreshhold = 75) : base(2)
    {
        ReproductionThreshhold = reproductionThreshhold;
    }

    protected override IList<IChromosome> PerformSelectChromosomes(int number, Generation generation)
    {
        var list = generation.Chromosomes.Where(x => (x as Chromosome).CurrentEnergy > ReproductionThreshhold).ToList();
        // foreach (var parent in list.Select(x => x as Chromosome))
        // {
        //     parent.CurrentEnergy -= parent.DefaultEnergy;
        // }
        return list;
    }
}