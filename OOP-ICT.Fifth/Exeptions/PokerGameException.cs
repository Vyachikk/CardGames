namespace OOP_ICT.Fifth.Exeptions;

public class PokerGameException : Exception
{
    public PokerGameException(string message) : base(message)
    {
    }

    public PokerGameException(string message, Exception innerException) : base(message, innerException)
    {
    }
}   