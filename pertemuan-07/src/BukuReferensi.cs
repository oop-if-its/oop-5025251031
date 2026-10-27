namespace Pertemuan07;

// SUDAH LENGKAP -- jangan diubah. Buku referensi hanya boleh dibaca di tempat:
// sengaja TIDAK mengimplementasikan IDapatDipinjam (jangan ditambahkan!).
public class BukuReferensi : Item
{
    public BukuReferensi(string judul) : base(judul) { }

    public override int HitungDenda(int hariTerlambat) => 0;

    public override int MasaPinjamHari => 0;
}
