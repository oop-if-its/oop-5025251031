// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Pertemuan07;

// TODO(Level 3): implementasikan IDapatDipinjam.
// TODO(Level 4): implementasikan IDapatDiperpanjang (hanya Buku yang bisa
//   diperpanjang).
// TODO(Level 8): implementasikan IDapatDicetak (implementasi biasa / implisit --
//   method Cetak() bersifat public).
public class Buku : Item, IDapatDipinjam, IDapatDiperpanjang
{
    public string Penulis { get; }

    // SUDAH LENGKAP -- jangan diubah.
    public Buku(string judul, string penulis) : base(judul)
    {
        if (string.IsNullOrWhiteSpace(penulis))
            throw new ArgumentException("Penulis tidak boleh kosong.", nameof(penulis));
        Penulis = penulis;
    }

    // TODO(Level 1): tulis `public override int HitungDenda(int hariTerlambat)`
    //   -- Rp2.000 per hari terlambat (hariTerlambat <= 0 -> 0).
    public override int HitungDenda(int hariTerlambat)
    {
        if (hariTerlambat <= 0)
    {
        return 0;
    }
        return hariTerlambat * 2000;
    }


    // TODO(Level 2): tulis `public override int MasaPinjamHari` -> 14.
    public override int MasaPinjamHari => 14;

    // TODO(Level 3): implementasi IDapatDipinjam. Saran: simpan `private
    //   readonly StatusPinjam _status = new();` lalu Tersedia =
    //   !_status.Dipinjam; Pinjam() -> _status.Tandai(); Kembalikan() ->
    //   _status.Lepas() dan JANGAN lupa reset JumlahPerpanjangan menjadi 0
    //   (Level 4). Pinjam dua kali / Kembalikan saat tidak dipinjam ->
    //   InvalidOperationException (sudah ditangani StatusPinjam).
    private readonly StatusPinjam _status = new();
    public bool Tersedia => !_status.Dipinjam;

    public void Pinjam()
    {
        _status.Tandai();
    }

    public void Kembalikan()
    {
        _status.Lepas();
        JumlahPerpanjangan = 0;
    }

    // TODO(Level 4): implementasi IDapatDiperpanjang. JumlahPerpanjangan (get
    //   publik, set private) mulai 0. Perpanjang(): kalau buku TIDAK sedang
    //   dipinjam -> InvalidOperationException; kalau JumlahPerpanjangan sudah 2
    //   -> kembalikan false (tidak berubah); selain itu naikkan satu dan
    //   kembalikan true.
    public int JumlahPerpanjangan { get; private set; } = 0; 
    public bool Perpanjang()
    {
        if (Tersedia)
        {
            throw new InvalidOperationException("Item sedang dipinjam");
        } 

        if(JumlahPerpanjangan >= 2)
        {
            return false;
        }

        JumlahPerpanjangan++;
        return true;
    }   

    // TODO(Level 8): implementasi IDapatDicetak -> "Buku: <Judul> oleh
    //   <Penulis>".
}
