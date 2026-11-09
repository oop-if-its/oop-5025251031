// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan08;

// Rantai konstruktor lewat this(...): satu konstruktor "utama" yang lengkap,
// konstruktor lain hanya meneruskan dengan nilai bawaan.
public class Peminjaman
{
    public static int JumlahInisialisasi { get; private set; }

    public string Judul { get; }
    public string Anggota { get; }
    public int LamaHari { get; }
    public DateOnly Tanggal { get; }

    // SUDAH LENGKAP -- konstruktor utama; semua validasi & pengisian ada DI SINI SAJA.
    public Peminjaman(string judul, string anggota, int lamaHari, DateOnly tanggal)
    {
        if (string.IsNullOrWhiteSpace(judul))
            throw new ArgumentException("Judul tidak boleh kosong.", nameof(judul));
        if (string.IsNullOrWhiteSpace(anggota))
            throw new ArgumentException("Anggota tidak boleh kosong.", nameof(anggota));
        if (lamaHari < 1)
            throw new ArgumentOutOfRangeException(nameof(lamaHari), "Lama pinjam minimal 1 hari.");

        Judul = judul;
        Anggota = anggota;
        LamaHari = lamaHari;
        Tanggal = tanggal;
        JumlahInisialisasi++;
    }

    // TODO(Level 1): teruskan ke konstruktor utama lewat `: this(...)` dengan
    //   tanggal = hari ini (`DateOnly.FromDateTime(DateTime.Today)`). Jangan
    //   menyalin ulang validasi/pengisian.
    public Peminjaman(string judul, string anggota, int lamaHari)
        : this(judul, anggota, lamaHari, DateOnly.FromDateTime(DateTime.Today))
    {
    }

    // TODO(Level 1): teruskan ke konstruktor 3-parameter di atas dengan lamaHari
    //   = 7.
    public Peminjaman(string judul, string anggota)
        : this(judul, anggota, 7)
    {
    }

    // TODO(Level 1): teruskan ke konstruktor 2-parameter dengan anggota =
    //   "Umum".
   public Peminjaman(string judul)
        : this(judul, "Umum")
    {
    }
}
