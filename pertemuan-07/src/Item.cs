// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan07;

// TODO(Level 1): jadikan kelas ini `abstract` -- Item hanya konsep, tidak ada
//   "item umum" yang bisa dibuat langsung.
// TODO(Level 9): buat Item mengimplementasikan IComparable<Item> (urut
//   berdasarkan Judul).
public abstract class Item : IComparable<Item>
{
    public string Judul { get; }

    // SUDAH LENGKAP -- jangan diubah.
    protected Item(string judul)
    {
        if (string.IsNullOrWhiteSpace(judul))
            throw new ArgumentException("Judul tidak boleh kosong.", nameof(judul));
        Judul = judul;
    }

    // TODO(Level 1): jadikan `abstract` (tanpa badan, titik koma saja) -- setiap
    //   jenis item wajib menentukan dendanya sendiri. Di kelas turunan tulis
    //   `public override int HitungDenda(int hariTerlambat)`.
    public abstract int HitungDenda(int hariTerlambat);

    // TODO(Level 2): jadikan `abstract` (properti tanpa badan: `public abstract
    //   int MasaPinjamHari { get; }`); override di Buku (14), Majalah (3), Dvd
    //   (2).
    public abstract int MasaPinjamHari{get;}

    // Method KONKRET di kelas abstrak: logika bersama yang memakai anggota abstrak.
    public DateOnly HitungTanggalKembali(DateOnly tanggalPinjam)
    {
        // TODO(Level 2): kembalikan tanggalPinjam ditambah MasaPinjamHari hari.
        //   (Item tidak perlu tahu jenis konkretnya -- MasaPinjamHari yang
        //   menentukan.)
        return tanggalPinjam.AddDays(MasaPinjamHari);
    }

    // Hook: kelas turunan BOLEH menimpa, tapi tidak wajib. SUDAH LENGKAP.
    protected virtual string Catatan() => "";

    // Template Method: urutan langkahnya dikunci di sini (BUKAN virtual),
    // bagian yang bervariasi diserahkan ke HitungDenda() dan Catatan().
    public string BuatStruk(int hariTerlambat)
    {
        // TODO(Level 10): hariTerlambat < 0 dianggap 0. Kembalikan teks bergaris
        //   baru "\n" (tanpa "\n" di akhir): baris 1 "STRUK PENGEMBALIAN", baris
        //   2 "Judul: <Judul>", baris 3 "Terlambat: <hari> hari", baris 4
        //   "Denda: Rp<HitungDenda(hari)>" (angka polos tanpa pemisah ribuan).
        //   Kalau Catatan() TIDAK kosong, tambahkan baris 5 "Catatan:
        //   <Catatan()>".
        throw new NotImplementedException("Level 10 belum diimplementasikan");
    }

    public int CompareTo(Item? other)
    {
        // TODO(Level 9): null dianggap lebih kecil (kembalikan 1); selain itu
        //   bandingkan Judul dengan string.Compare(...,
        //   StringComparison.OrdinalIgnoreCase).
        if (other == null)
        {
            return 1;
        }

        return string.Compare(Judul, other.Judul, StringComparison.OrdinalIgnoreCase);
    }
}
