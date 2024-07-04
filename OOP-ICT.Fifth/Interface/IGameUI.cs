using OOP_ICT.Fifth.Enums;
using OOP_ICT.Fifth.Models;
using OOP_ICT.Fifth.Services;

namespace OOP_ICT.Fifth.Interface
{
    internal interface IGameUI
    {
        void GetPlayerActionFirstBettingRound(Player player, PokerGame game);
        void GetPlayerAction(Player player, PokerGame game);
        void DisplayGreeting(PokerGame pokerGame, PlayerGeneratorServiceL5 gen);
        void DisplayMessage(string text);
        void DisplayCards(List<Card> cards);
        void DisplayPlayers(List<Player> players);
        void DisplayHeader(string text);
    }
}