using System;

namespace genetic
{
    public class Request
    {
        public Video video;
        public Endpoint endpoint;
        public int number_of_requests;

        public Request(Video video, Endpoint endpoint, int number_of_requests)
        {
            this.video = video;
            this.endpoint = endpoint;
            this.number_of_requests = number_of_requests;
        }
    }
}