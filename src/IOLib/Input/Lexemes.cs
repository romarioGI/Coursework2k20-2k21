using System.Collections.Generic;
using IOLib.Language;

namespace IOLib.Input
{
    public static class Lexemes
    {
        public static IEnumerable<Lexeme> All => GetAll();

        private static IEnumerable<Lexeme> GetAll()
        {
            yield return new Lexeme(Alphabet.Plus, "+");

            yield return new Lexeme(Alphabet.Minus, "-");

            yield return new Lexeme(Alphabet.Division, "/");
            yield return new Lexeme(Alphabet.Division, "\\over");

            yield return new Lexeme(Alphabet.Multiplication, "*");

            yield return new Lexeme(Alphabet.Exponentiation, "^");

            yield return new Lexeme(Alphabet.Less, "<");

            yield return new Lexeme(Alphabet.More, ">");

            yield return new Lexeme(Alphabet.Equal, "=");

            yield return new Lexeme(Alphabet.True, "\\true");

            yield return new Lexeme(Alphabet.False, "\\false");

            yield return new Lexeme(Alphabet.Conjunction, "\\land");

            yield return new Lexeme(Alphabet.Disjunction, "\\lor");

            yield return new Lexeme(Alphabet.Implication, "\\to");

            yield return new Lexeme(Alphabet.Negation, "\\lnot");

            yield return new Lexeme(Alphabet.ExistentialQuantifier, "\\exists");

            yield return new Lexeme(Alphabet.UniversalQuantifier, "\\forall");

            yield return new Lexeme(Alphabet.Comma, ",");

            yield return new Lexeme(Alphabet.LeftBracket, "(");

            yield return new Lexeme(Alphabet.RightBracket, ")");

            yield return new Lexeme(Alphabet.Space, " ");

            yield return new Lexeme(Alphabet.Underlining, "_");

            foreach (var (c, symbol) in Alphabet.Digits)
                yield return new Lexeme(symbol, c.ToString());

            foreach (var (c, symbol) in Alphabet.Letters)
                yield return new Lexeme(symbol, c.ToString());
        }
    }
}