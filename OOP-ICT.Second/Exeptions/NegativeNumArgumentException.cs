namespace OOP_ICT.Exceptions;

public class NegativeNumArgumentException: Exception
{
    public NegativeNumArgumentException() : base() {  }

    public NegativeNumArgumentException(string argName) : base($"Argument '{argName}' cannot be negative") { }
}