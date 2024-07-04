namespace OOP_ICT.Third.Exceptions;

public class InvalidPlayersListException : Exception
{
    public InvalidPlayersListException() : base("Invalid players list") {  }

    public InvalidPlayersListException(string description) : base($"Invalid players list: {description}") { }
}