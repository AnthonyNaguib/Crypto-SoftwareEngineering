using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crypto
{
    abstract class Encryption
    {
        public abstract void Encrypt();
        public abstract void Decrypt();
        public abstract string GetResult();
    }
}
