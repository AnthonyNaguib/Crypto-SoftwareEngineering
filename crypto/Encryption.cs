using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crypto
{
    class Encryption
    {
        public int GiveMeNewIndex(int n, int index, bool addOrSubtract) //if bool is true we are encrypting, if bool is false we are decrypting.
        {
            if (addOrSubtract)
            {
                for(int i = 0; i < n; i++)
                {
                    if(index == 26)
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    if (index == 26)
                    {
                        index = 0;
                    }
                    else
                    {
                        index--;
                    }
                }
            }
            return index;
        }
        public string EncryptSUB(int n, string input)
        {
            char[] keyUPPER = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] keyLOWER = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };


            char[] inputArray = input.ToCharArray();
            char[] cipheredText = new char[inputArray.Length];
            for(int i = 0; i < inputArray.Length; i++)
            {
                if(inputArray[i] == ' ') //checking if it's a space
                {
                    cipheredText[i] = ' ';
                }
                else
                {
                    if (keyUPPER.Contains(inputArray[i])) //checking if it's upper case
                    {
                        int index = Array.IndexOf(keyUPPER, inputArray[i]);
                        int newIndex = GiveMeNewIndex(n, index, true);
                        cipheredText[i] = keyUPPER[newIndex];
                    }
                    else
                    {
                        if(keyLOWER.Contains(inputArray[i])) //checking if it's lower case
                        {
                            int index = Array.IndexOf(keyLOWER, inputArray[i]);
                            int newIndex = GiveMeNewIndex(n, index, true);
                            cipheredText[i] = keyLOWER[newIndex];
                        }
                        else //must be punctuation or number 
                        {
                            cipheredText[i] = inputArray[i];
                        }
                    }
                }
            }

            string cipheredString = string.Join("", cipheredText);
            return cipheredString;
        }
        public string DecryptSUB()
        {
            string please = "";

            return please;
        }
    }
}
