using System;
using System.Collections.Generic;

namespace IOLib
{
    public static class Alphabet
    {
        public static readonly Symbol Plus = new("+");
        public static readonly Symbol Minus = new("-");
        public static readonly Symbol Division = new("/");
        public static readonly Symbol Multiplication = new("*");
        public static readonly Symbol Exponentiation = new("^");

        public static readonly Symbol Less = new("<");
        public static readonly Symbol More = new(">");
        public static readonly Symbol Equal = new("=");

        public static readonly Symbol True = new("TRUE");
        public static readonly Symbol False = new("FALSE");

        public static readonly Symbol Conjunction = new("&");
        public static readonly Symbol Disjunction = new("∨");
        public static readonly Symbol Implication = new("→");
        public static readonly Symbol Negation = new("¬");

        public static readonly Symbol ExistentialQuantifier = new("∃");
        public static readonly Symbol UniversalQuantifier = new("∀");

        public static readonly Symbol Comma = new(",");
        public static readonly Symbol LeftBracket = new("(");
        public static readonly Symbol RightBracket = new(")");
        public static readonly Symbol Space = new(" ");

        public static readonly Symbol Underlining = new("_");

        public static readonly IReadOnlyDictionary<char, Symbol> Digits = GetDigits();

        public static readonly IReadOnlyDictionary<char, Symbol> Letters = GetLetters();

        private static Dictionary<char, Symbol> GetDigits()
        {
            var result = new Dictionary<char, Symbol>();
            for (var i = '0'; i <= '9'; i++)
                result[i] = new Symbol(i.ToString());

            return result;
        }

        public static Symbol Digit(char c)
        {
            if (!char.IsDigit(c))
                throw new ArgumentException("Character must be digit.");

            return Digits[c];
        }

        private static Dictionary<char, Symbol> GetLetters()
        {
            var result = new Dictionary<char, Symbol>();
            for (var i = 'a'; i <= 'z'; i++)
                result[i] = new Symbol(i.ToString());

            for (var i = 'A'; i <= 'Z'; i++)
                result[i] = new Symbol(i.ToString());

            return result;
        }

        public static Symbol Letter(char c)
        {
            if (!char.IsLetter(c))
                throw new ArgumentException("Character must be letter.");

            return Letters[c];
        }
    }
}