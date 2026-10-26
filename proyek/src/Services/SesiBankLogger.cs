namespace BankMini.Services;

public class SesiBankLogger : IDisposable
{
    private readonly string _sesiId;
    private bool _disposed = false;

    public SesiBankLogger(string operatorName)
    {
        _sesiId = Guid.NewGuid().ToString()[..8];
        Console.WriteLine("=======================================================");
        Console.WriteLine($"[SESI DIMULAI] Operator: {operatorName} | ID Sesi: {_sesiId}");
        Console.WriteLine("=======================================================");
    }

    public void LogAksi(string pesan)
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(SesiBankLogger));

        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {pesan}");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                Console.WriteLine("=======================================================");
                Console.WriteLine($"[SESI DITUTUP] Sesi {_sesiId} selesai & log dibersihkan.");
                Console.WriteLine("=======================================================\n");
            }
            _disposed = true;
        }
    }
}