namespace Application.Common.Exceptions.Base;

public class BadRequestException : Exception
{
    public BadRequestException() {}
    
    public BadRequestException(string message) : base(message) {}
}