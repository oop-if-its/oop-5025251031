// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan06;

public class Dvd : Item
{
    public int DurasiMenit { get; }

    // SUDAH LENGKAP -- jangan diubah.
    public Dvd(string judul, int durasiMenit) : base(judul)
    {
        if (durasiMenit <= 0)
            throw new ArgumentOutOfRangeException(nameof(durasiMenit), "Durasi harus positif.");
        DurasiMenit = durasiMenit;
    }

    // TODO(Level 3): override HitungDenda(int): Rp5.000 per hari terlambat,
    //   TETAPI dibatasi maksimum Rp50.000 (hariTerlambat <= 0 -> 0).

    // TODO(Level 4): override MasaPinjamHari -> 2.

    // TODO(Level 5): override Deskripsi() -> "<base.Deskripsi()> (<DurasiMenit>
    //   menit)", mis. "[Laskar Pelangi] (125 menit)".
}
