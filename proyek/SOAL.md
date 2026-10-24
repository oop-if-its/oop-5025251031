<!-- Dibuat otomatis dari ets/proyek/buat-dokumen.py. Isinya sama dengan berkas Word di Classroom. -->

# Requirement ETS Pemrograman Berorientasi Objek: Proyek C# dan Ujian Lisan

## Gambaran umum

ETS PBO terdiri dari dua ujian. **Ujian 1** adalah kuis tertulis online di ITS Classroom pada **Selasa, 20 Oktober 2026** dengan materi pertemuan 4-8 (dijelaskan terpisah). **Ujian 2** adalah proyek C# yang kalian kerjakan **di rumah** secara individu, kemudian **ujian lisan** terjadwal di minggu ke-9 untuk membuktikan proses kerja dan pemahaman atas kode sendiri. AI boleh dipakai saat mengerjakan proyek; yang dinilai bukan apakah kalian memakai AI, tetapi apakah kalian benar-benar memahami dan mengendalikan hasilnya. **70% nilai Ujian 2 berasal dari ujian lisan.**

## Komposisi nilai

| Komponen | Bobot | Keterangan |
|---|---|---|
| Ujian 1: Kuis ITS Classroom | 30% | Materi pertemuan 4-8, dinilai otomatis oleh Classroom |
| Ujian 2: Proyek dan ujian lisan | 70% | Lisan 70% + aplikasi 30% dari nilai Ujian 2 (rincian di bagian Rubrik) |

## Ketentuan umum

- **Pelaksana:** dikerjakan individu, karena ujian lisan menilai pemahaman tiap orang.
- **Waktu pengerjaan:** dari Selasa, 20 Oktober 2026 sampai **Senin, 26 Oktober 2026 pukul 23.59 WIB**, di rumah. Tidak ada perpanjangan; yang dinilai adalah commit terakhir yang sudah di-push ke branch `main` sebelum batas waktu.
- **Ujian lisan:** 27-30 Oktober 2026, sekitar 10 menit per mahasiswa, sesuai jadwal yang disepakati.
- **Tempat kerja:** repo GitHub `oop-NRP` yang sudah kalian punya, **hanya di folder** `proyek/`. Jangan mengubah folder `pertemuan-XX`.
- **Teknologi:** C# dan .NET 8, aplikasi konsol. Boleh memakai pustaka standar .NET; paket NuGet tambahan hanya xUnit (untuk fitur bonus).
- **Kebijakan AI:** AI generatif (ChatGPT, Copilot, Claude, dan sejenisnya) boleh dipakai saat mengerjakan proyek, dengan syarat penggunaannya dicatat di `AI-LOG.md` (lihat Bukti proses). Selama ujian lisan, AI dan internet tidak dipakai.
- **Orisinalitas:** setiap mahasiswa mengerjakan domain yang berbeda menurut NRP-nya (lihat Domain proyek). Kode yang sangat mirip dengan milik mahasiswa lain akan dipanggil untuk klarifikasi; pemeriksaan kemiripan memakai alat bantu dan hasilnya hanya indikasi, bukan vonis.
- **Prinsip penilaian:** kode yang tidak bisa kalian jelaskan dianggap bukan hasil kerja kalian. Fitur yang berjalan tetapi tidak bisa dijelaskan tidak mendapat nilai penuh.

## Jadwal

| Tonggak | Waktu | Yang sudah harus ada di repo |
|---|---|---|
| Rilis soal | Selasa, 20 Oktober 2026 | Ambil kerangka proyek (lihat Ketentuan teknis) |
| Batas akhir | Senin, 26 Oktober 2026 pukul 23.59 WIB | Semua kebutuhan wajib, fitur khas domain, dan dokumen (README, AI-LOG, PROSES) |
| Ujian lisan | 26-30 Oktober 2026 | Jadwal disepakati; sekitar 10 menit per mahasiswa |

## Domain proyek

