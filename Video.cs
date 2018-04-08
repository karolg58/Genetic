using System;

namespace genetic
{
    public class Video
    {
        public int id;
        public int size; //MB

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
    }
}