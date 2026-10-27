# Pertemuan 7 — Interface & Abstract Class: Koleksi Perpustakaan yang Terkontrak

Tugas ini melatih dua alat abstraksi yang paling penting di C#: **kelas abstrak** (`abstract class` — kerangka bersama yang belum lengkap) dan **interface** (kontrak: *apa* yang bisa dilakukan, tanpa *bagaimana*). Kalian juga berlatih mengimplementasikan **beberapa interface sekaligus**, **implementasi eksplisit**, **default interface method**, **Dependency Injection**, dan **Template Method**. Studi kasusnya kelanjutan `Item` perpustakaan dari pertemuan 6.

Semua level dicek otomatis lewat `dotnet test`. Nilai mengikuti level tertinggi yang lolos **berurutan** dari 1 — kerjakan sejauh kemampuan, boleh tidak berurutan (jalankan `dotnet test pertemuan-07/tests -v normal` untuk lihat status tiap level apa adanya).

**Struktur folder yang wajib diikuti** (sudah disiapkan di starter, jangan diubah namanya):
```
pertemuan-07/
  SOAL.md
  src/
    Pertemuan07.csproj
    Interfaces.cs       <- edit hanya CetakDenganHeader (Level 8); sisanya SUDAH LENGKAP
    StatusPinjam.cs     <- SUDAH LENGKAP, jangan diubah
    BukuReferensi.cs    <- SUDAH LENGKAP, jangan diubah
    Item.cs             <- edit (Level 1, 2, 9, 10)
    Buku.cs             <- edit (Level 1, 2, 3, 4, 8)
    Majalah.cs          <- edit (Level 2, 3, 8)
    Dvd.cs              <- edit (Level 2, 3, 10)
    Notifikasi.cs       <- edit (Level 7)
    Layanan.cs          <- edit (Level 5, 6)
  tests/
    Pertemuan07.Tests.csproj
    InterfaceAbstrakTests.cs   <- JANGAN diubah, ini test grading dosen
```

Bagian yang harus kalian kerjakan ditandai `TODO(Level N)`. Di starter, `Item` masih kelas biasa dengan method `virtual` yang melempar `NotImplementedException`, dan kelas turunan belum mengimplementasikan interface apa pun — mengubahnya menjadi `abstract` dan menambahkan interface di deklarasi kelas adalah bagian dari tugas.

---

## Level 1 — Kelas Abstrak & Anggota Abstrak

1. Jadikan `Item` sebuah **`abstract class`** — tidak ada "item umum" yang masuk akal dibuat langsung (`new Item(...)` tidak boleh lagi bisa dikompilasi).
2. Jadikan `Item.HitungDenda(int hariTerlambat)` **`abstract`** (tanpa badan, hanya titik koma). Setiap jenis item **wajib** menentukan dendanya sendiri.
3. Di `Buku`, tulis `public override int HitungDenda(int hariTerlambat)`: **Rp2.000 per hari** (`hariTerlambat <= 0` → `0`).

## Level 2 — Properti Abstrak & Method Konkret di Kelas Abstrak

1. Jadikan `Item.MasaPinjamHari` sebagai **properti abstrak** (`public abstract int MasaPinjamHari { get; }`).
2. Override `MasaPinjamHari` dan `HitungDenda` di tiga kelas turunan:

| Kelas | `MasaPinjamHari` | `HitungDenda` per hari |
|---|---|---|
| `Buku` | 14 | Rp2.000 |
| `Majalah` | 3 | Rp500 |
| `Dvd` | 2 | Rp5.000, maksimum Rp50.000 |

3. Lengkapi method **konkret** `Item.HitungTanggalKembali(DateOnly tanggalPinjam)` → `tanggalPinjam` ditambah `MasaPinjamHari` hari. Kelas abstrak boleh punya logika lengkap yang memakai anggota abstraknya — `Item` tidak perlu tahu jenis konkretnya.

## Level 3 — Interface `IDapatDipinjam`

`IDapatDipinjam` (di `Interfaces.cs`, sudah lengkap) berisi `bool Tersedia { get; }`, `void Pinjam()`, dan `void Kembalikan()`. Implementasikan di **`Buku`, `Majalah`, dan `Dvd`** (tambahkan di deklarasi kelas: `class Buku : Item, IDapatDipinjam`).

Kelas `StatusPinjam` sudah disediakan — pakai lewat **komposisi**: `private readonly StatusPinjam _status = new();`, lalu `Tersedia => !_status.Dipinjam`, `Pinjam() => _status.Tandai()`, `Kembalikan() => _status.Lepas()`. Meminjam yang sudah dipinjam atau mengembalikan yang tidak sedang dipinjam melempar `InvalidOperationException` (sudah ditangani `StatusPinjam`). Status tiap objek berdiri sendiri.

## Level 4 — Interface Kedua: `IDapatDiperpanjang`

Sebuah kelas boleh mengimplementasikan **lebih dari satu** interface. Buat **hanya `Buku`** mengimplementasikan juga `IDapatDiperpanjang` (`int JumlahPerpanjangan { get; }` dan `bool Perpanjang()`); `Majalah` dan `Dvd` **tidak**.

