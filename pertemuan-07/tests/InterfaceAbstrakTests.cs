// File ini disediakan dosen untuk mengecek progres level secara otomatis.
// JANGAN DIUBAH — perubahan pada file ini tidak akan dipakai saat penilaian
// (dosen menimpa ulang folder tests/ sebelum menjalankan grading).
using System.Reflection;
using Pertemuan07;
using Xunit;

namespace Pertemuan07.Tests;

public class InterfaceAbstrakTests
{
    // ---------- helper ----------

    // Test memakai `as` (bukan cast langsung) untuk memeriksa apakah sebuah kelas
    // sudah mengimplementasikan interface -- supaya starter tetap bisa dikompilasi
    // sebelum kalian menuliskannya.
    private static T PastikanImplementasi<T>(object obj) where T : class
    {
        var hasil = obj as T;
        Assert.True(hasil is not null,
            $"{obj.GetType().Name} belum mengimplementasikan {typeof(T).Name} (tambahkan di deklarasi kelas)");
        return hasil!;
    }

    private sealed class NotifikasiPalsu : INotifikasi
    {
        public List<string> Pesan { get; } = new();
        public void Kirim(string pesan) => Pesan.Add(pesan);
    }

    // ---------- Level 1 ----------

    [Fact(DisplayName = "Level 1 - Kelas abstrak Item & metode abstrak HitungDenda")]
    public void Level1_KelasAbstrak()
    {
        Assert.True(typeof(Item).IsAbstract, "Item harus `abstract`");

        var m = typeof(Item).GetMethod("HitungDenda", BindingFlags.Public | BindingFlags.Instance);
        Assert.True(m is not null && m.IsAbstract, "Item.HitungDenda harus `abstract` (tanpa badan)");

        // kelas abstrak tidak bisa dibuat langsung
        Assert.ThrowsAny<Exception>(() => Activator.CreateInstance(typeof(Item), nonPublic: true));

        // tetapi bisa dipakai sebagai tipe variabel untuk turunannya
        Item buku = new Buku("Bumi Manusia", "Pramoedya");
        Assert.Equal(6000, buku.HitungDenda(3));
        Assert.Equal(0, buku.HitungDenda(0));
        Assert.Equal(0, buku.HitungDenda(-2));
        Assert.Equal(m!.DeclaringType, typeof(Item));
        Assert.Equal(typeof(Buku), typeof(Buku).GetMethod("HitungDenda")!.DeclaringType);
    }

    // ---------- Level 2 ----------

    [Fact(DisplayName = "Level 2 - Properti abstract + method konkret di kelas abstrak")]
    public void Level2_AbstractPropertiDanMethodKonkret()
    {
        var prop = typeof(Item).GetProperty("MasaPinjamHari", BindingFlags.Public | BindingFlags.Instance);
        Assert.True(prop?.GetMethod is not null && prop.GetMethod.IsAbstract,
            "Item.MasaPinjamHari harus properti `abstract`");
        Assert.True(typeof(Item).IsAbstract, "Item harus `abstract`");

        Item[] semua = { new Buku("B", "P"), new Majalah("M", 1), new Dvd("D", 90) };
        Assert.Equal(new[] { 14, 3, 2 }, semua.Select(i => i.MasaPinjamHari).ToArray());

        // method konkret yang memakai anggota abstrak (jenis konkretnya tidak dikenal Item)
        var pinjam = new DateOnly(2026, 10, 1);
        Assert.Equal(new DateOnly(2026, 10, 15), semua[0].HitungTanggalKembali(pinjam));
        Assert.Equal(new DateOnly(2026, 10, 4), semua[1].HitungTanggalKembali(pinjam));
        Assert.Equal(new DateOnly(2026, 10, 3), semua[2].HitungTanggalKembali(pinjam));

        // denda Majalah & Dvd
        Assert.Equal(1500, semua[1].HitungDenda(3));
        Assert.Equal(15000, semua[2].HitungDenda(3));
        Assert.Equal(50000, semua[2].HitungDenda(11));
        Assert.Equal(0, semua[2].HitungDenda(0));
    }

