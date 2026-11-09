// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan08;

// Sumber daya yang harus DITUTUP: setiap Koneksi yang dibuka wajib di-Dispose.
// TODO(Level 8): buat Koneksi mengimplementasikan IDisposable (`: IDisposable`).
public class Koneksi : IDisposable
{
    private static int _jumlahTerbuka;

    // SUDAH LENGKAP -- berapa Koneksi yang saat ini masih terbuka di seluruh program.
    public static int JumlahTerbuka => _jumlahTerbuka;

    public bool Terbuka { get; private set; }

    public Koneksi()
    {
        // TODO(Level 8): Koneksi baru langsung Terbuka = true dan menaikkan
        //   JumlahTerbuka (field _jumlahTerbuka) satu.
        Terbuka = true;
        _jumlahTerbuka++;
    }

    public string Query(string sql)
    {
        // TODO(Level 8): kalau sudah ditutup (Terbuka = false) -> lempar
        //   ObjectDisposedException(nameof(Koneksi)); selain itu kembalikan
        //   "hasil: <sql>".
        if (!Terbuka)
            throw new ObjectDisposedException(nameof(Koneksi));

        return $"hasil: {sql}";
    }

    // TODO(Level 8): tulis `public void Dispose()`: idempoten -- kalau sudah
    //   ditutup, tidak melakukan apa-apa; selain itu Terbuka = false dan
    //   turunkan _jumlahTerbuka satu (memanggil Dispose dua kali tidak boleh
    //   membuat hitungan turun dua kali).
    public void Dispose()
    {
        if (!Terbuka)
            return;

        Terbuka = false;
        _jumlahTerbuka--;
    }
}

public static class Pemakai
{
    public static string Jalankan(string sql, bool sengajaGagal)
    {
        // TODO(Level 8): buka Koneksi baru dengan pernyataan `using` (mis.
        //   `using var koneksi = new Koneksi();`), jalankan Query(sql). Kalau
        //   sengajaGagal = true, lempar InvalidOperationException("gagal")
        //   SETELAH koneksi dibuka. Koneksi HARUS tertutup di kedua kasus
        //   (sukses maupun exception) -- itulah gunanya `using`. Kembalikan
        //   hasil Query.
        using var koneksi = new Koneksi();
        var hasil = koneksi.Query(sql);

        if (sengajaGagal)
            throw new InvalidOperationException("gagal");

        return hasil;
    }
}