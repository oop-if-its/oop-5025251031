// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

namespace Pertemuan08;

// Pola Dispose lengkap: Dispose() untuk pembersihan DETERMINISTIK (dipanggil
// pemakai / `using`), finalizer ~SumberDaya() sebagai JARING PENGAMAN kalau
// pemakai lupa. Dispose() memanggil GC.SuppressFinalize supaya finalizer tidak
// perlu jalan lagi.
// TODO(Level 9): buat SumberDaya mengimplementasikan IDisposable dengan pola:
//   public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }  |
//   protected virtual void Dispose(bool disposing) { ... }  |  ~SumberDaya() {
//   Dispose(false); }.
public class SumberDaya
{
    private static int _dibersihkanDispose;
    private static int _dibersihkanFinalizer;

    // SUDAH LENGKAP -- statistik untuk test.
    public static int DibersihkanDispose => _dibersihkanDispose;
    public static int DibersihkanFinalizer => _dibersihkanFinalizer;

    private bool _sudahDibersihkan;

    // SUDAH LENGKAP -- panggil dari Dispose(bool) sesuai jalurnya.
    private static void CatatDispose() => Interlocked.Increment(ref _dibersihkanDispose);
    private static void CatatFinalizer() => Interlocked.Increment(ref _dibersihkanFinalizer);

    public void Gunakan()
    {
        // (pura-pura memakai sumber daya)
    }

    // TODO(Level 9): tulis `public void Dispose()` sesuai pola di atas kelas.

    // TODO(Level 9): tulis `protected virtual void Dispose(bool disposing)`:
    //   kalau _sudahDibersihkan sudah true -> return (idempoten). disposing =
    //   true -> CatatDispose(); disposing = false (dipanggil finalizer) ->
    //   CatatFinalizer(). Lalu set _sudahDibersihkan = true.

    // TODO(Level 9): tulis finalizer `~SumberDaya()` yang memanggil
    //   Dispose(false).
}
