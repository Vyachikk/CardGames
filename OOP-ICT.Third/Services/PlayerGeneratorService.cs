using OOP_ICT.Third.Models;

namespace OOP_ICT.Third.Services;

public class PlayerGeneratorService
{
    private readonly Random _random;
    private readonly string[] _names = new[] { "Alex", "Jordan", "Taylor", "Morgan", "Casey", "Skyler", "Riley", "Jamie", "Avery", "Payton" }; // Пример имен

    public PlayerGeneratorService()
    {
        _random = new Random();
    }

    public List<PlayerCasinoL3> GeneratePlayers(int numberOfPlayers)
    {
        var players = new List<PlayerCasinoL3>();
        for (int i = 0; i < numberOfPlayers; i++)
        {
            string name = _names[_random.Next(_names.Length)];
            int balance = _random.Next(100, 10000); 
            players.Add(new PlayerCasinoL3(name, balance));
        }
        return players;
    }
}