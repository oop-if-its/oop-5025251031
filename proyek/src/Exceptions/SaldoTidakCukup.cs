namespace BankMini.Exceptions;

public class SaldoTidakCukupException : Exception
{
    public SaldoTidakCukupException(string message) : base(message) { }
    public SaldoTidakCukupException(string message, Exception innerException) : base(message, innerException) { }
}