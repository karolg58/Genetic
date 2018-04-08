using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using genetic;
using System.Collections.Generic;
using System.Linq;

public class Fitness : IFitness
{
    public double Evaluate(IChromosome chromosome)
    {
        var ourChromosome = chromosome as Chromosome;
        double fitness = 0;

        List<int> serverFreeMemory = new List<int>(DataModel.number_of_cache_servers_C);
        for(int i = 0; i < serverFreeMemory.Count; i++) serverFreeMemory[i] = DataModel.capacity_of_server_X;

        Dictionary<Request, int> reqestPoints = DataModel.requests.ToDictionary(x => x,x => 0);

        foreach (var assignement in ourChromosome.VideoAssignments)
        {
            //TODO
            if(assignement.video.size >= serverFreeMemory[assignement.server.id])
            {
                serverFreeMemory[assignement.server.id] -= assignement.video.size;

                var requests = assignement.video.requests;

                foreach(var request in requests)
                {
                    var latencyToCache = request.endpoint.connections_to_servers.Where(x=>x.server == assignement.server).FirstOrDefault().latency;
                    var points = request.number_of_requests * (request.endpoint.latency_to_server - latencyToCache) - reqestPoints[request];
                    reqestPoints[request] = points;
                    fitness += points;
                }
            }
        }

        return fitness;
    }
}