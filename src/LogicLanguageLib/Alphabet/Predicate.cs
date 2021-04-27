using System;

namespace LogicLanguageLib.Alphabet
{
    public abstract class Predicate : Symbol, IOperator
    {
        private readonly string _name;

        protected Predicate(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Predicate name must not be null or empty");
            _name = name;
        }

        protected override string DefaultRepresentation => $"p_{_name}_{Arity}";

        public abstract byte Arity { get; }

        public abstract Associativity Associativity { get; }

        public abstract Notation Notation { get; }
    }
}