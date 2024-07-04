namespace OOP_ICT.Fifth.Exeptions
{
    public class DealerException : Exception
    {
        public DealerException() : base()
        {
        }

        public DealerException(string message) : base(message)
        {
        }

        public DealerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
