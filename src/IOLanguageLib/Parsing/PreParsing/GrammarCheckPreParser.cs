using System;
using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Exceptions;
using IOLanguageLib.Parsing.Contexts;

namespace IOLanguageLib.Parsing.PreParsing
{
    //TODO tests, maybe integration
    public class GrammarCheckPreParser : PreParser
    {
        public override IEnumerable<Symbol> Parse(IEnumerable<Symbol> input)
        {
            var context = new SymbolContext(input);

            var res = ExpectFormula(context);

            if (context.IsEnded)
                return res;

            throw BuildException(context, "Expected end of input.");
        }

        private static Exception BuildException(SymbolContext context, string message)
        {
            if (context.IsEnded)
                return new UnexpectedEndOfInput(message);

            return new UnexpectedSymbol(context.Index, message);
        }

        private static IEnumerable<Symbol> ExpectFormula(SymbolContext context)
        {
            return ExpectF1(context)
                .Concat(ExpectF2(context));
        }

        private static IEnumerable<Symbol> ExpectF1(SymbolContext context)
        {
            return context.CurrentSymbol switch
            {
                Quantifier => ExpectQuantifier(context)
                    .Concat(ExpectObjectVariable(context))
                    .Concat(ExpectFormula(context)),
                PropositionalConnective => ExpectPrefixUnaryConnective(context)
                    .Concat(ExpectFormula(context)),
                LeftBracket => ExpectLeftBracket(context)
                    .Concat(ExpectFormula(context))
                    .Concat(ExpectRightBracket(context)),
                _ => ExpectTerm(context)
                    .Concat(ExpectInfixBinaryPredicate(context))
                    .Concat(ExpectTerm(context))
            };
        }

        private static IEnumerable<Symbol> ReturnCurrentAndMove(SymbolContext context)
        {
            yield return context.CurrentSymbol;
            context.MoveNext();
        }

        private static IEnumerable<Symbol> ExpectQuantifier(SymbolContext context)
        {
            if (context.CurrentSymbol is not Quantifier)
                throw BuildException(context, "Expected quantifier.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectObjectVariable(SymbolContext context)
        {
            if (context.CurrentSymbol is not ObjectVariable)
                throw BuildException(context, "Expected object variable.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectPrefixUnaryConnective(SymbolContext context)
        {
            if (context.CurrentSymbol is not PropositionalConnective {Notation: Notation.Prefix, Arity: 1})
                throw BuildException(context, "Expected prefix unary propositional connective.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectLeftBracket(SymbolContext context)
        {
            if (context.CurrentSymbol is not LeftBracket)
                throw BuildException(context, "Expected left bracket.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectRightBracket(SymbolContext context)
        {
            if (context.CurrentSymbol is not RightBracket)
                throw BuildException(context, "Expected right bracket.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectInfixBinaryPredicate(SymbolContext context)
        {
            if (context.CurrentSymbol is not Predicate {Notation: Notation.Infix, Arity: 2})
                throw BuildException(context, "Expected infix binary predicate.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectTerm(SymbolContext context)
        {
            return ExpectT1(context)
                .Concat(ExpectT2(context));
        }

        private static IEnumerable<Symbol> ExpectT1(SymbolContext context)
        {
            return context.CurrentSymbol switch
            {
                ObjectVariable => ExpectObjectVariable(context),
                IndividualConstant => ExpectIndividualConstant(context),
                Function {Notation: Notation.Prefix, Arity: 1} => ExpectPrefixUnaryFunction(context),
                _ => ExpectTermLeftBracket(context).Concat(ExpectTerm(context)).Concat(ExpectTermRightBracket(context))
            };
        }

        private static IEnumerable<Symbol> ExpectTermLeftBracket(SymbolContext context)
        {
            if (context.CurrentSymbol is not TermLeftBracket leftBracket)
                throw BuildException(context, "Expected term left bracket.");

            yield return leftBracket.LeftBracket;
            context.MoveNext();
        }

        private static IEnumerable<Symbol> ExpectTermRightBracket(SymbolContext context)
        {
            if (context.CurrentSymbol is not TermRightBracket rightBracket)
                throw BuildException(context, "Expected term right bracket.");

            yield return rightBracket.RightBracket;
            context.MoveNext();
        }

        private static IEnumerable<Symbol> ExpectIndividualConstant(SymbolContext context)
        {
            if (context.CurrentSymbol is not IndividualConstant)
                throw BuildException(context, "Expected constant.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectPrefixUnaryFunction(SymbolContext context)
        {
            if (context.CurrentSymbol is not Function {Notation: Notation.Prefix, Arity: 1})
                throw BuildException(context, "Expected prefix unary function.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectT2(SymbolContext context)
        {
            if (context.IsEnded)
                return ExpectEpsilon(context);

            return context.CurrentSymbol is Exponentiation
                ? ReturnCurrentAndMove(context).Concat(ExpectIndividualConstant(context))
                : ExpectInfixBinaryFunction(context).Concat(ExpectTerm(context));
        }

        private static IEnumerable<Symbol> ExpectEpsilon(SymbolContext context)
        {
            if (context.IsEnded)
                yield break;

            throw new UnexpectedSymbol(context.Index, "Expected end.");
        }

        private static IEnumerable<Symbol> ExpectInfixBinaryFunction(SymbolContext context)
        {
            if (context.CurrentSymbol is not Function {Notation: Notation.Infix, Arity: 2})
                throw BuildException(context, "Expected infix binary function.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectF2(SymbolContext context)
        {
            return context.IsEnded
                ? ExpectEpsilon(context)
                : ExpectInfixBinaryConnective(context).Concat(ExpectFormula(context));
        }

        private static IEnumerable<Symbol> ExpectInfixBinaryConnective(SymbolContext context)
        {
            if (context.CurrentSymbol is not PropositionalConnective {Notation: Notation.Infix, Arity: 2})
                throw BuildException(context, "Expected infix binary propositional connective.");

            return ReturnCurrentAndMove(context);
        }
    }
}