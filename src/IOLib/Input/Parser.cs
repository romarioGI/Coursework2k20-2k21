using System;
using System.Collections.Generic;
using System.Linq;
using IOLib.Exceptions;
using IOLib.Language;

namespace IOLib.Input
{
    //TODO tests
    //TODO DRY
    public class Parser : IParser
    {
        public Formula Parse(IEnumerable<Token> input)
        {
            var word = input
                .Where(t => !t.Symbol.Equals(Alphabet.Space))
                .ToArray();

            var formula = F1(word, 0, out var newIndex);

            if (newIndex != word.Length)
                throw new UnexpectedToken(word[newIndex].Index, "Expected end of input.");

            return formula;
        }

        private static Symbol CheckAndGetSymbol(Token[] word, int index, out int newIndex, string name,
            Func<Symbol, bool> check)
        {
            if (index >= word.Length)
                throw new UnexpectedEndOfInput($"Expected {name}.");

            var current = word[index].Symbol;

            if (!check(current))
                throw new UnexpectedToken(word[index].Index, $"Expected {name}.");

            newIndex = index + 1;

            return current;
        }

        private static Symbol CheckAndGetSymbol(Token[] word, int index, out int newIndex, string name,
            params Symbol[] expected)
        {
            var check = new Func<Symbol, bool>(symbol => expected.Any(symbol.Equals));

            return CheckAndGetSymbol(word, index, out newIndex, name, check);
        }

