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
        public UserCommand(string input)
        {
            List<string> inputList = Regex.Matches(input, @"[\""].+?[\""]|[^ ]+").Cast<Match>().Select(m => m.Value).ToList();
            //Regex Matches creates a MatchCollection and so we need to cast Match. The regex itself has
            //two parts, split like a boolean. [\""].+?[\""] and [^ ]+. The first part we are looking for quotation marks using a quantifier + and a lazy ?
            //whereas the second part is looking for all spaces.
            //Using select with the lambda expression syntax means "goes to", seperates the argument from the body.
            //For example we will use the word "the", m will be "{the}", m.Value will be "the".
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
                        SUB = int.Parse(inputList[indexer+1]);
                        break;

                    case "-k": //TEA string
                        usingTEA = true;
                        TEA = inputList[indexer++];
                        break;

                    case "-f": //read data from file
                        readData = true;
                        readFileLocation = inputList[indexer+1];
                        break;

                    case "-o": //output data to file
                        writeData = true;
                        writeFileLocation = inputList[indexer+1];
                        break;

                    case "-O": //output data to file AND console
                        writeToConsoleToo = true;
                        break;

                    case "-i": //use string as 
                        useStringAsInput = true;
                        inputString = inputList[indexer+1];
                        Console.WriteLine(inputString);
                        break;
                }

                indexer++;
            }
            HandleInput handleInput = new HandleInput(encrypt, decrypt, useAlgorithm, algorithm, usingSUB, usingTEA, SUB, TEA, readData, readFileLocation, writeData, writeFileLocation, writeToConsoleToo, useStringAsInput, inputString, outputDebug);
          //  handleInput.DecideAction();
        }
    }
}
