# Pertemuan 6 — Polymorphism, Overriding & Type Casting: Koleksi Perpustakaan

Tugas ini melatih **polimorfisme**: satu pemanggilan (`item.HitungDenda(3)`), banyak perilaku — jenis objek yang **sebenarnya** menentukan method mana yang berjalan. Kalian juga berlatih **overriding** (`virtual`/`override`/`base`), perbedaannya dengan **overloading**, dan **type casting** (`is`, `as`, pattern matching, explicit cast). Studi kasusnya: `Item` perpustakaan dengan tiga jenis turunan — `Buku`, `Majalah`, `Dvd`.

Semua level dicek otomatis lewat `dotnet test`. Nilai mengikuti level tertinggi yang lolos **berurutan** dari 1 — kerjakan sejauh kemampuan, boleh tidak berurutan (jalankan `dotnet test pertemuan-06/tests -v normal` untuk lihat status tiap level apa adanya).

**Struktur folder yang wajib diikuti** (sudah disiapkan di starter, jangan diubah namanya):
```
pertemuan-06/
  SOAL.md
  src/
    Pertemuan06.csproj
    Item.cs           <- edit (Level 1, 2, 4, 5, 10)
    Buku.cs           <- edit (Level 2, 4, 5)
    Majalah.cs        <- edit (Level 3, 4, 5)
    Dvd.cs            <- edit (Level 3, 4, 5)
    Kasir.cs          <- edit (Level 6)
    PemilahItem.cs    <- edit (Level 7, 8)
    Pemroses.cs       <- edit (Level 9)
  tests/
    Pertemuan06.Tests.csproj
    PolimorfismeTests.cs   <- JANGAN diubah, ini test grading dosen
```

Konstruktor semua kelas sudah lengkap. Tiap bagian yang harus kalian kerjakan ditandai `TODO(Level N)`. **Perhatikan:** di starter, method/properti yang harus polimorfik di `Item` masih **tanpa `virtual`**, dan kelas turunan belum punya `override`-nya — menambahkan keduanya adalah bagian dari tugas. Test memakai *reflection* untuk membedakan `override` yang benar dari `new` (menyembunyikan method induk), jadi jalan pintas dengan `new` tidak akan lolos.

---

## Level 1 — Perilaku Umum di Kelas Induk

Lengkapi `Item.HitungDenda(int hariTerlambat)`: denda umum **Rp1.000 per hari** terlambat. `hariTerlambat <= 0` → `0`.
Contoh: `new Item("Umum").HitungDenda(3)` → `3000`.

## Level 2 — `virtual` + `override`: Denda Buku

1. Di `Item`, tandai `HitungDenda` sebagai **`virtual`**.
2. Di `Buku`, tulis **`public override int HitungDenda(int hariTerlambat)`**: **Rp2.000 per hari** (`hariTerlambat <= 0` → `0`).

Buktinya: `Item x = new Buku("Bumi Manusia", "Pramoedya"); x.HitungDenda(3)` → `6000` — variabelnya bertipe `Item`, tetapi yang berjalan versi `Buku`. `Item` biasa tetap `3000`.

## Level 3 — Override di `Majalah` & `Dvd`

Tulis `override HitungDenda` di dua kelas lain:
- `Majalah`: **Rp500** per hari.
- `Dvd`: **Rp5.000** per hari, tetapi **dibatasi maksimum Rp50.000** (jadi 10 hari atau lebih tetap `50000`).

Untuk semuanya `hariTerlambat <= 0` → `0`.

## Level 4 — Properti `virtual`: `MasaPinjamHari`

Properti (bukan cuma method) juga bisa `virtual`/`override`. Tandai `Item.MasaPinjamHari` (nilai dasar 7) sebagai `virtual`, lalu override di:

| Kelas | `MasaPinjamHari` |
|---|---|
| `Item` | 7 |
| `Buku` | 14 |
| `Majalah` | 3 |
| `Dvd` | 2 |

## Level 5 — Memperluas Perilaku Induk dengan `base.`

