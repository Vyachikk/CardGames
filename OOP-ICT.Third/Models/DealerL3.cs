using System.Collections.ObjectModel;
using OOP_ICT.Exceptions;
using OOP_ICT.Models;

namespace OOP_ICT.Third.Models;

public class DealerL3 : Dealer
{
    private List<Card> _inHandCards;

    public ReadOnlyCollection<Card> InHandCards
    {
        get { return _inHandCards.AsReadOnly(); }
    }

    public DealerL3(CardDeck deck) : base(deck)
    {
        _inHandCards = new List<Card>();
    }

    public DealerL3() : this(new CardDeck())
    {
    }

    public void InitCards()
    {
        ShuffleDeck();
    }

    public void TakeCards(int amount)
    {
        if (amount < 0)
            throw new NegativeNumArgumentException("amount");
        _inHandCards.AddRange(GiveCards(amount));
    }

    public void ClearCards()
    {
        _inHandCards.Clear();
    }
}