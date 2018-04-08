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

        public override bool Equals(object obj)
        {
            var other = obj as Server;

            if (other == null)
            {
                return false;
            }

            return this.id == other.id;
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }
    }
}