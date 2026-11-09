# Pertemuan 8 — Object Life Cycle, Constructor Chain & Exception Handling

Tugas ini mengikuti **perjalanan hidup sebuah objek** — dari dilahirkan (konstruktor, urutan inisialisasi), dipakai, sampai dibersihkan (`Dispose`, finalizer) — dan **apa yang terjadi ketika sesuatu gagal** di tengah jalan (exception: melempar, menangkap, membungkus, melempar ulang, `finally`). Studi kasusnya masih dunia perpustakaan, tetapi tiap level berdiri sendiri-sendiri.

Semua level dicek otomatis lewat `dotnet test`. Nilai mengikuti level tertinggi yang lolos **berurutan** dari 1 — kerjakan sejauh kemampuan, boleh tidak berurutan (jalankan `dotnet test pertemuan-08/tests -v normal` untuk lihat status tiap level apa adanya).

**Struktur folder yang wajib diikuti** (sudah disiapkan di starter, jangan diubah namanya):
```
pertemuan-08/
  SOAL.md
  src/
    Pertemuan08.csproj
    Peminjaman.cs         <- edit (Level 1)
    RantaiKonstruktor.cs  <- edit (Level 2)
    Konfigurasi.cs        <- edit (Level 3)
    Pengecualian.cs       <- edit (Level 4); DataTidakValidException & ValidasiException SUDAH LENGKAP
    Rak.cs                <- edit Rak.Ambil (Level 4) & LayananPinjam (Level 5)
    ParserAngka.cs        <- edit (Level 6)
    PencatatError.cs      <- edit (Level 7)
    Koneksi.cs            <- edit (Level 8)
    SumberDaya.cs         <- edit (Level 9)
    ValidatorAnggota.cs   <- edit (Level 10)
  tests/
    Pertemuan08.Tests.csproj
    SiklusHidupTests.cs    <- JANGAN diubah, ini test grading dosen
```

Bagian yang harus kalian kerjakan ditandai `TODO(Level N)`. Kode yang ditandai `SUDAH LENGKAP` jangan diubah.

---

## Level 1 — Rantai Konstruktor `this(...)`

`Peminjaman` punya empat konstruktor. Yang **utama** (4 parameter: `judul`, `anggota`, `lamaHari`, `tanggal`) sudah lengkap: semua validasi dan pengisian ada di sana. Tiga konstruktor lainnya kalian tulis sebagai **rantai** yang meneruskan ke konstruktor lain lewat `: this(...)` — tanpa menyalin ulang validasi:

| Konstruktor | Meneruskan ke | Nilai bawaan |
|---|---|---|
| `Peminjaman(judul, anggota, lamaHari)` | konstruktor 4-parameter | `tanggal` = hari ini (`DateOnly.FromDateTime(DateTime.Today)`) |
| `Peminjaman(judul, anggota)` | konstruktor 3-parameter | `lamaHari` = 7 |
| `Peminjaman(judul)` | konstruktor 2-parameter | `anggota` = `"Umum"` |

## Level 2 — Urutan Pemanggilan Konstruktor

Di `RantaiKonstruktor.cs`, `Tingkat1` ← `Tingkat2` ← `Tingkat3` adalah tiga tingkat pewarisan. `Jejak.Catat(...)` mencatat kejadian ke sebuah daftar. Setiap kelas harus mencatat **dua** hal:

1. **Inisialisasi field-nya** — tambahkan field `private readonly string _fN = Jejak.Catat("field TingkatN");` (nilai awal field dijalankan sebelum konstruktor).
2. **Badan konstruktornya** — `Jejak.Catat("ctor TingkatN");`

Tambahan: `Tingkat3(string tambahan)` memanggil `Tingkat3()` lewat `: this()` **lalu** mencatat `$"ctor Tingkat3({tambahan})"`.

