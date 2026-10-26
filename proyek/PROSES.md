# PROSES

Berkas ini menjadi bahan utama ujian lisan. Tulis dengan kata-kata sendiri.

## 1. Domain dan masalahnya

Domain yang saya dapatkan untuk aplikasi ini adalah mini bank. Aplikasi ini diharapkan dapat melakukan pengelolaan data nasabah, rekening simpanan, serta pemrosesan transaksi keuangan bulanan. Beberapa masalah yang menjadi latar belakang pembuatan aplikasi ini antara lain : 

- Terdapat beberapa jenis rekening yang ada di bank ini yakni tabungan, giro dan deposito. Dimana masing masing memiliki aturan yang berbeda. Aplikasi ini menyelesaikan kebutuhan struktur objek yang rapi agar tidak saling bertabrakan logika saldo dasarnya.

- Jika proses transfer antar rekening gagal di tengah jalan, dapat menyebabkan uang pengirim terpotong tetapi tidak terkirim ke penerima. Aplikasi ini menyelesaikan masalah tersebut dengan mekanisme transaksi All-or-Nothing dan rollback otomatis.  

- Memisahkan perhitungan suku bunga promo dari sistem utama agar perubahan skema promo tidak perlu merombak struktur kode rekening.

## 2. Rancangan kelas

Kelas apa saja, siapa mewarisi siapa, siapa memiliki siapa, dan mengapa dirancang begitu.

### Rekening (Abstract Class):
Bertindak sebagai base class. Kelas ini menyimpan atribut dasar yang pasti dimiliki semua rekening (NomorRekening, Pemilik, Saldo, dan RiwayatTransaksi), serta mendeklarasikan method abstrak HitungBunga(), Setor(), dan Tarik().

### Tabungan, Giro, dan Deposito (Anak/IS-A dari class Rekening)
Ketiga kelas ini mewarisi sifat dasar Rekening dan melakukan override pada method Tarik() serta HitungBunga() sesuai karakteristik masing-masing. Giro menambahkan fitur batas Overdraft, sedangkan Deposito menambahkan tanggal jatuh tempo.

### Nasabah (Komposisi/HAS-A dari class Rekening):
Kelas Nasabah mengomposisi koleksi List<Rekening>. Konsep ini dipilih agar identitas pemilik terpisah dari logika saldo, sehingga satu nasabah bisa memiliki banyak jenis rekening sekaligus.

### Transaksi:
Objek model yang mencatat stempel waktu, jenis, nominal, dan saldo akhir setiap kali ada mutasi di rekening.

### BankService:
Pusat layanan transaksi yang memproses pendaftaran rekening, pencarian data, transfer antar-rekening, dan penambahan bunga bulanan.

### IKebijakanBunga, BungaStandar, BungaPromo:
Interface dan implementasi strategi untuk menghitung bunga bulanan.

### SesiBankLogger:
Utilitas pemantau aktivitas operator yang mengimplementasikan interface IDisposable untuk pembersihan resource sesi.

## 3. Satu keputusan desain dan alasannya

Memilih Strategy Pattern (IKebijakanBunga) dan Dependency Injection pada BankService dibanding menggunakan struktur if-else biasa.

Suku bunga dan skema promo perbankan bersifat dinamis dan sering berubah. Jika perhitungan bunga ditulis menggunakan if-else di dalam BankService atau Rekening, setiap kali ada program promo baru kita wajib merombak kode yang sudah ada.

Dengan Strategy Pattern, kita cukup membuat kelas baru yang mengimplementasikan interface IKebijakanBunga, lalu menyuntikkannya ke dalam BankService. Hasilnya, logika kalkulasi bunga menjadi sangat fleksibel dan mudah diuji secara terpisah tanpa risiko merusak kode utama.

## 4. Bug atau kendala (minimal dua)

### Kendala 1: Saldo Pengirim Terpotong Saat Transfer Gagal di Tengah Jalan

Apa yang terjadi: Saat mengeksekusi metode Transfer(), saldo rekening pengirim sudah berkurang via Tarik(), namun proses Setor() pada rekening penerima mengalami kegagalan (exception). Akibatnya, saldo pengirim terlanjur terpotong tetapi tidak pernah sampai ke penerima.

Bagaimana menemukan penyebabnya: Penelusuran alur eksekusi (debugging) dilakukan pada metode Transfer() di BankService. Ditemukan bahwa pemanggilan pengirim.Tarik() dan penerima.Setor() berjalan secara berurutan tanpa adanya mekanisme pemulihan kondisi saldo jika tahap kedua melempar exception.

Bagaimana memperbaikinya: Membungkus pemanggilan penerima.Setor() di dalam blok try-catch. Jika Setor() gagal, blok catch secara otomatis mengeksekusi pengirim.Setor(jumlah) untuk mengembalikan saldo pengirim ke posisi semula (rollback) sebelum melempar kembali exception tersebut dengan instruksi throw.    
