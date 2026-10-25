using BankMini.Exceptions;

namespace BankMini.Models;

public class Tabungan : Rekening
{
    public decimal SukuBungaTahunan { get; private set; }

    public Tabungan(string nomorRekening, string pemilik, decimal saldoAwal = 0m, decimal sukuBungaTahunan = 0.025m)
        : base(nomorRekening, pemilik, saldoAwal)
    {
        if (sukuBungaTahunan < 0)
            throw new ArgumentException("Suku bunga tidak boleh negatif.", nameof(sukuBungaTahunan));
        SukuBungaTahunan = sukuBungaTahunan;
    }

    public override void Tarik(decimal jumlah)
    {
        if (jumlah <= 0)
            throw new ArgumentException("Nominal penarikan harus lebih besar dari nol.", nameof(jumlah));

        if (jumlah > Saldo)
            throw new SaldoTidakCukupException($"Saldo Tabungan tidak cukup! Saldo saat ini: Rp {Saldo:N0}, penarikan: Rp {jumlah:N0}");

        Saldo -= jumlah;
        TambahTransaksi(TipeTransaksi.Tarik, jumlah, "Penarikan Tabungan");
    }

    public override decimal HitungBunga()
    {
        return Saldo * (SukuBungaTahunan / 12m);
    }
}