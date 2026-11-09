# Tutorial: Catch the Stars dengan Windows Form di .NET 10

## Tujuan
Mempelajari cara membuat game 2D sederhana menggunakan Windows Form tanpa designer. Game ini memungkinkan pemain menangkap bintang yang jatuh menggunakan keranjang yang dikendalikan dengan tombol panah.

## Sub Capaian Pembelajaran
Mahasiswa mampu mengimplementasikan:
- Event handler untuk input keyboard.
- Logika pergerakan objek menggunakan timer.
- Deteksi tumbukan antara dua objek.

## Lingkungan Pengembangan

1. Platform: .NET 10
2. Bahasa: C# 14
3. IDE: Visual Studio 2026 atau VSCode

## Cara membuka project menggunakan Visual Studio
1. Clone repositori project `oop-catch-the-stars` ke direktori lokal git Anda.
2. Buka Visual Studio, pilih menu **File > Open > Project/Solution** > Pilih file *.sln.
3. Tekan tombol **Open** untuk membuka solusi.
4. Baca tutorial dengan seksama dan buat implementasi kode sesuai dengan petunjuk.

## Struktur Proyek

Proyek ini terdiri dari empat kelas utama:
- **Sky**: Kelas form utama yang mengoordinasikan objek `Basket`, `Star`, dan `ScoreLabel`.
- **Basket**: Kelas untuk keranjang yang bisa digerakkan oleh pemain untuk menangkap bintang.
- **Star**: Kelas untuk bintang yang jatuh.
- **Program**: Kelas entry point untuk menjalankan aplikasi.

### 1. Sky (Form Utama)
**Atribut Utama**:
- `_basket`: Menyimpan objek `Basket`.
- `_star`: Menyimpan objek `Star`.
- `_score`: Menyimpan skor permainan.

**Perilaku Utama**:
- **GameLoop**: Mengatur logika permainan, termasuk gerakan `Star`, input keyboard, dan deteksi tabrakan.
- **GenerateStar**: Membuat objek `Star` baru di layar.

### 2. Basket
**Atribut Utama**:
- `_basketPictureBox`: Visualisasi `Basket` pada layar.
- `_speed`: Kecepatan gerak `Basket`.

**Perilaku Utama**:
- **MoveLeft** dan **MoveRight**: Menggerakkan `Basket` ke kiri atau kanan.
- **IsCollidedWith**: Mengecek tabrakan dengan `Star`.

### 3. Star
**Atribut Utama**:
- `_starPictureBox`: Visualisasi `Star` pada layar.
- `_fallSpeed`: Kecepatan jatuh `Star`.

**Perilaku Utama**:
- **Fall**: Membuat `Star` jatuh ke bawah.
- **IsOutOfSky**: Mengecek jika `Star` sudah melewati layar.

### 4. Program
**Perilaku Utama**:
- **Main**: Entry point untuk menjalankan aplikasi dan membuka form `Sky`.

## Langkah-langkah

### 0. Membuat Proyek dan Solusi (Opsional)
1. Buka Visual Studio dan buat proyek **Windows Forms App (.NET)** baru dengan nama `CatchTheStars`.
2. Pilih target framework **.NET 10**.
3. Klik `Create`.

### 1. Membuat Kelas Sky
Buat kelas `Sky` yang akan berfungsi sebagai form utama dan menyiapkan tampilan awal dengan label skor serta tempat bagi `Basket` dan `Star`.

1. Buat file baru bernama `Sky.cs` dan tambahkan kode berikut:

   ```csharp
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   namespace CatchTheStars;

   public class Sky : Form
   {
       private Label _scoreLabel;

       public Sky()
       {
           // Mengatur properti form
           this.Text = "Catch the Falling Stars";
           this.Size = new Size(800, 600);
           this.StartPosition = FormStartPosition.CenterScreen;

           // Inisialisasi label skor
           InitializeGame();
       }

       private void InitializeGame()
       {
           // Membuat dan menambahkan label skor
           _scoreLabel = new Label
           {
               Text = "Score: 0",
               Location = new Point(10, 10),
               Font = new Font("Arial", 16),
               ForeColor = Color.Black
           };
           this.Controls.Add(_scoreLabel);
       }
   }
   ```

2. **Jalankan Aplikasi**:
   Buat kelas `Program.cs` untuk menjalankan aplikasi dan memastikan form `Sky` tampil dengan label skor.

   ```csharp
   namespace CatchTheStars;

   internal static class Program
   {
       [STAThread]
       static void Main()
       {
           Application.EnableVisualStyles();
           Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new Sky());
       }
   }
   ```