> **Tebak dulu, jalankan kemudian.** Sebelum menjalankan test, tulis di kertas urutan isi `Jejak` setelah `new Tingkat3()`. Test akan memeriksa urutan yang sebenarnya dari .NET. Yang perlu kalian tulis hanyalah pencatatannya — urutan itu ditentukan oleh bahasa C#, bukan oleh kalian. Nilai tebakan itu ada di pemahaman kalian sendiri: kalau tebakan dan hasil test berbeda, cari tahu kenapa. (Petunjuk: field initializer berjalan **sebelum** konstruktor induk dipanggil.)

## Level 3 — Static Constructor

Di `Konfigurasi.cs`, tulis **static constructor** `static Konfigurasi()` (tanpa modifier akses, tanpa parameter) yang:
- menaikkan `JumlahInisialisasi` satu kali,
- mengisi `DendaPerHari = 1000` dan `MaksPinjam = 3` (properti static get-only hanya boleh diisi dari static constructor).

Static constructor dijalankan otomatis **sekali saja**, sebelum anggota static pertama dipakai. Test mengakses kelas ini berkali-kali lalu memastikan `JumlahInisialisasi` tepat `1`.

## Level 4 — Exception Kustom & Melempar Exception

1. Lengkapi **`BukuTidakTersediaException : Exception`** di `Pengecualian.cs`. Pesannya `"Buku '<judul>' tidak tersedia."`, disalurkan ke konstruktor induk lewat `: base(...)`; simpan `judul` di properti `Judul`. Sediakan dua konstruktor: `(string judul)` dan `(string judul, Exception inner)` — yang kedua menyertakan exception penyebab.
2. Lengkapi `Rak.Ambil(string judul)`:
   - `judul` `null`/kosong/spasi → `ArgumentException`.
   - judul tidak ada di rak **atau** stoknya 0 → `throw new BukuTidakTersediaException(judul)`.
   - selain itu kurangi stok satu.

## Level 5 — `try` / `catch` / `finally`

Lengkapi `LayananPinjam.CobaPinjam(string judul)` (mengembalikan `HasilPinjam(bool Berhasil, string Pesan)`):

- Sukses → `HasilPinjam(true, "Berhasil meminjam: <judul>")`.
- Menangkap **`BukuTidakTersediaException`** → `HasilPinjam(false, ex.Message)`.
- Exception **lain** (mis. `ArgumentException` dari judul kosong) **tidak boleh ditangkap** — biarkan menjalar ke pemanggil.
- Di blok **`finally`**, naikkan `JumlahPercobaan`. Hitungan ini harus **selalu** bertambah tiap `CobaPinjam` dipanggil — sukses, gagal, maupun exception yang menjalar.

## Level 6 — Membungkus Exception (`InnerException`)

Lengkapi `ParserAngka.Parse(string teks)`: ubah teks menjadi `int` dengan `int.Parse`. Kalau melempar `FormatException` atau `OverflowException`, tangkap lalu lempar **`DataTidakValidException`** (sudah tersedia) dengan pesan `"'<teks>' bukan angka yang valid."` dan **sertakan exception aslinya** sebagai `InnerException` (konstruktor kedua) — supaya informasi penyebab tidak hilang.

`teks` `null` bukan "data tidak valid": `ArgumentNullException` dari `int.Parse` dibiarkan menjalar apa adanya (tidak dibungkus).

## Level 7 — `throw;` vs `throw ex;`

Lengkapi `PencatatError.JalankanDenganLog(Action aksi)`: jalankan `aksi()`; kalau melempar exception apa pun, catat `"ERROR: <ex.Message>"` ke log **lalu lempar ulang** — dengan **`throw;`**, bukan `throw ex;`. Bedanya: `throw ex;` menghapus jejak stack asli dan seolah-olah error muncul dari baris `throw` itu; `throw;` mempertahankan jejak lengkap sampai ke method penyebab sebenarnya. Test memeriksa bahwa `Sumber.Gagal` masih muncul di `StackTrace` exception yang tertangkap. Aksi yang sukses tidak mencatat apa-apa.

