using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace crypto
{
    enum algorithms { SUB, MD5, TEA}
    class UserCommand
    {
        bool encrypt;
        bool decrypt;
        bool useAlgorithm;
        algorithms algorithm;
        bool usingSUB;
        bool usingTEA;
        int SUB;
        string TEA;
        bool readData;
        string readFileLocation;
        bool writeData;
        string writeFileLocation;
        bool writeToConsoleToo;
        bool useStringAsInput;
        string inputString;
        bool outputDebug;
        public UserCommand(string[] input)
        {
            List<string> inputList = input.ToList();

            int indexer = 0;
            foreach(string command in inputList)
            {
                switch (command)
                {
                    case "-e": //encrypt
                        encrypt = true;
                        break;

                    case "-d": //decrypt
                        decrypt = true;
                        outputDebug = true;
                        break;

                    case "-a": //use algorithm
                        useAlgorithm = true;
                        break;

                    case "SUB": //algorithm SUB
                        algorithm = algorithms.SUB;
                        break;

                    case "MD5": //algorithm MD5
                        algorithm = algorithms.MD5;
                        break;

                    case "TEA": //algorithm TEA
                        algorithm = algorithms.TEA;
                        break;

                    case "-s": //SUB integer
                        usingSUB = true;
                        SUB = int.Parse(inputList[indexer + 1]);
                        break;

                    case "-k": //TEA string
                        usingTEA = true;
                        TEA = inputList[indexer + 1];
                        break;

                    case "-f": //read data from file
                        readData = true;
                        readFileLocation = inputList[indexer + 1];
                        break;

                    case "-o": //output data to file
                        writeData = true;
                        writeFileLocation = inputList[indexer + 1];
                        break;

                    case "-O": //output data to file AND console
                        writeFileLocation = inputList[indexer + 1];
                        writeToConsoleToo = true;
                        break;

                    case "-i": //use string as 
                        useStringAsInput = true;
                        inputString = inputList[indexer + 1];
                        break;

                    case "-b":
                        outputDebug = true;
                        break;
                }

                indexer++;
            }
            HandleInput handleInput = new HandleInput(encrypt, decrypt, useAlgorithm, algorithm, usingSUB, usingTEA, SUB, TEA, readData, readFileLocation, writeData, writeFileLocation, writeToConsoleToo, useStringAsInput, inputString, outputDebug);
        }
    }
}
