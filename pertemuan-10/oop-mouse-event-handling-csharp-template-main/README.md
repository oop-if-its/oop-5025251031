# Tutorial: Mouse Event Handling dengan Windows Form di C#

## Tujuan
Mempelajari cara menangani event mouse pada Windows Form tanpa menggunakan designer. Aplikasi ini akan menampilkan kotak (`PictureBox`) yang merespons berbagai event mouse seperti klik, gerakan, masuk, dan keluar dari area kotak.

## Sub Capaian Pembelajaran

Mahasiswa mampu mengimplementasikan event handler pada Windows Form.

## Lingkungan Pengembangan

1. Platform: .NET 10
2. Bahasa: C# 14
3. IDE: Visual Studio 2026 atau VSCode

## Cara membuka project menggunakan Visual Studio

1. Clone repositori project `oop-mouse-event-handling-csharp` ke direktori lokal git Anda.
2. Buka Visual Studio, pilih menu File > Open > Project/Solution > Pilih file *.sln.
3. Tekan tombol Open untuk  untuk membuka solusi.
4. Baca tutorial dengan seksama. Buat implementasi kode sesuai dengan petunjuk.

## Langkah-langkah

### 0. Membuka/Membuat Solusi (Opsional)

Buka Visual Studio dan buat proyek `Windows Forms App` (tanpa .NET Framework):
1. Buat proyek Console dengan nama `MouseEventHandling`.
2. Pilih target framework .NET 10.
3. Klik tombol `Create`.
3. Pastikan ada referensi **System.Windows.Forms** dan **System.Drawing** untuk menggunakan komponen Windows Form.

### 1. Membuat Kelas MainForm (Turunan dari Kelas System.Windows.Forms.Form)

Di dalam proyek Anda, tambahkan kelas baru bernama `MainForm.cs` yang akan berfungsi sebagai form utama aplikasi:

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;

public class MainForm : Form
{
    public MainForm()
    {
        
    }
}
```

### 2. Mengatur Properti Dasar Form

Di konstruktur `MainForm`, tambahkan statemen berikut untuk mengatur form.

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;

public class MainForm : Form
{
    public MainForm()
    {
        // Mengatur properti dasar form
        this.Text = "Mouse Event Handling";
        this.Size = new Size(400, 400);
        this.StartPosition = FormStartPosition.CenterScreen;
    }
}
```

- **Penjelasan**: Properti dasar seperti `Text`, `Size`, dan `StartPosition` mengatur judul form, ukuran form, dan posisi awal form di layar.

### 3. Menjalankan Aplikasi untuk Menguji Coba

1. Buat kelas `Program` yang berisi metode Main.
2. Ubah metode `Main` untuk menjalankan `MainForm`:

   ```csharp
   using System;
   using System.Windows.Forms;

   internal static class Program
   {
       [STAThread]
       static void Main()
       {
           Application.EnableVisualStyles();
           Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new MainForm());
       }
   }
   ```

3. Jalankan aplikasi dengan menekan **F5** atau klik **Start**.

### 4. Membuat Label untuk Menampilkan Informasi Event

Tambahkan `Label` yang akan digunakan untuk menampilkan informasi tentang event yang terjadi. Kemudian coba jalankan aplikasi untuk melihat hasilnya.

```csharp
private Label infoLabel;

public MainForm()
{
    // Mengatur properti dasar form
    this.Text = "Mouse Event Handling";
    this.Size = new Size(400, 400);
    this.StartPosition = FormStartPosition.CenterScreen;

    // Membuat label untuk menampilkan informasi event
    infoLabel = new Label
    {
        Text = "Arahkan mouse ke kotak atau klik kotak.",
        Location = new Point(10, 10),
        AutoSize = true
    };
    this.Controls.Add(infoLabel);
}
```

- **Penjelasan**: `infoLabel` ditambahkan ke form untuk memberikan informasi tentang status event mouse kepada pengguna. Lokasinya diatur di bagian atas form.

### 5. Membuat `PictureBox` sebagai Objek yang Akan Merespons Event Mouse

