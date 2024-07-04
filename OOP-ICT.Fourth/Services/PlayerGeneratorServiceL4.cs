using OOP_ICT.Fourth.Models;
using OOP_ICT.Third.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ICT.Fourth.Services
{
    public class PlayerGeneratorServiceL4
    {
        private readonly Random _random;
        private readonly string[] _names = new[] { "Alex", "Jordan", "Taylor", "Morgan", "Casey", "Skyler", "Riley", "Jamie", "Avery", "Payton" }; // Пример имен

        public PlayerGeneratorServiceL4()
        {
            _random = new Random();
        }

        public List<PlayerPoker> GeneratePlayers(int numberOfPlayers)
        {
            var players = new List<PlayerPoker>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                string name = _names[_random.Next(_names.Length)];
                int balance = _random.Next(100, 10000);
                players.Add(new PlayerPoker(name, balance));
            }
            return players;
        }
    }
}
