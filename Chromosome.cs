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

    public int CurrentEnergy {get;set;}
    public int DefaultEnergy {get;private set;} = 50;
    public int Differential {get; private set;} = 10;
    public int ReproductionThreshhold {get;private set;} = 75;
    public bool HasReproducedThisGeneration {get;set;} = false;


    public Chromosome(int length, int defaultEnergy) : base(length)
    {
        CurrentEnergy = DefaultEnergy = defaultEnergy;
        // VideoAssignments = new List<VideoAssignment>(length);
        for (int i = 0; i < length; i++)
        {
            // VideoAssignments.Add(null);
            ReplaceGene(i, GenerateGene(i));
        }
    }

    public Chromosome(int length) : base(length)
    {
        CurrentEnergy = DefaultEnergy;
        // VideoAssignments = new List<VideoAssignment>(length);
        for (int i = 0; i < length; i++)
        {
            // VideoAssignments.Add(null);
            ReplaceGene(i, GenerateGene(i));
        }
    }

    public override IChromosome CreateNew()
    {
        return new Chromosome(Length, DefaultEnergy);
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
            var tmp = GetAssignment();
            if (newVideoAssignments.Contains(tmp))
            {
                continue;
            }
            newVideoAssignments.Add(tmp);
            // newVideoAssignments = newVideoAssignments.Distinct().ToList(); //StackOverflowException
        }
        // VideoAssignments = newVideoAssignments;
        Resize(newVideoAssignments.Count);
        for (int i = 0; i < newVideoAssignments.Count; i++)
        {
            ReplaceGene(i, new Gene(newVideoAssignments[i]));
        }
    }

    public void DoMeeteing(Chromosome meetingPartner)
    {
        // this.HasMetDuringThisGeneration = true;
        // meetingPartner.HasMetDuringThisGeneration = true;
        if (CurrentEnergy == 0 || meetingPartner.CurrentEnergy == 0)
        {
            return;
        }
        if (this > meetingPartner)
        {
            CurrentEnergy += Differential;
            meetingPartner.CurrentEnergy -= Differential;
        }
        else if (this < meetingPartner)
        {
            CurrentEnergy -= Differential;
            meetingPartner.CurrentEnergy += Differential;
        }
    }
}