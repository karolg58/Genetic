using System.Collections.Generic;
using genetic;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Randomizations;

public class Chromosome : ChromosomeBase
{
    public List<VideoAssignment> VideoAssignments {get; private set;}
    protected Chromosome(int length) : base(length)
    {
        VideoAssignments = new List<VideoAssignment>(length);
        for (int i = 0; i < length; i++)
        {
            ReplaceGene(i,GenerateGene(i));
        }
    }

    public override IChromosome CreateNew()
    {
        return new Chromosome(Length);
    }

    public override Gene GenerateGene(int geneIndex)
    {
        var video = DataModel.videos[RandomizationProvider.Current.GetInt(0,DataModel.number_of_videos_V)];
        var server = DataModel.servers[RandomizationProvider.Current.GetInt(0,DataModel.number_of_cache_servers_C)];
        var assignment = new VideoAssignment(server,video);
        VideoAssignments[geneIndex] = assignment;
        return new Gene(assignment);
    }
}