using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace crypto
{
    class HandleInput
    {

        Data data;
        string input;
        bool encryptOrDecrypt;
        string result;
        public HandleInput(Data data)
        {
            this.data = data;
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
            if (data.readData)
            {
                data.readFileLocation = data.readFileLocation.Trim('\"');
            }
            if (data.writeData || data.writeToConsoleToo)
            {
                data.writeFileLocation = data.writeFileLocation.Trim('\"');
            }
        }
        private void ReadData()
        {
            if (data.readData) //read data from file
            {
                if (data.useStringAsInput)
                {
                    Console.WriteLine("Error - Cannot use file and console command input simultaneously");
                    Environment.Exit(0);
                }
                else
                {
                    try
                    {
                        input = File.ReadAllText(data.readFileLocation);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error - " + e.Message);
                        Environment.Exit(0);
                    }
                }
            }
            else if(data.useStringAsInput) //read data from console
            {
                input = data.inputString;
            }
            else
            {
                Console.WriteLine("Error - No Input string");
                Environment.Exit(0);
            }
        }
        public void EncryptOrDecrypt()
        {
            if (data.encrypt) //encrypt
            {
                if (data.decrypt)
                {
                    Console.WriteLine("Error - Cannot Encrypt and Decrypt simultaneously");
                    Environment.Exit(0);
                }
                else
                {
                    encryptOrDecrypt = true;
                }
            }
            else if(data.decrypt) //decrypt 
            {
                encryptOrDecrypt = false;
            }
            else
            {
                Console.WriteLine("Error - Please specify if you would like to encrypt or decrypt");
                Environment.Exit(0);
            }
        }
        public void Algorithm()
        {
            if (data.useAlgorithm)
            {
                if (data.algorithm == algorithms.TEA) //TEA
                {
                    if (data.usingTEA == false)
                    {
                        Console.WriteLine("Error - You are trying to use TEA without specifying a key");
                        Environment.Exit(0);
                    }
                    else
                    {
                        TEA tea = new TEA();
                        if (data.encrypt)
                        {
                            result = tea.EncryptTEA(input, data.TEA);
                        }
                        else
                        {
                            result = tea.DencryptTEA(input, data.TEA);
                        }
                    }
                }
                else if (data.algorithm == algorithms.SUB) //SUB
                {
                    if (data.usingSUB == false)
                    {
                        Console.WriteLine("Error - You are trying to use SUB without specifying the character offset");
                        Environment.Exit(0);
                    }
                    else
                    {
                        SUB sub = new SUB();
                        result = sub.algorithmSUB(data.SUB, input, encryptOrDecrypt);
                    }
                }
                else //MD5
                {
                    if(data.decrypt)
                    {
                        Console.WriteLine("Error - Cannot decrypt using MD5");
                        Environment.Exit(0);
                    }

                    Console.WriteLine("Error - MD5 not implemented");
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Error - You have not specified an algorithm");
                Environment.Exit(0);
            }
        }
        public void Output()
        {
            if (data.writeData)
            {
                OutputData(true, false, result); //to file 
            }
            else if (data.writeToConsoleToo)
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
                        File.WriteAllText(data.writeFileLocation, result);
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
                        File.WriteAllText(data.writeFileLocation, result);
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
