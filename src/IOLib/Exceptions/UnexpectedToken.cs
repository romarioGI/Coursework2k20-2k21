namespace IOLib.Exceptions
{
    public class UnexpectedToken : InputException
    {
        public readonly int IndexOfError;

        public UnexpectedToken(int indexOfError, string message = null) : base(message)
        {
            IndexOfError = indexOfError;
        }
    }
}