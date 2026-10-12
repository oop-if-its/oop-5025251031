// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan06;

// SUDAH LENGKAP -- jangan diubah. Satu baris peminjaman yang terlambat.
public record Peminjaman(Item Item, int HariTerlambat);

// Kasir TIDAK perlu tahu jenis item apa pun: ia hanya memanggil
// item.HitungDenda(...) dan objek yang sebenarnya menentukan hasilnya.
public class Kasir
{
    public int TotalDenda(IEnumerable<Peminjaman> daftar)
    {
        // TODO(Level 6): null -> ArgumentNullException; jumlahkan
        //   p.Item.HitungDenda(p.HariTerlambat) untuk semua peminjaman. JANGAN
        //   memeriksa jenis item (tidak boleh ada if/is/switch atas tipe) --
        //   biarkan polimorfisme bekerja.
        if (daftar == null)
            throw new ArgumentNullException(nameof(daftar));

        int total = 0;
        foreach (var p in daftar)
        {
            total += p.Item.HitungDenda(p.HariTerlambat);
        }

        return total;
    }

    public Item? ItemDenganDendaTertinggi(IEnumerable<Peminjaman> daftar)
    {
        // TODO(Level 6): null -> ArgumentNullException; kembalikan Item dengan
        //   denda TERTINGGI (kalau seri, ambil yang pertama muncul); daftar
        //   kosong -> null.
        if (daftar == null)
            throw new ArgumentNullException(nameof(daftar));

        Item? itemTertinggi = null;
        int dendaMaksimal = -1;

        foreach (var p in daftar)
        {
            int denda = p.Item.HitungDenda(p.HariTerlambat);
            if (itemTertinggi == null || denda > dendaMaksimal)
            {
                dendaMaksimal = denda;
                itemTertinggi = p.Item;
            }
        }

        return itemTertinggi;
    }
}
