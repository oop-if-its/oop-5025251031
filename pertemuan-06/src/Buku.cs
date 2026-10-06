// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan06;

public class Buku : Item
{
    public string Penulis { get; }

    // SUDAH LENGKAP -- jangan diubah.
    public Buku(string judul, string penulis) : base(judul)
    {
        if (string.IsNullOrWhiteSpace(penulis))
            throw new ArgumentException("Penulis tidak boleh kosong.", nameof(penulis));
        Penulis = penulis;
    }

    // TODO(Level 2): tulis override HitungDenda(int) untuk Buku: Rp2.000 per
    //   hari terlambat (hariTerlambat <= 0 -> 0). Pakai `public override`, BUKAN
    //   `new`.

    // TODO(Level 4): override properti MasaPinjamHari -> 14.

    // TODO(Level 5): override Deskripsi() -> "<base.Deskripsi()> oleh
    //   <Penulis>", mis. "[Bumi Manusia] oleh Pramoedya".
}
