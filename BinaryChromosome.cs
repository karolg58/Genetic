using System.Collections.Generic;
using genetic;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;

public class BinaryChromosome : ChromosomeBase, IBinaryChromosome, IChromosome
{
    public List<VideoAssignment> VideoAssignments {get; private set;} = new List<VideoAssignment>(DataModel.possibleVideoAssignments);

    public BinaryChromosome(int length) : base(length)
    {
        for (int i = 0; i < length; i++)
        {
            ReplaceGene(i,GenerateGene(i));
        }
    }

    public void FlipGene(int index)
    {
        var value = (bool) GetGene (index).Value;
        VideoAssignments[index].IsActive=!value;
        ReplaceGene (index, new Gene (!value));
    }

    public override Gene GenerateGene(int geneIndex)
    {
        bool value = RandomizationProvider.Current.GetInt(0, 20) == 1;
        VideoAssignments[geneIndex].IsActive = value;
        return new Gene(value);
    }

    public override IChromosome CreateNew()
    {
        return new BinaryChromosome(Length);
    }
}