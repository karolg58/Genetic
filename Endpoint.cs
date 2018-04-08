using System;
using System.Collections.Generic;

namespace genetic
{
    public class Endpoint
    {
        public int latency_to_server;//ms
        public int number_of_cache_servers;
        public List<Connection> connections_to_servers = new List<Connection>();
    }
}