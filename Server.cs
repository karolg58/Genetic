using System;
using System.Collections.Generic;

namespace genetic
{
    public class Server
    {
        public int id;
        public int capacity;//MB
        public List<Video> videos = new List<Video>();

        public Server(int id, int capacity)
        {
            this.id = id;
            this.capacity = capacity;
        }

        public static bool operator ==(Server first, Server second)
        {
            if (((object)first == null) || ((object)second == null))
            {
                return false;
            }

            if (first.id == second.id)
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(Server first, Server second)
        {
            return !(first == second);
        }
    }
}