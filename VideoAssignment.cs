using System;

namespace genetic
{
    public class VideoAssignment
    {
        public Video video;
        public Server server;

        public VideoAssignment(Server server, Video video)
        {
            this.server = server;
            this.video = video;
        }
    }
}