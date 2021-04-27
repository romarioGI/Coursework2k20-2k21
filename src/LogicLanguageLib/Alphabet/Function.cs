using System;

namespace LogicLanguageLib.Alphabet
{
    public abstract class Function : Symbol, IOperator
    {
        private readonly string _name;

        protected Function(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Function name must not be null or empty");
            _name = name;
        }

        protected override string DefaultRepresentation => $"f_{_name}_{Arity}";

        public abstract byte Arity { get; }

        public abstract Associativity Associativity { get; }

        public abstract Notation Notation { get; }
    }
}