Tambahkan `PictureBox` di dalam form, yang akan merespons berbagai event mouse. Coba jalankan aplikasi untuk melihat hasilnya. Kemudian, coba gerakkan mouse ke arah `PictureBox` yang berwarna biru. Apakah terjadi perubahan pada `infoLabel`?

```csharp
private PictureBox box;

public MainForm()
{
    // Mengatur properti dasar form
    this.Text = "Mouse Event Handling";
    this.Size = new Size(400, 400);
    this.StartPosition = FormStartPosition.CenterScreen;

    // Membuat label untuk menampilkan informasi event
    infoLabel = new Label
    {
        Text = "Arahkan mouse ke kotak atau klik kotak.",
        Location = new Point(10, 10),
        AutoSize = true
    };
    this.Controls.Add(infoLabel);

    // Membuat PictureBox sebagai objek yang akan merespons event mouse
    box = new PictureBox
    {
        Size = new Size(100, 100),
        Location = new Point(150, 150),
        BackColor = Color.Blue
    };
    this.Controls.Add(box);
}
```

- **Penjelasan**: `PictureBox` (dengan nama `box`) dibuat di tengah form dan memiliki warna latar `Blue`.

### 6. Membuat Method Handler untuk `MouseClick`

Agar teks pada `infoLabel` dan warna berubah ketika mouse diklik, tambahkan metode handler untuk menangani event `MouseClick` pada `box`. 

```csharp
private void OnBoxMouseClick(object sender, MouseEventArgs e)
{
    // Mengubah warna kotak saat diklik
    box.BackColor = Color.FromArgb(new Random().Next(256), new Random().Next(256), new Random().Next(256));
    infoLabel.Text = $"Mouse clicked at ({e.X}, {e.Y})";
}
```

Sambungkan event handler `OnBoxMouseClick` ke event `MouseClick` pada objek `box`.

```csharp
public MainForm()
{
    // ... kode sebelumnya ...

    // Menangani beberapa event mouse
    box.MouseClick += new MouseEventHandler(OnBoxMouseClick);
}
```

Coba jalankan aplikasi untuk melihat hasilnya. Kemudian, coba klik mouse pada area `PictureBox`. Apa yang terjadi?

- **Penjelasan**: `OnBoxMouseClick` adalah handler yang merespons event `MouseClick`. Saat kotak diklik, warnanya akan berubah secara acak, dan `infoLabel` menampilkan posisi klik.

### 7. Membuat Method Handler untuk `MouseEnter`

Tambahkan metode handler untuk menangani event `MouseEnter`, yang terjadi ketika mouse masuk ke area `box`. 

```csharp
private void OnBoxMouseEnter(object sender, EventArgs e)
{
    infoLabel.Text = "Mouse entered the box area.";
}
```

Sambungkan event handler `OnBoxMouseEnter` ke event `MouseEnter` pada objek `box`.

```csharp
public MainForm()
{
    // ... kode sebelumnya ...

    // Menangani beberapa event mouse
    box.MouseClick += new MouseEventHandler(OnBoxMouseClick);
    box.MouseEnter += new EventHandler(OnBoxMouseEnter);
}
```

Coba jalankan aplikasi untuk melihat hasilnya. Kemudian, coba arahkan mouse dari luar area ke area `PictureBox`. Apa yang terjadi?

- **Penjelasan**: `OnBoxMouseEnter` mengubah teks `infoLabel` menjadi "Mouse entered the box area." ketika mouse masuk ke area `box`.

### 8. Membuat Method Handler untuk `MouseLeave`

Tambahkan metode handler untuk menangani event `MouseLeave`, yang terjadi ketika mouse keluar dari area `box`. 

```csharp
private void OnBoxMouseLeave(object sender, EventArgs e)
{
    infoLabel.Text = "Mouse left the box area.";
}
```

Sambungkan event handler `OnBoxMouseLeave` ke event `MouseLeave` pada objek `box`.

```csharp
public MainForm()
{
    // ... kode sebelumnya ...

    // Menangani beberapa event mouse
    box.MouseClick += new MouseEventHandler(OnBoxMouseClick);
    box.MouseEnter += new EventHandler(OnBoxMouseEnter);
    box.MouseLeave += new EventHandler(OnBoxMouseLeave);
}
```