3. Jalankan aplikasi untuk melihat form dengan label skor di bagian atas.

### 2. Menampilkan Basket
Tambahkan kelas `Basket` yang akan menampilkan `PictureBox` bergambar keranjang di layar.

1. Buat file baru bernama `Basket.cs` dan tambahkan kode berikut:

   ```csharp
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   namespace CatchTheStars;

   public class Basket
   {
       private PictureBox _basketPictureBox;
       private int _speed;

       public Basket(Size skySize, int speed)
       {
           _speed = speed;
           _basketPictureBox = new PictureBox
           {
               Size = new Size(120, 80),
               Location = new Point(skySize.Width / 2 - 60, skySize.Height - 80),
               SizeMode = PictureBoxSizeMode.StretchImage
           };

           try
           {
               _basketPictureBox.Image = Resource.basket;
           }
           catch (Exception ex)
           {
               _basketPictureBox.BackColor = Color.Black;
               Console.WriteLine("Gagal load image basket: " + ex.Message);
           }
       }

       public PictureBox GetPictureBox()
       {
           return _basketPictureBox;
       }
   }
   ```

2. **Menampilkan Basket di Form**:
   Tambahkan kode berikut di `Sky.cs` untuk menampilkan `Basket` di layar:

   ```csharp
   private Basket _basket;

   private void InitializeGame()
   {
       _scoreLabel = new Label
       {
           Text = "Score: 0",
           Location = new Point(10, 10),
           Font = new Font("Arial", 16),
           ForeColor = Color.Black
       };
       this.Controls.Add(_scoreLabel);

       _basket = new Basket(this.ClientSize, 15);
       this.Controls.Add(_basket.GetPictureBox());
   }
   ```

3. **Jalankan Aplikasi** untuk melihat `Basket` tampil di layar.

### 3. Menampilkan Star
Tambahkan kelas `Star` yang menampilkan `PictureBox` bergambar bintang yang jatuh dari atas layar.

1. Buat file baru bernama `Star.cs` dan tambahkan kode berikut:

   ```csharp
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   namespace CatchTheStars;

   public class Star
   {
       private PictureBox _starPictureBox;
       private Random _random;

       public Star(int fallSpeed, Size skySize)
       {
           _random = new Random();
           _starPictureBox = new PictureBox
           {
               Size = new Size(40, 40),
               Location = new Point(_random.Next(0, skySize.Width - 40), 0),
               SizeMode = PictureBoxSizeMode.StretchImage
           };

           try
           {
               _starPictureBox.Image = Resource.star;
           }
           catch (Exception e)
           {
               _starPictureBox.BackColor = Color.Gold;
               Console.WriteLine("Gagal load image star: " + e.Message);
           }
       }

       public PictureBox GetPictureBox()
       {
           return _starPictureBox;
       }
   }
   ```

2. **Menampilkan Star di Form**:
   Tambahkan inisialisasi `Star` di `Sky.cs` untuk menampilkan `Star` di layar.

   ```csharp
   private Star _star;

   private void InitializeGame()
   {
       // Kode sebelumnya
       _basket = new Basket(this.ClientSize, 15);
       this.Controls.Add(_basket.GetPictureBox());

       _star = new Star(5, this.ClientSize);
       this.Controls.Add(_star.GetPictureBox());
   }
   ```

3. **Jalankan Aplikasi** untuk melihat `Star` tampil di layar.

### 4. Mengimplementasi GameTimer
Tambahkan timer untuk menjalankan logika permainan secara berkala.

1. Di dalam `Sky.cs`, tambahkan timer:

   ```csharp
   private System.Windows.Forms.Timer _gameTimer;

   private void InitializeGame()
   {
       // Kode sebelumnya

       _gameTimer = new System.Windows.Forms.Timer
       {
           Interval = 20
       };
       _gameTimer.Tick += new EventHandler(GameLoop);
       _gameTimer.Start();
   }

   private void GameLoop(object sender, EventArgs e)
   {
       // Kosongkan dulu, akan diisi pada langkah berikutnya
   }
   ```

2. **Jalankan Aplikasi** untuk memastikan timer berhasil dijalankan tanpa error.

### 5. Membuat Gerakan Star Jatuh
Tambahkan logika untuk membuat bintang bergerak jatuh ke bawah.