Domain proyek ditentukan dari NRP, supaya tiap mahasiswa mengerjakan hal yang berbeda: **Domain = NRP (sebagai bilangan) dibagi 6, ambil sisanya.** Contoh: NRP 5025251020 dibagi 6 bersisa 4, jadi domainnya Ekspedisi Paket. Aturan khas domain kalian **wajib** dibuat dan menjadi pembeda proyek.

| Sisa | Domain |
|---|---|
| 0 | Kasir Kantin |
| 1 | Rental Kendaraan |
| 2 | Klinik Mini |
| 3 | Bank Mini |
| 4 | Ekspedisi Paket |
| 5 | Bioskop Mini |

Tulis di README, dengan kata-kata sendiri, domain kalian dan masalah yang diselesaikan aplikasinya (2-3 kalimat). Rancang dulu kelas-kelasnya (di kertas atau diagram), baru mulai menulis kode.

| Domain | Situasi | Aturan khas wajib |
|---|---|---|
| Kasir Kantin | Kantin kampus mencatat pesanan mahasiswa. Harga dan diskon berbeda menurut jenis produk dan jenis pelanggan, dan stok terbatas. | 1. `Produk` abstrak dengan `HitungHarga(int jumlah)` yang berbeda di tiap turunan (minimal Makanan, Minuman, PaketHemat; mis. minuman dingin ada biaya es, paket hemat memotong harga bundel). `Pesanan` menghitung total dengan memanggil method itu lewat referensi `Produk`, tanpa memeriksa tipe.<br>2. Diskon lewat interface `IDiskon` dengan minimal 2 implementasi (mis. diskon mahasiswa, potongan hari Jumat) yang disuntikkan ke `Kasir` lewat konstruktor. Mengganti jenis diskon tidak boleh mengubah kode `Kasir`.<br>3. Stok produk dijaga (tidak boleh negatif). Memesan melebihi stok melempar `StokHabisException`. Struk mencetak rincian per item, diskon, dan total. |
| Rental Kendaraan | Usaha rental menyewakan beberapa jenis kendaraan. Tarif dan aturan berbeda untuk tiap jenis, dan kendaraan yang sedang disewa tidak boleh disewa orang lain. | 1. `Kendaraan` abstrak dengan `HitungBiaya(int hari)` yang berbeda untuk minimal Motor, Mobil, dan Sepeda (mis. mobil ada biaya asuransi, sepeda ada paket mingguan).<br>2. `Penyewaan` menghubungkan `Penyewa` dan `Kendaraan`. Kebijakan denda keterlambatan lewat interface `IKebijakanDenda` dengan minimal 2 implementasi, disuntikkan lewat konstruktor.<br>3. Menyewa kendaraan yang belum dikembalikan melempar `KendaraanTidakTersediaException`. `Kembalikan()` mengubah status kendaraan dan menghitung total biaya termasuk denda. |
| Klinik Mini | Klinik mencatat kunjungan pasien. Biaya jasa tiap tenaga medis dihitung berbeda, dan satu tenaga medis tidak boleh punya dua kunjungan pada jam yang sama. | 1. `TenagaMedis` abstrak dengan `HitungJasa()` yang berbeda untuk minimal Dokter, Perawat, dan Apoteker.<br>2. `Kunjungan` (komposisi: pasien, tenaga medis, daftar tindakan) dan `Resep` sama-sama mengimplementasikan `IBisaDitagih`. Total tagihan dihitung lewat interface itu.<br>3. Jadwal bentrok melempar `JadwalBentrokException`. Daftar tindakan/rekam medis hanya dapat ditambah lewat method; tidak ada `List` publik. |
| Bank Mini | Bank kecil mengelola beberapa jenis rekening dengan aturan tarik dan bunga berbeda, serta transfer antar rekening yang harus aman. | 1. `Rekening` abstrak dengan `Tarik(decimal)` dan `HitungBunga()` yang berbeda untuk minimal Tabungan, Giro (overdraft terbatas), dan Deposito (penalti bila ditarik sebelum jatuh tempo).<br>2. `Nasabah` memiliki banyak rekening. `Transfer` antar rekening bersifat semua-atau-tidak-sama-sekali: bila gagal di tengah, saldo kembali seperti semula (gunakan try/catch).<br>3. `SaldoTidakCukupException` dan `RekeningTidakDitemukanException`. Riwayat transaksi dapat dibaca tetapi tidak dapat diubah dari luar (`IReadOnlyList`). |
| Ekspedisi Paket | Perusahaan ekspedisi mengirim paket reguler, kilat, dan kargo, dan pelanggan dapat melacak statusnya. | 1. `Paket` abstrak dengan `HitungOngkir()` yang berbeda untuk minimal Reguler, Kilat, dan Kargo (berdasarkan berat, jarak, atau asuransi).<br>2. Interface `IBisaDilacak` diimplementasikan oleh `Paket` dan minimal satu kelas lain (mis. `Kontainer`). Riwayat status disimpan dalam objek `RiwayatPelacakan` milik paket (komposisi).<br>3. Status hanya boleh maju berurutan (DiTerima, DiKirim, Sampai) lewat method. Langkah tidak sah melempar `StatusTidakValidException`; pencarian resi yang tidak ada melempar `PaketTidakDitemukanException`. |
| Bioskop Mini | Bioskop menjual tiket untuk beberapa jenis studio dengan harga dan kapasitas berbeda, dan satu kursi tidak boleh terjual dua kali. | 1. `Studio` abstrak dengan `HitungHarga(...)` yang berbeda untuk minimal Reguler, Premiere, dan Imax (mis. kenaikan harga akhir pekan).<br>2. `Pemesanan` memiliki daftar `Kursi`. Pencetakan tiket lewat interface `ICetakTiket` dengan minimal 2 implementasi (mis. teks lengkap dan ringkas).<br>3. Memesan kursi yang sudah terisi melempar `KursiSudahDipesanException`; nomor kursi di luar rentang melempar `KursiTidakValidException`; kapasitas studio tidak boleh dilampaui. |