    // ---------- Level 3 ----------

    [Fact(DisplayName = "Level 3 - Interface IDapatDipinjam pada Buku, Majalah, Dvd")]
    public void Level3_IDapatDipinjam()
    {
        Assert.True(typeof(IDapatDipinjam).IsInterface);

        object[] semua = { new Buku("B", "P"), new Majalah("M", 1), new Dvd("D", 90) };
        foreach (var obj in semua)
        {
            var d = PastikanImplementasi<IDapatDipinjam>(obj);
            Assert.True(d.Tersedia);

            d.Pinjam();
            Assert.False(d.Tersedia);
            Assert.Throws<InvalidOperationException>(() => d.Pinjam());   // sudah dipinjam

            d.Kembalikan();
            Assert.True(d.Tersedia);
            Assert.Throws<InvalidOperationException>(() => d.Kembalikan()); // tidak sedang dipinjam
        }

        // status tiap objek berdiri sendiri
        var a = PastikanImplementasi<IDapatDipinjam>(new Buku("A", "P"));
        var b = PastikanImplementasi<IDapatDipinjam>(new Buku("B", "P"));
        a.Pinjam();
        Assert.True(b.Tersedia);
    }

    // ---------- Level 4 ----------

    [Fact(DisplayName = "Level 4 - Interface IDapatDiperpanjang hanya pada Buku")]
    public void Level4_IDapatDiperpanjang()
    {
        Assert.Null(new Majalah("M", 1) as IDapatDiperpanjang);
        Assert.Null(new Dvd("D", 90) as IDapatDiperpanjang);
        Assert.Null(new BukuReferensi("Kamus") as IDapatDiperpanjang);

        var buku = new Buku("B", "P");
        var pinjam = PastikanImplementasi<IDapatDipinjam>(buku);
        var panjang = PastikanImplementasi<IDapatDiperpanjang>(buku);

        Assert.Equal(0, panjang.JumlahPerpanjangan);
        Assert.Throws<InvalidOperationException>(() => panjang.Perpanjang());   // belum dipinjam

        pinjam.Pinjam();
        Assert.True(panjang.Perpanjang());
        Assert.True(panjang.Perpanjang());
        Assert.Equal(2, panjang.JumlahPerpanjangan);
        Assert.False(panjang.Perpanjang());          // batas 2 kali
        Assert.Equal(2, panjang.JumlahPerpanjangan);

        // dikembalikan -> hitungan perpanjangan direset
        pinjam.Kembalikan();
        pinjam.Pinjam();
        Assert.Equal(0, panjang.JumlahPerpanjangan);
        Assert.True(panjang.Perpanjang());
    }

    // ---------- Level 5 ----------

    [Fact(DisplayName = "Level 5 - Bekerja lewat interface: koleksi & filter")]
    public void Level5_BekerjaLewatInterface()
    {
        var buku = new Buku("B", "P");
        var majalah = new Majalah("M", 1);
        var dvd = new Dvd("D", 90);
        var referensi = new BukuReferensi("Kamus Besar");

        Assert.Null(referensi as IDapatDipinjam);   // referensi tidak boleh dipinjam

        var campur = new List<Item> { buku, referensi, majalah, dvd };
        var bisaDipinjam = Katalog.HanyaBisaDipinjam(campur);
        Assert.Equal(3, bisaDipinjam.Count);
        Assert.Same(buku, bisaDipinjam[0]);
        Assert.Same(majalah, bisaDipinjam[1]);
        Assert.Same(dvd, bisaDipinjam[2]);
        Assert.Throws<ArgumentNullException>(() => Katalog.HanyaBisaDipinjam(null!));

        // PinjamSemua: yang gagal dilewati, sisanya tetap diproses
        var b1 = PastikanImplementasi<IDapatDipinjam>(new Buku("B1", "P"));
        var b2 = PastikanImplementasi<IDapatDipinjam>(new Buku("B2", "P"));
        b2.Pinjam();   // sudah dipinjam duluan
        var b3 = PastikanImplementasi<IDapatDipinjam>(new Dvd("D3", 60));
        Assert.Equal(2, Katalog.PinjamSemua(new[] { b1, b2, b3 }));
        Assert.False(b1.Tersedia);
        Assert.False(b3.Tersedia);
        Assert.Throws<ArgumentNullException>(() => Katalog.PinjamSemua(null!));
    }

