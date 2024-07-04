using OOP_ICT.Enums;
using OOP_ICT.Models;
using Xunit;
using OOP_ICT.Third.Services;
using OOP_ICT.Third.Models;
using Xunit.Abstractions;

namespace OOP_ICT.Third.Tests;

public class BlackJackTestsL3
{
    private readonly ITestOutputHelper _testOutputHelper;
    private PlayerGeneratorService playersGenerator = new PlayerGeneratorService();
    private CardGeneratorService cardsGenerator = new CardGeneratorService();
    private Random random = new Random();
    private readonly string dumpFilePath = Path.Combine(Path.GetTempPath(), "test_blackjack_casino_Dump.json");

    public BlackJackTestsL3(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void CardServiceTest()
    {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(Rank.Two, Suit.Clubs));
        cards.Add(new Card(Rank.King, Suit.Spades));
        CardService service = new CardService();
        int sum = CardService.CardCount(cards);
        Assert.Equal(12, sum);
    }

    [Fact]
    public void CardServiceTestNum2()
    {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(Rank.Four, Suit.Clubs));
        cards.Add(new Card(Rank.Ace, Suit.Spades));
        CardService service = new CardService();
        int sum = CardService.CardCount(cards);
        Assert.Equal(15, sum);
    }

    [Fact]
    public void CheckIfHasMoreThan21Point()
    {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(Rank.Ace, Suit.Clubs));
        cards.Add(new Card(Rank.Ace, Suit.Spades));
        Assert.True(BlackJackCasinoL3.CheckCardSumIsMoreThan21Point(cards));
    }

    [Fact]
    public void CheckIfHasLessThan21Point()
    {
        List<Card> cards = new List<Card>();
        cards.Add(new Card(Rank.Ace, Suit.Clubs));
        cards.Add(new Card(Rank.Ten, Suit.Spades));
        Assert.False(BlackJackCasinoL3.CheckCardSumIsMoreThan21Point(cards));
    }

    [Fact]
    public void RemoveCards_AllPlayersCardsShouldBeEmpty()
    {
        // Arrange
        var players = playersGenerator.GeneratePlayers(10);
        var game = new BlackJackCasinoL3("TestCasino", 0);

        // Act
        game.RemoveCards(players);

        // Assert
        foreach (var player in players)
        {
            Assert.Empty(player.Cards);
        }
    }

    [Fact]
    public void RemoveCards_WithEmptyPlayerList_ShouldNotThrow()
    {
        // Arrange
        var players = new List<PlayerCasinoL3>();
        var game = new BlackJackCasinoL3("TestCasino", 0);

        // Act & Assert
        var exception = Record.Exception(() => game.RemoveCards(players));
        Assert.Null(exception);
    }

    [Fact]
    public void RemoveCards_WithNullPlayers_ShouldThrowArgumentNullException()
    {
        // Arrange
        var game = new BlackJackCasinoL3("TestCasino", 0);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => game.RemoveCards(null));
    }

    [Fact]
    public void MakeBets_WithNullPlayers_ShouldThrowNullReferenceException()
    {
        // Arrange
        var casino = new BlackJackCasinoL3("TestCasino", 0);

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => casino.MakeBets(null));
    }

    [Fact]
    public void MakeBets_WithPlayers_ShouldSetBets()
    {
        // Arrange
        var casino = new BlackJackCasinoL3("TestCasino", 0);
        var players = new List<PlayerCasinoL3>
        {
            new PlayerCasinoL3("Player1", 1000),
            new PlayerCasinoL3("Player2", 500)
        };

        // Act
        casino.MakeBets(players);

        // Assert
        Assert.All(players, player => Assert.NotEqual(0, player.Bet));
    }

    [Fact]
    public void GiveCards_WithNullPlayers_ShouldThrowNullReferenceException()
    {
        // Arrange
        var casino = new BlackJackCasinoL3("TestCasino", 0);
        var dealer = new DealerL3(); 

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => casino.GiveCards(dealer, null));
        Assert.Throws<NullReferenceException>(() => casino.GiveCards(null, null));
        Assert.Throws<NullReferenceException>(() => casino.GiveCards(null, new List<PlayerCasinoL3>()));
    }

    [Fact]
    public void GiveCards_WithPlayers_ShouldAddCards()
    {
        var casino = new BlackJackCasinoL3("TestCasino", 0);
        var dealer = new DealerL3();
        var players = new List<PlayerCasinoL3>
        {
            new PlayerCasinoL3("Player1", 1000) { Cards = new List<Card>()  },
            new PlayerCasinoL3("Player2", 500) { Cards = new List<Card>()  }
        };

        casino.GiveCards(dealer, players);


        Assert.All(players, player => Assert.True(CardService.CardCount(player.Cards) >= 17));
    }

    [Fact]
    public void CalculateWinnings_WithTie_ShouldNotChangeBalance()
    {
        var casino = new BlackJackCasinoL3("TestCasino", 0);
        var dealer = new DealerL3();
        dealer.TakeCards(2);
        while (CardService.CardCount(new List<Card>(dealer.InHandCards)) < 17)
        {
            dealer.TakeCards(1);
        }

        var playersGenerator = new PlayerGeneratorService();
        var players = playersGenerator.GeneratePlayers(5);
        var playerInitialBalances = players.Select(player => player.Balance).ToList();

        casino.MakeBets(players);
        //casino.GiveCards(dealer, players);
        
        foreach (var player in players)
        {
            player.Cards = new List<Card>(dealer.InHandCards);
            // Здесь код для выставления карт игрокам
        }

        casino.CalculateWinnings(dealer, players);

        for (int i = 0; i < players.Count; i++)
        {
            Assert.Equal(playerInitialBalances[i], players[i].Balance); // Баланс должен остаться неизменным
        }
    }


    [Fact]
    public void CalculateWinnings_WithPlayers_ShouldUpdateWinOrLoss()
    {
        int testsCount = 10000;

        for (int testIndex = 0; testIndex < testsCount; testIndex++)
        {
            int casinoBalance = Int32.MaxValue / 2;
            // Arrange
            var casino = new BlackJackCasinoL3("TestCasino", casinoBalance);
            var dealer = new DealerL3();
            dealer.TakeCards(2);
            var players = playersGenerator.GeneratePlayers(random.Next(2, 7));
            var playerInitial = players.Select(player => new PlayerCasinoL3(player)).ToList();
            casino.MakeBets(players);
            foreach (var player in players)
            {
                Assert.True(player.Bet > 0);
            }

            while (CardService.CardCount(new List<Card>(dealer.InHandCards)) < 17)
            {
                casino.GiveCards(dealer, players);
                dealer.TakeCards(1);
            }

            foreach (var player in players)
            {
                Assert.True(player.Cards.Any());
            }

            // Act
            casino.CalculateWinnings(dealer, players);
            var dealerscore = CardService.CardCount(new List<Card>(dealer.InHandCards));
            // Assert
            Assert.True(dealerscore >= 17);
            Assert.Equal(players.Count, playerInitial.Count);
            int sum = 0;
            for (int i = 0; i < players.Count; i++)
            {
                Assert.Equal(players[i].Name, playerInitial[i].Name);
                int playerScore = CardService.CardCount(players[i].Cards);
                if (playerScore == dealerscore)
                    Assert.Equal(players[i].Balance, playerInitial[i].Balance);
                if (playerScore < dealerscore)
                {
                    Assert.Equal(players[i].Balance, playerInitial[i].Balance - players[i].Bet);
                    sum += players[i].Bet;
                }

                if (playerScore > dealerscore)
                {
                    Assert.Equal(players[i].Balance, playerInitial[i].Balance + players[i].Bet);
                    sum -= players[i].Bet;
                }
            }

            Assert.Equal(casinoBalance + sum, casino.Balance);
        }
    }

    [Fact]
    public void StartTest_CasinoZeroBalance_ShouldThrowArgumentOutOfRangeException()
    {
        var casino = new BlackJackCasinoL3("TestCasino", 0);
        var players = playersGenerator.GeneratePlayers(random.Next(2, 7));
        Assert.Throws<ArgumentOutOfRangeException>(() => casino.Start(players));
    }

    [Fact]
    public void StartTest_SomePlayerZeroBalance_ShouldThrowArgumentOutOfRangeException()
    {
        var casino = new BlackJackCasinoL3("TestCasino", 1000);
        var players = new List<PlayerCasinoL3>();
        players.Add(new PlayerCasinoL3("Player1", 500));
        players.Add(new PlayerCasinoL3("Player2", 0));
        players.Add(new PlayerCasinoL3("Player3", 100));
        Assert.Throws<ArgumentOutOfRangeException>(() => casino.Start(players));
    }

    [Fact]
    public void StartTest_ManyOneTimeGames()
    {
        int testsCount = 1000;
        for (int testIndex = 0; testIndex < testsCount; testIndex++)
        {
            int casinoBalance = Int32.MaxValue / 2;
            var casino = new BlackJackCasinoL3("TestCasino", casinoBalance);
            var players = playersGenerator.GeneratePlayers(random.Next(2, 7));
            var playerInitial = players.Select(player => new PlayerCasinoL3(player)).ToList();
            casino.Start(players, false);
            Assert.NotNull(casino.CurrentPlayers);
            Assert.Equal(players.Count, casino.CurrentPlayers.Count);
            Assert.Equal(players, casino.CurrentPlayers);
            var dealerscore = CardService.CardCount(new List<Card>(casino.Dealer.InHandCards));
            Assert.True(dealerscore >= 17);
            int sum = 0;
            for (int i = 0; i < players.Count; i++)
            {
                Assert.Equal(players[i].Name, playerInitial[i].Name);
                Assert.True(players[i].Cards.Count > 0);
                int playerScore = CardService.CardCount(players[i].Cards);
                if (playerScore == dealerscore)
                    Assert.Equal(players[i].Balance, playerInitial[i].Balance);
                if (playerScore < dealerscore)
                {
                    Assert.Equal(players[i].Balance, playerInitial[i].Balance - players[i].Bet);
                    sum += players[i].Bet;
                }

                if (playerScore > dealerscore)
                {
                    Assert.Equal(players[i].Balance, playerInitial[i].Balance + players[i].Bet);
                    sum -= players[i].Bet;
                }
            }

            Assert.Equal(casinoBalance + sum, casino.Balance);
        }
    }

    [Fact]
    public void StartTest_ManyMultiTimeGames()
    {
        int roundsCount = 3;
        for (int round = 0; round < roundsCount; round++)
        {
            int testsCount = 1000;
            int casinoBalance = Int32.MaxValue / 2;
            var casino = new BlackJackCasinoL3("TestCasino", casinoBalance);
            var players = playersGenerator.GeneratePlayers(random.Next(2, 7));
            for (int testIndex = 0; testIndex < testsCount; testIndex++)
            {
                casino.RemoveCards(players);
                int casinoBalancePrev = casino.Balance;
                foreach (var player in players)
                {
                    if (player.Balance == 0)
                        player.Balance += 100;
                }


                var playerInitial = players.Select(player => new PlayerCasinoL3(player)).ToList();

                //int playersBalances = 0;
                //foreach (var player in players)
                //{
                    //playersBalances += player.Balance;
                //}
                
                //if (casino.Balance <= playersBalances)
                //{
                    //_testOutputHelper.WriteLine("Reset casino balance");
                    //casino.Balance = casinoBalance;
                    //casinoBalancePrev = casinoBalance;
                //}
                //try
                //{
                    casino.Start(players, false);
                //}
                //catch (Exception ex)
                //{
                    //Console.WriteLine(ex.Message);
                    //SaveService.SaveToFile(casino, this.dumpFilePath);
                //}

                var dealerScore = CardService.CardCount(new List<Card>(casino.Dealer.InHandCards));
                Assert.True(dealerScore >= 17);
                int sum = 0;
                for (int i = 0; i < players.Count; i++)
                {
                    Assert.Equal(players[i].Name, playerInitial[i].Name);
                    Assert.True(players[i].Cards.Count > 0);
                    int playerScore = CardService.CardCount(players[i].Cards);
                    if (playerScore == dealerScore)
                        Assert.Equal(players[i].Balance, playerInitial[i].Balance);
                    if (playerScore < dealerScore)
                    {
                        Assert.Equal(players[i].Balance, playerInitial[i].Balance - players[i].Bet);
                        sum += players[i].Bet;
                    }

                    if (playerScore > dealerScore)
                    {
                        Assert.Equal(players[i].Balance, playerInitial[i].Balance + players[i].Bet);
                        sum -= players[i].Bet;
                    }
                }

                Assert.Equal(casinoBalancePrev + sum, casino.Balance);
                casino.RemoveCards(players);
            }
        }
    }

    [Fact]
    public void StartTest_ManyOneTimeGames_ClearCards()
    {
        int testsCount = 1000;
        int casinoBalance = Int32.MaxValue / 2;
        int casinoBalanceCurrent = 0;
        for (int testIndex = 0; testIndex < testsCount; testIndex++)
        {
            var casino = new BlackJackCasinoL3("TestCasino", casinoBalance);
            var players = playersGenerator.GeneratePlayers(random.Next(2, 7));
            var playerInitial = players.Select(player => new PlayerCasinoL3(player)).ToList();
            casino.Start(players);
            var dealerscore = CardService.CardCount(new List<Card>(casino.Dealer.InHandCards));
            Assert.True(dealerscore >= 17);
            int sum = 0;
            for (int i = 0; i < players.Count; i++)
            {
                Assert.Equal(players[i].Name, playerInitial[i].Name);
                Assert.True(players[i].Cards.Count == 0);
            }

            casinoBalanceCurrent = casino.Balance;
        }

        Assert.NotEqual(casinoBalance, casinoBalanceCurrent);
    }
}