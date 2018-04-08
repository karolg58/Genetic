using System;
using System.Collections.Generic;

namespace genetic
{
    public class DataModel
    {
        public int number_of_videos_V;
        public int number_of_endpoints_E;
        public int number_of_requests_R;
        public int number_of_cache_servers_C;
        public int capacity_of_server_X;
        public List<Video> videos = new List<Video>();
        public List<Endpoint> endpoints = new List<Endpoint>();
        public List<Request> requests = new List<Request>();
        public List<Server> servers = new List<Server>();
        public List<Connection> connections = new List<Connection>();
    }
}