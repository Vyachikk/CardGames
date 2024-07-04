using OOP_ICT.Models;

namespace OOP_ICT.Third.Services;

public class ShuffleService
{
    public static IReadOnlyList<Card> Shuffle(List<Card> cards)
    {
        List<Card> shuffledDeck = new List<Card>(cards);
        Random random = new Random();

        int n = shuffledDeck.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Card value = shuffledDeck[k];
            shuffledDeck[k] = shuffledDeck[n];
            shuffledDeck[n] = value;
        }

        return shuffledDeck;
    }
}