using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamTV.Utilities.Identification
{
    public class Code
    {
        private int QtCaracteres;

        public Code()
        {
            QtCaracteres = 5;
        }

        public string GenerateCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var random = new Random();
            
            var codeGenerated = new string(
                Enumerable.Repeat(chars, QtCaracteres)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return codeGenerated.ToUpper();
        }
    }
}
