// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan04;

public class Buku 
{
    // TODO(Level 1): field PUBLIK di bawah ini melanggar enkapsulasi (siapa pun
    //   bisa mengubahnya sembarangan). Jadikan field PRIVATE (awali nama dengan
    //   _) lalu ekspos lewat properti read-only: public get, tanpa setter
    //   publik. Nama properti tetap Isbn, Judul, StokTotal, StokTersedia.
    private string _Isbn;
    private string _Judul;
    private int _StokTotal;
    private int _StokTersedia;

    public string Isbn => _Isbn;              
    public string Judul => _Judul;
    public int StokTotal => _StokTotal;
    public int StokTersedia => _StokTersedia;

    // TODO(Level 8): properti di bawah ini menerima nilai apa saja. Beri nilai
    //   awal 7 dan tambahkan logika validasi di accessor set (perlu field
    //   pendukung): nilai harus 1..30, di luar itu lempar
    //   ArgumentOutOfRangeException dan JANGAN mengubah nilai lama.
    public int BatasHariPinjam { get; set; }

    // TODO(Level 2): validasi di AWAL konstruktor -- judul null/kosong/spasi
    //   saja atau stokTotal negatif -> lempar ArgumentException
    //   (ArgumentOutOfRangeException juga boleh); jangan ada state yang berubah
    //   kalau ditolak.
    // TODO(Level 6): validasi & normalisasi ISBN -- buang tanda '-' dan spasi;
    //   hasilnya harus tepat 13 digit angka dengan digit cek ISBN-13 yang benar;
    //   kalau tidak, lempar ArgumentException. Isbn menyimpan versi TANPA '-'.
    public Buku(string isbn, string judul, int stokTotal)
    {
        if(string.IsNullOrWhiteSpace(judul))
        {
            throw new ArgumentException("Judul tidak boleh kosong");
        }else if(stokTotal < 0)
        {
            throw new ArgumentException("Stok tidak boleh negatif");
        }
        // TODO(Level 1): isi Isbn, Judul, StokTotal dari parameter; StokTersedia
        //   awal = stokTotal.
        _Isbn = isbn;
        _Judul = judul;
        _StokTotal = stokTotal;
        _StokTersedia = stokTotal;
    }

    public void Pinjam()
    {
        // TODO(Level 3): kurangi StokTersedia satu. Kalau stok sudah 0, lempar
        //   InvalidOperationException dan biarkan stok tetap.
        if(_StokTersedia > 0)
        {
            _StokTersedia--;
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    public void Kembalikan()
    {
        // TODO(Level 4): tambah StokTersedia satu. Kalau stok sudah sama dengan
        //   StokTotal (tidak ada yang sedang dipinjam), lempar
        //   InvalidOperationException dan biarkan stok tetap.
        if(_StokTersedia == _StokTotal)
        {
            throw new InvalidOperationException();
        }

        _StokTersedia++;
    }

    // Level 5: properti TERHITUNG -- tanpa field pendukung, tanpa setter.
    public double PersentaseTersedia
    {
        get
        {
            // TODO(Level 5): kembalikan StokTersedia / StokTotal * 100 (double).
            //   Kalau StokTotal = 0 kembalikan 0 (bukan NaN).
            if (_StokTotal != 0)
            {
                return _StokTersedia * 100.0 / _StokTotal;
            } 

            return 0;
        }
    }

    public string Status
    {
        get
        {
            // TODO(Level 5): kembalikan "Tersedia" kalau StokTersedia > 0,
            //   selain itu "Habis".
            if (_StokTersedia > 0)
            {
                return "Tersedia";
            }
            else
            {
                return "Habis";
            }
        }
    }

}
