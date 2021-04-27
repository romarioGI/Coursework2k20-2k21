using System.Collections.Generic;
using IOLanguageLib.Parsing.Contexts;
using LogicLanguageLib.Alphabet;

namespace IOLanguageLib.Parsing.PreParsing
{
    public class QuantifierPreParser : PreParser
    {
        public override IEnumerable<Symbol> Parse(IEnumerable<Symbol> input)
        {
            var context = new SymbolContextWithNext(input);

            return PreParse(context);
        }

        private static IEnumerable<Symbol> PreParse(SymbolContextWithNext context)
        {
            while (context.MoveNext())
                if (context.CurrentSymbol is LeftBracket && context.NextSymbol is Quantifier)
                {
                    yield return context.NextSymbol;
                    yield return context.CurrentSymbol;
                    context.MoveNext();
                }
                else
                {
                    yield return context.CurrentSymbol;
                }
        }
    }
}