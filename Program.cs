using System;

namespace genetic
{
    class Program
    {
        static void Main(string[] args)
        {
            InputReader input_reader = new InputReader();
            input_reader.ReadDataFromFile("data/me_at_the_zoo.in");
            DataModel data_model;
            Console.WriteLine("Done");
        }
    }
}
