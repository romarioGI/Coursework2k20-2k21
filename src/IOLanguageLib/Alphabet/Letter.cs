using System;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.Alphabet
{
    public sealed class Letter : Symbol
    {
        public Letter(char c)
        {
            if (!char.IsLetter(c))
                throw new ArgumentException("symbol must be letter");

            DefaultRepresentation = c.ToString();
        }

        protected override string DefaultRepresentation { get; }

        public static implicit operator char(Letter letter)
        {
            return letter.DefaultRepresentation[0];
        }
    }
}