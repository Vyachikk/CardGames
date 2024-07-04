namespace OOP_ICT.Fifth.Exeptions
{
    public class BankException : Exception
    {
        public BankException() : base()
        {
        }

        public BankException(string message) : base(message)
        {
        }

        public BankException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
