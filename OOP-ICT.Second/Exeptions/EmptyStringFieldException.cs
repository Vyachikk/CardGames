namespace OOP_ICT.Exceptions;

public class EmptyStringFieldException : Exception
{
    public EmptyStringFieldException() : base() {  }

    public EmptyStringFieldException(string fieldName) : base($"Field '{fieldName}' cannot be empty") { }
}