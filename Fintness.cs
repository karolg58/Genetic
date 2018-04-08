using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;

public class Fitness : IFitness
{
    public double Evaluate(IChromosome chromosome)
    {
        var ourChromosome = chromosome as Chromosome;
        double fitness = 0;
        foreach (var assignement in ourChromosome.VideoAssignments)
        {
            //TODO
        }
        return fitness;
    }
}