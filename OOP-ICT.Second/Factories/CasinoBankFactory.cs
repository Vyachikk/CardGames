using OOP_ICT.Second.Models;
namespace OOP_ICT.Second.Factories;

public class CasinoBankFactory
{
    public static Bank CreateBankAccount(int BankBalance)
    {
        return new Bank(BankBalance);
    }
}