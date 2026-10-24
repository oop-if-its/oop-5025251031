namespace BankMini.Models;

public enum TipeTransaksi
{
    Setor,
    Tarik,
    TransferMasuk,
    TransferKeluar,
    Bunga
}

public class Transaksi
{
    public DateTime Waktu { get; }
    public TipeTransaksi Tipe { get; }
    public decimal Jumlah { get; }
    public string Keterangan { get; }

    public Transaksi(TipeTransaksi tipe, decimal jumlah, string keterangan)
    {
        if (jumlah <= 0)
            throw new ArgumentException("Nominal transaksi harus lebih besar dari nol.", nameof(jumlah));

        Waktu = DateTime.Now;
        Tipe = tipe;
        Jumlah = jumlah;
        Keterangan = string.IsNullOrWhiteSpace(keterangan) ? "-" : keterangan;
    }

    public override string ToString()
    {
        return $"[{Waktu:yyyy-MM-dd HH:mm:ss}] {Tipe,-15} | Rp {Jumlah,12:N0} | {Keterangan}";
    }
}