1. Tambahkan metode `Fall` di `Star.cs`:

   ```csharp
   public void Fall()
   {
       _starPictureBox.Top += 5;
   }
   ```

2. Panggil metode `Fall` dalam `GameLoop` di `Sky.cs`:

   ```csharp
   private void GameLoop(object sender, EventArgs e)
   {
       _star.Fall();
   }
   ```

3. **Jalankan Aplikasi** untuk melihat `Star` jatuh dari atas layar.

### 6. Membuat Gerakan Kanan Kiri Basket

Pada langkah ini, kita akan menambahkan kemampuan untuk menggerakkan keranjang (`Basket`) ke kiri dan kanan menggunakan tombol panah pada keyboard. Agar `Basket` bisa bergerak, kita perlu menambahkan metode `MoveLeft` dan `MoveRight` di dalam kelas `Basket`.

#### a. Menambahkan Method `MoveLeft` dan `MoveRight` di Kelas Basket
1. Buka file `Basket.cs`.
2. Tambahkan method `MoveLeft` dan `MoveRight` berikut ke dalam kelas `Basket`:

   ```csharp
   public void MoveLeft()
   {
       if (_basketPictureBox.Left > 0)
       {
           _basketPictureBox.Left -= _speed;
       }
   }

   public void MoveRight(int boundary)
   {
       if (_basketPictureBox.Right < boundary)
       {
           _basketPictureBox.Left += _speed;
       }
   }
   ```

   - **Penjelasan**:
     - **MoveLeft**: Metode ini menggerakkan keranjang ke kiri sebesar nilai `_speed` jika posisi keranjang masih berada di dalam batas kiri layar (tidak kurang dari 0).
     - **MoveRight**: Metode ini menggerakkan keranjang ke kanan sebesar nilai `_speed` jika posisi keranjang masih berada di dalam batas kanan layar (tidak lebih dari batas layar yang ditentukan oleh `boundary`).

#### b. Menambahkan Gerakan Kiri dan Kanan di Kelas Sky
Setelah menambahkan metode `MoveLeft` dan `MoveRight`, kita bisa memanggil metode ini di dalam kelas `Sky` untuk menggerakkan `Basket` sesuai input dari tombol panah.

1. Buka file `Sky.cs`.
2. Tambahkan variabel `bool` `_goLeft` dan `_goRight` untuk mendeteksi apakah tombol kiri atau kanan ditekan.
3. Tambahkan event handler untuk menangkap input keyboard seperti berikut:

   ```csharp
   private bool _goLeft, _goRight;

   public Sky()
   {
       this.KeyDown += new KeyEventHandler(OnKeyDown);
       this.KeyUp += new KeyEventHandler(OnKeyUp);
   }

   private void OnKeyDown(object sender, KeyEventArgs e)
   {
       if (e.KeyCode == Keys.Left) _goLeft = true;
       if (e.KeyCode == Keys.Right) _goRight = true;
   }

   private void OnKeyUp(object sender, KeyEventArgs e)
   {
       if (e.KeyCode == Keys.Left) _goLeft = false;
       if (e.KeyCode == Keys.Right) _goRight = false;
   }
   ```

4. Tambahkan logika untuk menggerakkan `Basket` di dalam `GameLoop` dengan memanggil `MoveLeft` dan `MoveRight` sesuai input keyboard:

   ```csharp
   private void GameLoop(object sender, EventArgs e)
   {
       _star.Fall();

       if (_goLeft) _basket.MoveLeft();
       if (_goRight) _basket.MoveRight(this.ClientSize.Width);
   }
   ```

5. **Jalankan Aplikasi**: Sekarang Anda bisa menggerakkan `Basket` ke kiri dan kanan menggunakan tombol panah pada keyboard.

### 7. Mendeteksi Tabrakan Star dengan Basket
Tambahkan logika untuk mendeteksi tumbukan antara `Star` dan `Basket`.

1. Tambahkan metode `IsCollidedWith` di `Basket.cs`:

   ```csharp
   public bool IsCollidedWith(Star star)
   {
       return star.GetPictureBox().Bounds.IntersectsWith(this.GetPictureBox().Bounds);
   }
   ```

2. Tambahkan deteksi tumbukan di `GameLoop`:

   ```csharp
   if (_basket.IsCollidedWith(_star))
   {
       // Akan diisi di langkah berikutnya
   }
   ```

3. **Jalankan Aplikasi** untuk memastikan deteksi berjalan tanpa error.

