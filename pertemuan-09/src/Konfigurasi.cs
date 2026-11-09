// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan08;

// Static constructor: dijalankan OTOMATIS satu kali saja, sebelum anggota static
// pertama kali dipakai -- tidak bisa dipanggil manual, tanpa modifier akses,
// tanpa parameter.
public static class Konfigurasi
{
    public static int JumlahInisialisasi { get; private set; }
    public static int DendaPerHari { get; }
    public static int MaksPinjam { get; }

    // TODO(Level 3): tulis static constructor `static Konfigurasi()` yang
    //   menaikkan JumlahInisialisasi satu kali, dan mengisi DendaPerHari = 1000
    //   dan MaksPinjam = 3. (Properti get-only static hanya boleh diisi di
    //   static constructor.) Test memastikan JumlahInisialisasi bernilai TEPAT 1
    //   walau anggota kelas ini diakses berkali-kali.
    // (static constructor belum ditulis)
    static Konfigurasi()
    {
        JumlahInisialisasi++;
        DendaPerHari = 1000;
        MaksPinjam = 3;
    }
}
