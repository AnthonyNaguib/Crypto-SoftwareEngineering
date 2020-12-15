using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace crypto
{
    enum algorithms { SUB, MD5, TEA}
    class UserCommand:Data
    {
        public UserCommand(string[] input, Data data)
        {
            List<string> inputList = input.ToList();

            int indexer = 0;
            foreach(string command in inputList)
            {
                switch (command)
                {
                    case "-e": //encrypt
                        data.encrypt = true;
                        break;

                    case "-d": //decrypt
                        data.decrypt = true;
                        data.outputDebug = true;
                        break;

                    case "-a": //use algorithm
                        data.useAlgorithm = true;
                        break;

                    case "SUB": //algorithm SUB
                        data.algorithm = algorithms.SUB;
                        break;

                    case "MD5": //algorithm MD5
                        data.algorithm = algorithms.MD5;
                        break;

                    case "TEA": //algorithm TEA
                        data.algorithm = algorithms.TEA;
                        break;

                    case "-s": //SUB integer
                        data.usingSUB = true;
                        data.SUB = int.Parse(inputList[indexer + 1]);
                        break;

                    case "-k": //TEA string
                        data.usingTEA = true;
                        data.TEA = inputList[indexer + 1];
                        break;

                    case "-f": //read data from file
                        data.readData = true;
                        data.readFileLocation = inputList[indexer + 1];
                        break;

                    case "-o": //output data to file
                        data.writeData = true;
                        data.writeFileLocation = inputList[indexer + 1];
                        break;

                    case "-O": //output data to file AND console
                        data.writeFileLocation = inputList[indexer + 1];
                        data.writeToConsoleToo = true;
                        break;

                    case "-i": //use string as 
                        data.useStringAsInput = true;
                        data.inputString = inputList[indexer + 1];
                        break;

                    case "-b":
                        data.outputDebug = true;
                        break;
                }

                indexer++;
            }
        }
    }
}
