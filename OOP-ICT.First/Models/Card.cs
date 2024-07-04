using OOP_ICT.Enums;

namespace OOP_ICT.Models;

public class Card : IComparable<Card>
{
    public readonly Rank cardRank;
    public readonly Suit cardSuit;

    public Card(Rank rank, Suit suit)
    {
        cardRank = rank;
        cardSuit = suit;
    }
    
    public Card(Card other)
    {
        cardRank = other.cardRank;
        cardSuit = other.cardSuit;
    }

    public int Rank { get; set; }
    public object Suit { get; set; }

    public int CompareTo(Card other)
    {
        var suitComparison = cardSuit.CompareTo(other.cardSuit);
        if (suitComparison != 0) return suitComparison;
        return cardRank.CompareTo(other.cardRank);
    }
    
    
    public override string ToString()
    {
        return $"({cardRank}, {cardSuit})";
    }
}