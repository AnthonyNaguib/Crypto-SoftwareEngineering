using System;
using System.Collections.Generic;
using System.IO;
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


        string input { get; set; }
        bool encryptOrDecrypt { get; set; }
        string result { get; set; }
        Encryption encryption { get; set; }
        public HandleInput(bool _encrypt, bool _decrypt, bool _useAlgorithm, algorithms _algorithm, bool _usingSUB, bool _usingTEA, int _SUB, string _TEA, bool _readData, string _readFileLocation, bool _writeData, string _writeFileLocation, bool _writeToConsoleToo, bool _useStringAsInput, string _inputstring, bool _outputDebug)
        {
            this.encrypt = _encrypt; //
            this.decrypt = _decrypt; //
            this.useAlgorithm = _useAlgorithm; //
            this.algorithm = _algorithm; //
            this.usingSUB = _usingSUB; //
            this.usingTEA = _usingTEA;//
            this.SUB = _SUB; //
            this.TEA = _TEA; //
            this.readData = _readData; // 
            this.readFileLocation = _readFileLocation; //
            this.writeData = _writeData;
            this.writeFileLocation = _writeFileLocation;
            this.writeToConsoleToo = _writeToConsoleToo;
            this.useStringAsInput = _useStringAsInput; //
            this.inputString = _inputstring; //
            this.outputDebug = _outputDebug;

            this.input = null;
            this.encryptOrDecrypt = true;
            this.result = null;
            this.encryption = new Encryption();
            DecideAction();
        }

        private void DecideAction()
        {
            Precautions();

            ReadData();

            EncryptOrDecrypt();

            Algorithm();

            Output();
        }
        private void ReadData()
        {
            if (readData) //read data from file
            {
                if (useStringAsInput)
                {
                    //asking to use file location and console input as your input string
                }
                else
                {
                    input = File.ReadAllText(readFileLocation);
                }
            }
            else //read data from console
            {
                input = inputString;
            }
        }
        private void Precautions()
        {
            if (readData)
            {
                readFileLocation = readFileLocation.Trim('\"');
            }
            if (writeData || writeToConsoleToo)
            {
                writeFileLocation = writeFileLocation.Trim('\"');
            }
        }
        public void EncryptOrDecrypt()
        {
            if (encrypt) //encrypt
            {
                if (decrypt)
                {
                    //stating encrypt and decrypt in the same command
                }
                else
                {
                    encryptOrDecrypt = true;
                }
            }
            else //decrypt 
            {
                encryptOrDecrypt = false;
            }
        }
        public void Algorithm()
        {
            if (useAlgorithm)
            {
                if (algorithm == algorithms.TEA) //TEA
                {
                    if (usingTEA == false)
                    {
                        //using TEA without stating any TEA string 
                    }
                    else
                    {
                        result = encryption.TEA(input, TEA, encryptOrDecrypt);
                    }
                }
                else if (algorithm == algorithms.SUB) //SUB
                {
                    if (usingSUB == false)
                    {
                        //using SUB without stating any SUB key
                    }
                    else
                    {
                        result = encryption.SUB(SUB, input, encryptOrDecrypt);
                    }
                }
                else //MD5
                {

                }
            }
            else
            {
                //not using any algorithm 
            }
        }
        public void Output()
        {
            if (writeData)
            {
                OutputData(true, false, result); //to file 
            }
            else if (writeToConsoleToo)
            {
                OutputData(true, true, result); //to file and console
            }
            else
            {
                OutputData(false, true, result); //to console
            }
        }
        private void OutputData(bool file, bool console, string result)
        {
            if (file)
            {
                if (file && console)
                {
                    File.WriteAllText(writeFileLocation, result);
                    Console.WriteLine();
                    Console.WriteLine(result);
                }
                else
                {
                    File.WriteAllText(writeFileLocation, result);
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(result);
            }
        }
    }
}