## Kebutuhan wajib

K1-K6 adalah batas minimum untuk **semua** domain. Setiap kebutuhan sengaja dipetakan ke materi pertemuan 4-8.

| ID | Kebutuhan | Detail |
|---|---|---|
| K1 | Enkapsulasi (P4) | Semua field `private`. Data dibuka lewat properti atau method yang memvalidasi: konstruktor atau setter melempar `ArgumentException` (atau turunannya) untuk data tidak sah. Minimal satu `private set` atau `init`. Koleksi internal tidak dibuka sebagai `List` publik (pakai `IReadOnlyList<T>` atau `IEnumerable<T>`). |
| K2 | Komposisi dan pewarisan (P5) | Minimal satu hierarki: induk dengan minimal 2 turunan, memakai `: base(...)`. Minimal dua hubungan komposisi (has-a), misalnya pesanan memiliki daftar item. Pewarisan hanya untuk hubungan is-a. |
| K3 | Polimorfisme (P6) | Minimal dua method `virtual` atau `abstract` yang di-`override` turunan dan dipanggil lewat referensi atau koleksi bertipe induk. `ToString()` di-override. Tidak ada rantai `if (x is A) ... else if (x is B)` sebagai pengganti polimorfisme; `is`, `as`, atau pattern matching dipakai hanya bila beralasan dan bisa kalian jelaskan. |
| K4 | Abstract class dan interface (P7) | Minimal satu `abstract class` yang memuat anggota abstrak sekaligus method konkret. Minimal satu interface dengan minimal 2 implementasi. Ketergantungan pada interface disuntikkan lewat konstruktor (DI) pada minimal satu kelas layanan. |
| K5 | Exception dan life cycle (P8) | Minimal dua exception kustom (turunan `Exception`, dengan pesan yang jelas). `try/catch/finally` di tempat yang bermakna (bukan `catch` kosong). `throw;` bila perlu melempar ulang. Minimal satu kelas `IDisposable` yang dipakai dengan `using`. Minimal satu konstruktor berantai `: this(...)`. |
| K6 | Aplikasi berjalan | `dotnet build proyek/src` tanpa error. `dotnet run --project proyek/src` menjalankan demo (menu atau skenario berurutan) yang memperlihatkan semua aturan khas domain, termasuk skenario gagal yang ditangani exception: program tidak crash. |

