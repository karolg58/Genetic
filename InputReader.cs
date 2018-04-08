using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace genetic
{
    public class InputReader
    {
        public DataModel data_model = new DataModel();

        public DataModel ReadDataFromFile(string path)
        {
            var file_lines = File.ReadAllLines(path).ToList();

            var first_line = file_lines[0].Split(' ');
            data_model.number_of_videos_V = Int32.Parse(first_line[0]);
            data_model.number_of_endpoints_E = Int32.Parse(first_line[1]);
            data_model.number_of_requests_R = Int32.Parse(first_line[2]);
            data_model.number_of_cache_servers_C = Int32.Parse(first_line[3]);
            data_model.capacity_of_server_X = Int32.Parse(first_line[4]);

            for(int i = 0; i < data_model.number_of_cache_servers_C; i++)
            {
                data_model.servers.Add(new Server(i, data_model.capacity_of_server_X));
            }

            var video_sizes = file_lines[1].Split(' ');
            for(int i = 0; i < video_sizes.Count(); i++)
            {
                data_model.videos.Add(new Video(i, Int32.Parse(video_sizes[i])));
            }

            file_lines.RemoveAt(0);
            file_lines.RemoveAt(0);

            for(int i = 0; i < data_model.number_of_endpoints_E; i++)
            {
                var endpoint_line = file_lines[0].Split(' ');
                Endpoint endpoint = new Endpoint(i, Int32.Parse(endpoint_line[0]), Int32.Parse(endpoint_line[1]));
                file_lines.RemoveAt(0);
                for(int j = 0; j < endpoint.number_of_cache_servers; j++)
                {
                    var cache_line = file_lines[0].Split(' ');
                    Connection connection = new Connection(
                        data_model.servers.Where(x=>x.id == Int32.Parse(cache_line[0])).FirstOrDefault(),
                        endpoint,
                        Int32.Parse(cache_line[1])
                    );
                    endpoint.connections_to_servers.Add(connection);
                    data_model.connections.Add(connection);
                    file_lines.RemoveAt(0);
                }
                data_model.endpoints.Add(endpoint);
            }

            for(int i = 0; i < data_model.number_of_requests_R; i++)
            {
                var request_line = file_lines[0].Split(' ');
                Console.WriteLine(file_lines[0]);
                Request request = new Request(
                    data_model.videos.Where(x=>x.id == Int32.Parse(request_line[0])).FirstOrDefault(),
                    data_model.endpoints.Where(x=>x.id == Int32.Parse(request_line[1])).FirstOrDefault(),
                    Int32.Parse(request_line[2])
                );
                file_lines.RemoveAt(0);
            }

            Console.WriteLine(file_lines[0]);
            return data_model;
        }
    }
}