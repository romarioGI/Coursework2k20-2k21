using System.Collections.Generic;
using IOLanguageLib.Alphabet;

namespace IOLanguageLib.Parsing.Contexts
{
    public class SymbolContextWithStack<T> : SymbolContext
    {
        private readonly Stack<T> _stack;

        public SymbolContextWithStack(IEnumerable<Symbol> symbols) : base(symbols)
        {
            _stack = new Stack<T>();
        }

        public int Count => _stack.Count;

        public bool StackIsEmpty => Count == 0;

        public T Peek => _stack.Peek();

        public void Push(T item)
        {
            _stack.Push(item);
        }

        public IEnumerable<T> PopItems(byte count)
        {
            while (count-- > 0)
                yield return _stack.Pop();
        }

        public T Pop()
        {
            return _stack.Pop();
        }
    }
}