        private static Formula F1(Token[] word, int index, out int newIndex)
        {
            try
            {
                var quantifier = Q(word, index, out newIndex);
                var objectVariable = V(word, newIndex, out newIndex);
                var subFormula = F1(word, newIndex, out newIndex);

                return new QuantifierFormula(quantifier, objectVariable, subFormula);
            }
            catch (InputException)
            {
            }

            try
            {
                return F2(word, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(word[index].Index);
        }

        private static Symbol Q(Token[] word, int index, out int newIndex)
        {
            return CheckAndGetSymbol(word, index, out newIndex, "quantifier", Alphabet.UniversalQuantifier,
                Alphabet.ExistentialQuantifier);
        }

        private static ObjectVariable V(Token[] word, int index, out int newIndex)
        {
            try
            {
                var letter = L(word, index, out newIndex);
                Ul(word, newIndex, out newIndex);
                var constant = N(word, newIndex, out newIndex);

                return new ObjectVariable(letter, constant);
            }
            catch (InputException)
            {
            }

            try
            {
                var letter = L(word, index, out newIndex);
                return new ObjectVariable(letter);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(word[index].Index);
        }

        private static Symbol L(Token[] word, int index, out int newIndex)
        {
            return CheckAndGetSymbol(word, index, out newIndex, "letter", Alphabet.IsLetter);
        }

        private static void Ul(Token[] word, int index, out int newIndex)
        {
            CheckAndGetSymbol(word, index, out newIndex, "underlining", Alphabet.Underlining);
        }

        private static IndividualConstant N(Token[] word, int index, out int newIndex)
        {
            try
            {
                var digit = D(word, index, out newIndex);
                var constant = N(word, newIndex, out newIndex);

                return new IndividualConstant(digit, constant);
            }
            catch (InputException)
            {
            }

            try
            {
                var digit = D(word, index, out newIndex);

                return new IndividualConstant(digit);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(word[index].Index);
        }

        private static Symbol D(Token[] word, int index, out int newIndex)
        {
            return CheckAndGetSymbol(word, index, out newIndex, "digit", Alphabet.IsDigit);
        }

        private static Formula F2(Token[] word, int index, out int newIndex)
        {
            try
            {
                var f3 = F3(word, index, out newIndex);
                var implication = Implication(word, newIndex, out newIndex);
                var f2 = F2(word, newIndex, out newIndex);

                return new PropositionalConnectiveFormula(implication, f3, f2);
            }
            catch (InputException)
            {
            }

            try
            {
                return F3(word, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(word[index].Index);
        }

        private static Formula F3(Token[] word, int index, out int newIndex)
        {
            try
            {
                var f4 = F4(word, index, out newIndex);
                var disjunction = Disjunction(word, newIndex, out newIndex);
                var f3 = F3(word, newIndex, out newIndex);

                return new PropositionalConnectiveFormula(disjunction, f4, f3);
            }
            catch (InputException)
            {
            }

            try
            {
                return F4(word, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(word[index].Index);
        }

        private static Formula F4(Token[] word, int index, out int newIndex)
        {
            try
            {
                var f5 = F5(word, index, out newIndex);
                var conjunction = Conjunction(word, newIndex, out newIndex);
                var f4 = F4(word, newIndex, out newIndex);

                return new PropositionalConnectiveFormula(conjunction, f5, f4);
            }
            catch (InputException)
            {
            }

            try
            {
                return F5(word, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(word[index].Index);
        }

        private static Formula F5(Token[] word, int index, out int newIndex)
        {
            try
            {
                var negation = Negation(word, index, out newIndex);
                var f5 = F5(word, newIndex, out newIndex);

                return new PropositionalConnectiveFormula(negation, f5);
            }
            catch (InputException)
            {
            }

            try
            {
                return F6(word, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(word[index].Index);
        }

        private static Formula F6(Token[] word, int index, out int newIndex)
        {
            try
            {
                var p0 = P0(word, index, out newIndex);

                return new PredicateFormula(p0);
            }
            catch (InputException)
            {
            }

            try
            {
                var t1 = T(word, index, out newIndex);
                var p2 = P2(word, newIndex, out newIndex);
                var t2 = T(word, newIndex, out newIndex);

                return new PredicateFormula(p2, t1, t2);
            }
            catch (InputException)
            {
            }

            try
            {
                LBracket(word, index, out newIndex);
                var f = F1(word, newIndex, out newIndex);
                RBracket(word, newIndex, out newIndex);

                return f;
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(word[index].Index);
        }

        private static Symbol P0(Token[] word, int index, out int newIndex)
        {
            return CheckAndGetSymbol(word, index, out newIndex, "zero arity predicate", Alphabet.True, Alphabet.False);
        }

        private static Term T(Token[] word, int index, out int newIndex)
        {
            return T1(word, index, out newIndex);
        }

        private static Term T1(Token[] word, int index, out int newIndex)
        {
            try
            {
                var t2 = T2(word, index, out newIndex);
                var plusOrMinus = PlusOrMinus(word, newIndex, out newIndex);
                var t1 = T1(word, newIndex, out newIndex);

                return new FunctionTerm(plusOrMinus, t2, t1);
            }
            catch (InputException)
            {
            }

            try
            {
                return T2(word, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(word[index].Index);
        }

        private static Term T2(Token[] word, int index, out int newIndex)
        {
            try
            {
                var t3 = T3(word, index, out newIndex);
                var multiplicationOrDivision = MultiplicationOrDivision(word, newIndex, out newIndex);
                var t2 = T2(word, newIndex, out newIndex);

                return new FunctionTerm(multiplicationOrDivision, t3, t2);
            }
            catch (InputException)
            {
            }

            try
            {
                return T3(word, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(word[index].Index);
        }

        private static Term T3(Token[] word, int index, out int newIndex)
        {
            try
            {
                var t4 = T4(word, index, out newIndex);
                var exponentiation = Exponentiation(word, newIndex, out newIndex);
                var n = N(word, newIndex, out newIndex);

                return new FunctionTerm(exponentiation, t4, n);
            }
            catch (InputException)
            {
            }

            try
            {
                return T4(word, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(word[index].Index);
        }

        private static Term T4(Token[] word, int index, out int newIndex)
        {
            try
            {
                var plusOrMinus = PlusOrMinus(word, index, out newIndex);
                var t5 = T5(word, newIndex, out newIndex);

                return new FunctionTerm(plusOrMinus, t5);
            }
            catch (InputException)
            {
            }

            try
            {
                return T5(word, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(word[index].Index);
        }

        private static Term T5(Token[] word, int index, out int newIndex)
        {
            try
            {
                return V(word, index, out newIndex);
            }
            catch (InputException)
            {
            }

            try
            {
                return N(word, index, out newIndex);
            }
            catch (InputException)
            {
            }

            try
            {
                LBracket(word, index, out newIndex);
                var t = T(word, newIndex, out newIndex);
                RBracket(word, newIndex, out newIndex);

                return t;
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(word[index].Index);
        }

        private static Symbol Exponentiation(Token[] word, int index, out int newIndex)
        {
            return CheckAndGetSymbol(word, index, out newIndex, "exponentiation", Alphabet.Exponentiation);
        }

        private static Symbol MultiplicationOrDivision(Token[] word, int index, out int newIndex)
        {
            return CheckAndGetSymbol(word, index, out newIndex, "multiplication or division", Alphabet.Multiplication,
                Alphabet.Division);
        }

        private static Symbol PlusOrMinus(Token[] word, int index, out int newIndex)
        {
            return CheckAndGetSymbol(word, index, out newIndex, "plus or minus", Alphabet.Plus,
                Alphabet.Minus);
        }

        private static Symbol P2(Token[] word, int index, out int newIndex)
        {
            return CheckAndGetSymbol(word, index, out newIndex, "binary predicate", Alphabet.More,
                Alphabet.Less, Alphabet.Equal);
        }

        private static void LBracket(Token[] word, int index, out int newIndex)
        {
            CheckAndGetSymbol(word, index, out newIndex, "left bracket", Alphabet.LeftBracket);
        }

        private static void RBracket(Token[] word, int index, out int newIndex)
        {
            CheckAndGetSymbol(word, index, out newIndex, "right bracket", Alphabet.RightBracket);
        }

        private static Symbol Implication(Token[] word, int index, out int newIndex)
        {
            return CheckAndGetSymbol(word, index, out newIndex, "implication", Alphabet.Implication);
        }

        private static Symbol Disjunction(Token[] word, int index, out int newIndex)
        {
            return CheckAndGetSymbol(word, index, out newIndex, "disjunction", Alphabet.Disjunction);
        }

        private static Symbol Conjunction(Token[] word, int index, out int newIndex)
        {
            return CheckAndGetSymbol(word, index, out newIndex, "conjunction", Alphabet.Conjunction);
        }

        private static Symbol Negation(Token[] word, int index, out int newIndex)
        {
            return CheckAndGetSymbol(word, index, out newIndex, "negation", Alphabet.Negation);
        }
    }
}