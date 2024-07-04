using OOP_ICT.Fifth.Interface;
using OOP_ICT.Fifth.Models;
using System.Text.Json;

namespace OOP_ICT.Fifth.Services
{
    public class PlayerStatisticsService : IPlayerStatisticsService
    {
        private TextGameUI gameUI;

        public PlayerStatisticsService()
        {
            gameUI = new TextGameUI();
        }

        public void PrintAllPlayerStats()
        {
            string json = File.ReadAllText("PlayerStats.json");
            var loadedPlayers = JsonSerializer.Deserialize<List<Player>>(json);
            gameUI.PrintStatistics(loadedPlayers);
        }

        public void SavePlayerStatsToJson(string filePath, List<Player> players)
        {
            string json = JsonSerializer.Serialize(players, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
