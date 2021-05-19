using System;
using System.Linq;
using IOLib;
using Xunit;

namespace IOLibTests
{
    public class LexemesTests
    {
        [Fact]
        public void CheckPrefixProperty()
        {
            var lexemes = Lexemes.All.Select(l => l.String).ToArray();

            for (var i = 0; i < lexemes.Length; i++)
            for (var j = 0; j < lexemes.Length; j++)
            {
                if (i == j)
                    continue;

                var pos = lexemes[i].IndexOf(lexemes[j], StringComparison.Ordinal);

                Assert.NotEqual(0, pos);
            }
        }
    }
}