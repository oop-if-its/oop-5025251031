// File ini disediakan dosen untuk mengecek progres level secara otomatis.
// JANGAN DIUBAH — perubahan pada file ini tidak akan dipakai saat penilaian
// (dosen menimpa ulang folder tests/ sebelum menjalankan grading).
using System.Reflection;
using System.Runtime.CompilerServices;
using Pertemuan08;
using Xunit;

namespace Pertemuan08.Tests;

public class SiklusHidupTests
{
    // ---------- helper ----------

    private static T PastikanImplementasi<T>(object obj) where T : class
    {
        var hasil = obj as T;
        Assert.True(hasil is not null,
            $"{obj.GetType().Name} belum mengimplementasikan {typeof(T).Name} (tambahkan di deklarasi kelas)");
        return hasil!;
    }

    // ---------- Level 1 ----------

    [Fact(DisplayName = "Level 1 - Rantai konstruktor this(...)")]
    public void Level1_RantaiThis()
    {
        var hariIni = DateOnly.FromDateTime(DateTime.Today);

        var p1 = new Peminjaman("Bumi Manusia");
        Assert.Equal("Bumi Manusia", p1.Judul);
        Assert.Equal("Umum", p1.Anggota);
        Assert.Equal(7, p1.LamaHari);
        Assert.InRange(p1.Tanggal, hariIni.AddDays(-1), hariIni.AddDays(1));

        var p2 = new Peminjaman("Bumi Manusia", "Sari");
        Assert.Equal("Sari", p2.Anggota);
        Assert.Equal(7, p2.LamaHari);

        var p3 = new Peminjaman("Bumi Manusia", "Sari", 14);
        Assert.Equal(14, p3.LamaHari);
        Assert.InRange(p3.Tanggal, hariIni.AddDays(-1), hariIni.AddDays(1));

        var tanggal = new DateOnly(2026, 10, 20);
        var p4 = new Peminjaman("Bumi Manusia", "Sari", 14, tanggal);
        Assert.Equal(tanggal, p4.Tanggal);

        // semua jalur melewati konstruktor utama tepat SEKALI per objek
        int sebelum = Peminjaman.JumlahInisialisasi;
        _ = new Peminjaman("A");
        _ = new Peminjaman("A", "B");
        _ = new Peminjaman("A", "B", 3);
        Assert.Equal(sebelum + 3, Peminjaman.JumlahInisialisasi);

        // validasi ada di konstruktor utama, jadi berlaku untuk semua overload
        Assert.ThrowsAny<ArgumentException>(() => new Peminjaman(""));
        Assert.ThrowsAny<ArgumentException>(() => new Peminjaman("A", " "));
        Assert.ThrowsAny<ArgumentException>(() => new Peminjaman("A", "B", 0));
    }

    // ---------- Level 2 ----------

    [Fact(DisplayName = "Level 2 - Urutan pemanggilan konstruktor & inisialisasi field")]
    public void Level2_UrutanKonstruktor()
    {
        Jejak.Reset();
        _ = new Tingkat3();
        Assert.Equal(new[]
        {
            "field Tingkat3", "field Tingkat2", "field Tingkat1",   // 1) field initializer: turunan -> induk
            "ctor Tingkat1", "ctor Tingkat2", "ctor Tingkat3",       // 2) badan konstruktor: induk -> turunan
        }, Jejak.Isi);

        Jejak.Reset();
        _ = new Tingkat2();
        Assert.Equal(new[] { "field Tingkat2", "field Tingkat1", "ctor Tingkat1", "ctor Tingkat2" }, Jejak.Isi);

        Jejak.Reset();
        _ = new Tingkat3("x");    // this() dijalankan dulu, badan konstruktor ini SESUDAHNYA
        Assert.Equal(new[]
        {
            "field Tingkat3", "field Tingkat2", "field Tingkat1",
            "ctor Tingkat1", "ctor Tingkat2", "ctor Tingkat3",
            "ctor Tingkat3(x)",
        }, Jejak.Isi);
    }

    // ---------- Level 3 ----------

    [Fact(DisplayName = "Level 3 - Static constructor: dijalankan sekali saja")]
    public void Level3_StaticConstructor()
    {
        // diakses berkali-kali dari beberapa anggota
        Assert.Equal(1000, Konfigurasi.DendaPerHari);
        Assert.Equal(3, Konfigurasi.MaksPinjam);
        Assert.Equal(1000, Konfigurasi.DendaPerHari);
        Assert.Equal(1, Konfigurasi.JumlahInisialisasi);

        Assert.NotNull(typeof(Konfigurasi).TypeInitializer);
    }

    // ---------- Level 4 ----------

