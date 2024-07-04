using OOP_ICT.Enums;
using OOP_ICT.Models;

namespace OOP_ICT.Third.Services;

public class CardGeneratorService
{
    private readonly Random _random;

    public CardGeneratorService()
    {
        _random = new Random();
    }

    public Card GenerateRandomCard()
    {
        var rank = (Rank)_random.Next(1, Enum.GetNames(typeof(Rank)).Length + 1);
        var suit = (Suit)_random.Next(1, Enum.GetNames(typeof(Suit)).Length + 1);
        return new Card(rank, suit);
    }

    public List<Card> GenerateRandomCards(int numberOfCards)
    {
        var cards = new List<Card>();
        for (int i = 0; i < numberOfCards; i++)
        {
            cards.Add(GenerateRandomCard());
        }
        return cards;
    }
}