using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace HashComparer
{
    public class MyHashAlgorithm
    {
        public string Name { get; set; }
        public HashAlgorithm Algorithm { get; set; }

        public MyHashAlgorithm(string name, HashAlgorithm algorithm)
        {
            this.Name = name;
            this.Algorithm = algorithm;
        }
    }
}
