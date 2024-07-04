using OOP_ICT.Enums;
using OOP_ICT.Fourth.Models;
using OOP_ICT.Models;
using OOP_ICT.Third.Models;
using Xunit;

namespace OOP_ICT.Fourth.Tests;

public class CardDeckL4Tests
{
    private DealerL4 dealer;

    public CardDeckL4Tests()
    {
        dealer = new DealerL4();
    }
    
    [Fact]
    public void CheckPairs()
    {
        CardDeckL4 cardDeckL4 = new CardDeckL4(dealer.GiveCards(5));
        cardDeckL4.GetType();
    }

    [Fact]
    public void AddSingleCard_ShouldIncreaseCardCount()
    {
        // Arrange
        var cardDeck = new CardDeckL4();
        var card = new Card(Rank.Ace, Suit.Hearts);

        // Act
        cardDeck.Add(card);

        // Assert
        Assert.Equal(1, cardDeck.Cards.Count);
        Assert.Equal(card, cardDeck.Cards[0]);
    }

    [Fact]
    public void AddMultipleCards_ShouldIncreaseCardCount()
    {
        // Arrange
        var cardDeck = new CardDeckL4();
        var cards = new List<Card> { new Card(Rank.Ace, Suit.Hearts), new Card(Rank.King, Suit.Diamonds) };

        // Act
        cardDeck.Add(cards);

        // Assert
        Assert.Equal(cards.Count, cardDeck.Cards.Count);
        Assert.Equal(cards, cardDeck.Cards);
    }

    [Fact]
    public void GetScore_ShouldReturnCorrectScore()
    {
        // Arrange
        var cardDeck = new CardDeckL4(new List<Card> { new Card(Rank.Ace, Suit.Hearts), new Card(Rank.King, Suit.Diamonds) });

        // Act
        var score = cardDeck.Score;

        // Assert
        Assert.Equal((int)Rank.Ace + (int)Rank.King, score);
    }

    [Fact]
    public void GetPokerType_WithLessThanFiveCards_ShouldReturnNone()
    {
        // Arrange
        var cardDeck = new CardDeckL4(new List<Card> { new Card(Rank.Ace, Suit.Hearts), new Card(Rank.King, Suit.Diamonds) });

        // Act
        var pokerType = cardDeck.PokerType;

        // Assert
        Assert.Equal(CardDeckPokerType.None, pokerType);
    }
}