    [Fact(DisplayName = "Level 4 - Exception kustom & melempar exception")]
    public void Level4_ExceptionKustom()
    {
        Assert.Equal(typeof(Exception), typeof(BukuTidakTersediaException).BaseType);

        var ex1 = new BukuTidakTersediaException("Bumi Manusia");
        Assert.Equal("Bumi Manusia", ex1.Judul);
        Assert.Contains("Bumi Manusia", ex1.Message);
        Assert.Null(ex1.InnerException);

        var penyebab = new InvalidOperationException("dasar");
        var ex2 = new BukuTidakTersediaException("X", penyebab);
        Assert.Same(penyebab, ex2.InnerException);
        Assert.Equal("X", ex2.Judul);
        Assert.Contains("X", ex2.Message);

        // dilempar oleh Rak.Ambil
        var rak = new Rak(new Dictionary<string, int> { ["Bumi Manusia"] = 1, ["Kosong"] = 0 });
        rak.Ambil("bumi manusia");   // huruf besar/kecil diabaikan oleh Rak
        Assert.Equal(0, rak.Stok("Bumi Manusia"));

        var habis = Assert.Throws<BukuTidakTersediaException>(() => rak.Ambil("Bumi Manusia"));
        Assert.Equal("Bumi Manusia", habis.Judul);
        Assert.Throws<BukuTidakTersediaException>(() => rak.Ambil("Kosong"));
        Assert.Throws<BukuTidakTersediaException>(() => rak.Ambil("Tidak Ada"));
        Assert.ThrowsAny<ArgumentException>(() => rak.Ambil("  "));
        Assert.ThrowsAny<ArgumentException>(() => rak.Ambil(null!));
    }

    // ---------- Level 5 ----------

    [Fact(DisplayName = "Level 5 - try/catch/finally")]
    public void Level5_TryCatchFinally()
    {
        var rak = new Rak(new Dictionary<string, int> { ["Bumi Manusia"] = 1 });
        var layanan = new LayananPinjam(rak);
        Assert.Equal(0, layanan.JumlahPercobaan);

        var sukses = layanan.CobaPinjam("Bumi Manusia");
        Assert.True(sukses.Berhasil);
        Assert.Equal("Berhasil meminjam: Bumi Manusia", sukses.Pesan);
        Assert.Equal(1, layanan.JumlahPercobaan);

        var gagal = layanan.CobaPinjam("Bumi Manusia");   // stok habis -> ditangkap
        Assert.False(gagal.Berhasil);
        Assert.Contains("Bumi Manusia", gagal.Pesan);
        Assert.Equal(2, layanan.JumlahPercobaan);

        // exception lain TIDAK ditangkap -- tapi finally tetap jalan
        Assert.ThrowsAny<ArgumentException>(() => layanan.CobaPinjam(""));
        Assert.Equal(3, layanan.JumlahPercobaan);

        Assert.Throws<ArgumentNullException>(() => new LayananPinjam(null!));
    }

    // ---------- Level 6 ----------

    [Fact(DisplayName = "Level 6 - Membungkus exception (InnerException)")]
    public void Level6_Membungkus()
    {
        Assert.Equal(42, ParserAngka.Parse("42"));
        Assert.Equal(-7, ParserAngka.Parse("-7"));

        var format = Assert.Throws<DataTidakValidException>(() => ParserAngka.Parse("abc"));
        Assert.Contains("abc", format.Message);
        Assert.IsType<FormatException>(format.InnerException);

        var luber = Assert.Throws<DataTidakValidException>(() => ParserAngka.Parse("99999999999"));
        Assert.IsType<OverflowException>(luber.InnerException);

        // null bukan "data tidak valid" -- ArgumentNullException dibiarkan menjalar
        Assert.Throws<ArgumentNullException>(() => ParserAngka.Parse(null!));
    }

    // ---------- Level 7 ----------

    [Fact(DisplayName = "Level 7 - throw; menjaga stack trace asli")]
    public void Level7_ThrowUlang()
    {
        var pencatat = new PencatatError();

        var ex = Assert.Throws<InvalidOperationException>(() => pencatat.JalankanDenganLog(Sumber.Gagal));
        Assert.Equal("boom", ex.Message);
        Assert.Equal(new[] { "ERROR: boom" }, pencatat.Log);

        // Jejak method penyebab asli (Sumber.Gagal) harus masih ada di stack trace.
        // `throw ex;` menghapusnya; `throw;` mempertahankannya.
        Assert.True(ex.StackTrace is not null && ex.StackTrace.Contains("Sumber.Gagal"),
            "Stack trace kehilangan Sumber.Gagal -- kemungkinan memakai `throw ex;`. Gunakan `throw;`");

        // aksi yang sukses tidak mencatat apa pun
        pencatat.JalankanDenganLog(() => { });
        Assert.Single(pencatat.Log);
    }

    // ---------- Level 8 ----------

