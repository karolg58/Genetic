using System.Linq;
using genetic;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Randomizations;

public class BruteforceMutation : MutationBase
{
    public int Intesity { get; private set; } = 1;
    public BruteforceMutation(int intesity)
    {
        Intesity = intesity;
    }

    public BruteforceMutation(){}  

    protected override void PerformMutate(IChromosome chromosome, float probability)
    {
        var rng = RandomizationProvider.Current.GetFloat();
        if (rng <= probability)
        {
            var ourChromosome = chromosome as Chromosome;
            for (int i = 0; i < Intesity; i++)
            {
                var minPoints = ourChromosome.VideoAssignments.Select(x => x.points).Min();
                var index = ourChromosome.VideoAssignments.FindIndex(x => x.points == minPoints);
                var video = DataModel.videos[RandomizationProvider.Current.GetInt(0,DataModel.number_of_videos_V)];
                var server = DataModel.servers[RandomizationProvider.Current.GetInt(0,DataModel.number_of_cache_servers_C)];
                var assignment = new VideoAssignment(server,video);

                ourChromosome.ReplaceGene(index,new Gene(assignment));
            }
        }
    }
}