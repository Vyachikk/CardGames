namespace OOP_ICT.Fifth.Exceptions;

public class InvalidBetExeptions : Exception
{
    public InvalidBetExeptions(string playerName, string message) : base($"Player {playerName}: {message}") { }
}
