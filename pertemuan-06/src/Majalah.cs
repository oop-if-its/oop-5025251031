// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan06;

public class Majalah : Item
{
    public int Edisi { get; }

    // SUDAH LENGKAP -- jangan diubah.
    public Majalah(string judul, int edisi) : base(judul)
    {
        if (edisi <= 0)
            throw new ArgumentOutOfRangeException(nameof(edisi), "Edisi harus positif.");
        Edisi = edisi;
    }

    // TODO(Level 3): override HitungDenda(int): Rp500 per hari terlambat
    //   (hariTerlambat <= 0 -> 0).

    // TODO(Level 4): override MasaPinjamHari -> 3.

    // TODO(Level 5): override Deskripsi() -> "<base.Deskripsi()> edisi <Edisi>",
    //   mis. "[Tempo] edisi 12".
}
