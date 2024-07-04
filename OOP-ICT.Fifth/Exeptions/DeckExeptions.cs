namespace OOP_ICT.Fifth.Exeptions
{
    public class DeckException : Exception
    {
        public DeckException() : base()
        {
        }

        public DeckException(string message) : base(message)
        {
        }

        public DeckException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
