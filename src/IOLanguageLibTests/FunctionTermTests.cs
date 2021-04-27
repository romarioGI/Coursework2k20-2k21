using IOLanguageLib.Alphabet;
using IOLanguageLib.Words;
using Xunit;

namespace IOLanguageLibTests
{
    public class FunctionTermTests
    {
        [Fact]
        public void ToStringTest()
        {
            var function = new Addition();
            ITerm leftTerm = new IndividualConstant(2);
            ITerm rightTerm = new IndividualConstant(40);
            var functionTerm = new FunctionTerm(function, leftTerm, rightTerm);

            var actual = functionTerm.ToString();
            var expected = $"{function}({leftTerm},{rightTerm})";
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FreeObjectVariables_DuplicateVariables()
        {
            var function = new Subtraction();
            ITerm leftTerm = new ObjectVariable('x', 0);
            ITerm rightTerm = new ObjectVariable('x', 0);
            var functionTerm = new FunctionTerm(function, leftTerm, rightTerm);

            var actual = functionTerm.FreeObjectVariables;
            var expected = new[] {new ObjectVariable('x', 0)};
            
            Assert.Equal(expected, actual);
        }
    }
}