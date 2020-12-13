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
        private void ReadData()
        {
            if (readData) //read data from file
            {
                if (useStringAsInput)
                {
                    Console.WriteLine("Error - Cannot use file and console command input simultaneously");
                    Environment.Exit(0);
                }
                else
                {
                    try
                    {
                        input = File.ReadAllText(readFileLocation);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error - " + e.Message);
                        Environment.Exit(0);
                    }
                }
            }
            else //read data from console
            {
                input = inputString;
            }
        }
        public void EncryptOrDecrypt()
        {
            if (encrypt) //encrypt
            {
                if (decrypt)
                {
                    Console.WriteLine("Error - Cannot Encrypt and Decrypt simultaneously");
                    Environment.Exit(0);
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
                        Console.WriteLine("Error - You are trying to use TEA without specifying a key");
                        Environment.Exit(0);
                    }
                    else
                    {
                        TEA tea = new TEA();
                        result = tea.algorithmTEA(input, TEA, encryptOrDecrypt);
                    }
                }
                else if (algorithm == algorithms.SUB) //SUB
                {
                    if (usingSUB == false)
                    {
                        Console.WriteLine("Error - You are trying to use SUB without specifying the character offset");
                        Environment.Exit(0);
                    }
                    else
                    {
                        SUB sub = new SUB();
                        result = sub.algorithmSUB(SUB, input, encryptOrDecrypt);
                    }
                }
                else //MD5
                {
                    Console.WriteLine("Error - MD5 not implemented");
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Error - You have not specified an algorithm.");
                Environment.Exit(0);
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
                    try
                    {
                        File.WriteAllText(writeFileLocation, result);
                        Console.WriteLine();
                        Console.WriteLine(result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error - " + e.Message);
                        Environment.Exit(0);
                    }
                }
                else
                {
                    try
                    {
                        File.WriteAllText(writeFileLocation, result);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Error - " + e.Message);
                        Environment.Exit(0);
                    }
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
