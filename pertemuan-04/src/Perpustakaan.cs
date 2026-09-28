// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan04;

public class Perpustakaan
{
    // TODO(Level 7): koleksi di bawah ini PUBLIK -- pihak luar bisa Add/Clear
    //   seenaknya, melewati aturan Tambah(). Simpan daftar di field PRIVATE
    //   (List<Buku>) dan ekspos DaftarBuku sebagai properti read-only bertipe
    //   IReadOnlyList<Buku> (atau IReadOnlyCollection/IEnumerable) yang tidak
    //   bisa dipakai untuk mengubah koleksi asli.

    private List<Buku> _daftarBuku = new();
    public IReadOnlyList<Buku> DaftarBuku => _daftarBuku.AsReadOnly();
    public int JumlahJudul => _daftarBuku.Count;
    public void Tambah(Buku buku)
    {
        if (buku == null)
        throw new ArgumentNullException(nameof(buku), "Buku tidak boleh null");

        if (_daftarBuku.Any(b => b.Isbn == buku.Isbn))
        throw new InvalidOperationException("Buku dengan ISBN tersebut sudah ada.");

        _daftarBuku.Add(buku);
    }

    public Buku? Cari(string isbn)
    {
        return _daftarBuku.FirstOrDefault(b => b.Isbn == isbn);
    }

    public void PinjamBuku(string isbn, AkunAnggota akun)
    {
        // TODO(Level 10): "Tell, don't ask" -- Perpustakaan memutuskan semuanya.
        //   akun null -> ArgumentNullException; ISBN tidak ada ->
        //   ArgumentException; akun.Denda > 0 -> InvalidOperationException;
        //   akun.JumlahPinjamanAktif sudah sama dengan AkunAnggota.MaksPinjaman
        //   -> InvalidOperationException; selain itu panggil buku.Pinjam()
        //   (boleh melempar kalau stok habis) lalu akun.CatatPinjam().
        
        if (akun == null)
        {
            throw new ArgumentNullException(nameof(akun), "Akun anggota tidak boleh null.");
        }

        Buku? buku = Cari(isbn);
        if (buku == null)
        {
            throw new ArgumentException("Buku dengan ISBN tersebut tidak ditemukan.", nameof(isbn));
        }

        if (akun.Denda > 0)
        {
            throw new InvalidOperationException("Anggota tidak dapat meminjam buku karena masih memiliki denda.");
        }

        if (akun.JumlahPinjamanAktif >= AkunAnggota.MaksPinjaman)
        {
            throw new InvalidOperationException("Anggota telah mencapai batas maksimal peminjaman aktif.");
        }

        buku.Pinjam();
        
        akun.CatatPinjam();
    }

    public void KembalikanBuku(string isbn, AkunAnggota akun)
    {
        // TODO(Level 10): akun null -> ArgumentNullException; ISBN tidak ada ->
        //   ArgumentException; akun.JumlahPinjamanAktif = 0 ->
        //   InvalidOperationException; selain itu panggil buku.Kembalikan() lalu
        //   akun.CatatKembali().
        
        if (akun == null)
        {
            throw new ArgumentNullException(nameof(akun), "Akun anggota tidak boleh null.");
        }

        Buku? buku = Cari(isbn);
        if (buku == null) 
        {
            throw new ArgumentException("Buku dengan ISBN tersebut tidak ditemukan.", nameof(isbn));
        }

        if (akun.JumlahPinjamanAktif == 0)
        {
            throw new InvalidOperationException("Tidak ada pinjaman aktif pada akun anggota ini.");
        }

        buku.Kembalikan();
        
        akun.CatatKembali();
    }
}
