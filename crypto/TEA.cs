using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crypto
{
    class TEA : Encryption
    {
        string key;
        string input;
        string result;
        public TEA(string _key, string _input)
        {
            this.key = _key;
            this.input = _input;
        }
        public override void Encrypt()
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

            byte[] keyByte = Encoding.UTF8.GetBytes(key);

            ulong[] keyLong = new ulong[keyByte.Length];

            for (int i = 0; i < keyByte.Length; i += 8)
            {
                keyLong[i] = BitConverter.ToUInt64(keyByte, i);
            }

            if (!(input.Length % 8 == 0))
            {
                int nearestMultiple = input.Length + (8 - input.Length % 8);
                input = input.PadRight(nearestMultiple, ' ');
            }

            byte[] inputByte = Encoding.UTF8.GetBytes(input); //string is char[,] and each char is can be a byte, a byte is an 8-bit int

            ulong[] inputLong = new ulong[inputByte.Length];

            for (int i = 0; i < inputByte.Length; i += 8) //go through the array and turn the byte to a ulong
            {
                inputLong[i] = BitConverter.ToUInt64(inputByte, i); // Which byte position to convert
            }

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

            result = String.Join("", inputLong.Where(l => l != 0));
        }
        public override void Decrypt()
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

            byte[] keyByte = Encoding.UTF8.GetBytes(key);

            ulong[] keyLong = new ulong[keyByte.Length];

            for (int i = 0; i < keyByte.Length; i += 8)
            {
                keyLong[i] = BitConverter.ToUInt64(keyByte, i);
            }

            if (!(input.Length % 8 == 0))
            {
                int nearestMultiple = input.Length + (8 - input.Length % 8);
                input = input.PadRight(nearestMultiple, ' ');
            }


            byte[] inputByte = Encoding.UTF8.GetBytes(input);

            ulong[] inputLong = new ulong[inputByte.Length];
            for (int i = 0; i < inputByte.Length; i += 8) //go through the array and turn the byte to a ulong
            {
                inputLong[i] = BitConverter.ToUInt64(inputByte, i); // Which byte position to convert
            }

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

            List<ulong> inputLongNoZeroList = new List<ulong>();
            foreach (ulong d in inputLong)
            {
                if (!(d == 0))
                {
                    inputLongNoZeroList.Add(d);
                }
            }

            ulong[] inputLongNoZero = inputLongNoZeroList.ToArray();
            List<byte> byteList = new List<byte>();

            foreach (ulong longValue in inputLongNoZero)
            {
                byte[] temp = BitConverter.GetBytes(longValue);
                byteList.AddRange(temp);
            }

            byte[] resultByte = byteList.ToArray();
            result = Convert.ToBase64String(resultByte);
        }
        public override string GetResult()
        {
            return result;
        }
    }
}