## Fitur khas dan fitur bonus

Aturan khas domain (tabel Domain proyek) wajib dan berbobot besar. Aturan itu harus memakai kelas-kelas yang sama dengan K1-K6, bukan program terpisah yang berdiri sendiri. Setelah K1-K6 dan aturan khas selesai, kerjakan bonus sebanyak yang sempat. Tiap bonus yang berfungsi menambah nilai.

- **Unit test xUnit:** minimal 5 test pada `proyek/tests`, dan `dotnet test` lulus.
- **Penyimpanan data:** simpan dan muat data ke berkas JSON (`System.Text.Json`), dengan penanganan exception untuk berkas yang tidak ada atau formatnya rusak.
- **Pengurutan dan kesetaraan:** `IComparable<T>` atau `IEquatable<T>` pada satu kelas, dan benar-benar dipakai (mis. mengurutkan).
- **Pola desain:** Template Method atau Strategy yang diterapkan dengan sengaja dan dijelaskan di README.
- **Diagram kelas:** diagram (Mermaid) di README yang cocok dengan kode sebenarnya.
- **Menu yang tahan salah input:** input kosong atau bukan angka tidak membuat program berhenti.

## Ketentuan teknis

- Struktur folder: `proyek/src/` berisi `.csproj` dan semua kode, `proyek/tests/` (opsional, untuk bonus), serta `proyek/README.md`, `proyek/AI-LOG.md`, dan `proyek/PROSES.md`.
- Ambil kerangka proyek dari template:

```
git fetch https://github.com/oop-if-its/oop-template.git main
git checkout FETCH_HEAD -- proyek
```

- Nama kelas dan anggota boleh bahasa Indonesia atau Inggris, tetapi konsisten.
- Tidak ada field `public`. Tidak ada `catch` kosong yang menelan exception.
- Satu kelas atau tipe per berkas bila wajar, supaya mudah ditelusuri saat ujian lisan.
- Folder `bin/` dan `obj/` tidak ikut ter-commit (sudah diatur di `.gitignore`).
- Commit sering: minimal **8 commit**, tersebar di minimal **3 hari kalender berbeda**, dengan pesan yang menjelaskan isinya, misalnya "K1: Produk abstrak dan validasi harga". Harus ada commit berisi kode C# sebelum Checkpoint 1 dan Checkpoint 2. Satu commit besar di akhir dianggap tidak memenuhi.

## Bukti proses

Tiga bukti berikut menjadi bahan utama ujian lisan. Penguji akan membukanya bersama kalian dan bertanya tentang isinya.

| Bukti | Isi minimum |
|---|---|
| Riwayat commit | Minimal 8 commit di minimal 3 hari berbeda, ada commit kode sebelum tiap checkpoint, pesan commit bermakna. |
| AI-LOG.md | Untuk setiap penggunaan AI yang menghasilkan kode: prompt yang dikirim (ringkas), bagian kode yang dipakai, dan apa yang kalian ubah atau perbaiki dari hasil AI. Boleh menautkan percakapan AI. Tulis "tidak memakai AI" jika memang tidak. |
| PROSES.md | Domain dan masalahnya dengan kata-kata sendiri, rancangan kelas, satu keputusan desain beserta alasannya, dan minimal dua bug atau kendala yang ditemui beserta cara mengatasinya. |

Contoh satu entri `AI-LOG.md`:

```
## Denda keterlambatan (aturan khas)
- Prompt: "buat interface kebijakan denda dan dua implementasinya untuk rental kendaraan"
- Dipakai: `IKebijakanDenda`, `DendaFlat` di Denda.cs
- Diubah: hasil AI menaruh `new DendaFlat()` langsung di `Penyewaan`; saya pindah ke parameter konstruktor (DI)
  supaya kebijakan bisa diganti tanpa mengubah `Penyewaan`
```

Log yang jujur tidak mengurangi nilai, sebanyak apa pun AI dipakai. Yang mengurangi nilai adalah log yang tidak sesuai dengan kode, atau isi log yang tidak bisa kalian jelaskan.

