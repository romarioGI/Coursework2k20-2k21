namespace IOLib.Exceptions
{
    public class UnexpectedEndOfInput : InputException
    {
        public UnexpectedEndOfInput(string message = null) : base(message)
        {
        }
    }
}