using OOP_ICT.Second.Models;
namespace OOP_ICT.Second.Factories;

public class PlayerFactory 
{
    public static PlayerCasinoL2 CreateNewPlayer(string name, int initialBalance)
    {
        return new PlayerCasinoL2(name, initialBalance);
    }
}