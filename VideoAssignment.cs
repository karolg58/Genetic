using System;

namespace genetic
{
    public class VideoAssignment
    {
        public Video video;
        public Server server;
        public int points = 0;

        public VideoAssignment(Server server, Video video)
        {
            this.server = server;
            this.video = video;
        }

        public static bool operator ==(VideoAssignment first, VideoAssignment second)
        {
            if (((object)first == null) || ((object)second == null))
            {
                return false;
            }

            if (first.video == second.video && first.server == second.server)
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(VideoAssignment first, VideoAssignment second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            var other = obj as VideoAssignment;

            if (other == null)
            {
                return false;
            }

            return this.server == other.server && this.video == other.video;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = 31 * hash + video.id;
            hash = 31 * hash + server.id;
            return hash;
        }
    }
}