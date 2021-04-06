using System;

namespace LogicLanguageLib.Alphabet
{
    public class Predicate : Symbol
    {
        public readonly byte Arity;

        public readonly string Name;

        public Predicate(string name, byte arity)
        {
            Arity = arity;

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Predicate name must not be null or empty");
            Name = name;
        }

        protected override string DefaultRepresentation => $"p_{Name}_{Arity}";
    }
}