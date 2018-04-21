using System;
using System.Collections.Generic;

namespace genetic
{
    public class DataModel
    {
        public static int number_of_videos_V;
        public static int number_of_endpoints_E;
        public static int number_of_requests_R;
        public static int number_of_cache_servers_C;
        public static int capacity_of_server_X;
        public static int numberOfAllRequests;
        public static List<Video> videos = new List<Video>();
        public static List<Endpoint> endpoints = new List<Endpoint>();
        public static List<Request> requests = new List<Request>();
        public static List<Server> servers = new List<Server>();
        public static List<Connection> connections = new List<Connection>();

        public static void Reset(){
            videos = new List<Video>();
            endpoints = new List<Endpoint>();
            requests = new List<Request>();
            servers = new List<Server>();
            connections = new List<Connection>();
        }
    }
}