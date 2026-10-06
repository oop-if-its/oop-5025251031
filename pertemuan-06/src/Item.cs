// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan06;

// Kelas dasar semua koleksi perpustakaan. Perilaku yang BERBEDA antar jenis
// (denda, masa pinjam, deskripsi) dibuat `virtual` supaya kelas turunan bisa
// meng-`override`-nya -- inilah dasar polimorfisme.
public class Item
{
    public string Judul { get; }

    // SUDAH LENGKAP -- jangan diubah.
    public Item(string judul)
    {
        if (string.IsNullOrWhiteSpace(judul))
            throw new ArgumentException("Judul tidak boleh kosong.", nameof(judul));
        Judul = judul;
    }

    // TODO(Level 2): jadikan method ini `virtual` supaya bisa di-override (lalu
    //   tulis override-nya di Buku, Majalah, Dvd).
    public int HitungDenda(int hariTerlambat)
    {
        // TODO(Level 1): denda umum = Rp1.000 per hari terlambat. hariTerlambat
        //   <= 0 -> 0.
        if (hariTerlambat <= 0)
        {
            return 0;
        }
        
        return hariTerlambat * 1000;
    }

    // TODO(Level 4): jadikan properti ini `virtual`, lalu override di Buku (14),
    //   Majalah (3), dan Dvd (2).
    public int MasaPinjamHari => 7;

    // TODO(Level 5): jadikan `virtual`; override di Buku/Majalah/Dvd dengan
    //   MEMANGGIL versi induk lewat base.Deskripsi() lalu menambahkan detailnya.
    public string Deskripsi()
    {
        // TODO(Level 5): kembalikan "[<Judul>]" -- mis. "[Bumi Manusia]".
        throw new NotImplementedException("Level 5 belum diimplementasikan");
    }

    public override string ToString()
    {
        // TODO(Level 5): kembalikan Deskripsi() (dipanggil secara polimorfik --
        //   jenis objek yang sebenarnya menentukan hasilnya).
        throw new NotImplementedException("Level 5 belum diimplementasikan");
    }

    // TODO(Level 10 (bonus)): override Equals(object?) dan GetHashCode(): dua
    //   Item dianggap SAMA bila jenis (GetType()) sama DAN Judul sama (huruf
    //   besar/kecil diabaikan). GetHashCode harus konsisten dengan Equals.
}
