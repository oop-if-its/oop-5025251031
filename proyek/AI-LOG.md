# AI-LOG

Catat setiap penggunaan AI yang menghasilkan kode. Satu entri per penggunaan. Tulis "tidak memakai AI" jika memang tidak.
Log yang jujur tidak mengurangi nilai; log yang tidak sesuai dengan kode, atau isi log yang tidak bisa dijelaskan, yang mengurangi nilai.

## Entri saya

### Pembuatan Test Case
- Prompt: "Saya sudah membuat kelas model (Rekening, Tabungan, Giro, Deposito, Nasabah) dan BankService. Buatkan alur pengujian di Program.cs yang mengeksekusi skenario demo untuk kriteria K1–K6 secara berurutan."
- Dipakai: Struktur alur blok Main() di Program.cs, pemanggilan BankService.Transfer(), serta blok try-catch untuk menangkap SaldoTidakCukupException dan RekeningTidakDitemukanException.
- Diubah: Menyesuaikan nilai nominal transaksi (500_000m, 150_000m), mengganti nama teller pada SesiBankLogger, dan menambahkan keterangan pencetakan log terminal agar lebih mudah dibaca.