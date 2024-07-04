using OOP_ICT.Fifth.Models;

namespace OOP_ICT.Fifth.Services;

class PlayerGeneratorServiceL5
{
    private readonly Random _random;
    private readonly string[] _names = new[] { "Alex", "Jordan", "Taylor", "Morgan", "Casey", "Skyler", "Riley", "Jamie", "Avery", "Payton" }; // Пример имен

    public PlayerGeneratorServiceL5()
    {
        _random = new Random();
    }

    public List<Player> GeneratePlayers(int numberOfPlayers)
    {
        var players = new List<Player>();
        for (int i = 0; i < numberOfPlayers; i++)
        {
            string name = _names[_random.Next(_names.Length)];
            int balance = _random.Next(500, 10000);
            players.Add(new Player(name, balance));
        }
        return players;
    }
}
