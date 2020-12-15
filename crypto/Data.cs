using System;
using System.Collections.Generic;
using System.Text;

namespace crypto
{
    class Data
    {
        public bool encrypt { get; set; }
        public bool decrypt { get; set; }
        public bool useAlgorithm { get; set; }
        public algorithms algorithm { get; set; }
        public bool usingSUB { get; set; }
        public bool usingTEA { get; set; }
        public int SUB { get; set; }
        public string TEA { get; set; }
        public bool readData { get; set; }
        public string readFileLocation { get; set; }
        public bool writeData { get; set; }
        public string writeFileLocation { get; set; }
        public bool writeToConsoleToo { get; set; }
        public bool useStringAsInput { get; set; }
        public string inputString { get; set; }
        public bool outputDebug { get; set; }
    }
}
