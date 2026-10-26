using BankMini.Exceptions;
using BankMini.Models;
using BankMini.Services;

namespace BankMini;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("==========================================================");
        Console.WriteLine("       DEMO APLIKASI BANK MINI - ETS PBO (NRP: 5025251031)  ");
        Console.WriteLine("==========================================================");

        IKebijakanBunga kebijakanBunga = new BungaPromo(1.2m);
        BankService bankService = new BankService(kebijakanBunga);

        using (var logger = new SesiBankLogger("Teller-Najwan"))
        {
            logger.LogAksi("Inisialisasi data nasabah dan pendaftaran rekening...");

            var nasabah1 = new Nasabah("NSB001", "Najwan Sigit Cahya Buana");
            
            Rekening rekTabungan = new Tabungan("TAB-101", nasabah1.Nama, 500_000m);
            Rekening rekGiro = new Giro("GIR-202", nasabah1.Nama, 200_000m, batasOverdraft: 500_000m);
            Rekening rekDeposito = new Deposito("DEP-303", nasabah1.Nama, 2_000_000m, DateTime.Now.AddMonths(6));

            nasabah1.TambahRekening(rekTabungan);
            nasabah1.TambahRekening(rekGiro);
            nasabah1.TambahRekening(rekDeposito);

            bankService.DaftarkanRekening(rekTabungan);
            bankService.DaftarkanRekening(rekGiro);
            bankService.DaftarkanRekening(rekDeposito);

            logger.LogAksi($"Daftar Rekening {nasabah1.Nama}:");
            foreach (var rek in nasabah1.DaftarRekening)
            {
                Console.WriteLine($" - {rek}");
            }

            logger.LogAksi("\n--- TEST CASE 1: Penarikan Giro (Fitur Overdraft) ---");
            Console.WriteLine($"Saldo Giro Awal: Rp {rekGiro.Saldo:N0}");
            rekGiro.Tarik(400_000m);
            Console.WriteLine($"Penarikan Rp 400.000 -> Saldo Akhir: Rp {rekGiro.Saldo:N0} (Diizinkan Overdraft)");

            logger.LogAksi("\n--- TEST CASE 2: Transfer All-or-Nothing Sukses ---");
            Console.WriteLine($"Saldo Tabungan Awal: Rp {rekTabungan.Saldo:N0} | Saldo Giro Awal: Rp {rekGiro.Saldo:N0}");
            bankService.Transfer("TAB-101", "GIR-202", 150_000m);
            Console.WriteLine($"Transfer Rp 150.000 dari TAB-101 ke GIR-202 Selesai.");
            Console.WriteLine($"Saldo Tabungan Akhir: Rp {rekTabungan.Saldo:N0} | Saldo Giro Akhir: Rp {rekGiro.Saldo:N0}");

            logger.LogAksi("\n--- TEST CASE 3: Transfer Gagal (Saldo Tidak Cukup) ---");
            try
            {
                bankService.Transfer("TAB-101", "GIR-202", 10_000_000m);
            }
            catch (SaldoTidakCukupException ex)
            {
                Console.WriteLine($"[TERTANGGANI EXCEPTION] {ex.Message}");
            }

            logger.LogAksi("\n--- TEST CASE 4: Pencarian Gagal (Rekening Tidak Ada) ---");
            try
            {
                bankService.CariRekening("REK-UNKNOWN");
            }
            catch (RekeningTidakDitemukanException ex)
            {
                Console.WriteLine($"[TERTANGGANI EXCEPTION] {ex.Message}");
            }

            logger.LogAksi("\n--- TEST CASE 5: Perhitungan Bunga Promo (Strategy Pattern & DI) ---");
            decimal bungaDiterima = bankService.ProsesBungaBulanan("TAB-101");
            Console.WriteLine($"Bunga Promo Ditambahkan ke TAB-101: Rp {bungaDiterima:N2}");
            Console.WriteLine($"Saldo Tabungan Sekarang: Rp {rekTabungan.Saldo:N0}");


            logger.LogAksi("\n--- TEST CASE 6: Riwayat Transaksi (IReadOnlyList) ---");
            Console.WriteLine($"Riwayat Transaksi TAB-101 (Total: {rekTabungan.RiwayatTransaksi.Count}):");
            foreach (var tx in rekTabungan.RiwayatTransaksi)
            {
                Console.WriteLine($"   {tx}");
            }
        }

        Console.WriteLine("\n[DEMO SELESAI] Semua Test Case K1-K6 dan Aturan Khas Berhasil Dieksekusi.");
    }
}