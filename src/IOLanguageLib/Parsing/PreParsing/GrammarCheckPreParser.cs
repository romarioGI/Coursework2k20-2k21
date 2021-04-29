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

            return ExpectF(context);
        }

        private static IEnumerable<Symbol> ExpectF(SymbolContext context)
        {
            return ExpectF1(context)
                .Concat(ExpectF2(context))
                .Concat(ExpectEpsilon(context));
        }

        private static IEnumerable<Symbol> ExpectF1(SymbolContext context)
        {
            return context.CurrentSymbol switch
            {
                Quantifier => ExpectQuantifier(context)
                    .Concat(ExpectObjectVariable(context))
                    .Concat(ExpectF(context)),
                PropositionalConnective => ExpectPrefixUnaryConnective(context)
                    .Concat(ExpectF(context)),
                LeftBracket => ExpectLeftBracket(context)
                    .Concat(ExpectF(context))
                    .Concat(ExpectRightBracket(context)),
                _ => ExpectT(context)
                    .Concat(ExpectInfixBinaryPredicate(context))
                    .Concat(ExpectT(context))
            };
        }

        private static IEnumerable<Symbol> ExpectEpsilon(SymbolContext context)
        {
            if (context.IsEnded)
                yield break;

            throw new UnexpectedSymbol(context.Index, "Expected end.");
        }

        private static IEnumerable<Symbol> ReturnCurrentAndMove(SymbolContext context)
        {
            yield return context.CurrentSymbol;
            context.MoveNext();
        }

        private static void ThrowException(SymbolContext context, string message)
        {
            if (context.IsEnded)
                throw new UnexpectedEndOfInput(message);

            throw new UnexpectedSymbol(context.Index, message);
        }

        private static IEnumerable<Symbol> ExpectQuantifier(SymbolContext context)
        {
            if (context.CurrentSymbol is not Quantifier)
                ThrowException(context, "Expected quantifier.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectObjectVariable(SymbolContext context)
        {
            if (context.CurrentSymbol is not ObjectVariable)
                ThrowException(context, "Expected object variable.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectPrefixUnaryConnective(SymbolContext context)
        {
            if (context.CurrentSymbol is not PropositionalConnective {Notation: Notation.Prefix, Arity: 1})
                ThrowException(context, "Expected prefix unary propositional connective.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectLeftBracket(SymbolContext context)
        {
            if (context.CurrentSymbol is not LeftBracket)
                ThrowException(context, "Expected left bracket.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectRightBracket(SymbolContext context)
        {
            if (context.CurrentSymbol is not RightBracket)
                ThrowException(context, "Expected right bracket.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectInfixBinaryPredicate(SymbolContext context)
        {
            if (context.CurrentSymbol is not Predicate {Notation: Notation.Infix, Arity: 2})
                ThrowException(context, "Expected infix binary predicate.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectT(SymbolContext context)
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
                _ => ExpectLeftBracket(context).Concat(ExpectT(context)).Concat(ExpectRightBracket(context))
            };
        }

        private static IEnumerable<Symbol> ExpectIndividualConstant(SymbolContext context)
        {
            if (context.CurrentSymbol is not IndividualConstant)
                ThrowException(context, "Expected constant.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectPrefixUnaryFunction(SymbolContext context)
        {
            if (context.CurrentSymbol is not Function {Notation: Notation.Prefix, Arity: 1})
                ThrowException(context, "Expected prefix unary function.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectT2(SymbolContext context)
        {
            return context.IsEnded
                ? ExpectEpsilon(context)
                : ExpectInfixBinaryFunction(context).Concat(ExpectT(context));
        }

        private static IEnumerable<Symbol> ExpectInfixBinaryFunction(SymbolContext context)
        {
            if (context.CurrentSymbol is not Function {Notation: Notation.Infix, Arity: 2})
                ThrowException(context, "Expected infix binary function.");

            return ReturnCurrentAndMove(context);
        }

        private static IEnumerable<Symbol> ExpectF2(SymbolContext context)
        {
            return context.IsEnded
                ? ExpectEpsilon(context)
                : ExpectInfixBinaryConnective(context).Concat(ExpectF(context));
        }

        private static IEnumerable<Symbol> ExpectInfixBinaryConnective(SymbolContext context)
        {
            if (context.CurrentSymbol is not PropositionalConnective {Notation: Notation.Infix, Arity: 2})
                ThrowException(context, "Expected infix binary propositional connective.");

            return ReturnCurrentAndMove(context);
        }
    }
}