using System;
using System.Collections.Generic;
using System.IO;
using GeneticSharp.Domain.Chromosomes;

namespace genetic
{
    public class SaveToFile
    {
        public static void Save(Chromosome chromosome, string path)
        {
            List<List<int>> videos = new List<List<int>>();
            for(int i = 0; i < DataModel.number_of_cache_servers_C; i++){
                videos.Add(new List<int>());
            }
            List<VideoAssignment> vas = chromosome.VideoAssignments;
            for(int i = 0; i < vas.Count; i++){
                VideoAssignment va = vas[i];
                videos[va.server.id].Add(va.video.id);
            }
            int counter = 0;
            string content = "";
            for(int i = 0; i < videos.Count; i++){
                if(videos[i].Count > 0) counter ++;
            }
            content += counter.ToString() + Environment.NewLine;
            for(int i = 0; i < videos.Count; i++){
                content += i.ToString() + " ";
                for(int j = 0; j < videos[i].Count; j++){
                    content += videos[i][j].ToString() + " ";
                }
                content += Environment.NewLine;
            }

            path = Path.Combine(path, chromosome.Fitness.ToString() + ".txt");
            File.WriteAllText(path, content);
        }
    }
}