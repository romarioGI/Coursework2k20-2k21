using System;
using System.Collections;
using System.Collections.Generic;
using IOLanguageLib.Words;

namespace IOLanguageLib.Alphabet
{
    public sealed class ObjectVariable : Symbol, ITerm
    {
        private readonly char _char;
        private readonly uint? _index;

        public ObjectVariable(char c, uint? i = null)
        {
            if (!char.IsLetter(c))
                throw new ArgumentException("c should be letter");

            _char = c;
            _index = i;
        }

        protected override string DefaultRepresentation => $"{_char}{(_index is null ? "" : $"_{_index}")}";

        public IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { yield return this; }
        }

        public IEnumerator<Symbol> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}