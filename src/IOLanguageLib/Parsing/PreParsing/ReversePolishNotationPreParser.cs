using System.Collections.Generic;
using System.Linq;
using IOLanguageLib.Alphabet;
using IOLanguageLib.Parsing.Contexts;

namespace IOLanguageLib.Parsing.PreParsing
{
    //TODO test, maybe integration
    public class ReversePolishNotationPreParser : PreParser
    {
        private static readonly DijkstraSymbolComparer Comparer = new();

        public override IEnumerable<Symbol> Parse(IEnumerable<Symbol> input)
        {
            var context = new SymbolContextWithStack<Symbol>(input);

            var res = Enumerable.Empty<Symbol>();
            while (context.MoveNext())
                res = res.Concat(PreParse(context));

            res = res.Concat(GetRemainingSymbols(context));

            return res;
        }

        private static IEnumerable<Symbol> GetRemainingSymbols(SymbolContextWithStack<Symbol> context)
        {
            while (!context.StackIsEmpty)
            {
                var top = context.Pop();
                if (top is IOperator)
                    yield return top;
            }
        }

        private static IEnumerable<Symbol> PushCurrent(SymbolContextWithStack<Symbol> context)
        {
            context.Push(context.CurrentSymbol);
            yield break;
        }

        private static IEnumerable<Symbol> PreParse(SymbolContextWithStack<Symbol> context)
        {
            return context.CurrentSymbol switch
            {
                LeftBracket => PushCurrent(context),
                RightBracket => ProcessRightBracket(context),
                IndividualConstant or ObjectVariable => context.CurrentSymbol.Yield(),
                _ => ProcessOther(context)
            };
        }

        private static IEnumerable<Symbol> ProcessRightBracket(SymbolContextWithStack<Symbol> context)
        {
            while (!context.StackIsEmpty)
            {
                var top = context.Pop();
                if (top is IOperator)
                    yield return top;

                break;
            }
        }

        private static IEnumerable<Symbol> ProcessOther(SymbolContextWithStack<Symbol> context)
        {
            while (!context.StackIsEmpty
                   && context.Peek is not LeftBracket
                   && Comparer.Compare(context.Peek, context.CurrentSymbol) < 0)
                yield return context.Pop();

            context.Push(context.CurrentSymbol);
        }
    }
}