    // ---------- Level 6 ----------

    [Fact(DisplayName = "Level 6 - Dependency Injection lewat interface INotifikasi")]
    public void Level6_DependencyInjection()
    {
        Assert.Throws<ArgumentNullException>(() => new LayananPeminjaman(null!));

        var palsu = new NotifikasiPalsu();          // pengganti buatan test (bukan email/SMS sungguhan)
        var layanan = new LayananPeminjaman(palsu);

        var buku = new Buku("Bumi Manusia", "Pramoedya");
        layanan.Pinjamkan(buku, "Sari");
        Assert.False(PastikanImplementasi<IDapatDipinjam>(buku).Tersedia);
        Assert.Equal(new[] { "Sari meminjam Bumi Manusia" }, palsu.Pesan);

        // dipinjam lagi -> gagal, dan TIDAK ada notifikasi tambahan
        Assert.Throws<InvalidOperationException>(() => layanan.Pinjamkan(buku, "Tono"));
        Assert.Single(palsu.Pesan);

        // buku referensi tidak bisa dipinjam
        Assert.Throws<InvalidOperationException>(() => layanan.Pinjamkan(new BukuReferensi("Kamus"), "Sari"));
        Assert.Single(palsu.Pesan);

        Assert.Throws<ArgumentNullException>(() => layanan.Pinjamkan(null!, "Sari"));
    }

    // ---------- Level 7 ----------

    [Fact(DisplayName = "Level 7 - Dua implementasi INotifikasi (Email & SMS)")]
    public void Level7_DuaImplementasi()
    {
        var email = new NotifikasiEmail();
        var sms = new NotifikasiSms();
        var lewatEmail = PastikanImplementasi<INotifikasi>(email);
        var lewatSms = PastikanImplementasi<INotifikasi>(sms);

        lewatEmail.Kirim("Halo");
        lewatSms.Kirim("Halo");
        Assert.Equal(new[] { "EMAIL: Halo" }, email.Terkirim);
        Assert.Equal(new[] { "SMS: Halo" }, sms.Terkirim);

        // SMS dipotong 40 karakter
        string panjang = new string('x', 55);
        lewatSms.Kirim(panjang);
        Assert.Equal("SMS: " + new string('x', 40), sms.Terkirim[1]);
        lewatEmail.Kirim(panjang);
        Assert.Equal("EMAIL: " + panjang, email.Terkirim[1]);   // email tidak dipotong

        Assert.ThrowsAny<ArgumentException>(() => lewatEmail.Kirim("  "));
        Assert.ThrowsAny<ArgumentException>(() => lewatSms.Kirim(null!));

        // LayananPeminjaman yang SAMA bekerja dengan keduanya
        var buku = new Buku("A", "P");
        new LayananPeminjaman(lewatEmail).Pinjamkan(buku, "Sari");
        Assert.Equal("EMAIL: Sari meminjam A", email.Terkirim[^1]);
        var dvd = new Dvd("B", 90);
        new LayananPeminjaman(lewatSms).Pinjamkan(dvd, "Tono");
        Assert.Equal("SMS: Tono meminjam B", sms.Terkirim[^1]);
    }

    // ---------- Level 8 ----------

