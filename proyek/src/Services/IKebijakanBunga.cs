using BankMini.Models;

namespace BankMini.Services;

public interface IKebijakanBunga
{
    string NamaKebijakan { get; }
    decimal HitungBungaBulanan(Rekening rekening);
}