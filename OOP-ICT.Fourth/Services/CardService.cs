using OOP_ICT.Enums;
using OOP_ICT.Models;

public class CardService
{
    public static int CardCount(List<Card> cards)
    {
        if (cards == null)
            return 0;
        int sum = 0;
        foreach (var card in cards)
        {
            sum += (int)card.cardRank;
        }

        return sum;
    }

    private static void GetCardDeckInfo(List<Card> cards)
    {
        Dictionary<Suit, int> suitsCount = new Dictionary<Suit, int>();
        foreach (var card in cards)
        {
            if (!suitsCount.ContainsKey(card.cardSuit))
                suitsCount.Add(card.cardSuit, 1);
            else
                suitsCount[card.cardSuit]++;
        }
    }
    
    
}