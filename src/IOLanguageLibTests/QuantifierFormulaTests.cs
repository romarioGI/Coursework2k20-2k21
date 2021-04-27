using IOLanguageLib.Alphabet;
using IOLanguageLib.Words;
using Xunit;

namespace IOLanguageLibTests
{
    public class QuantifierFormulaTests
    {
        [Fact]
        public void FreeObjectVariables_NotFreeVariables()
        {
            var quantifier = new UniversalQuantifier();
            var variable = new ObjectVariable('x');
            var predicate = new EqualityPredicate();
            var leftTerm = new ObjectVariable('x');
            var rightTerm = new ObjectVariable('y');
            var subFormula = new PredicateFormula(predicate, leftTerm, rightTerm);
            var formula = new QuantifierFormula(quantifier, variable, subFormula);

            var actual = formula.FreeObjectVariables;
            var expected = new[] {new ObjectVariable('y')};
            
            Assert.Equal(expected, actual);
        }
    }
}