- `JumlahPerpanjangan` mulai dari 0 (setter `private`).
- `Perpanjang()`: buku tidak sedang dipinjam → `InvalidOperationException`; `JumlahPerpanjangan` sudah 2 → kembalikan `false` (tidak berubah); selain itu naikkan satu dan kembalikan `true`.
- `Kembalikan()` (Level 3) juga me-reset `JumlahPerpanjangan` ke 0.

## Level 5 — Bekerja Lewat Interface

Di `Layanan.cs`, kelas static `Katalog` bekerja dengan **kontrak**, bukan kelas konkret:

- `int PinjamSemua(IEnumerable<IDapatDipinjam> daftar)`: panggil `Pinjam()` pada tiap elemen; elemen yang melempar `InvalidOperationException` **dilewati** tanpa menghentikan yang lain. Kembalikan jumlah yang berhasil. `null` → `ArgumentNullException`.
- `List<IDapatDipinjam> HanyaBisaDipinjam(IEnumerable<Item> daftar)`: dari campuran `Item`, ambil hanya yang mengimplementasikan `IDapatDipinjam`, urutan asli dipertahankan. `BukuReferensi` (sengaja tidak mengimplementasikannya) tidak ikut. `null` → `ArgumentNullException`.

## Level 6 — Dependency Injection

`LayananPeminjaman` **tidak** membuat pemberitahuannya sendiri — ia menerimanya dari luar lewat konstruktor sebagai `INotifikasi`. Lengkapi:

- Konstruktor: `null` → `ArgumentNullException`; selain itu simpan ke field `private readonly`.
- `Pinjamkan(Item item, string namaAnggota)`: `item` `null` → `ArgumentNullException`; item **bukan** `IDapatDipinjam` → `InvalidOperationException`; selain itu panggil `Pinjam()` (kalau melempar, biarkan menjalar dan **jangan** kirim notifikasi), lalu kirim `"<namaAnggota> meminjam <Judul>"` lewat `INotifikasi`.

Test memakai pengganti buatan (*test double*) yang hanya mencatat pesan — bukti bahwa layanan tidak bergantung pada email/SMS sungguhan.

## Level 7 — Dua Implementasi untuk Satu Interface

Di `Notifikasi.cs`, buat `NotifikasiEmail` dan `NotifikasiSms` mengimplementasikan `INotifikasi` (tambahkan `: INotifikasi` dan tulis `Kirim`). Properti `Terkirim` sudah disediakan.

- Keduanya: `pesan` `null`/kosong/spasi → `ArgumentException`.
- `NotifikasiEmail.Kirim`: tambahkan `"EMAIL: <pesan>"` ke `Terkirim`.
- `NotifikasiSms.Kirim`: SMS dibatasi **40 karakter** — pesan lebih panjang **dipotong** jadi 40 karakter pertama; tambahkan `"SMS: <pesan>"`.

`LayananPeminjaman` yang sama harus bekerja dengan keduanya tanpa diubah sedikit pun.

## Level 8 — Default Interface Method & Implementasi Eksplisit

`IDapatDicetak` punya satu method wajib `Cetak()` dan satu **default method** `CetakDenganHeader()`.

1. Lengkapi `CetakDenganHeader()` di `Interfaces.cs` → `"=== <Cetak()> ==="`.
2. `Buku` mengimplementasikan `IDapatDicetak` secara **implisit** (`public string Cetak()`) → `"Buku: <Judul> oleh <Penulis>"`.
3. `Majalah` mengimplementasikannya secara **eksplisit** (`string IDapatDicetak.Cetak()`) → `"Majalah: <Judul> edisi <Edisi>"`. Method ini **tidak muncul** pada tipe `Majalah` — hanya lewat variabel bertipe `IDapatDicetak`.
4. `Dvd` tidak mengimplementasikan `IDapatDicetak`.

`CetakDenganHeader()` juga hanya bisa dipanggil lewat variabel bertipe interface, bukan lewat `Buku`.

## Level 9 — `IComparable<Item>`

Buat `Item` mengimplementasikan `IComparable<Item>`: `CompareTo(Item? other)` → `null` dianggap lebih kecil (kembalikan 1); selain itu bandingkan `Judul` dengan `string.Compare(..., StringComparison.OrdinalIgnoreCase)`. Hasilnya, `Array.Sort(...)` atas campuran `Buku`/`Majalah`/`Dvd` mengurutkan berdasarkan judul, tanpa peduli jenis.

## Level 10 — Bonus: Template Method

Lengkapi `Item.BuatStruk(int hariTerlambat)` — **bukan** `virtual`, karena urutan langkahnya dikunci di kelas dasar; bagian yang bervariasi diserahkan ke `HitungDenda()` (abstrak) dan hook `Catatan()`. `hariTerlambat < 0` dianggap 0. Hasilnya teks dipisah `"\n"` (tanpa `"\n"` di akhir):

```
STRUK PENGEMBALIAN
Judul: <Judul>
Terlambat: <hari> hari
Denda: Rp<HitungDenda(hari)>
```

(angka polos tanpa pemisah ribuan, mis. `Rp6000`). Kalau `Catatan()` **tidak kosong**, tambahkan baris kelima `Catatan: <Catatan()>`. Hook `Catatan()` bawaannya `""`; di `Dvd`, override `protected override string Catatan()` → `"Periksa kondisi disk"`.
