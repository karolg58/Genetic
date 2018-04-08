using System;
using System.Collections.Generic;

namespace genetic
{
    public class Endpoint
    {
        public int id;
        public int latency_to_server;//ms
        public int number_of_cache_servers;
        public List<Connection> connections_to_servers = new List<Connection>();

        public Endpoint(int id, int latency_to_server, int number_of_cache_servers)
        {
            this.id = id;
            this.latency_to_server = latency_to_server;
            this.number_of_cache_servers = number_of_cache_servers;
        }
    }
}