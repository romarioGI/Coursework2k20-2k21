namespace IOLib.Exceptions
{
    public class IndexedInputException : InputException
    {
        public readonly int IndexOfError;

        public IndexedInputException(int indexOfError, string message) : base(message)
        {
            IndexOfError = indexOfError;
        }
    }
}