using genetic;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Randomizations;

public class Mutation : MutationBase
{
    public int Intesity { get; private set; } = 1;
    public Mutation(int intesity)
    {
        Intesity = intesity;
    }

    public Mutation(){}  

    protected override void PerformMutate(IChromosome chromosome, float probability)
    {
        var rng = RandomizationProvider.Current.GetFloat();
        if (rng > probability)
        {
            var ourChromosome = chromosome as Chromosome;
            for (int i = 0; i < Intesity; i++)
            {
                var index = RandomizationProvider.Current.GetInt(0,ourChromosome.VideoAssignments.Count-1);
                var video = DataModel.videos[RandomizationProvider.Current.GetInt(0,DataModel.number_of_videos_V)];
                var server = DataModel.servers[RandomizationProvider.Current.GetInt(0,DataModel.number_of_cache_servers_C)];
                var assignment = new VideoAssignment(server,video);

                ourChromosome.ReplaceGene(index,new Gene(assignment));
            }
        }
    }
}