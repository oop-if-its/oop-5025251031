using BankMini.Exceptions;

namespace BankMini.Models;

public class Giro : Rekening
{
    public decimal BatasOverdraft { get; private set; }
    protected override bool BolehSaldoNegatif => true;

    public Giro(string nomorRekening, string pemilik, decimal saldoAwal = 0m, decimal batasOverdraft = 1_000_000m)
        : base(nomorRekening, pemilik, saldoAwal)
    {
        if (batasOverdraft < 0)
            throw new ArgumentException("Batas overdraft tidak boleh negatif.", nameof(batasOverdraft));
        BatasOverdraft = batasOverdraft;
    }

    public override void Tarik(decimal jumlah)
    {
        if (jumlah <= 0)
            throw new ArgumentException("Nominal penarikan harus lebih besar dari nol.", nameof(jumlah));

        if (Saldo - jumlah < -BatasOverdraft)
            throw new SaldoTidakCukupException($"Penarikan melebihi batas overdraft Giro! Saldo: Rp {Saldo:N0}, Limit Overdraft: Rp {BatasOverdraft:N0}");

        Saldo -= jumlah;
        TambahTransaksi(TipeTransaksi.Tarik, jumlah, "Penarikan Giro (Overdraft)");
    }

    public override decimal HitungBunga()
    {
        if (Saldo <= 0) return 0m;
        return Saldo * (0.01m / 12m);
    }
}