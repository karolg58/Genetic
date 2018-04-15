using System.Collections.Generic;
using System.Linq;
using genetic;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Randomizations;

public class Chromosome : ChromosomeBase
{
    public List<VideoAssignment> VideoAssignments { 
        get => this.GetGenes().Select(x => x.Value as VideoAssignment).ToList();
    }
    public Chromosome(int length) : base(length)
    {
        // VideoAssignments = new List<VideoAssignment>(length);
        for (int i = 0; i < length; i++)
        {
            // VideoAssignments.Add(null);
            ReplaceGene(i, GenerateGene(i));
        }
    }

    public override IChromosome CreateNew()
    {
        return new Chromosome(Length);
    }

    public override Gene GenerateGene(int geneIndex)
    {
        VideoAssignment assignment = GetAssignment();
        // VideoAssignments[geneIndex] = assignment;
        return new Gene(assignment);
    }

    private static VideoAssignment GetAssignment()
    {
        var video = DataModel.videos[RandomizationProvider.Current.GetInt(0, DataModel.number_of_videos_V)];
        var server = DataModel.servers[RandomizationProvider.Current.GetInt(0, DataModel.number_of_cache_servers_C)];
        var assignment = new VideoAssignment(server, video);
        return assignment;
    }

    public void ReplaceGenes(List<VideoAssignment> newVideoAssignments)
    {
        while (newVideoAssignments.Count<2)
        {
            newVideoAssignments.Add(GetAssignment());
            newVideoAssignments = newVideoAssignments.Distinct().ToList();
        }
        // VideoAssignments = newVideoAssignments;
        Resize(newVideoAssignments.Count);
        for (int i = 0; i < newVideoAssignments.Count; i++)
        {
            ReplaceGene(i, new Gene(newVideoAssignments[i]));
        }
    }
}