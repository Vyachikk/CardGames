namespace OOP_ICT.Fifth.Exceptions;

public class InvalidPlayersListException : Exception
{
    public InvalidPlayersListException() : base("Invalid players list") {  }

    public InvalidPlayersListException(string description) : base($"Invalid players list: {description}") { }
}