## Ujian lisan

Ujian berlangsung sekitar 10 menit sesuai jadwal masing-masing, dalam empat bagian. Bahannya adalah kode dan bukti proses dari commit terakhir sebelum batas waktu, jadi pertanyaannya berbeda untuk tiap mahasiswa dan tidak ada kunci jawaban yang bisa dihafal. Bawa laptop dengan repo tersebut dan pastikan `dotnet` serta editor kalian siap dipakai.

| Bagian | Waktu | Yang dilakukan | Contoh |
|---|---|---|---|
| 1. Ceritakan proses | ±3 menit | Menjelaskan riwayat commit, AI-LOG.md, dan PROSES.md | "Apa yang kamu kerjakan antara Checkpoint 1 dan 2?" "Untuk entri AI-LOG ini, kenapa hasil AI-nya kamu ubah?" "Ceritakan satu bug di PROSES.md dan bagaimana kamu menemukannya." |
| 2. Telusuri | ±2 menit | Menunjukkan dan menjelaskan alur kode dari satu aksi sampai hasil tercetak | "Saat method ini dipanggil dari Program, method apa saja yang berjalan sampai hasilnya tercetak? Mana yang dipilih saat runtime, dan mengapa?" "Di mana aturan data tidak boleh negatif dijaga?" |
| 3. Prediksi | ±2 menit | Penguji menunjuk atau mengubah satu bagian kode; kalian menebak akibatnya sebelum dicoba | "Apa yang terjadi jika kata `override` di baris ini dihapus?" "Jika `private set` ini menjadi `public set`, aturan apa yang bisa dilanggar?" "Jika blok `catch` ini dipindah ke atas?" "Jika `using` dihapus, kapan `Dispose()` dipanggil?" |
| 4. Ubah langsung | ±3 menit | Melakukan satu perubahan kecil tanpa AI dan tanpa internet | Tambah satu kelas turunan baru dengan aturan tarif sendiri tanpa mengubah kelas lain; tambah implementasi interface baru dan pasang lewat konstruktor; tambah exception kustom dan tangani; ubah satu aturan hitung |

Konsep yang pasti ditanyakan: enkapsulasi (`private set`, validasi, koleksi hanya-baca), pewarisan dan `: base(...)`, `virtual`/`override`/`abstract`, interface dan DI, serta exception (kustom, `try/catch/finally`, `throw;`) dan konstruktor/`Dispose`.

## Pengumpulan

Tidak ada pengumpulan tautan: **push** ke repo `oop-NRP` (branch `main`) **paling lambat Minggu, 25 Oktober 2026 pukul 23.59 WIB**. Commit terakhir sebelum batas itulah yang dinilai. Sistem pemantauan membaca langsung dari repo.

| Item | Isi |
|---|---|
| Folder proyek/src | Kode C# yang dapat dibangun dengan `dotnet build proyek/src` |
| proyek/README.md | Nama dan NRP, domain, cara menjalankan, diagram kelas, daftar kebutuhan yang selesai (K1-K6, aturan khas, bonus), dan yang belum selesai |
| proyek/AI-LOG.md dan proyek/PROSES.md | Sesuai ketentuan di bagian Bukti proses |

## Rubrik penilaian

Nilai Ujian 2: **ujian lisan 70%** dan **aplikasi 30%**. Aplikasi yang lengkap tetapi tidak dipahami tidak akan mendapat nilai bagus.

| Komponen | Bobot | Dinilai dari |
|---|---|---|
| Lisan: Ceritakan proses | 15% | Kesesuaian penjelasan dengan riwayat commit, AI-LOG.md, dan PROSES.md |
| Lisan: Telusuri | 20% | Kemampuan menjelaskan alur kode, termasuk pemilihan method saat runtime |
| Lisan: Prediksi | 15% | Ketepatan menebak akibat perubahan kode beserta alasannya |
| Lisan: Ubah langsung | 20% | Menyelesaikan perubahan kecil tanpa AI dan tanpa internet |
| Aplikasi: Kebutuhan wajib K1-K6 | 12% | Keenam kebutuhan terpenuhi dan berjalan tanpa error |
| Aplikasi: Aturan khas domain | 10% | Semua aturan di tabel domain berjalan memakai kelas-kelas yang sama |
| Aplikasi: Bonus | 3% | Minimal 3 bonus berfungsi (proporsional jika kurang) |
| Aplikasi: Bukti proses dan pengumpulan | 5% | Riwayat commit memenuhi ketentuan, AI-LOG.md dan PROSES.md terisi, README lengkap |

