// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan07;

// TODO(Level 3): implementasikan IDapatDipinjam.
public class Dvd : Item, IDapatDipinjam
{
    public int DurasiMenit { get; }

    // SUDAH LENGKAP -- jangan diubah.
    public Dvd(string judul, int durasiMenit) : base(judul)
    {
        if (durasiMenit <= 0)
            throw new ArgumentOutOfRangeException(nameof(durasiMenit), "Durasi harus positif.");
        DurasiMenit = durasiMenit;
    }

    // TODO(Level 2): tulis `public override int HitungDenda(int hariTerlambat)`
    //   -- Rp5.000 per hari, MAKSIMUM Rp50.000 (hariTerlambat <= 0 -> 0).
    public override int HitungDenda(int hariTerlambat)
    {
        if (hariTerlambat <= 0 )
        {
            return 0;
        }

        int totaldenda = hariTerlambat * 5000;
        
        if (totaldenda > 50000)
        {
            return 50000;
        }

        return totaldenda;
    }

    // TODO(Level 2): tulis `public override int MasaPinjamHari` -> 2.
    public override int MasaPinjamHari => 2;

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

    // TODO(Level 10 (bonus)): override hook `protected override string
    //   Catatan()` -> "Periksa kondisi disk".
    protected override string Catatan()
    {
        return "Periksa kondisi disk";
    }
}
