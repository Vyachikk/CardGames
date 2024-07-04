using OOP_ICT.Fifth.Enums;
using OOP_ICT.Fifth.Exeptions;
using OOP_ICT.Fifth.Models;
using OOP_ICT.Fifth.Services;
using Spectre.Console;
using static OOP_ICT.Fifth.Enums.HandEvaluator;

class PokerGame
{ 
    private List<Player> players;
    private Dealer dealer;
    private Bank bank;
    private int currentBet;
    private TextGameUI gameUI;
    private PlayerStatisticsService playerStatisticsService;

    public PokerGame()
    {
        players = new List<Player>();
        dealer = new Dealer();
        bank = new Bank();
        currentBet = 0;
        gameUI = new TextGameUI();
        playerStatisticsService = new PlayerStatisticsService();
    }

    public List<Player> Players => players;

    public void AddPlayer(Player player)
    {
        if (player == null)
            throw new PokerGameException("Cannot add null player to the game.");

        players.Add(player);
    }

    public void StartGame()
    {
        if (players.Count < 2)
            throw new PokerGameException("Not enough players to start the game. Minimum 2 players required.");

        dealer.DealCards(players);
        FirstBettingRound();
        dealer.DealFlop();
        BettingRound("Flop!");
        dealer.DealTurn();
        BettingRound("Turn!");
        dealer.DealRiver();
        BettingRound("River!");

        foreach (var player in players)
        {
            if (player.IsActive)
            {
                DetermineWinner();
                break;
            }
        }

        playerStatisticsService.SavePlayerStatsToJson("PlayerStats.json", players);
        gameUI.DisplayGreeting(this, new PlayerGeneratorServiceL5());
    }

    public void PlayerFolded(Player player)
    {
        player.IsActive = false;
    }

    public void PlayerCalled(Player player)
    {
        player.BetAmount = currentBet;
        player.RemoveChips(player.BetAmount);
        bank.AddToBank(currentBet);
    }

    public void PlayerRaised(Player player)
    {
        int raiseAmount = gameUI.EnterBet(player);
        player.BetAmount = raiseAmount + currentBet;
        player.RemoveChips(player.BetAmount);
        bank.AddToBank(player.BetAmount);
        currentBet = player.BetAmount;
    }

    public void PlayerBetting(Player player)
    {
        int betAmount = gameUI.EnterBet(player);
        player.BetAmount = betAmount;
        player.RemoveChips(player.BetAmount);
        bank.AddToBank(betAmount);
        currentBet = betAmount;
    }

    private void FirstBettingRound()
    {
        foreach (var player in players.Where(p => p.IsActive))
        {
            gameUI.DisplayHeader($"First betting round! Bank: [yellow]{bank.TotalChips}[/]");
            gameUI.DisplayPlayers(players);
            gameUI.DisplayCards(player.Hand);
            gameUI.GetPlayerActionFirstBettingRound(player, this);
            AnsiConsole.Clear();
        }
    }

    private void BettingRound(string round)
    {
        foreach (var player in players.Where(p => p.IsActive))
        {
            gameUI.DisplayHeader($"{round} Bank: [yellow]{bank.TotalChips}[/]");
            gameUI.DisplayPlayers(players);
            gameUI.DisplayCards(dealer.GetCommunityCards);
            gameUI.DisplayCards(player.Hand);
            gameUI.GetPlayerAction(player, this);
            AnsiConsole.Clear();
        }
    }

    private void DetermineWinner()
    {
        var activePlayers = players.Where(p => p.IsActive).ToList();

        if (activePlayers.Count > 1)
        {
            Player winner = activePlayers.First();
            HandRanking winnerHandRanking = EvaluateHand(winner.Hand.Concat(dealer.GetCommunityCards).ToList());

            foreach (var player in activePlayers.Skip(1))
            {
                HandRanking playerHandRanking = EvaluateHand(player.Hand.Concat(dealer.GetCommunityCards).ToList());

                if (playerHandRanking > winnerHandRanking || (playerHandRanking == winnerHandRanking && CompareHands(player, winner) > 0))
                {
                    winner = player;
                    winnerHandRanking = playerHandRanking;
                }
            }

            int winnings = bank.GetTotalChips();
            winner.AddChips(winnings);
            gameUI.DisplayMessage($"[green]Player [white]{winner.Name}[/] wins [yellow]{winnings}[/] chips with {winnerHandRanking}![/]");
            bank.ClearBank();
        }
        else
        {
            gameUI.DisplayMessage($"[green]Player [white]{activePlayers.First().Name}[/] wins![/]");
            throw new PokerGameException("Not enough active players to determine the winner.");
        }
    }

    private int CompareHands(Player player1, Player player2)
    {
        List<Card> combinedCards1 = player1.Hand.Concat(dealer.GetCommunityCards).ToList();
        List<Card> combinedCards2 = player2.Hand.Concat(dealer.GetCommunityCards).ToList();

        combinedCards1 = combinedCards1.OrderByDescending(card => card.Rank).ToList();
        combinedCards2 = combinedCards2.OrderByDescending(card => card.Rank).ToList();

        for (int i = combinedCards1.Count - 1; i >= 0; i--)
        {
            int comparisonResult = combinedCards1[i].Rank.CompareTo(combinedCards2[i].Rank);

            if (comparisonResult != 0)
            {
                return comparisonResult;
            }
        }

        return 0;
    }
}