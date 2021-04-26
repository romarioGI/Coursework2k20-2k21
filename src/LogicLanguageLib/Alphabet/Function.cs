using System;
using LogicLanguageLib.Words;

namespace LogicLanguageLib.Alphabet
{
    public abstract class Function : Symbol, IOperator
    {
        private readonly string _name;

        protected Function(string name, byte arity)
        {
            Arity = arity;

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Function name must not be null or empty");
            _name = name;
        }

        protected override string DefaultRepresentation => $"f_{_name}_{Arity}";

        public byte Arity { get; }
    }
}