using IOLib.Exceptions;
using IOLib.Language;

namespace IOLib.Input
{
    //TODO tests
    //TODO что делать с пробелами?
    public class Parser : IParser
    {
        public Formula Parse(Word input)
        {
            var formula = F1(input, 0, out var newIndex);

            if (newIndex != input.Length)
                throw new UnexpectedToken(input[newIndex], "Expected end of input.");

            return formula;
        }

        private static Formula F1(Word input, int index, out int newIndex)
        {
            try
            {
                var quantifier = Q(input, index, out newIndex);
                var objectVariable = V(input, newIndex, out newIndex);
                var subFormula = F1(input, newIndex, out newIndex);

                return new QuantifierFormula(quantifier, objectVariable, subFormula);
            }
            catch (InputException)
            {
            }

            try
            {
                return F2(input, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(input[index]);
        }

        private static Symbol Q(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected quantifier.");

            var current = input[index].Symbol;

            if (!current.Equals(Alphabet.UniversalQuantifier) &&
                !current.Equals(Alphabet.ExistentialQuantifier))
                throw new UnexpectedToken(input[index], "Expected quantifier.");

            newIndex = index + 1;
            return current;
        }

        private static ObjectVariable V(Word input, int index, out int newIndex)
        {
            try
            {
                var letter = L(input, index, out newIndex);
                Ul(input, newIndex, out newIndex);
                var constant = N(input, newIndex, out newIndex);

                return new ObjectVariable(letter, constant);
            }
            catch (InputException)
            {
            }

            try
            {
                var letter = L(input, index, out newIndex);
                return new ObjectVariable(letter);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(input[index]);
        }

        private static Symbol L(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected letter.");

            var current = input[index].Symbol;

            if (!Alphabet.IsLetter(current))
                throw new UnexpectedToken(input[index], "Expected letter.");

            newIndex = index + 1;
            return current;
        }

        private static void Ul(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected underlining.");

            var current = input[index].Symbol;

            if (!current.Equals(Alphabet.Underlining))
                throw new UnexpectedToken(input[index], "Expected underlining.");

            newIndex = index + 1;
        }

        private static IndividualConstant N(Word input, int index, out int newIndex)
        {
            try
            {
                var digit = D(input, index, out newIndex);
                var constant = N(input, newIndex, out newIndex);

                return new IndividualConstant(digit, constant);
            }
            catch (InputException)
            {
            }

            try
            {
                var digit = D(input, index, out newIndex);

                return new IndividualConstant(digit);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(input[index]);
        }

        private static Symbol D(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected digit.");

            var current = input[index].Symbol;

            if (!Alphabet.IsDigit(current))
                throw new UnexpectedToken(input[index], "Expected digit.");

            newIndex = index + 1;
            return current;
        }

        private static Formula F2(Word input, int index, out int newIndex)
        {
            try
            {
                var f3 = F3(input, index, out newIndex);
                var implication = Implication(input, newIndex, out newIndex);
                var f2 = F2(input, newIndex, out newIndex);

                return new PropositionalConnectiveFormula(implication, f3, f2);
            }
            catch (InputException)
            {
            }

            try
            {
                return F3(input, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(input[index]);
        }

        private static Formula F3(Word input, int index, out int newIndex)
        {
            try
            {
                var f4 = F4(input, index, out newIndex);
                var disjunction = Disjunction(input, newIndex, out newIndex);
                var f3 = F3(input, newIndex, out newIndex);

                return new PropositionalConnectiveFormula(disjunction, f4, f3);
            }
            catch (InputException)
            {
            }

            try
            {
                return F4(input, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(input[index]);
        }

        private static Formula F4(Word input, int index, out int newIndex)
        {
            try
            {
                var f5 = F5(input, index, out newIndex);
                var conjunction = Conjunction(input, newIndex, out newIndex);
                var f4 = F4(input, newIndex, out newIndex);

                return new PropositionalConnectiveFormula(conjunction, f5, f4);
            }
            catch (InputException)
            {
            }

            try
            {
                return F5(input, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(input[index]);
        }

        private static Formula F5(Word input, int index, out int newIndex)
        {
            try
            {
                var negation = Negation(input, index, out newIndex);
                var f5 = F5(input, newIndex, out newIndex);

                return new PropositionalConnectiveFormula(negation, f5);
            }
            catch (InputException)
            {
            }

            try
            {
                return F6(input, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(input[index]);
        }

        private static Formula F6(Word input, int index, out int newIndex)
        {
            try
            {
                var p0 = P0(input, index, out newIndex);

                return new PredicateFormula(p0);
            }
            catch (InputException)
            {
            }

            try
            {
                var t1 = T(input, index, out newIndex);
                var p2 = P2(input, newIndex, out newIndex);
                var t2 = T(input, newIndex, out newIndex);

                return new PredicateFormula(p2, t1, t2);
            }
            catch (InputException)
            {
            }

            try
            {
                LBracket(input, index, out newIndex);
                var f = F1(input, newIndex, out newIndex);
                RBracket(input, newIndex, out newIndex);

                return f;
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(input[index]);
        }

        private static Symbol P0(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected zero arity predicate.");

            var current = input[index].Symbol;

            if (!Alphabet.IsDigit(current))
                throw new UnexpectedToken(input[index], "Expected zero arity predicate.");

            newIndex = index + 1;
            return current;
        }

        private static Term T(Word input, int index, out int newIndex)
        {
            return T1(input, index, out newIndex);
        }

        private static Term T1(Word input, int index, out int newIndex)
        {
            try
            {
                var t2 = T2(input, index, out newIndex);
                var plusOrMinus = PlusOrMinus(input, newIndex, out newIndex);
                var t1 = T1(input, newIndex, out newIndex);

                return new FunctionTerm(plusOrMinus, t2, t1);
            }
            catch (InputException)
            {
            }

            try
            {
                return T2(input, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(input[index]);
        }

        private static Term T2(Word input, int index, out int newIndex)
        {
            try
            {
                var t3 = T3(input, index, out newIndex);
                var multiplicationOrDivision = MultiplicationOrDivision(input, newIndex, out newIndex);
                var t2 = T2(input, newIndex, out newIndex);

                return new FunctionTerm(multiplicationOrDivision, t3, t2);
            }
            catch (InputException)
            {
            }

            try
            {
                return T3(input, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(input[index]);
        }

        private static Term T3(Word input, int index, out int newIndex)
        {
            try
            {
                var t4 = T4(input, index, out newIndex);
                var exponentiation = Exponentiation(input, newIndex, out newIndex);
                var n = N(input, newIndex, out newIndex);

                return new FunctionTerm(exponentiation, t4, n);
            }
            catch (InputException)
            {
            }

            try
            {
                return T4(input, index, out newIndex);
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(input[index]);
        }

        private static Term T4(Word input, int index, out int newIndex)
        {
            try
            {
                return V(input, index, out newIndex);
            }
            catch (InputException)
            {
            }

            try
            {
                return N(input, index, out newIndex);
            }
            catch (InputException)
            {
            }

            try
            {
                LBracket(input, index, out newIndex);
                var t = T(input, newIndex, out newIndex);
                RBracket(input, newIndex, out newIndex);

                return t;
            }
            catch (InputException)
            {
            }

            throw new UnexpectedToken(input[index]);
        }

        private static Symbol Exponentiation(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected exponentiation.");

            var current = input[index].Symbol;

            if (!current.Equals(Alphabet.Exponentiation))
                throw new UnexpectedToken(input[index], "Expected exponentiation.");

            newIndex = index + 1;
            return current;
        }

        private static Symbol MultiplicationOrDivision(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected multiplication or division.");

            var current = input[index].Symbol;

            if (!current.Equals(Alphabet.Multiplication) && !current.Equals(Alphabet.Division))
                throw new UnexpectedToken(input[index], "Expected multiplication or division.");

            newIndex = index + 1;
            return current;
        }

        private static Symbol PlusOrMinus(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected plus or minus.");

            var current = input[index].Symbol;

            if (!current.Equals(Alphabet.Plus) && !current.Equals(Alphabet.Minus))
                throw new UnexpectedToken(input[index], "Expected plus or minus.");

            newIndex = index + 1;
            return current;
        }

        private static Symbol P2(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected binary predicate.");

            var current = input[index].Symbol;

            if (!current.Equals(Alphabet.More) && !current.Equals(Alphabet.Less) && !current.Equals(Alphabet.Equal))
                throw new UnexpectedToken(input[index], "Expected binary predicate.");

            newIndex = index + 1;
            return current;
        }

        private static void LBracket(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected left bracket.");

            var current = input[index].Symbol;

            if (!current.Equals(Alphabet.LeftBracket))
                throw new UnexpectedToken(input[index], "Expected left bracket.");

            newIndex = index + 1;
        }

        private static void RBracket(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected right bracket.");

            var current = input[index].Symbol;

            if (!current.Equals(Alphabet.RightBracket))
                throw new UnexpectedToken(input[index], "Expected right bracket.");

            newIndex = index + 1;
        }

        private static Symbol Implication(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected implication.");

            var current = input[index].Symbol;

            if (!current.Equals(Alphabet.Implication))
                throw new UnexpectedToken(input[index], "Expected implication.");

            newIndex = index + 1;
            return current;
        }

        private static Symbol Disjunction(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected disjunction.");

            var current = input[index].Symbol;

            if (!current.Equals(Alphabet.Disjunction))
                throw new UnexpectedToken(input[index], "Expected disjunction.");

            newIndex = index + 1;
            return current;
        }

        private static Symbol Conjunction(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected conjunction.");

            var current = input[index].Symbol;

            if (!current.Equals(Alphabet.Conjunction))
                throw new UnexpectedToken(input[index], "Expected conjunction.");

            newIndex = index + 1;
            return current;
        }

        private static Symbol Negation(Word input, int index, out int newIndex)
        {
            if (index >= input.Length)
                throw new UnexpectedEndOfInput("Expected negation.");

            var current = input[index].Symbol;

            if (!current.Equals(Alphabet.Negation))
                throw new UnexpectedToken(input[index], "Expected negation.");

            newIndex = index + 1;
            return current;
        }
    }
}