using OOP_ICT.Interfaces;
using OOP_ICT.Third.Services;

namespace OOP_ICT.Models;

public class Dealer : IDealer
{
    private IReadOnlyList<Card> _cards;
    private CardDeck _deck;

    public Dealer(CardDeck deck)
    {
        _deck = deck;
    }

    public IReadOnlyList<Card> SeeCards()
    {
        return _deck.Cards.ToList();
    }

    public void ShuffleDeck()
    {
        _cards = _deck.Cards.ToList();
        _cards = ShuffleService.Shuffle((List<Card>)_cards);
        _deck.Cards = _cards.ToList();
    }

    public List<Card> GiveCards(int n)
    {
        return _deck.GiveCards(n);
    } 
}