### 8. Mengupdate Score Ketika Tabrakan

Pada langkah ini, kita akan menambahkan logika untuk memperbarui skor ketika `Basket` menangkap `Star`. Untuk itu, kita perlu mendeklarasikan variabel `_score` dan membuat metode `GenerateStar` di kelas `Sky` agar dapat memperbarui skor dan mengganti bintang setelah tabrakan.

#### a. Menambahkan Variabel `_score` di Kelas Sky
1. Buka file `Sky.cs`.
2. Di bagian deklarasi atribut kelas, tambahkan variabel `_score` yang akan digunakan untuk menyimpan skor permainan.

   ```csharp
   private int _score;

   public int Score { get { return _score; } }

   ```

3. Di dalam konstruktor `Sky`, inisialisasi `_score` dengan nilai 0.

   ```csharp
   public Sky()
   {
       _score = 0;
       // Kode lainnya untuk mengatur form
   }
   ```

#### b. Menambahkan Metode `GenerateStar` di Kelas Sky
1. Tambahkan metode `GenerateStar` di kelas `Sky`. Metode ini bertugas membuat atau memposisikan ulang objek `Star` di bagian atas layar.

   ```csharp
   public void GenerateStar()
   {
       if (_star != null)
       {
           this.Controls.Remove(_star.GetPictureBox());
       }
       _star = new Star(5, this.ClientSize);
       this.Controls.Add(_star.GetPictureBox());
   }
   ```

   - Metode `GenerateStar` akan menghapus objek `Star` yang lama (jika ada) dan membuat objek `Star` baru di posisi atas layar.

#### c. Membuat Metode UpdateScore
1. Tambahkan metode `UpdateScore` di kelas `Sky`. Metode ini bertugas melakukan increment variabel `_score` dan mengupdate tampilan di `_scoreLabel`.

   ```csharp
   public void UpdateScore()
   {
        _score++;
        _scoreLabel.Text = "Score: " + _score;
   }
   ```

#### d. Mengupdate Skor dan Mengganti Bintang Ketika Tabrakan
1. Di dalam metode `GameLoop`, tambahkan logika untuk memperbarui skor dan memanggil `GenerateStar` saat terjadi tabrakan antara `Basket` dan `Star`.

   ```csharp
   private void GameLoop(object sender, EventArgs e)
   {
       _star.Fall();

       if (_goLeft) _basket.MoveLeft();
       if (_goRight) _basket.MoveRight(this.ClientSize.Width);

       // Deteksi tabrakan antara Basket dan Star
       if (_basket.IsCollidedWith(_star))
       {
           UpdateScore();
           GenerateStar();
       }
   }
   ```

2. **Jalankan Aplikasi**: Sekarang, ketika `Basket` menangkap `Star`, skor akan bertambah, dan bintang baru akan muncul di atas layar.

### 9. Mengganti Star Ketika Star Sudah Melewati Layar

Pada langkah ini, kita akan menambahkan logika untuk mengganti objek `Star` ketika bintang tersebut sudah melewati batas bawah layar, sehingga bintang baru akan muncul di atas layar.

#### a. Menambahkan Metode `IsOutOfSky` di Kelas Star
1. Buka file `Star.cs`.
2. Tambahkan metode `IsOutOfSky` di dalam kelas `Star`. Metode ini akan mengecek apakah posisi bintang sudah melewati batas bawah layar.

   ```csharp
   public bool IsOutOfSky(Size skySize)
   {
       return _starPictureBox.Top > skySize.Height;
   }
   ```

   - **Penjelasan**: Metode ini mengembalikan `true` jika posisi `Top` dari `Star` lebih besar dari tinggi layar (`skySize.Height`), yang berarti bintang telah melewati batas bawah layar.

#### b. Memanggil `IsOutOfSky` di `GameLoop` untuk Mengganti `Star`
1. Buka file `Sky.cs`.
2. Di dalam metode `GameLoop`, tambahkan logika untuk memanggil `GenerateStar` jika `Star` sudah melewati layar.

   ```csharp
   private void GameLoop(object sender, EventArgs e)
   {
       _star.Fall();

       if (_goLeft) _basket.MoveLeft();
       if (_goRight) _basket.MoveRight(this.ClientSize.Width);

       // Deteksi tabrakan antara Basket dan Star
       if (_basket.IsCollidedWith(_star))
       {
           UpdateScore();
           GenerateStar();
       }
       // Ganti Star jika sudah melewati layar
       else if (_star.IsOutOfSky(this.ClientSize))
       {
           GenerateStar();
       }
   }
   ```

