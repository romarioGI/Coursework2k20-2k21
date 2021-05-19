namespace IOLib.Exceptions
{
    public class UnexpectedCharacter : IndexedInputException
    {
        public UnexpectedCharacter(int indexOfError, string message = null)
            : base(indexOfError, GetMessage(message))
        {
        }

        private static string GetMessage(string message)
        {
            var result = "Unexpected character.";

            if (message is not null)
                result = $"{result} {message}";

            return result;
        }
    }
}