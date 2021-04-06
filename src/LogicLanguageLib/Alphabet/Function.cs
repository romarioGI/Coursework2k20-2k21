using System;

namespace LogicLanguageLib.Alphabet
{
    public abstract class Function : Symbol
    {
        public readonly byte Arity;

        public readonly string Name;

        public Function(string name, byte arity)
        {
            Arity = arity;

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Function name must not be null or empty");
            Name = name;
        }

        protected override string DefaultRepresentation => $"f_{Name}_{Arity}";
    }
}