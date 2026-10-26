using BankMini.Exceptions;
using BankMini.Models;

namespace BankMini.Services;

public class BankService
{
    private readonly List<Rekening> _semuaRekening = new();
    private readonly IKebijakanBunga _kebijakanBunga; 

    public BankService(IKebijakanBunga kebijakanBunga)
    {
        _kebijakanBunga = kebijakanBunga ?? throw new ArgumentNullException(nameof(kebijakanBunga));
    }

    public void DaftarkanRekening(Rekening rekening)
    {
        ArgumentNullException.ThrowIfNull(rekening);
        _semuaRekening.Add(rekening);
    }

    public Rekening CariRekening(string nomorRekening)
    {
        var rek = _semuaRekening.FirstOrDefault(r => r.NomorRekening.Equals(nomorRekening, StringComparison.OrdinalIgnoreCase));
        return rek ?? throw new RekeningTidakDitemukanException($"Rekening '{nomorRekening}' tidak ditemukan dalam sistem.");
    }

    public void Transfer(string noRekPengirim, string noRekPenerima, decimal jumlah)
    {
        var pengirim = CariRekening(noRekPengirim);
        var penerima = CariRekening(noRekPenerima);

        if (pengirim.NomorRekening == penerima.NomorRekening)
            throw new ArgumentException("Nomor rekening pengirim dan penerima tidak boleh sama.");

        pengirim.Tarik(jumlah);
        try
        {
            penerima.Setor(jumlah);
        }
        catch (Exception ex)
        {
            pengirim.Setor(jumlah);
            Console.WriteLine($"[ROLLBACK] Transfer dibatalkan. Dana dikembalikan ke {noRekPengirim}. Alasan: {ex.Message}");
            throw; 
        }
    }

    public decimal ProsesBungaBulanan(string nomorRekening)
    {
        var rek = CariRekening(nomorRekening);
        decimal bunga = _kebijakanBunga.HitungBungaBulanan(rek);
        if (bunga > 0)
        {
            rek.Setor(bunga);
        }
        return bunga;
    }
}
