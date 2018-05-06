using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GeneticSharp.Domain.Chromosomes;

namespace genetic
{
    public class SaveToFile
    {
        public static void Save(Chromosome chromosome, string path)
        {
            List<List<int>> videos = new List<List<int>>();
            for (int i = 0; i < DataModel.number_of_cache_servers_C; i++)
            {
                videos.Add(new List<int>());
            }
            List<VideoAssignment> vas = chromosome.VideoAssignments;
            for (int i = 0; i < vas.Count; i++)
            {
                VideoAssignment va = vas[i];
                videos[va.server.id].Add(va.video.id);
            }
            int counter = 0;
            string content = "";
            for (int i = 0; i < videos.Count; i++)
            {
                if (videos[i].Count > 0) counter++;
            }
            content += counter.ToString() + Environment.NewLine;
            for (int i = 0; i < videos.Count; i++)
            {
                content += i.ToString() + " ";
                for (int j = 0; j < videos[i].Count; j++)
                {
                    content += videos[i][j].ToString() + " ";
                }
                content += Environment.NewLine;
            }
            Directory.CreateDirectory(path);
            path = Path.Combine(path, chromosome.Fitness.ToString() + ".txt");
            File.WriteAllText(path, content);
        }

        public static string initFilesForPlots(string version, string startDateTime)
        {
            string dir = Path.Combine("data", "plots", version);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string filePath = Path.Combine(dir, startDateTime + ".txt");
            if (!File.Exists(filePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Generation number, Milliseconds from start, Fitness");
                }
            }
            return filePath;
        }
        public static void PlotsData(int generationNumber, List<IChromosome> chromosomes, double? fitness, string filePath, Stopwatch forPlotsWatch)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(generationNumber.ToString() + "," + forPlotsWatch.ElapsedMilliseconds.ToString() + "," + fitness.ToString());
            }
            
            var fitnessPath = filePath.Replace("plots", "fitnessForAll");
            Directory.CreateDirectory(Path.GetDirectoryName(fitnessPath));
            using (StreamWriter sw = File.AppendText(fitnessPath))
            {
                if(chromosomes != null){
                    for(int i = 0; i < chromosomes.Count; i++){
                        sw.Write(chromosomes[i].Fitness);
                        if(i < chromosomes.Count - 1) sw.Write(",");
                    }
                    sw.Write("\n");
                }
            }
        }
    }
}