    [Fact(DisplayName = "Level 8 - IDisposable & using")]
    public void Level8_DisposeDanUsing()
    {
        int sebelum = Koneksi.JumlahTerbuka;

        var koneksi = new Koneksi();
        var disposable = PastikanImplementasi<IDisposable>(koneksi);
        Assert.True(koneksi.Terbuka);
        Assert.Equal(sebelum + 1, Koneksi.JumlahTerbuka);
        Assert.Equal("hasil: SELECT 1", koneksi.Query("SELECT 1"));

        disposable.Dispose();
        Assert.False(koneksi.Terbuka);
        Assert.Equal(sebelum, Koneksi.JumlahTerbuka);
        Assert.Throws<ObjectDisposedException>(() => koneksi.Query("SELECT 1"));

        disposable.Dispose();     // idempoten: tidak boleh menurunkan hitungan dua kali
        Assert.Equal(sebelum, Koneksi.JumlahTerbuka);

        // Pemakai memakai `using`: koneksi tertutup walau terjadi exception
        Assert.Equal("hasil: SELECT 2", Pemakai.Jalankan("SELECT 2", sengajaGagal: false));
        Assert.Equal(sebelum, Koneksi.JumlahTerbuka);

        var ex = Assert.Throws<InvalidOperationException>(() => Pemakai.Jalankan("SELECT 3", sengajaGagal: true));
        Assert.Equal("gagal", ex.Message);
        Assert.Equal(sebelum, Koneksi.JumlahTerbuka);
    }

    // ---------- Level 9 ----------

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void BuatTanpaDispose()
    {
        var s = new SumberDaya();
        s.Gunakan();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void BuatLaluDispose()
    {
        var s = new SumberDaya();
        s.Gunakan();
        PastikanImplementasi<IDisposable>(s).Dispose();
    }

    private static bool TungguHingga(Func<bool> kondisi)
    {
        for (int i = 0; i < 40; i++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (kondisi()) return true;
            Thread.Sleep(25);
        }
        return kondisi();
    }

    [Fact(DisplayName = "Level 9 - Pola Dispose(bool) + finalizer + SuppressFinalize")]
    public void Level9_PolaDisposeDanFinalizer()
    {
        var tipe = typeof(SumberDaya);
        Assert.True(typeof(IDisposable).IsAssignableFrom(tipe), "SumberDaya harus mengimplementasikan IDisposable");

        var disposeBool = tipe.GetMethod("Dispose", BindingFlags.NonPublic | BindingFlags.Instance, new[] { typeof(bool) });
        Assert.True(disposeBool is not null && disposeBool.IsFamily && disposeBool.IsVirtual,
            "Harus ada `protected virtual void Dispose(bool disposing)`");
        var finalizer = tipe.GetMethod("Finalize", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        Assert.True(finalizer is not null, "SumberDaya harus punya finalizer `~SumberDaya()`");

        int d0 = SumberDaya.DibersihkanDispose;
        int f0 = SumberDaya.DibersihkanFinalizer;

        // (1) di-Dispose oleh pemakai -> pembersihan lewat jalur Dispose, finalizer TIDAK jalan lagi
        BuatLaluDispose();
        TungguHingga(() => false);            // beri kesempatan GC + finalizer
        Assert.Equal(d0 + 1, SumberDaya.DibersihkanDispose);
        Assert.Equal(f0, SumberDaya.DibersihkanFinalizer);

        // (2) Dispose dua kali tidak membersihkan dua kali
        var s = new SumberDaya();
        var disposable = PastikanImplementasi<IDisposable>(s);
        disposable.Dispose();
        disposable.Dispose();
        Assert.Equal(d0 + 2, SumberDaya.DibersihkanDispose);

        // (3) LUPA Dispose -> finalizer menjadi jaring pengaman
        BuatTanpaDispose();
        bool selesai = TungguHingga(() => SumberDaya.DibersihkanFinalizer == f0 + 1);
        Assert.True(selesai, "Finalizer tidak berjalan untuk objek yang lupa di-Dispose");
        Assert.Equal(d0 + 2, SumberDaya.DibersihkanDispose);   // jalur Dispose tidak ikut bertambah
    }

    // ---------- Level 10 (bonus) ----------

    [Fact(DisplayName = "Level 10 - Bonus: kumpulkan semua kesalahan (ValidasiException & TryValidasi)")]
    public void Level10_KumpulkanKesalahan()
    {
        // valid
        ValidatorAnggota.Validasi("Sari", "5025201001", 20);
        Assert.True(ValidatorAnggota.TryValidasi("Sari", "5025201001", 20, out var kosong));
        Assert.Empty(kosong);

        // satu kesalahan
        var satu = Assert.Throws<ValidasiException>(() => ValidatorAnggota.Validasi("Sari", "123", 20));
        Assert.Equal(new[] { "NRP harus 10 digit" }, satu.Daftar);

        // SEMUA kesalahan dilaporkan sekaligus, berurutan (nama, nrp, usia)
        var semua = Assert.Throws<ValidasiException>(() => ValidatorAnggota.Validasi(" ", "12345678AB", 10));
        Assert.Equal(new[] { "Nama wajib diisi", "NRP harus 10 digit", "Usia harus 17-80" }, semua.Daftar);
        Assert.Contains("3", semua.Message);

        // batas usia & NRP null
        ValidatorAnggota.Validasi("A", "0123456789", 17);
        ValidatorAnggota.Validasi("A", "0123456789", 80);
        Assert.Throws<ValidasiException>(() => ValidatorAnggota.Validasi("A", "0123456789", 81));
        Assert.Throws<ValidasiException>(() => ValidatorAnggota.Validasi("A", null!, 20));

        // versi tanpa exception
        Assert.False(ValidatorAnggota.TryValidasi("", "1", 99, out var daftar));
        Assert.Equal(3, daftar.Count);
    }
}
