using genetic;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Randomizations;

public class AddGenesMutation : MutationBase
{
    public int Intesity { get; private set; } = 1;
    public AddGenesMutation(int intesity)
    {
        Intesity = intesity;
    }

    public AddGenesMutation(){}  

    protected override void PerformMutate(IChromosome chromosome, float probability)
    {
        var rng = RandomizationProvider.Current.GetFloat();
        if (rng > probability)
        {
            var ourChromosome = chromosome as Chromosome;
            var n = ourChromosome.VideoAssignments.Count;
            ourChromosome.Resize(n+Intesity);
            for (int i = 0; i < Intesity; i++)
            {
                var video = DataModel.videos[RandomizationProvider.Current.GetInt(0,DataModel.number_of_videos_V)];
                var server = DataModel.servers[RandomizationProvider.Current.GetInt(0,DataModel.number_of_cache_servers_C)];
                var assignment = new VideoAssignment(server,video);

                ourChromosome.ReplaceGene(n+i,new Gene(assignment));
            }
        }
    }
}