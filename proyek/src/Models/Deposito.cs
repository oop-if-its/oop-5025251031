using BankMini.Exceptions;

namespace BankMini.Models;

public class Deposito : Rekening
{
    public DateTime TanggalJatuhTempo { get; private set; }
    public decimal PersentasePenalti { get; private set; }
    public decimal SukuBungaTahunan { get; private set; }

    public Deposito(string nomorRekening, string pemilik, decimal saldoAwal, DateTime tanggalJatuhTempo, decimal sukuBungaTahunan = 0.05m, decimal persentasePenalti = 0.05m)
        : base(nomorRekening, pemilik, saldoAwal)
    {
        if (tanggalJatuhTempo <= DateTime.Now)
            throw new ArgumentException("Tanggal jatuh tempo harus di masa depan.", nameof(tanggalJatuhTempo));
        if (persentasePenalti < 0 || persentasePenalti > 1)
            throw new ArgumentException("Persentase penalti harus antara 0 dan 1.", nameof(persentasePenalti));

        TanggalJatuhTempo = tanggalJatuhTempo;
        SukuBungaTahunan = sukuBungaTahunan;
        PersentasePenalti = persentasePenalti;
    }

    public override void Tarik(decimal jumlah)
    {
        if (jumlah <= 0)
            throw new ArgumentException("Nominal penarikan harus lebih besar dari nol.", nameof(jumlah));

        decimal totalDibutuhkan = jumlah;
        bool kenaPenalti = DateTime.Now < TanggalJatuhTempo;

        if (kenaPenalti)
        {
            decimal biayaPenalti = jumlah * PersentasePenalti;
            totalDibutuhkan += biayaPenalti;
        }

        if (totalDibutuhkan > Saldo)
            throw new SaldoTidakCukupException($"Saldo Deposito tidak cukup termasuk penalti pencairan dini. Diperlukan: Rp {totalDibutuhkan:N0}, Saldo: Rp {Saldo:N0}");

        Saldo -= totalDibutuhkan;
        TambahTransaksi(TipeTransaksi.Tarik, jumlah, kenaPenalti ? $"Penarikan Deposito (Pencairan Dini - Penalti Rp {jumlah * PersentasePenalti:N0})" : "Penarikan Deposito (Jatuh Tempo)");
    }

    public override decimal HitungBunga()
    {
        return Saldo * (SukuBungaTahunan / 12m);
    }
}