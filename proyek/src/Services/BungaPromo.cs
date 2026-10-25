using BankMini.Models;

namespace BankMini.Services;

public class BungaPromo : IKebijakanBunga
{
    private readonly decimal _bonusMultiplier;

    public string NamaKebijakan => $"Kebijakan Bunga Promo (Bonus {(_bonusMultiplier - 1) * 100:N0}%)";

    public BungaPromo(decimal bonusMultiplier = 1.2m)
    {
        if (bonusMultiplier < 1m)
            throw new ArgumentException("Bonus multiplier tidak boleh kurang dari 1.0", nameof(bonusMultiplier));
        _bonusMultiplier = bonusMultiplier;
    }

    public decimal HitungBungaBulanan(Rekening rekening)
    {
        return rekening.HitungBunga() * _bonusMultiplier;
    }
}
