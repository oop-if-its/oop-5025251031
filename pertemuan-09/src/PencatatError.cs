// File ini berisi TODO -- lihat SOAL.md untuk kontrak lengkap tiap level.
// Bagian bertanda TODO(Level N) adalah tugas kalian; kode lain sudah
// disediakan. Jangan mengubah nama/tipe yang sudah ada kecuali TODO
// memintanya secara eksplisit.

using System.Runtime.CompilerServices;

namespace Pertemuan08;

// SUDAH LENGKAP -- jangan diubah. Sumber error untuk test; [NoInlining] supaya
// namanya selalu muncul di stack trace.
public static class Sumber
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Gagal() => throw new InvalidOperationException("boom");
}

public class PencatatError
{
    private readonly List<string> _log = new();

    public IReadOnlyList<string> Log => _log.AsReadOnly();

    public void JalankanDenganLog(Action aksi)
    {
        // TODO(Level 7): jalankan aksi() di dalam try. Kalau melempar exception
        //   apa pun: catat "ERROR: <ex.Message>" ke _log, lalu LEMPAR ULANG
        //   dengan `throw;` (BUKAN `throw ex;`) supaya stack trace aslinya --
        //   termasuk method yang benar-benar menyebabkan error -- tidak hilang.
        //   Kalau aksi sukses, tidak ada yang dicatat.
        throw new NotImplementedException("Level 7 belum diimplementasikan");
    }
}
