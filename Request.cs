using System;
using System.Collections.Generic;
using System.Linq;

namespace genetic
{
    public class Request
    {
        public int Id {get;private set;}
        public Video video;
        public Endpoint endpoint;
        public int number_of_requests;

        public Dictionary<int,int> servers {get;private set;}

        public Request(int id,Video video, Endpoint endpoint, int number_of_requests)
        {
            Id = id;
            this.video = video;
            this.endpoint = endpoint;
            this.number_of_requests = number_of_requests;
            servers = endpoint.connections_to_servers.ToDictionary(x => x.server.id, x=> x.latency);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Request;

            if (other == null)
            {
                return false;
            }

            return this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}