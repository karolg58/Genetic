using System;
using System.Collections.Generic;
using System.IO;

namespace genetic
{
    public class InputReader
    {
        public DataModel data_model = new DataModel();

        public DataModel ReadDataFromFile(string path)
        {
            string[] file_lines = File.ReadAllLines(path);

            var first_line = file_lines[0].Split(' ');
            data_model.number_of_videos_V = Int32.Parse(first_line[0]);
            data_model.number_of_endpoints_E = Int32.Parse(first_line[1]);
            data_model.number_of_requests_R = Int32.Parse(first_line[2]);
            data_model.number_of_cache_servers_C = Int32.Parse(first_line[3]);
            data_model.capacity_of_server_X = Int32.Parse(first_line[4]);
            Console.WriteLine(file_lines[0]);
            return null;
        }
    }
}