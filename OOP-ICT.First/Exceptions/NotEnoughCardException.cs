namespace OOP_ICT.Exceptinos;

public class NotEnoughCardException: Exception
{
    public NotEnoughCardException() : base("Not enough cards in the deck")
    {
        
    }
    
    public NotEnoughCardException(string message) : base(message)
    {
        
    }
}