namespace BankMini.Exceptions;

public class RekeningTidakDitemukanException : Exception
{
    public RekeningTidakDitemukanException(string message) : base(message) { }
}