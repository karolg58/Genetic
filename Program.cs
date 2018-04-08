using System;

namespace genetic
{
    class Program
    {
        static void Main(string[] args)
        {
            DataModel data_model = new DataModel();
            InputReader input_reader = new InputReader();
            input_reader.ReadDataFromFile("data/me_at_the_zoo.in");
            Console.WriteLine("Done");
        }
    }
}
