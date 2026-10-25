using BankMini.Models;

namespace BankMini.Services;

public class BungaStandar : IKebijakanBunga
{
    public string NamaKebijakan => "Kebijakan Bunga Standar Bank Mini";

    public decimal HitungBungaBulanan(Rekening rekening)
    {
        return rekening.HitungBunga();
    }
}
