using IOLib.Input;

namespace IOLib.Exceptions
{
    public class UnexpectedToken : InputException
    {
        public readonly Token Token;

        public UnexpectedToken(Token token, string message = null) : base(message)
        {
            Token = token;
        }
    }
}