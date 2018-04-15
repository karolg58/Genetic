using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace genetic
{
    public class InputReader
    {
        public void ReadDataFromFile(string path)
        {
            var file_lines = File.ReadAllLines(path).ToList();

            var first_line = file_lines[0].Split(' ');
            DataModel.number_of_videos_V = Int32.Parse(first_line[0]);
            DataModel.number_of_endpoints_E = Int32.Parse(first_line[1]);
            DataModel.number_of_requests_R = Int32.Parse(first_line[2]);
            DataModel.number_of_cache_servers_C = Int32.Parse(first_line[3]);
            DataModel.capacity_of_server_X = Int32.Parse(first_line[4]);

            for(int i = 0; i < DataModel.number_of_cache_servers_C; i++)
            {
                DataModel.servers.Add(new Server(i, DataModel.capacity_of_server_X));
            }

            var video_sizes = file_lines[1].Split(' ');
            for(int i = 0; i < video_sizes.Count(); i++)
            {
                DataModel.videos.Add(new Video(i, Int32.Parse(video_sizes[i])));
            }

            file_lines.RemoveAt(0);
            file_lines.RemoveAt(0);

            for(int i = 0; i < DataModel.number_of_endpoints_E; i++)
            {
                var endpoint_line = file_lines[0].Split(' ');
                Endpoint endpoint = new Endpoint(i, Int32.Parse(endpoint_line[0]), Int32.Parse(endpoint_line[1]));
                file_lines.RemoveAt(0);
                for(int j = 0; j < endpoint.number_of_cache_servers; j++)
                {
                    var cache_line = file_lines[0].Split(' ');
                    Connection connection = new Connection(
                        DataModel.servers[Int32.Parse(cache_line[0])],
                        endpoint,
                        Int32.Parse(cache_line[1])
                    );
                    endpoint.connections_to_servers.Add(connection);
                    DataModel.connections.Add(connection);
                    file_lines.RemoveAt(0);
                }
                DataModel.endpoints.Add(endpoint);
            }

            for(int i = 0; i < DataModel.number_of_requests_R; i++)
            {
                var request_line = file_lines[0].Split(' ');
                var video = DataModel.videos[Int32.Parse(request_line[0])];
                var endpoint = DataModel.endpoints[Int32.Parse(request_line[1])];
                Request request = new Request(i,video, endpoint,Int32.Parse(request_line[2]));
                video.requests.Add(request);
                foreach (var serv in endpoint.connections_to_servers.Select(x => x.server))
                {
                    if (!video.dict.ContainsKey(serv.id))
                    {
                        video.dict.Add(serv.id,new List<Request>());
                    }
                    video.dict[serv.id].Add(request);
                }
                DataModel.requests.Add(request);
                file_lines.RemoveAt(0);
            }

            DataModel.numberOfAllRequests = DataModel.requests.Sum(x => x.number_of_requests);

            return;
        }
    }
}