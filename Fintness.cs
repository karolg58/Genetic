using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using genetic;
using System.Collections.Generic;
using System.Linq;
using System;

public class Fitness : IFitness
{
    public static List<int> serverFreeMemoryBase = DataModel.servers.Select(x => x.capacity).ToList();
    public static List<int> requestPointsBase = DataModel.requests.Select(x => 0).ToList();
    public double Evaluate(IChromosome chromosome)
    {
        var serverFreeMemory = new List<int>(serverFreeMemoryBase);
        var requestPoints = new List<int>(requestPointsBase);
        var cachedVideos = DataModel.servers.ToDictionary(x => x.id,x => new List<int>());

        var ourChromosome = chromosome as Chromosome;
        List<VideoAssignment> videoAssignments = ourChromosome.VideoAssignments;
        double fitness = 0;
        var fittingAssignments = new List<VideoAssignment>();

        DateTime t0 = DateTime.Now;


        // videoAssignments.Where(x => x.video.size <= serverFreeMemory[x.server.id]);
        foreach (var assignement in videoAssignments)
        {
            if (cachedVideos[assignement.server.id].Contains(assignement.video.id))
            {
                continue;
            }
            if (!assignement.video.dict.ContainsKey(assignement.server.id))
            {
                continue;
            }
            else cachedVideos[assignement.server.id].Add(assignement.video.id);
            if(assignement.video.size <= serverFreeMemory[assignement.server.id])
            {
                fittingAssignments.Add(assignement);
                serverFreeMemory[assignement.server.id] -= assignement.video.size;

                var requests = assignement.video.requests;
                // var enumerable = requests.Where(x => x.servers.ContainsKey(assignement.server.id)).Select(x => x.Id).ToList();

                var list = assignement.video.dict[assignement.server.id];

                foreach (var request in list)
                {
                    var latencyToCache = request.servers[assignement.server.id];
                    var points = request.number_of_requests * (request.endpoint.latency_to_server - latencyToCache);
                    if (points > requestPoints[request.Id])
                    {
                        fitness += points - requestPoints[request.Id];
                        requestPoints[request.Id] = points;
                    }
                }
            }
        }

        DateTime t2 = DateTime.Now;

        ourChromosome.ReplaceGenes(fittingAssignments);

        var result = System.Math.Floor(fitness * 1000 / DataModel.numberOfAllRequests);

        DateTime t3 = DateTime.Now;

        Program.fitnessGenerationTimeEnd += t3-t2;
        Program.fitnessGenerationTimeMiddle += t2-t0;
        Program.fitnessGenerationTime += t3-t0;
        
        // Console.WriteLine($"Fitnes timing: {(t3 - t0).TotalMilliseconds}ms, initialization = {(t1 - t0).TotalMilliseconds}ms, main loop: {(t2 - t1).TotalMilliseconds}ms, finishing: {(t3 - t2).TotalMilliseconds}ms");

        return result;
    }
}