using System;

namespace genetic
{
    class Server
    {
        public int id;
        public int capacity;//MB
    }

    public Server(int id, int capacity)
    {
            this.id = id;
            this.capacity = capacity;
    }
}