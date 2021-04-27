using System.Collections.Generic;
using LogicLanguageLib.Alphabet;
using LogicLanguageLib.Words;

namespace IOLanguageLib.Parsing.Contexts
{
    public class ReversePolishNotationParserContext : SymbolContext
    {
        private readonly Stack<IWord> _stack;

        public ReversePolishNotationParserContext(IEnumerable<Symbol> symbols) : base(symbols)
        {
            _stack = new Stack<IWord>();
        }

        public int Count => _stack.Count;

        public IWord Peek => _stack.Peek();

        public void PushWord(IWord word)
        {
            _stack.Push(word);
        }

        public IEnumerable<IWord> PopWords(byte count)
        {
            while (count-- > 0)
                yield return _stack.Pop();
        }
    }
}