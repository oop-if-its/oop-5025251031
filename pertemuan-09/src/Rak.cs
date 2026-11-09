// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan08;

// SUDAH LENGKAP -- jangan diubah, kecuali method Ambil.
public record HasilPinjam(bool Berhasil, string Pesan);

public class Rak
{
    private readonly Dictionary<string, int> _stok;

    public Rak(IDictionary<string, int> stokAwal)
    {
        _stok = new Dictionary<string, int>(stokAwal, StringComparer.OrdinalIgnoreCase);
    }

    public int Stok(string judul) => _stok.TryGetValue(judul, out var s) ? s : 0;

    public void Ambil(string judul)
    {
        // TODO(Level 4): judul null/kosong/spasi -> ArgumentException; judul
        //   tidak ada di rak ATAU stoknya 0 -> lempar
        //   BukuTidakTersediaException(judul); selain itu kurangi stok satu.
        if (string.IsNullOrWhiteSpace(judul))
            throw new ArgumentException("Judul tidak boleh kosong.", nameof(judul));

        if (!_stok.TryGetValue(judul, out var stok) || stok <= 0)
            throw new BukuTidakTersediaException(judul);

        _stok[judul] = stok - 1;
    }
}

public class LayananPinjam
{
    private readonly Rak _rak;

    public int JumlahPercobaan { get; private set; }

    public LayananPinjam(Rak rak)
    {
        ArgumentNullException.ThrowIfNull(rak);
        _rak = rak;
    }

    public HasilPinjam CobaPinjam(string judul)
    {
        // TODO(Level 5): bungkus _rak.Ambil(judul) dengan try/catch/finally.
        //   Sukses -> HasilPinjam(true, "Berhasil meminjam: <judul>"). Menangkap
        //   BukuTidakTersediaException -> HasilPinjam(false, ex.Message).
        //   Exception LAIN (mis. ArgumentException) JANGAN ditangkap -- biarkan
        //   menjalar. Di blok `finally`, naikkan JumlahPercobaan -- harus selalu
        //   bertambah tiap CobaPinjam dipanggil, apa pun hasilnya (sukses,
        //   gagal, atau exception yang menjalar).
        try
        {
            _rak.Ambil(judul);
            return new HasilPinjam(true, $"Berhasil meminjam: {judul}");
        }
        catch (BukuTidakTersediaException ex)
        {
            return new HasilPinjam(false, ex.Message);
        }
        finally
        {
            JumlahPercobaan++;
        }
    }
}