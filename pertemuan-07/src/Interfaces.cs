// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan07;

// Kontrak: "yang bisa dipinjam". Interface hanya menyatakan APA yang bisa
// dilakukan, bukan BAGAIMANA. SUDAH LENGKAP -- jangan diubah.
public interface IDapatDipinjam
{
    bool Tersedia { get; }
    void Pinjam();
    void Kembalikan();
}

// Kontrak tambahan yang tidak dimiliki semua item (mis. majalah tidak bisa
// diperpanjang). SUDAH LENGKAP -- jangan diubah.
public interface IDapatDiperpanjang
{
    int JumlahPerpanjangan { get; }
    bool Perpanjang();
}

// Kontrak untuk pemberitahuan ke anggota. SUDAH LENGKAP -- jangan diubah.
public interface INotifikasi
{
    void Kirim(string pesan);
}

public interface IDapatDicetak
{
    string Cetak();

    // Default interface method (C# 8+): interface boleh menyediakan
    // implementasi bawaan. Hanya bisa dipanggil lewat VARIABEL BERTIPE INTERFACE.
    string CetakDenganHeader()
    {
        // TODO(Level 8): kembalikan "=== <Cetak()> ===" -- mis. "=== Buku: Bumi
        //   Manusia oleh Pramoedya ===".
        throw new NotImplementedException("Level 8 belum diimplementasikan");
    }
}