Lengkapi `Item.Deskripsi()` → `"[<Judul>]"` (mis. `"[Bumi Manusia]"`), tandai `virtual`, lalu override di turunan dengan **memanggil versi induk** lewat `base.Deskripsi()` dan menambahkan detailnya:

| Kelas | `Deskripsi()` |
|---|---|
| `Item` | `[Umum]` |
| `Buku` | `[Bumi Manusia] oleh Pramoedya` |
| `Majalah` | `[Tempo] edisi 12` |
| `Dvd` | `[Laskar Pelangi] (125 menit)` |

Lengkapi juga `Item.ToString()` agar mengembalikan `Deskripsi()`. Karena `Deskripsi()` polimorfik, `ToString()` yang dipanggil lewat variabel bertipe `object` pun menampilkan versi jenis aslinya.

## Level 6 — Koleksi Polimorfik: `Kasir`

Di `Kasir.cs`:
- `int TotalDenda(IEnumerable<Peminjaman> daftar)`: jumlahkan `p.Item.HitungDenda(p.HariTerlambat)` untuk semua peminjaman. `null` → `ArgumentNullException`.
- `Item? ItemDenganDendaTertinggi(IEnumerable<Peminjaman> daftar)`: item dengan denda tertinggi (seri → yang pertama muncul); daftar kosong → `null`; `null` → `ArgumentNullException`.

**Aturan keras:** `Kasir` tidak boleh memeriksa jenis item (tidak ada `is`, `as`, `switch` atas tipe, atau `GetType()`). Kalau kalian terpaksa melakukannya, berarti polimorfisme di Level 2–3 belum benar.

## Level 7 — Type Checking: `is`, `as`, Pattern Matching

Di `PemilahItem`:
- `List<Buku> AmbilBuku(IEnumerable<Item> daftar)`: hanya item bertipe `Buku`, urutan asli dipertahankan. `null` → `ArgumentNullException`.
- `string? PenulisAtauNull(Item item)`: `Penulis` bila item adalah `Buku`, selain itu `null`. Gunakan pattern matching `item is Buku b`.

## Level 8 — Explicit Cast & Pola `TryXxx`

Masih di `PemilahItem`:
- `Buku KeBuku(Item item)`: `null` → `ArgumentNullException`. Selain itu lakukan **explicit cast** `(Buku)item`; kalau item bukan `Buku`, biarkan .NET melempar `InvalidCastException` (jangan ditangkap atau diganti).
- `bool CobaKeBuku(Item? item, out Buku? buku)`: kembalikan `true` dan isi `buku` kalau item adalah `Buku`; selain itu (termasuk `null`) kembalikan `false` dengan `buku = null`. **Tidak boleh melempar exception.**

## Level 9 — Overloading vs Overriding, `switch` Type Pattern

Di `Pemroses`:
- `Proses(Item)` → `"Item: <Judul>"`, `Proses(Buku)` → `"Buku: <Judul>"`, `Proses(Majalah)` → `"Majalah: <Judul>"`.
  Ini **overloading**: overload dipilih *kompilator* dari **tipe variabel**. Jadi `Item x = new Buku(...); Proses(x)` menghasilkan `"Item: ..."` — sedangkan `Proses((Buku)x)` menghasilkan `"Buku: ..."`. Bandingkan dengan overriding di Level 2, yang dipilih *saat program berjalan*.
- `string Kategori(Item item)`: `null` → `ArgumentNullException`; selain itu pakai **switch expression** dengan type pattern: `Buku` → `"Buku"`, `Majalah` → `"Majalah"`, `Dvd` → `"DVD"`, sisanya `"Item"`.

## Level 10 — Bonus: Override `Equals` & `GetHashCode`

`object.Equals` juga `virtual`. Di `Item`, override `Equals(object?)` dan `GetHashCode()`: dua item dianggap **sama** bila **jenisnya sama** (`GetType()`) **dan** `Judul`-nya sama (huruf besar/kecil diabaikan). `Buku("A", "P1")` sama dengan `Buku("a", "P2")`, tetapi tidak sama dengan `Majalah("A", 1)`. `GetHashCode()` harus konsisten dengan `Equals` — buktinya `HashSet<Item>` membuang duplikatnya.
