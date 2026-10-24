namespace BankMini.Models;

public class Nasabah
{
    private readonly List<Rekening> _rekeningList = new();

    public string IdNasabah { get; }
    public string Nama { get; private set; }

    public IReadOnlyList<Rekening> DaftarRekening => _rekeningList.AsReadOnly();

    public Nasabah(string idNasabah, string nama)
    {
        if (string.IsNullOrWhiteSpace(idNasabah))
            throw new ArgumentException("ID Nasabah tidak boleh kosong.", nameof(idNasabah));
        if (string.IsNullOrWhiteSpace(nama))
            throw new ArgumentException("Nama tidak boleh kosong.", nameof(nama));

        IdNasabah = idNasabah;
        Nama = nama;
    }

    public void TambahRekening(Rekening rekening)
    {
        ArgumentNullException.ThrowIfNull(rekening);
        if (_rekeningList.Any(r => r.NomorRekening == rekening.NomorRekening))
            throw new InvalidOperationException($"Rekening {rekening.NomorRekening} sudah terdaftar pada nasabah ini.");

        _rekeningList.Add(rekening);
    }

    public Rekening? CariRekening(string nomorRekening)
    {
        return _rekeningList.FirstOrDefault(r => r.NomorRekening.Equals(nomorRekening, StringComparison.OrdinalIgnoreCase));
    }
}