## Level 8 — `IDisposable` & `using`

`Koneksi` memegang sumber daya yang harus ditutup. Kerjakan di `Koneksi.cs`:

1. Buat `Koneksi` mengimplementasikan **`IDisposable`**.
2. Konstruktor: `Terbuka = true` dan naikkan `JumlahTerbuka` (lewat field `_jumlahTerbuka`) satu.
3. `Query(string sql)`: koneksi sudah ditutup → `ObjectDisposedException`; selain itu kembalikan `"hasil: <sql>"`.
4. `Dispose()`: **idempoten** — kalau sudah ditutup, tidak melakukan apa-apa; selain itu `Terbuka = false` dan turunkan `_jumlahTerbuka`. Memanggil `Dispose()` dua kali tidak boleh menurunkan hitungan dua kali.
5. `Pemakai.Jalankan(string sql, bool sengajaGagal)`: buka `Koneksi` dengan pernyataan **`using`**, jalankan `Query(sql)`. Kalau `sengajaGagal` = `true`, lempar `InvalidOperationException("gagal")` **setelah** koneksi terbuka. `JumlahTerbuka` harus kembali seperti semula di kedua kasus (sukses maupun exception) — itulah gunanya `using`.

## Level 9 — Pola Dispose Lengkap: Finalizer sebagai Jaring Pengaman

`Dispose()` bersifat **deterministik** (dipanggil pemakai), sedangkan **finalizer** (`~NamaKelas()`) dipanggil garbage collector kapan saja setelah objek tak terpakai — hanya untuk jaga-jaga kalau pemakai **lupa** memanggil `Dispose()`. Tulis pola standarnya di `SumberDaya`:

```csharp
public void Dispose()
{
    Dispose(true);
    GC.SuppressFinalize(this);   // sudah dibersihkan -> finalizer tidak perlu jalan
}

protected virtual void Dispose(bool disposing) { ... }

~SumberDaya() { Dispose(false); }
```

Isi `Dispose(bool disposing)`: kalau `_sudahDibersihkan` sudah `true` → `return`. `disposing = true` → `CatatDispose()`; `disposing = false` (dipanggil dari finalizer) → `CatatFinalizer()`. Lalu set `_sudahDibersihkan = true`. Buat `SumberDaya` mengimplementasikan `IDisposable`.

Test memeriksa tiga hal: objek yang di-`Dispose()` dibersihkan lewat jalur Dispose dan finalizernya **tidak** berjalan; `Dispose()` dua kali tidak membersihkan dua kali; objek yang **lupa** di-`Dispose()` dibersihkan oleh finalizer.

## Level 10 — Bonus: Kumpulkan Semua Kesalahan

Lengkapi `ValidatorAnggota` (`ValidasiException` sudah tersedia dan membawa `Daftar` pesan):

- `Periksa(...)` mengumpulkan pesan ke daftar, **tanpa berhenti** di kesalahan pertama, dengan urutan: `nama` `null`/kosong/spasi → `"Nama wajib diisi"`; `nrp` bukan tepat 10 digit angka (termasuk `null`) → `"NRP harus 10 digit"`; `usia` di luar 17..80 → `"Usia harus 17-80"`.
- `Validasi(nama, nrp, usia)`: ada kesalahan → lempar `ValidasiException` berisi **seluruh** daftar; tidak ada → tidak terjadi apa-apa.
- `TryValidasi(nama, nrp, usia, out daftarError)`: versi **tanpa exception** (pola `TryXxx`) — isi `daftarError`, kembalikan `true` bila kosong. Tidak boleh melempar.

Renungkan: kapan exception yang tepat, dan kapan pola `TryXxx`? (Petunjuk: apakah kegagalannya *luar biasa* atau hal yang wajar terjadi?)
