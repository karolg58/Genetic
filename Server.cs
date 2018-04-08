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
    }
}