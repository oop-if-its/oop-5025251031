using BankMini.Exceptions;

namespace BankMini.Models;

public abstract class Rekening
{
    private decimal _saldo;
    private readonly List<Transaksi> _riwayatTransaksi = new();

    public string NomorRekening { get; }
    public string Pemilik { get; }

    public decimal Saldo
    {
        get => _saldo;
        protected set
        {
            if (value < 0 && !BolehSaldoNegatif)
                throw new InvalidOperationException("Saldo tidak boleh negatif.");
            _saldo = value;
        }
    }

    protected virtual bool BolehSaldoNegatif => false;

    public IReadOnlyList<Transaksi> RiwayatTransaksi => _riwayatTransaksi.AsReadOnly();

    protected Rekening(string nomorRekening, string pemilik) : this(nomorRekening, pemilik, 0m)
    {
    }

    protected Rekening(string nomorRekening, string pemilik, decimal saldoAwal)
    {
        if (string.IsNullOrWhiteSpace(nomorRekening))
            throw new ArgumentException("Nomor rekening tidak boleh kosong.", nameof(nomorRekening));
        if (string.IsNullOrWhiteSpace(pemilik))
            throw new ArgumentException("Nama pemilik tidak boleh kosong.", nameof(pemilik));
        if (saldoAwal < 0)
            throw new ArgumentException("Saldo awal tidak boleh negatif.", nameof(saldoAwal));

        NomorRekening = nomorRekening;
        Pemilik = pemilik;
        _saldo = saldoAwal;

        if (saldoAwal > 0)
        {
            TambahTransaksi(TipeTransaksi.Setor, saldoAwal, "Setoran Awal");
        }
    }

    public virtual void Setor(decimal jumlah)
    {
        if (jumlah <= 0)
            throw new ArgumentException("Nominal setoran harus lebih besar dari 0.", nameof(jumlah));

        Saldo += jumlah;
        TambahTransaksi(TipeTransaksi.Setor, jumlah, "Setor Tunai");
    }

    public abstract void Tarik(decimal jumlah);
    public abstract decimal HitungBunga();

    protected void TambahTransaksi(TipeTransaksi tipe, decimal jumlah, string keterangan)
    {
        _riwayatTransaksi.Add(new Transaksi(tipe, jumlah, keterangan));
    }

    public override string ToString()
    {
        return $"[{GetType().Name}] No: {NomorRekening} | Pemilik: {Pemilik} | Saldo: Rp {Saldo:N0}";
    }
}