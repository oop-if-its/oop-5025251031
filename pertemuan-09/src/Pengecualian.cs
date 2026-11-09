// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan08;

// Exception kustom: turunan Exception, membawa DATA tambahan (Judul) yang
// berguna bagi penangkapnya.
public class BukuTidakTersediaException : Exception
{
    public string Judul { get; }

    // TODO(Level 4): konstruktor (string judul) -> pesan "Buku '<judul>' tidak
    //   tersedia." lewat `: base(...)`, dan simpan judul ke properti Judul.
    public BukuTidakTersediaException(string judul)
        : base($"Buku '{judul}' tidak tersedia.")
    {
        Judul = judul;
    }

    // TODO(Level 4): konstruktor (string judul, Exception inner) -> pesan sama,
    //   tetapi menyertakan exception penyebab lewat `: base(pesan, inner)`.
    public BukuTidakTersediaException(string judul, Exception inner)
        : base($"Buku '{judul}' tidak tersedia.", inner)
    {
        Judul = judul;
    }
}

// SUDAH LENGKAP -- jangan diubah. Dipakai Level 6.
public class DataTidakValidException : Exception
{
    public DataTidakValidException(string pesan) : base(pesan) { }

    public DataTidakValidException(string pesan, Exception inner) : base(pesan, inner) { }
}

// Dipakai Level 10 (bonus): exception yang membawa SEMUA kesalahan sekaligus.
public class ValidasiException : Exception
{
    public IReadOnlyList<string> Daftar { get; }

    public ValidasiException(IReadOnlyList<string> daftar)
        : base($"Ditemukan {daftar.Count} kesalahan validasi.")
    {
        Daftar = daftar;
    }
}