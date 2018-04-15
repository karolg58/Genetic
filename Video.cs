using System;
using System.Collections.Generic;

namespace genetic
{
    public class Video
    {
        public int id;
        public int size; //MB
        public List<Request> requests = new List<Request>();
        public Dictionary<int,List<Request>> dict {get;set;} = new Dictionary<int, List<Request>>();

        public Video(int id, int size)
        {
            this.id = id;
            this.size = size;
        }

        public static bool operator ==(Video first, Video second)
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

        public static bool operator !=(Video first, Video second)
        {
            return !(first == second);
        }
        
        public override bool Equals(object obj)
        {
            var other = obj as Video;

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