using System;
using System.Collections.Generic;
using LogicLanguageLib.Words;

namespace LogicLanguageLib.Alphabet
{
    public sealed class ObjectVariable : Symbol, ITerm
    {
        public readonly char Char;
        public readonly uint? Index;

        public ObjectVariable(char c, uint? i = null)
        {
            if (!char.IsLetter(c))
                throw new ArgumentException("c should be letter");

            Char = c;
            Index = i;
        }

        protected override string DefaultRepresentation => $"{Char}{(Index is null ? "" : $"_{Index}")}";

        public IEnumerable<ObjectVariable> FreeObjectVariables
        {
            get { yield return this; }
        }
    }
}