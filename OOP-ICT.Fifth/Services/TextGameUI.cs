using OOP_ICT.Fifth.Enums;
using OOP_ICT.Fifth.Interface;
using OOP_ICT.Fifth.Models;
using Spectre.Console;
using System.Text.Json;

namespace OOP_ICT.Fifth.Services
{
    class TextGameUI : IGameUI
    {
        public void DisplayGreeting(PokerGame pokerGame, PlayerGeneratorServiceL5 gen)
        {
            var stats = new PlayerStatisticsService();

            var choise = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title($"Welcome to Texas Hold'em Poker!")
                .PageSize(4)
                .AddChoices(new[] {
                    "New game", "Load game from file", "Exit", "Check statistics"
                }));

            switch (choise)
            {
                case "New game":
                    int playersNum = EnterPlayersNum();
                    var players = gen.GeneratePlayers(playersNum);
                    foreach (var player in players)
                        pokerGame.AddPlayer(player);
                    pokerGame.StartGame();
                    break;
                case "Load game from file":
                    if (File.Exists("PlayerStats.json"))
                    {
                        string json = File.ReadAllText("PlayerStats.json");
                        var loadedPlayers = JsonSerializer.Deserialize<List<Player>>(json);
                        foreach (var player in loadedPlayers)
                            pokerGame.AddPlayer(player);
                        pokerGame.StartGame();
                    }
                    else
                    {
                        DisplayMessage("[red]File does not exist.[/] Returning to the main menu.");
                        DisplayGreeting(pokerGame, gen);
                    }
                    break;
                case "Check statistics":
                    stats.PrintAllPlayerStats();
                    break;
                case "Exit":

                    break;
            }
        }

        public void GetPlayerActionFirstBettingRound(Player player, PokerGame game)
        {
            string choise = "";

            AnsiConsole.Status()
                .Spinner(Spinner.Known.Clock)
                .SpinnerStyle(Style.Parse("darkgoldenrod"))
                .Start("[darkgoldenrod]Make bets...[/]", ctx =>
                 {
                     choise = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title($"Player [green]{player.Name}[/], it's your turn. Your chips: [Yellow]{player.Chips}[/]")
                        .PageSize(3)
                        .AddChoices(new[] {
                            "Fold", "Call", "Raise",
                        }));
                 });

            switch (choise)
            {
                case "Fold":
                    game.PlayerFolded(player);
                    break;
                case "Call":
                    game.PlayerCalled(player);
                    break;
                case "Raise":
                    game.PlayerRaised(player);
                    break;
            }
        }

        public void GetPlayerAction(Player player, PokerGame game)
        {
            string choise = "";

            AnsiConsole.Status()
                .Spinner(Spinner.Known.Clock)
                .SpinnerStyle(Style.Parse("darkgoldenrod"))
                .Start("[darkgoldenrod]Make bets...[/]", ctx =>
                {
                    choise = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title($"Player [green]{player.Name}[/], it's your turn.")
                        .PageSize(4)
                        .AddChoices(new[] {
                        "Fold", "Check", "Bet", "Call",
                        }));
                });

            switch (choise)
            {
                case "Fold":
                    game.PlayerFolded(player);
                    break;
                case "Check":
                    break;
                case "Bet":
                    game.PlayerBetting(player);
                    break;
                case "Call":
                    game.PlayerCalled(player);
                    break;
            }
        }


        public void DisplayHeader(string text)
        {
            var rule = new Rule($"[red]{text}[/]");
            AnsiConsole.Write(rule);
        }

        public void DisplayMessage(string text)
        {
            string line = $"{text}";
            AnsiConsole.MarkupLine(line);
        }

        public void DisplayCards(List<Card> cards)
        {
            var table = new Table().Centered();

            foreach (var card in cards)
                table.AddColumn($"    {card.GetRankSymbol()}\n\n" +
                                $"  {card.GetSuitSymbol()}  \n\n" +
                                $"{card.GetRankSymbol()}  ");

            AnsiConsole.Render(table);
        }

        public void DisplayPlayers(List<Player> players)
        {
            var table = new Table().Centered();
            foreach (var player in players)
            {
                var name = new Markup(player.IsActive ? $"[green]{player.Name}[/]" : $"[red]{player.Name}[/]");
                table.AddColumn(new TableColumn(name).Centered());
            }
            AnsiConsole.Render(table);
        }


        public int EnterBet(Player player)
        {
            int minBet = 10;
            int maxBet = player.Chips;

            string minBetErrorMessage = $"[red]You must bet at least {minBet}[/]";
            string maxBetErrorMessage = $"[red]You can't bet more than your available chips ({maxBet})[/]";

            return AnsiConsole.Prompt(
                new TextPrompt<int>($"{player.Name}, enter the amount you want to raise ({minBet} - {maxBet})")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]That's not a valid bet[/]")
                    .Validate(bet =>
                    {
                        if (bet < minBet)
                        {
                            return ValidationResult.Error(minBetErrorMessage);
                        }
                        else if (bet > maxBet)
                        {
                            return ValidationResult.Error(maxBetErrorMessage);
                        }
                        else
                        {
                            return ValidationResult.Success();
                        }
                    }));
        }

        public int EnterPlayersNum()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<int>($"Enter the number of players between 2 and 10: ")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]That's not a valid num[/]")
                    .Validate(num =>
                    {
                        return num switch
                        {
                            <= 2 => ValidationResult.Error("[red]You must at least be 1 years old[/]"),
                            >= 10 => ValidationResult.Error("[red]You must be younger than the oldest person alive[/]"),
                            _ => ValidationResult.Success(),
                        };
                    }));
        }

        public void PrintStatistics(List<Player> players)
        {
            var table = new Table();
            var tableTitle = new TableTitle("All Player Statistics:");

            table.Title = tableTitle;

            table.AddColumn("Name");
            table.AddColumn("Chips");
            table.AddColumn("Matches");
            table.AddColumn("[green]Wins[/]");
            table.AddColumn("[red]Loses[/]");

            foreach (var player in players)
                table.AddRow(player.Name, player.Chips.ToString(), player.MatchesPlayed.ToString(), player.Wins.ToString(), player.Losses.ToString());

            AnsiConsole.Write(table);
        }
    }
}