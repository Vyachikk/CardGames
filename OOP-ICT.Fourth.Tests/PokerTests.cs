using OOP_ICT.Enums;
using OOP_ICT.Fourth.Models;
using OOP_ICT.Models;
using Xunit;

namespace OOP_ICT.Fourth.Tests;

public class PokerTests
{
    [Fact]
    public void Constructor_WithNameAndBalance_CreatesPokerCasinoObject()
    {
        // Arrange
        string name = "Test Casino";
        int balance = 1000;

        // Act
        PokerCasino casino = new PokerCasino(name, balance);

        // Assert
        Assert.Equal(name, casino.Name);
        Assert.Equal(balance, casino.Balance);
    }

    [Fact]
    public void DefaultConstructor_CreatesPokerCasinoObjectWithDefaultValues()
    {
        // Arrange & Act
        PokerCasino casino = new PokerCasino();

        // Assert
        Assert.Equal("Casino Royal", casino.Name);
        Assert.Equal(0, casino.Balance);
    }

    [Fact]
    public void IsEnoughCards_WithNullPlayers_ShouldReturnFalse()
    {
        // Arrange
        var pockerGame = new PokerCasino();
        List<PlayerPoker> players = null;

        // Act
        var result = pockerGame.IsEnoughCards(players);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void RemoveCards_ShouldRemoveCardsFromPlayers()
    {
        // Arrange
        var pockerGame = new PokerCasino();
        var player1 = new PlayerPoker("Player1");
        var player2 = new PlayerPoker("Player2");
        List<PlayerPoker> players = new List<PlayerPoker> { player1, player2 };

        // Act
        pockerGame.RemoveCards(players);

        // Assert
        Assert.Empty(player1.Cards);
        Assert.Empty(player2.Cards);
    }

    [Fact]
    public void GiveCards_ShouldGiveCardsToPlayersUntilTheyHaveEnough()
    {
        // Arrange
        var pockerGame = new PokerCasino();
        var dealer = new DealerL4();
        var player1 = new PlayerPoker("Player1");
        var player2 = new PlayerPoker("Player2");

        List<PlayerPoker> players = new List<PlayerPoker> { player1, player2 };
        player1.Cards = new List<Card> { new Card(Rank.Ace, Suit.Hearts), new Card(Rank.King, Suit.Diamonds) };
        player2.Cards = new List<Card> { new Card(Rank.Ten, Suit.Clubs) };

        // Act
        pockerGame.GiveCards(dealer, players);

        // Assert
        Assert.True(player1.IsEnough());
        Assert.True(player2.IsEnough());
        Assert.NotEmpty(player1.Cards);
        Assert.NotEmpty(player2.Cards);
    }

    [Fact]
    public void MakeBets_ShouldSetBetsForPlayers()
    {
        // Arrange
        var pockerGame = new PokerCasino();
        var player1 = new PlayerPoker("Player1");
        var player2 = new PlayerPoker("Player2");
        List<PlayerPoker> players = new List<PlayerPoker> { player1, player2 };

        // Act
        pockerGame.MakeBets(players);

        // Assert
        Assert.InRange(player1.Bet, 0, player1.Balance / 2);
        Assert.InRange(player2.Bet, 0, player2.Balance / 2);
    }
}
    