using System;
using System.Collections.Generic;
using System.IO;

namespace genetic
{
    public class InputReader
    {
        public DataModel data_model;

        public DataModel ReadDataFromFile(string path)
        {
            string[] file_content = File.ReadAllLines(path);
            Console.WriteLine(file_content[0]);
            return null;
        }
    }
}