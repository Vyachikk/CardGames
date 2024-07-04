using OOP_ICT.Fifth.Services;

namespace OOP_ICT.Fifth
{
    class Program
    {
        static void Main()
        {
            var gameUI = new TextGameUI();
            var PlayerGenerator = new PlayerGeneratorServiceL5();
            var pokerGame = new PokerGame();

            gameUI.DisplayGreeting(pokerGame, PlayerGenerator);
        }
    }
}