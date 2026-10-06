// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan06;

// Type casting & type checking: mengambil kembali tipe turunan dari variabel
// bertipe induk.
public static class PemilahItem
{
    public static List<Buku> AmbilBuku(IEnumerable<Item> daftar)
    {
        // TODO(Level 7): null -> ArgumentNullException; kembalikan hanya item
        //   yang bertipe Buku (urutan asli dipertahankan). Boleh dengan
        //   `is`/`as` di dalam foreach atau OfType<Buku>().
        throw new NotImplementedException("Level 7 belum diimplementasikan");
    }

    public static string? PenulisAtauNull(Item item)
    {
        // TODO(Level 7): kembalikan Penulis kalau item adalah Buku, selain itu
        //   null. Pakai pattern matching `is Buku b`.
        throw new NotImplementedException("Level 7 belum diimplementasikan");
    }

    public static Buku KeBuku(Item item)
    {
        // TODO(Level 8): item null -> ArgumentNullException. Selain itu lakukan
        //   EXPLICIT CAST `(Buku)item` -- kalau item bukan Buku, biarkan .NET
        //   melempar InvalidCastException (jangan ditangkap, jangan diganti
        //   pesan sendiri).
        throw new NotImplementedException("Level 8 belum diimplementasikan");
    }

    public static bool CobaKeBuku(Item? item, out Buku? buku)
    {
        // TODO(Level 8): pola TryXxx -- kembalikan true dan isi `buku` kalau
        //   item adalah Buku; selain itu (termasuk item null) kembalikan false
        //   dan isi `buku` = null. TIDAK boleh melempar exception.
        throw new NotImplementedException("Level 8 belum diimplementasikan");
    }
}