Setiap bagian lisan diberi skor 1-4 dengan acuan berikut, supaya semua penguji menilai dengan standar yang sama.

| Skor | Ceritakan proses | Telusuri | Prediksi | Ubah langsung |
|---|---|---|---|---|
| 4 | Penjelasan runtut dan cocok dengan commit serta log; bisa menyebut alasan tiap perubahan dari hasil AI | Menjelaskan alur lengkap tanpa ragu, menyebut method yang terlibat, dan alasan pemilihan method saat runtime | Tebakan benar dan alasannya tepat secara konsep | Selesai dan benar tanpa petunjuk |
| 3 | Cocok dengan bukti, tetapi alasan sebagian keputusan kurang jelas | Alur benar tetapi ada langkah terlewat atau perlu sedikit pancingan | Tebakan benar, alasannya kurang tepat | Selesai dengan satu petunjuk |
| 2 | Hanya membacakan ulang isi log tanpa menjelaskan alasannya | Bisa menunjuk lokasi kode, tetapi tidak bisa menjelaskan alurnya | Tebakan salah, tetapi bisa menjelaskan setelah dicoba | Tahu bagian yang harus diubah, tetapi tidak selesai |
| 1 | Penjelasan tidak sesuai dengan bukti, atau bukti tidak ada | Tidak bisa menemukan bagian kode yang ditanyakan | Tidak bisa menebak maupun menjelaskan | Tidak tahu harus mengubah di mana |

Mahasiswa yang tidak hadir pada jadwal ujian lisan tanpa alasan sah mendapat skor 0 untuk seluruh komponen lisan. Alasan sah (mis. sakit) disampaikan sebelum jadwal, dan jadwal pengganti ditentukan dosen.

## FAQ

**Boleh berlatih membuat aplikasi serupa sebelum mulai?**

Boleh dan dianjurkan. Yang tidak boleh adalah membawa kode jadi; kode proyek harus dibuat dan di-commit selama masa pengerjaan.

**Bagaimana jika ada kebutuhan yang belum selesai saat batas waktu?**

Tulis di README bagian mana yang belum selesai. Bagian yang berjalan sebagian tetap dinilai sesuai yang berfungsi.

**Boleh memakai AI atau mencari contoh kode di internet?**

Boleh saat mengerjakan di rumah, asalkan dicatat di AI-LOG.md. Pakai AI seperti tutor: minta penjelasan, bukan hanya kode jadi, karena semua kode itu akan kalian pertanggungjawabkan saat ujian lisan.

**Boleh memperbaiki proyek setelah batas waktu, sebelum ujian lisan?**

Tidak. Penilaian memakai commit terakhir sebelum batas waktu, dan commit sesudahnya tidak dinilai. Kalian sangat dianjurkan mempelajari ulang kode sendiri sebelum jadwal ujian.

**Domain saya terasa mudah/sulit dibanding teman. Boleh ganti?**

Tidak. Domain ditentukan dari NRP. Standar penilaiannya sama untuk semua domain: yang dinilai adalah pemahaman konsep, bukan banyaknya fitur.

**Repo saya private. Apakah bisa dinilai?**

Bisa. Repo `oop-NRP` berada di organisasi kelas dan dapat dibaca dosen. Jangan memindahkan atau mengganti nama repo.

**Apa yang terjadi jika saya melanggar kebijakan AI atau menyalin proyek teman?**

Dipanggil untuk klarifikasi. Bila terbukti kode bukan hasil kerja sendiri, nilai komponen terkait dapat dinolkan dan kasusnya diproses sesuai aturan akademik.
