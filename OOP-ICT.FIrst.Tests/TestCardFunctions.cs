using OOP_ICT.Exceptinos;
using OOP_ICT.Models;
using Xunit;

namespace OOP_ICT.FIrst.Tests;

public class TestCardFunctions
{
    [Fact]
    public void InitializeCardDeckTest()
    {
        var dealer = new Dealer(new CardDeck());
        Assert.NotNull(dealer.GiveCards(52));
        Assert.Throws<NotEnoughCardException>(() => dealer.GiveCards(53));
    }

    [Fact]
    public void DeckIsShuffledTest()
    {
        var dealer = new Dealer(new CardDeck());

        var cardsInitial = dealer.SeeCards();
        dealer.ShuffleDeck();
        var cardsShuffled = dealer.SeeCards();
        
        Assert.NotEqual(cardsInitial, cardsShuffled);
    }
    
    [Fact]
    public void DealerHasLessCardsTest()
    {
        var dealer = new Dealer(new CardDeck());

        Assert.NotNull(dealer.GiveCards(51));
        Assert.NotNull(dealer.GiveCards(1));
        Assert.Throws<NotEnoughCardException>(() => dealer.GiveCards(1));
    }
}