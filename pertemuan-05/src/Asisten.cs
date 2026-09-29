// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan05;

// TODO(Level 6): tulis kelas `Asisten` DI SINI dari nol (file ini sengaja
//   kosong): public sealed class Asisten : Mahasiswa. Konstruktor: (string id,
//   string nama, Alamat alamat, string nrp, string prodi, string mataKuliah) --
//   teruskan lima parameter pertama ke konstruktor Mahasiswa lewat `:
//   base(...)`. Properti get-only `MataKuliah`. BatasPinjam = 5. Method
//   `InfoAsisten()` mengembalikan "<InfoLengkap()> | Asisten: <MataKuliah>".

public sealed class Asisten : Mahasiswa
{
    public string MataKuliah { get; }

    public Asisten(string id, string nama, Alamat alamat, string nrp, string prodi, string mataKuliah) 
        : base(id, nama, alamat, nrp, prodi)
    {
        if (string.IsNullOrWhiteSpace(mataKuliah))
        {
            throw new ArgumentException("Mata kuliah tidak boleh kosong atau spasi saja.", nameof(mataKuliah));
        }

        MataKuliah = mataKuliah;
        
        // Mengubah batas pinjam khusus untuk Asisten
        BatasPinjam = 5;
    }

    public string InfoAsisten()
    {
        return $"{InfoLengkap()} | Asisten: {MataKuliah}";
    }
}