    [Fact(DisplayName = "Level 8 - Default interface method & implementasi eksplisit")]
    public void Level8_DefaultDanEksplisit()
    {
        var buku = new Buku("Bumi Manusia", "Pramoedya");
        var majalah = new Majalah("Tempo", 12);

        var cetakBuku = PastikanImplementasi<IDapatDicetak>(buku);
        var cetakMajalah = PastikanImplementasi<IDapatDicetak>(majalah);

        Assert.Equal("Buku: Bumi Manusia oleh Pramoedya", cetakBuku.Cetak());
        Assert.Equal("Majalah: Tempo edisi 12", cetakMajalah.Cetak());

        // default interface method: implementasi bawaan dari interface
        Assert.Equal("=== Buku: Bumi Manusia oleh Pramoedya ===", cetakBuku.CetakDenganHeader());
        Assert.Equal("=== Majalah: Tempo edisi 12 ===", cetakMajalah.CetakDenganHeader());
        Assert.Null(typeof(Buku).GetMethod("CetakDenganHeader"));   // bukan anggota kelas Buku

        // Buku: implementasi implisit (public). Majalah: EKSPLISIT (tidak terlihat di tipe kelas)
        Assert.NotNull(typeof(Buku).GetMethod("Cetak", BindingFlags.Public | BindingFlags.Instance));
        Assert.Null(typeof(Majalah).GetMethod("Cetak", BindingFlags.Public | BindingFlags.Instance));

        // Dvd tidak mencetak
        Assert.Null(new Dvd("D", 90) as IDapatDicetak);
    }

    // ---------- Level 9 ----------

    [Fact(DisplayName = "Level 9 - IComparable<Item>: urut berdasarkan judul")]
    public void Level9_IComparable()
    {
        Assert.True(typeof(IComparable<Item>).IsAssignableFrom(typeof(Item)),
            "Item harus mengimplementasikan IComparable<Item>");

        Item[] daftar =
        {
            new Dvd("cinta", 90),
            new Buku("Bumi Manusia", "P"),
            new Majalah("Tempo", 1),
            new Buku("Antologi", "P"),
        };
        Array.Sort(daftar);   // bekerja untuk SEMUA jenis turunan sekaligus

        Assert.Equal(new[] { "Antologi", "Bumi Manusia", "cinta", "Tempo" }, daftar.Select(i => i.Judul).ToArray());

        Assert.True(daftar[0].CompareTo(null) > 0);
        Assert.Equal(0, new Buku("abc", "P").CompareTo(new Majalah("ABC", 1)));   // huruf besar/kecil diabaikan
    }

    // ---------- Level 10 (bonus) ----------

    [Fact(DisplayName = "Level 10 - Bonus: Template Method BuatStruk + hook Catatan")]
    public void Level10_TemplateMethod()
    {
        var m = typeof(Item).GetMethod("BuatStruk", BindingFlags.Public | BindingFlags.Instance);
        Assert.True(m is not null && !m.IsVirtual, "BuatStruk harus method biasa (bukan virtual) -- kerangka langkahnya dikunci");

        Item buku = new Buku("Bumi Manusia", "Pramoedya");
        Assert.Equal(
            "STRUK PENGEMBALIAN\nJudul: Bumi Manusia\nTerlambat: 3 hari\nDenda: Rp6000",
            buku.BuatStruk(3));

        // hari negatif dianggap 0
        Assert.Equal(
            "STRUK PENGEMBALIAN\nJudul: Bumi Manusia\nTerlambat: 0 hari\nDenda: Rp0",
            buku.BuatStruk(-5));

        // Dvd menimpa hook Catatan()
        Item dvd = new Dvd("Laskar Pelangi", 125);
        Assert.Equal(
            "STRUK PENGEMBALIAN\nJudul: Laskar Pelangi\nTerlambat: 2 hari\nDenda: Rp10000\nCatatan: Periksa kondisi disk",
            dvd.BuatStruk(2));

        Item majalah = new Majalah("Tempo", 1);
        Assert.DoesNotContain("Catatan", majalah.BuatStruk(1));
    }
}
