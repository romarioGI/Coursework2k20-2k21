using System;
using System.Numerics;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.Alphabet
{
    public class Digit : Symbol
    {
        public Digit(char c)
        {
            if (!char.IsDigit(c))
                throw new ArgumentException("symbol must be digit");

            DefaultRepresentation = c.ToString();
        }

        protected override string DefaultRepresentation { get; }

        public static implicit operator byte(Digit digit)
        {
            return byte.Parse(digit.DefaultRepresentation);
        }
        
        public static implicit operator BigInteger(Digit digit)
        {
            return BigInteger.Parse(digit.DefaultRepresentation);
        }
    }
}