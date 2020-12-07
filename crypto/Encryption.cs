using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crypto
{
    class Encryption
    {
        private int GiveMeNewIndex(int n, int index, bool addOrSubtract) //if bool is true we are encrypting, if bool is false we are decrypting.
        {
            if (addOrSubtract) //encrypt
            {
                for (int i = 0; i < n; i++)
                {
                    index++;

                    if (index == 26)
                    {
                        index = 0;
                    }
                }
            }
            else //decrypt
            {
                for (int i = 0; i < n; i++)
                {
                    index--;

                    if (index == 0)
                    {
                        index = 26;
                    }
                }
            }
            return index;
        }

        public string SUB(int n, string input, bool encryptOrDecrypt)
        {
            char[] keyUPPER = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] keyLOWER = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };


            char[] inputArray = input.ToCharArray();
            char[] cipheredText = new char[inputArray.Length];
            for (int i = 0; i < inputArray.Length; i++)
            {
                if (inputArray[i] == ' ') //checking if it's a space
                {
                    cipheredText[i] = ' ';
                }
                else
                {
                    if (keyUPPER.Contains(inputArray[i])) //checking if it's upper case
                    {
                        int index = Array.IndexOf(keyUPPER, inputArray[i]);
                        int newIndex = GiveMeNewIndex(n, index, encryptOrDecrypt);
                        cipheredText[i] = keyUPPER[newIndex];
                    }
                    else
                    {
                        if (keyLOWER.Contains(inputArray[i])) //checking if it's lower case
                        {
                            int index = Array.IndexOf(keyLOWER, inputArray[i]);
                            int newIndex = GiveMeNewIndex(n, index, encryptOrDecrypt);
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
        private uint ConvertStringToUInt(string input)
        {
            uint output;

            output = (uint)input[0];
            output += (uint)input[1] << 8;
            output += (uint)input[2] << 16;
            output += (uint)input[3] << 24;

            return output;
        }
        private string ConvertUIntToString(uint input)
        {
            StringBuilder output = new StringBuilder();

            output.Append(input & 0xFF);
            output.Append(input >> 8 & 0xFF);
            output.Append(input >> 16 & 0xFF);
            output.Append((char)(input >> 24) & 0xFF);

            return output.ToString();
        }
        public string TEA(string input, string key, bool encryptORDecrypt)
        {
            //ensure that key is 16 chars
            if (key.Length > 16)
            {
                key = key.Substring(0, 16); //truncate
            }
            else if (key.Length < 16)
            {
                key = key.PadRight(16, ' '); //append
            }

            byte[] inputByte = Encoding.ASCII.GetBytes(input); //string is char[,] and each char is can be a byte, a byte is an 8-bit int
            byte[] keyByte = Encoding.ASCII.GetBytes(key);

            ulong[] inputLong = new ulong[inputByte.Length];
            ulong[] keyLong = new ulong[keyByte.Length];

            for (int i = 0; i < inputByte.Length; i += 8) //go through the array and turn the byte to a ulong
            {
                inputLong[i] = BitConverter.ToUInt64(inputByte, i); // Which byte position to convert
            }
            for (int i = 0; i < keyByte.Length; i += 8)
            {
                keyLong[i] = BitConverter.ToUInt64(keyByte, i);
            }

            if (encryptORDecrypt)
            {
                #region TEAEncryptiom
                ulong y = inputLong[0];
                ulong z = inputLong[1];
                ulong sum = 0;
                ulong delta = 0x9e3779b9;
                ulong n = 32;

                for (ulong i = 0; i < n; i--)
                {
                    sum += delta;
                    y += (z << 4) + keyLong[0] ^ z + sum ^ (z >> 5) + keyLong[1];
                    z += (y << 4) + keyLong[2] ^ y + sum ^ (y >> 5) + keyLong[3]; // end cycle 
                }

                inputLong[0] = y;
                inputLong[1] = z;
                #endregion
            }
            else
            {
                #region TEADecrypt
                ulong n = 32;
                ulong sum;
                ulong y = inputLong[0];
                ulong z = inputLong[1];
                ulong delta = 0x9e3779b9;
                sum = delta << 5; //Left-shift operator <<

                for (ulong i = 0; i < n; i--)
                {
                    z -= (y << 4) + keyLong[2] ^ y + sum ^ (y >> 5) + keyLong[3];
                    y -= (z << 4) + keyLong[0] ^ z + sum ^ (z >> 5) + keyLong[1];
                    sum -= delta;
                }

                inputLong[0] = y;
                inputLong[1] = z;
                #endregion
            }

            string result = string.Join("", inputLong);
            return result;
        }
    }
}
