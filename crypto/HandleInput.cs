using System;
using System.Collections.Generic;
using System.Text;

namespace crypto
{
    class HandleInput
    {
        bool encrypt { get; set; }
        bool decrypt { get; set; }
        bool useAlgorithm { get; set; }
        algorithms algorithm { get; set; }
        bool usingSUB { get; set; }
        bool usingTEA { get; set; }
        int SUB { get; set; }
        string TEA { get; set; }
        bool readData { get; set; }
        string readFileLocation { get; set; }
        bool writeData { get; set; }
        string writeFileLocation { get; set; }
        bool writeToConsoleToo { get; set; }
        bool useStringAsInput { get; set; }
        string inputString { get; set; }
        bool outputDebug { get; set; }
        public HandleInput(bool _encrypt, bool _decrypt, bool _useAlgorithm, algorithms _algorithm, bool _usingSUB, bool _usingTEA, int _SUB, string _TEA, bool _readData, string _readFileLocation, bool _writeData, string _writeFileLocation, bool _writeToConsoleToo, bool _useStringAsInput, string _inputstring, bool _outputDebug)
        {
            this.encrypt = _encrypt;
            this.decrypt = _decrypt;
            this.useAlgorithm = _useAlgorithm;
            this.algorithm = _algorithm;
            this.usingSUB = _usingSUB;
            this.usingTEA = _usingTEA;
            this.SUB = _SUB;
            this.TEA = _TEA;
            this.readData = _readData;
            this.readFileLocation = _readFileLocation;
            this.writeData = _writeData;
            this.writeFileLocation = _writeFileLocation;
            this.writeToConsoleToo = _writeToConsoleToo;
            this.useStringAsInput = _useStringAsInput;
            this.inputString = _inputstring;
            this.outputDebug = _outputDebug;
        }
        
        private void DecideAction()
        {
            string input;
            if (readData)
            {
                input = System.IO.File.ReadAllText(readFileLocation);
            }
            else
            {
                input = inputString;
            }

        }
    }
}
