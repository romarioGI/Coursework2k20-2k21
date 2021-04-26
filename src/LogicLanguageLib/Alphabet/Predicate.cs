using System;
using LogicLanguageLib.Words;

namespace LogicLanguageLib.Alphabet
{
    public class Predicate : Symbol, IOperator
    {
        private readonly string _name;

        protected Predicate(string name, byte arity)
        {
            Arity = arity;

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Predicate name must not be null or empty");
            _name = name;
        }

        protected override string DefaultRepresentation => $"p_{_name}_{Arity}";

        public byte Arity { get; }
    }
}