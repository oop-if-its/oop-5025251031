// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan07;

// Dua implementasi berbeda dari SATU interface INotifikasi.
// TODO(Level 7): buat NotifikasiEmail mengimplementasikan INotifikasi.
//   Kirim(pesan): pesan null/kosong/spasi -> ArgumentException; selain itu
//   tambahkan "EMAIL: <pesan>" ke daftar Terkirim.
public class NotifikasiEmail : INotifikasi
{
    private readonly List<string> _terkirim = new();

    // SUDAH LENGKAP.
    public IReadOnlyList<string> Terkirim => _terkirim.AsReadOnly();

    // TODO(Level 7): tulis method Kirim(string pesan) sesuai petunjuk di atas
    //   kelas.
    public void Kirim(string pesan)
    {
     if (string.IsNullOrWhiteSpace(pesan))
        {
            throw new ArgumentException("Pesan tidak boleh null, kosong, atau hanya berisi spasi.", nameof(pesan));
        }

        _terkirim.Add($"EMAIL: {pesan}");   
    }

}

// TODO(Level 7): buat NotifikasiSms mengimplementasikan INotifikasi.
//   Kirim(pesan): pesan null/kosong/spasi -> ArgumentException; SMS dibatasi 40
//   karakter -- pesan yang lebih panjang DIPOTONG menjadi 40 karakter pertama;
//   tambahkan "SMS: <pesan>" ke daftar Terkirim.
public class NotifikasiSms : INotifikasi
{
    private readonly List<string> _terkirim = new();

    // SUDAH LENGKAP.
    public IReadOnlyList<string> Terkirim => _terkirim.AsReadOnly();

    // TODO(Level 7): tulis method Kirim(string pesan) sesuai petunjuk di atas
    //   kelas.
    public void Kirim(string pesan)
    {
       if (string.IsNullOrWhiteSpace(pesan))
        {
            throw new ArgumentException("Pesan tidak boleh null, kosong, atau hanya berisi spasi.", nameof(pesan));
        }

        if (pesan.Length > 40)
        {
            pesan = pesan.Substring(0, 40);
        }

        _terkirim.Add($"SMS: {pesan}"); 
    }
}