Coba jalankan aplikasi untuk melihat hasilnya. Kemudian, coba arahkan mouse dari dalam ke luar area `PictureBox`. Apa yang terjadi?

- **Penjelasan**: `OnBoxMouseLeave` mengubah teks `infoLabel` menjadi "Mouse left the box area." ketika mouse keluar dari `box`.

### 9. Membuat Method Handler untuk `MouseMove`

Tambahkan metode handler untuk menangani event `MouseMove`, yang terjadi ketika mouse bergerak di dalam area `box`. 

```csharp
private void OnBoxMouseMove(object sender, MouseEventArgs e)
{
    infoLabel.Text = $"Mouse moving at ({e.X}, {e.Y})";
}
```

Sambungkan event handler `OnBoxMouseMove` ke event `MouseMove` pada objek `box`.

```csharp
public MainForm()
{
    // ... kode sebelumnya ...

    // Menangani beberapa event mouse
    box.MouseClick += new MouseEventHandler(OnBoxMouseClick);
    box.MouseEnter += new EventHandler(OnBoxMouseEnter);
    box.MouseLeave += new EventHandler(OnBoxMouseLeave);
    box.MouseMove += new MouseEventHandler(OnBoxMouseMove);
}
```

Coba jalankan aplikasi untuk melihat hasilnya. Kemudian, coba gerakkan mouse di dalam area `PictureBox`. Apa yang terjadi?

- **Penjelasan**: `OnBoxMouseMove` menampilkan posisi mouse relatif terhadap `box` di `infoLabel` ketika mouse bergerak di dalam `box`.


### Kode Lengkap `MainForm.cs`

Berikut adalah kode lengkap `MainForm` dengan semua langkah di atas:

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;

public class MainForm : Form
{
    private Label infoLabel;
    private PictureBox box;

    public MainForm()
    {
        // Mengatur properti dasar form
        this.Text = "Mouse Event Handling";
        this.Size = new Size(400, 400);
        this.StartPosition = FormStartPosition.CenterScreen;

        // Membuat label untuk menampilkan informasi event
        infoLabel = new Label
        {
            Text = "Arahkan mouse ke kotak atau klik kotak.",
            Location = new Point(10, 10),
            AutoSize = true
        };
        this.Controls.Add(infoLabel);

        // Membuat PictureBox sebagai objek yang akan merespons event mouse
        box = new PictureBox
        {
            Size = new Size(100, 100),
            Location = new Point(150, 150),
            BackColor = Color.Blue
        };
        this.Controls.Add(box);

        // Menangani beberapa event mouse
        box.MouseClick += new MouseEventHandler(OnBoxMouseClick);
        box.MouseEnter += new EventHandler(OnBoxMouseEnter);
        box.MouseLeave += new EventHandler(OnBoxMouseLeave);
        box.MouseMove += new MouseEventHandler(OnBoxMouseMove);
    }

    private void OnBoxMouseClick(object sender, MouseEventArgs e)
    {
        box.BackColor = Color.FromArgb(new Random().Next(256), new Random().Next(256), new Random().Next(256));
        infoLabel.Text = $"Mouse clicked at ({e.X}, {e.Y})";
    }

    private void OnBoxMouseEnter(object sender, EventArgs e)
    {
        infoLabel.Text = "Mouse entered the box area.";
    }

    private void OnBoxMouseLeave(object sender, EventArgs e)
    {
        infoLabel.Text = "Mouse left the box area.";
    }

    private void OnBoxMouseMove(object sender, MouseEventArgs e)
    {
        infoLabel.Text = $"Mouse moving at ({e.X}, {e.Y})";
    }
}
```

### 10. Menjalankan Aplikasi

Jalankan aplikasi dengan menekan **F5** atau klik **Start**.

### Uji Program

- **Arahkan mouse ke kotak**: Teks `infoLabel` akan berubah menjadi "Mouse entered the box area."
- **Gerakkan mouse di dalam kotak**: `infoLabel` akan menampilkan posisi mouse di dalam kotak.
- **Klik kotak**: Warna kotak akan berubah, dan `infoLabel` akan menampilkan posisi klik.
- **Keluarkan mouse dari kotak**: `infoLabel` akan berubah menjadi "Mouse left the box area."