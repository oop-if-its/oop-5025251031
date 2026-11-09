// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan08;

// SUDAH LENGKAP -- jangan diubah. Pencatat urutan kejadian. Catat() MENGEMBALIKAN
// pesannya supaya bisa dipakai langsung sebagai nilai awal field:
//     private readonly string _f = Jejak.Catat("field ...");
public static class Jejak
{
    private static readonly List<string> _isi = new();

    public static IReadOnlyList<string> Isi => _isi.AsReadOnly();

    public static string Catat(string pesan)
    {
        _isi.Add(pesan);
        return pesan;
    }

    public static void Reset() => _isi.Clear();
}

// Tiga tingkat pewarisan. Tugas Level 2: setiap kelas MENCATAT dua kejadian ke Jejak --
//   (a) inisialisasi field-nya:   "field TingkatN"
//   (b) badan konstruktornya:     "ctor TingkatN"
// TEBAK dulu urutan hasilnya untuk `new Tingkat3()` sebelum menjalankan test!

public class Tingkat1
{
    // TODO(Level 2): tambahkan field `private readonly string _f1 =
    //   Jejak.Catat("field Tingkat1");`
    private readonly string _f1 = Jejak.Catat("field Tingkat1");

    public Tingkat1()
    {
        // TODO(Level 2): catat "ctor Tingkat1" lewat Jejak.Catat.
        Jejak.Catat("ctor Tingkat1");
    }
}

public class Tingkat2 : Tingkat1
{
    // TODO(Level 2): tambahkan field `private readonly string _f2 =
    //   Jejak.Catat("field Tingkat2");`
    private readonly string _f2 = Jejak.Catat("field Tingkat2");

    public Tingkat2()
    {
        // TODO(Level 2): catat "ctor Tingkat2".
        Jejak.Catat("ctor Tingkat2");
    }
}

public class Tingkat3 : Tingkat2
{
    // TODO(Level 2): tambahkan field `private readonly string _f3 =
    //   Jejak.Catat("field Tingkat3");`
    private readonly string _f3 = Jejak.Catat("field Tingkat3");

    public Tingkat3()
    {
        // TODO(Level 2): catat "ctor Tingkat3".
        Jejak.Catat("ctor Tingkat3");
    }

    // TODO(Level 2): konstruktor ini memanggil konstruktor tanpa parameter di
    //   atas lewat `: this()`, lalu SETELAH itu mencatat $"ctor
    //   Tingkat3({tambahan})".
    public Tingkat3(string tambahan) : this()
    {
        Jejak.Catat($"ctor Tingkat3({tambahan})");
    }
}