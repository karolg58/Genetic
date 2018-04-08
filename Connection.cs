using System;

namespace genetic
{
    public class Connection
    {
        public int latency;//ms
        public Server server;
        public Endpoint endpoint;

        public Connection(Server server, Endpoint endpoint, int latency)
        {
            this.server = server;
            this.endpoint = endpoint;
            this.latency = latency;
        }
    }
}