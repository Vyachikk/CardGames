using OOP_ICT.Enums;
using OOP_ICT.Models;

public class CardGeneratorServiceL4
{
    private readonly Random _random;

    public CardGeneratorServiceL4()
    {
        _random = new Random();
    }

    public Card GenerateRandomCard()
    {
        var rank = (Rank)_random.Next(2, Enum.GetNames(typeof(Rank)).Length);
        var suit = (Suit)_random.Next(0, Enum.GetNames(typeof(Suit)).Length);
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