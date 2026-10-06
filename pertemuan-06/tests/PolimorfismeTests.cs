// File ini disediakan dosen untuk mengecek progres level secara otomatis.
// JANGAN DIUBAH — perubahan pada file ini tidak akan dipakai saat penilaian
// (dosen menimpa ulang folder tests/ sebelum menjalankan grading).
using System.Reflection;
using Pertemuan06;
using Xunit;

namespace Pertemuan06.Tests;

public class PolimorfismeTests
{
    // ---------- helper ----------

    // Memastikan `nama` di-OVERRIDE (bukan disembunyikan dengan `new`):
    // definisi dasarnya harus ada di Item, dan tipe turunan mendeklarasikannya sendiri.
    private static void PastikanOverride(Type turunan, string nama)
    {
        var m = turunan.GetMethod(nama, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                ?? (turunan.GetProperty(nama, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)?.GetMethod);
        Assert.True(m is not null, $"{turunan.Name} belum mendeklarasikan {nama} sendiri (override belum ditulis)");
        Assert.True(m!.GetBaseDefinition().DeclaringType == typeof(Item),
            $"{turunan.Name}.{nama} bukan override dari Item.{nama} (mungkin menyembunyikan dengan `new`, atau " +
            $"{nama} di Item belum `virtual`). Pastikan di Item bertanda `virtual` dan di {turunan.Name} bertanda `override`.");
    }

    private static void PastikanVirtualDiItem(string nama)
    {
        var m = typeof(Item).GetMethod(nama, BindingFlags.Public | BindingFlags.Instance)
                ?? typeof(Item).GetProperty(nama, BindingFlags.Public | BindingFlags.Instance)?.GetMethod;
        Assert.True(m is not null, $"Item.{nama} tidak ditemukan");
        Assert.True(m!.IsVirtual, $"Item.{nama} harus bertanda `virtual` supaya bisa di-override");
    }

    // ---------- Level 1 ----------

    [Fact(DisplayName = "Level 1 - Denda umum di kelas induk Item")]
    public void Level1_DendaUmum()
    {
        var item = new Item("Umum");
        Assert.Equal(3000, item.HitungDenda(3));
        Assert.Equal(1000, item.HitungDenda(1));
        Assert.Equal(0, item.HitungDenda(0));
        Assert.Equal(0, item.HitungDenda(-4));

        Assert.ThrowsAny<ArgumentException>(() => new Item("  "));
    }

    // ---------- Level 2 ----------

    [Fact(DisplayName = "Level 2 - Override HitungDenda di Buku (virtual + override)")]
    public void Level2_OverrideDendaBuku()
    {
        PastikanVirtualDiItem("HitungDenda");
        PastikanOverride(typeof(Buku), "HitungDenda");

        Item lewatInduk = new Buku("Bumi Manusia", "Pramoedya");   // variabel bertipe INDUK
        Assert.Equal(6000, lewatInduk.HitungDenda(3));             // tetapi yang jalan versi Buku
        Assert.Equal(0, lewatInduk.HitungDenda(0));
        Assert.Equal(0, lewatInduk.HitungDenda(-1));

        // Item biasa tetap memakai perilaku induk
        Assert.Equal(3000, new Item("Umum").HitungDenda(3));
    }

    // ---------- Level 3 ----------

    [Fact(DisplayName = "Level 3 - Override di Majalah & Dvd (dengan batas maksimum)")]
    public void Level3_OverrideMajalahDvd()
    {
        PastikanOverride(typeof(Majalah), "HitungDenda");
        PastikanOverride(typeof(Dvd), "HitungDenda");

        Item majalah = new Majalah("Tempo", 12);
        Assert.Equal(1500, majalah.HitungDenda(3));
        Assert.Equal(0, majalah.HitungDenda(0));

        Item dvd = new Dvd("Laskar Pelangi", 125);
        Assert.Equal(15000, dvd.HitungDenda(3));
        Assert.Equal(50000, dvd.HitungDenda(10));
        Assert.Equal(50000, dvd.HitungDenda(11));     // dibatasi maksimum
        Assert.Equal(50000, dvd.HitungDenda(999));
        Assert.Equal(0, dvd.HitungDenda(-2));
    }

    // ---------- Level 4 ----------

    [Fact(DisplayName = "Level 4 - Properti virtual MasaPinjamHari")]
    public void Level4_PropertiVirtual()
    {
        PastikanVirtualDiItem("MasaPinjamHari");
        PastikanOverride(typeof(Buku), "MasaPinjamHari");
        PastikanOverride(typeof(Majalah), "MasaPinjamHari");
        PastikanOverride(typeof(Dvd), "MasaPinjamHari");

        Item[] semua =
        {
            new Item("Umum"), new Buku("B", "P"), new Majalah("M", 1), new Dvd("D", 90),
        };
        Assert.Equal(new[] { 7, 14, 3, 2 }, semua.Select(i => i.MasaPinjamHari).ToArray());
    }

    // ---------- Level 5 ----------

    [Fact(DisplayName = "Level 5 - Deskripsi() memanggil base.Deskripsi() + ToString polimorfik")]
    public void Level5_DeskripsiDanBase()
    {
        PastikanVirtualDiItem("Deskripsi");
        PastikanOverride(typeof(Buku), "Deskripsi");

        Item[] semua =
        {
            new Item("Umum"),
            new Buku("Bumi Manusia", "Pramoedya"),
            new Majalah("Tempo", 12),
            new Dvd("Laskar Pelangi", 125),
        };
        Assert.Equal("[Umum]", semua[0].Deskripsi());
        Assert.Equal("[Bumi Manusia] oleh Pramoedya", semua[1].Deskripsi());
        Assert.Equal("[Tempo] edisi 12", semua[2].Deskripsi());
        Assert.Equal("[Laskar Pelangi] (125 menit)", semua[3].Deskripsi());

        // ToString() ikut polimorfik: dipanggil lewat variabel bertipe object sekalipun
        object[] sebagaiObject = semua;
        Assert.Equal("[Bumi Manusia] oleh Pramoedya", sebagaiObject[1].ToString());
        Assert.Equal("[Umum]", sebagaiObject[0].ToString());
    }

    // ---------- Level 6 ----------

    [Fact(DisplayName = "Level 6 - Koleksi polimorfik: Kasir menghitung tanpa cek tipe")]
    public void Level6_KoleksiPolimorfik()
    {
        var kasir = new Kasir();
        var daftar = new List<Peminjaman>
        {
            new(new Buku("A", "P"), 3),        // 6000
            new(new Majalah("M", 1), 2),       // 1000
            new(new Dvd("D", 90), 4),          // 20000
            new(new Item("I"), 1),             // 1000
        };
        Assert.Equal(28000, kasir.TotalDenda(daftar));
        Assert.Equal(0, kasir.TotalDenda(new List<Peminjaman>()));

        var tertinggi = kasir.ItemDenganDendaTertinggi(daftar);
        Assert.IsType<Dvd>(tertinggi);
        Assert.Null(kasir.ItemDenganDendaTertinggi(new List<Peminjaman>()));

        Assert.Throws<ArgumentNullException>(() => kasir.TotalDenda(null!));
        Assert.Throws<ArgumentNullException>(() => kasir.ItemDenganDendaTertinggi(null!));
    }

    // ---------- Level 7 ----------

    [Fact(DisplayName = "Level 7 - Type checking: as / is / pattern matching")]
    public void Level7_TypeChecking()
    {
        var buku1 = new Buku("A", "Penulis A");
        var buku2 = new Buku("B", "Penulis B");
        var campur = new List<Item> { new Majalah("M", 1), buku1, new Dvd("D", 90), buku2, new Item("I") };

        var hanyaBuku = PemilahItem.AmbilBuku(campur);
        Assert.Equal(2, hanyaBuku.Count);
        Assert.Same(buku1, hanyaBuku[0]);      // urutan asli dipertahankan
        Assert.Same(buku2, hanyaBuku[1]);
        Assert.Empty(PemilahItem.AmbilBuku(new Item[] { new Majalah("M", 1) }));
        Assert.Throws<ArgumentNullException>(() => PemilahItem.AmbilBuku(null!));

        Assert.Equal("Penulis A", PemilahItem.PenulisAtauNull(buku1));
        Assert.Null(PemilahItem.PenulisAtauNull(new Majalah("M", 1)));
        Assert.Null(PemilahItem.PenulisAtauNull(new Item("I")));
    }

    // ---------- Level 8 ----------

    [Fact(DisplayName = "Level 8 - Explicit cast (InvalidCastException) & pola TryXxx")]
    public void Level8_ExplicitCast()
    {
        Item asBuku = new Buku("A", "P");
        Item bukanBuku = new Majalah("M", 1);

        Assert.Same(asBuku, PemilahItem.KeBuku(asBuku));
        Assert.Throws<InvalidCastException>(() => PemilahItem.KeBuku(bukanBuku));
        Assert.Throws<ArgumentNullException>(() => PemilahItem.KeBuku(null!));

        Assert.True(PemilahItem.CobaKeBuku(asBuku, out var hasil));
        Assert.Same(asBuku, hasil);

        Assert.False(PemilahItem.CobaKeBuku(bukanBuku, out var kosong));
        Assert.Null(kosong);

        Assert.False(PemilahItem.CobaKeBuku(null, out var nol));   // tidak boleh melempar
        Assert.Null(nol);
    }

    // ---------- Level 9 ----------

    [Fact(DisplayName = "Level 9 - Overloading (saat kompilasi) vs overriding, switch type pattern")]
    public void Level9_OverloadingDanKategori()
    {
        var p = new Pemroses();
        Item variabelInduk = new Buku("Bumi Manusia", "Pramoedya");
        var variabelBuku = new Buku("Bumi Manusia", "Pramoedya");

        // overload dipilih dari TIPE VARIABEL saat kompilasi, bukan tipe objek sebenarnya
        Assert.Equal("Item: Bumi Manusia", p.Proses(variabelInduk));
        Assert.Equal("Buku: Bumi Manusia", p.Proses(variabelBuku));
        Assert.Equal("Buku: Bumi Manusia", p.Proses((Buku)variabelInduk));   // setelah downcast
        Assert.Equal("Majalah: Tempo", p.Proses(new Majalah("Tempo", 1)));
        Assert.Equal("Item: Umum", p.Proses(new Item("Umum")));

        // switch expression dengan type pattern -- evaluasi saat berjalan
        Assert.Equal("Buku", p.Kategori(variabelInduk));
        Assert.Equal("Majalah", p.Kategori(new Majalah("M", 1)));
        Assert.Equal("DVD", p.Kategori(new Dvd("D", 90)));
        Assert.Equal("Item", p.Kategori(new Item("I")));
        Assert.Throws<ArgumentNullException>(() => p.Kategori(null!));
    }

    // ---------- Level 10 (bonus) ----------

    [Fact(DisplayName = "Level 10 - Bonus: override Equals & GetHashCode")]
    public void Level10_EqualsDanHashCode()
    {
        var a = new Buku("Bumi Manusia", "Pramoedya");
        var b = new Buku("bumi manusia", "Penulis Lain");   // judul sama (abaikan huruf besar/kecil), jenis sama
        Assert.True(a.Equals(b));
        Assert.Equal(a.GetHashCode(), b.GetHashCode());

        Assert.False(a.Equals(new Majalah("Bumi Manusia", 1)));   // judul sama TAPI jenis beda
        Assert.False(a.Equals(new Buku("Judul Lain", "Pramoedya")));
        Assert.False(a.Equals(null));

        var himpunan = new HashSet<Item> { a, b, new Majalah("Bumi Manusia", 1), new Item("Bumi Manusia") };
        Assert.Equal(3, himpunan.Count);   // a & b dianggap satu

        Assert.Equal(new Item("X"), new Item("x"));
    }
}
