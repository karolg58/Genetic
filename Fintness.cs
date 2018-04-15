using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using genetic;
using System.Collections.Generic;
using System.Linq;
using System;

public class Fitness : IFitness
{
    public double Evaluate(IChromosome chromosome)
    {
        DateTime t0 = DateTime.Now;
        var ourChromosome = chromosome as Chromosome;
        double fitness = 0;

        List<int> serverFreeMemory = DataModel.servers.Select(x => x.capacity).ToList();

        Dictionary<Request, int> requestPoints = DataModel.requests.ToDictionary(x => x,x => 0);

        List<VideoAssignment> videoAssignments = ourChromosome.VideoAssignments;
        var fittingAssignments = new List<VideoAssignment>();

        DateTime t1 = DateTime.Now;
        foreach (var assignement in videoAssignments)
        {
            if(assignement.video.size <= serverFreeMemory[assignement.server.id])
            {
                fittingAssignments.Add(assignement);
                serverFreeMemory[assignement.server.id] -= assignement.video.size;

                var requests = assignement.video.requests;

                foreach(var request in requests)
                {
                    var latencyToCache = request.endpoint.connections_to_servers.Where(x=>x.server == assignement.server).FirstOrDefault()?.latency ?? request.endpoint.latency_to_server;
                    var points = request.number_of_requests * (request.endpoint.latency_to_server - latencyToCache);
                    if (points > requestPoints[request])
                    {
                        fitness += points - requestPoints[request];
                        requestPoints[request] = points;
                    }
                }
            }
        }

        DateTime t2 = DateTime.Now;

        ourChromosome.ReplaceGenes(fittingAssignments);

        var result = System.Math.Floor(fitness * 1000 / DataModel.requests.Sum(x => x.number_of_requests));

        DateTime t3 = DateTime.Now;
        
        Console.WriteLine($"Fitnes timing: {(t3 - t0).TotalMilliseconds}ms, initialization = {(t1 - t0).TotalMilliseconds}ms, main loop: {(t2 - t1).TotalMilliseconds}ms, finishing: {(t3 - t2).TotalMilliseconds}ms");

        return result;
    }
}