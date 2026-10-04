// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan05;

// Komposisi: Perpustakaan MEMILIKI kumpulan Anggota. Karena Mahasiswa dan Dosen
// adalah Anggota, satu daftar bertipe Anggota bisa menampung keduanya.
public class Perpustakaan
{
    private readonly List<Anggota> _anggota = new();

    public int JumlahAnggota => _anggota.Count;

    public void Daftarkan(Anggota anggota)
    {
        // TODO(Level 8): anggota null -> ArgumentNullException; Id yang sudah
        //   terdaftar -> InvalidOperationException; selain itu tambahkan ke
        //   daftar.
        
        if (anggota == null)
        {
            throw new ArgumentNullException(nameof(anggota), "Anggota tidak boleh null.");
        }

        // Cek apakah ada anggota di dalam list yang Id-nya sama
        if (_anggota.Any(a => a.Id == anggota.Id))
        {
            throw new InvalidOperationException("Anggota dengan Id tersebut sudah terdaftar.");
        }

        _anggota.Add(anggota);
    }

    public Anggota? Cari(string id)
    {
        // TODO(Level 8): kembalikan anggota dengan Id yang sama persis, atau
        //   null.
        
        // LINQ FirstOrDefault akan mencari elemen pertama yang cocok, jika tidak ada mengembalikan null
        return _anggota.FirstOrDefault(a => a.Id == id);
    }

    public int JumlahMahasiswa()
    {
        // TODO(Level 8): hitung anggota yang bertipe Mahasiswa (termasuk
        //   turunannya, mis. Asisten -- ingat: turunan "adalah sebuah"
        //   Mahasiswa). Petunjuk: `is` atau OfType<T>().
        
        // OfType<Mahasiswa>() akan menyaring hanya objek yang bertipe Mahasiswa (dan turunannya seperti Asisten)
        return _anggota.OfType<Mahasiswa>().Count();
        
        // Alternatif lain yang sama benarnya:
        // return _anggota.Count(a => a is Mahasiswa);
    }

    public int JumlahDosen()
    {
        // TODO(Level 8): hitung anggota yang bertipe Dosen.
        
        return _anggota.OfType<Dosen>().Count();
    }
}
