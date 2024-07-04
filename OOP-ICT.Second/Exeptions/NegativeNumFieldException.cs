namespace OOP_ICT.Exceptions;

public class NegativeNumFieldException: Exception
{
    public NegativeNumFieldException() : base() {  }

    public NegativeNumFieldException(string fieldName) : base($"Field '{fieldName}' cannot be negative") { }
    
    public NegativeNumFieldException(string fieldName, int value) : base($"Field '{fieldName}' with value:'{value}' cannot be negative") { }
}