using OOP_ICT.Enums;
using OOP_ICT.Exceptinos;
using OOP_ICT.Interfaces;

namespace OOP_ICT.Models;

public class CardDeck
{
    private List<Card> _cards;

    public List<Card> Cards
    {
        get => _cards;
        set => _cards = value;
    }

    public CardDeck()
    {
        _cards = Enum.GetValues(typeof(Suit))
            .Cast<Suit>()
            .SelectMany(suit => Enum.GetValues(typeof(Rank)).Cast<Rank>()
                .Select(rank => new Card(rank, suit)))
            .ToList();
    }
    
    public List<Card> GiveCards(int n)
    {
        if (_cards.Count < n)
        {
            throw new NotEnoughCardException();
        }
        
        List<Card> selectedCards = _cards.Take(n).ToList();
        _cards = _cards.SelectMany(card => selectedCards.Contains(card) ? new List<Card>() : new List<Card> { card }).ToList();

        Console.WriteLine(selectedCards.Count);

        return selectedCards;
    }
}