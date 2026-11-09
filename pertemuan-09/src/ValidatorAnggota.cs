// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan08;

// Bonus (Level 10): mengumpulkan SEMUA kesalahan sebelum melapor, alih-alih
// berhenti di kesalahan pertama.
public static class ValidatorAnggota
{
    private static List<string> Periksa(string nama, string nrp, int usia)
    {
        var galat = new List<string>();
        // TODO(Level 10): kumpulkan pesan ke daftar `galat`, TANPA berhenti di
        //   kesalahan pertama: nama null/kosong/spasi -> "Nama wajib diisi"; nrp
        //   bukan tepat 10 digit angka (null juga tidak valid) -> "NRP harus 10
        //   digit"; usia di luar 17..80 -> "Usia harus 17-80". Urutannya harus
        //   begitu (nama, nrp, usia).
        if (string.IsNullOrWhiteSpace(nama))
            galat.Add("Nama wajib diisi");

        if (nrp is null || nrp.Length != 10)
        {
            galat.Add("NRP harus 10 digit");
        }
        else
        {
            foreach (var c in nrp)
            {
                if (c < '0' || c > '9')
                {
                    galat.Add("NRP harus 10 digit");
                    break;
                }
            }
        }

        if (usia < 17 || usia > 80)
            galat.Add("Usia harus 17-80");

        return galat;
    }

    public static void Validasi(string nama, string nrp, int usia)
    {
        // TODO(Level 10): bila ada kesalahan (dari Periksa), lempar
        //   ValidasiException dengan SELURUH daftarnya; kalau tidak ada, tidak
        //   terjadi apa-apa.
        var galat = Periksa(nama, nrp, usia);
        if (galat.Count > 0)
            throw new ValidasiException(galat);
    }

    public static bool TryValidasi(string nama, string nrp, int usia, out IReadOnlyList<string> daftarError)
    {
        // TODO(Level 10): versi TANPA exception (pola TryXxx): isi daftarError
        //   dengan hasil Periksa dan kembalikan true kalau daftarnya kosong.
        //   TIDAK boleh melempar exception.
        var galat = Periksa(nama, nrp, usia);
        daftarError = galat;
        return galat.Count == 0;
    }
}