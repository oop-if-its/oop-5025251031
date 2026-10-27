// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan07;

// TODO(Level 3): implementasikan IDapatDipinjam.
// TODO(Level 8): implementasikan IDapatDicetak dengan cara EKSPLISIT (`string
//   IDapatDicetak.Cetak()`) -- method-nya tidak muncul di tipe Majalah, hanya
//   lewat variabel bertipe IDapatDicetak.
// Majalah TIDAK mengimplementasikan IDapatDiperpanjang.
public class Majalah : Item, IDapatDipinjam
{
    public int Edisi { get; }

    // SUDAH LENGKAP -- jangan diubah.
    public Majalah(string judul, int edisi) : base(judul)
    {
        if (edisi <= 0)
            throw new ArgumentOutOfRangeException(nameof(edisi), "Edisi harus positif.");
        Edisi = edisi;
    }

    // TODO(Level 2): tulis `public override int HitungDenda(int hariTerlambat)`
    //   -- Rp500 per hari (hariTerlambat <= 0 -> 0).
    public override int HitungDenda(int hariTerlambat)
    {
        if (hariTerlambat <= 0) 
        {
            return 0;       
        }
        return hariTerlambat * 500;
    }

    // TODO(Level 2): tulis `public override int MasaPinjamHari` -> 3.
    public override int MasaPinjamHari => 3;

    // TODO(Level 3): implementasi IDapatDipinjam (lihat petunjuk di Buku).
    private readonly StatusPinjam _status = new();
    public bool Tersedia => !_status.Dipinjam;

    public void Pinjam()
    {
        _status.Tandai();
    }

    public void Kembalikan()
    {
        _status.Lepas();
    }

    // TODO(Level 8): implementasi EKSPLISIT IDapatDicetak.Cetak() -> "Majalah:
    //   <Judul> edisi <Edisi>".
}
