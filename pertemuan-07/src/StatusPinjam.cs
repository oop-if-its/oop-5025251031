namespace Pertemuan07;

// SUDAH LENGKAP -- jangan diubah. Pencatat status dipinjam/tidak, dipakai
// lewat KOMPOSISI oleh kelas-kelas yang mengimplementasikan IDapatDipinjam.
public class StatusPinjam
{
    public bool Dipinjam { get; private set; }

    public void Tandai()
    {
        if (Dipinjam)
            throw new InvalidOperationException("Item sedang dipinjam.");
        Dipinjam = true;
    }

    public void Lepas()
    {
        if (!Dipinjam)
            throw new InvalidOperationException("Item tidak sedang dipinjam.");
        Dipinjam = false;
    }
}
