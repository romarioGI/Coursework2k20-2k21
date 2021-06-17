namespace IOLib.Exceptions
{
    public class UnexpectedCharacter : InputException
    {
        public readonly int IndexOfError;

        public UnexpectedCharacter(int indexOfError, string message = null)
            : base(message)
        {
            IndexOfError = indexOfError;
        }
    }
}