3. **Jalankan Aplikasi**: Sekarang, setiap kali `Star` melewati batas bawah layar, `GenerateStar` akan dipanggil untuk mengganti `Star` dengan yang baru di bagian atas layar.

### 10. Membunyikan Suara Ketika Tabrakan
Tambahkan suara yang dimainkan setiap kali `Basket` menangkap `Star`.

1. Di `Sky.cs`, tambahkan `SoundPlayer`:

   ```csharp
   private SoundPlayer _collisionSound;

   public Sky()
   {
       _collisionSound = new SoundPlayer(Resource.coin);
       // Kode lainnya
   }
   ```

2. Mainkan suara dalam `GameLoop`:

   ```csharp
   if (_basket.IsCollidedWith(_star))
   {
       _collisionSound.Play();
       UpdateScore();
       GenerateStar();
   }
   ```

3. **Jalankan Aplikasi** dan uji suara yang dimainkan saat `Basket` menangkap `Star`.

### Menambahkan Aset ke Aplikasi Game Menggunakan Resource File

#### 1. Membuat File Resource.resx
1. Di dalam proyek Visual Studio, klik kanan pada folder **Properties** di **Solution Explorer**.
2. Pilih **Add > New Item**.
3. Di jendela **Add New Item**, pilih **Resources File**.
4. Beri nama file ini **Resources.resx** dan klik **Add**.

#### 2. Membuka Resource.resx untuk Menambahkan File
1. Setelah file **Resources.resx** ditambahkan, buka file tersebut dengan mengklik dua kali pada **Resources.resx** di bawah folder **Properties**.
2. Jendela editor resources akan terbuka. Di sini Anda bisa menambahkan gambar dan suara.

#### 3. Menambahkan File **basket.png** sebagai Gambar untuk Basket
1. Di dalam editor **Resources.resx**, pastikan Anda berada di tab **Images**.
2. Klik **Add Resource > Add Existing File**.
3. Cari file **basket.png** yang sudah Anda miliki di komputer Anda dan pilih file tersebut.
4. File **basket.png** sekarang akan ditambahkan ke dalam **Resources.resx** dan dapat diakses dalam kode dengan nama `Resource.basket`.

#### 4. Menambahkan File **star.png** sebagai Gambar untuk Star
1. Di dalam editor **Resources.resx**, tetap berada di tab **Images**.
2. Klik lagi **Add Resource > Add Existing File**.
3. Cari file **star.png** di komputer Anda dan pilih file tersebut.
4. File **star.png** akan ditambahkan ke dalam **Resources.resx** dan dapat diakses dalam kode dengan nama `Resource.star`.

#### 5. Menambahkan File **coin.wav** sebagai Suara untuk Tabrakan
1. Di dalam editor **Resources.resx**, beralih ke tab **Audio** atau tab **Files** (kedua tab ini dapat digunakan untuk suara).
2. Klik **Add Resource > Add Existing File**.
3. Cari file **coin.wav** di komputer Anda dan pilih file tersebut.
4. File **coin.wav** akan ditambahkan ke dalam **Resources.resx** dan dapat diakses dalam kode dengan nama `Resource.coin`.

#### 6. Mengakses Resource di Kode
Setelah menambahkan file **basket.png**, **star.png**, dan **coin.wav** ke dalam **Resources.resx**, Anda bisa mengakses resource ini di kode dengan cara berikut:

- **Gambar Basket**: `Resource.basket`
- **Gambar Star**: `Resource.star`
- **Suara Coin**: `Resource.coin`

Contoh penggunaan di kode:

```csharp
_basketPictureBox.Image = Resource.basket;
_starPictureBox.Image = Resource.star;
_collisionSound = new SoundPlayer(Resource.coin);
```


### Menjalankan Aplikasi
1. Pastikan gambar `basket`, `star`, dan `coin` tersedia dalam resource project dengan nama yang sesuai.
2. Jalankan aplikasi dengan menekan **F5** atau klik **Start**.

### Uji Program
- **Tekan tombol panah kiri atau kanan**: `Basket` akan bergerak sesuai arah yang ditekan.
- **Ketika bintang bertumbukan dengan keranjang**: Skor akan bertambah, dan suara `coin` akan dimainkan.
- **Jika bintang melewati layar**: Bintang baru akan muncul dari atas layar.