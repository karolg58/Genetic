using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using genetic;
using System.Collections.Generic;
using System.Linq;
using System;

public class BinaryFitness : IFitness
{
    public double Evaluate(IChromosome chromosome)
    {
        var ourChromosome = (BinaryChromosome)chromosome;
        double fitness = 0;

        List<int> serverFreeMemory = DataModel.servers.Select(x => x.capacity).ToList();

        Dictionary<Request, int> requestPoints = DataModel.requests.ToDictionary(x => x,x => 0);

        var list = ourChromosome.VideoAssignments.Where(x => x.IsActive).ToList();
        foreach (var assignement in list)
        {
            if(assignement.video.size <= serverFreeMemory[assignement.server.id])
            {
                serverFreeMemory[assignement.server.id] -= assignement.video.size;

                var requests = assignement.video.requests;

                foreach(var request in requests)
                {
                    var latencyToCache = request.endpoint.connections_to_servers.Where(x=>x.server == assignement.server).FirstOrDefault()?.latency ?? request.endpoint.latency_to_server;
                    var points = request.number_of_requests * (request.endpoint.latency_to_server - latencyToCache);
                    if (points > requestPoints[request])
                    {
                        requestPoints[request] = points - requestPoints[request];
                        fitness += points;
                    }
                }
            }
            else fitness = 0;
        }

        return System.Math.Floor(fitness * 1000 / DataModel.requests.Sum(x => x.number_of_requests));
    }
}