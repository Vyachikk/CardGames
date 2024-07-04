using System.Diagnostics.CodeAnalysis;
using OOP_ICT.Enums;
using OOP_ICT.Models;
using OOP_ICT.Third.Services;

namespace OOP_ICT.Fourth.Models;

[SuppressMessage("ReSharper", "CommentTypo")]
public enum CardDeckPokerType
{
    /// <summary>
    ///  Старшая карта
    ///  Колода состоит из пяти разных карт
    /// </summary>
    HighCard,

    /// <summary>
    ///  Пара карт одного достоинства
    /// </summary>
    Pair,

    /// <summary>
    /// Две пары карт одного достоинства
    /// </summary>
    TwoPairs,

    /// <summary>
    ///  Три карты одного достоинства
    /// </summary>
    ThreeOfAKind,

    /// <summary>
    /// Последовательность из пяти карт разной масти 
    /// </summary>
    Straight,

    /// <summary>
    /// пять карт одинаковой масти 
    /// </summary>
    Flush,

    /// <summary>
    /// Одна пара+одна тройка
    /// </summary>
    FullHouse,

    /// <summary>
    /// Четыре карты одного достоинства
    /// </summary>
    FourOfAKind,

    /// <summary>
    /// Стрит из пяти кард одной масти
    /// </summary>
    StraightFlush,

    /// <summary>
    /// Карты одинаковой масти от 10 до туза
    /// </summary>
    RoyalFlush,

    /// <summary>
    /// Не возможно установить тип комбинации
    /// </summary>
    None
}

public class CardDeckL4 : CardDeck
{
    private Dictionary<Suit, int> suitAmount;
    private Dictionary<Rank, int> rankAmount;

    public CardDeckPokerType PokerType
    {
        get => GetPockerType();
    }

    public int Score
    {
        get => GetScore();
    }

    public CardDeckL4()
    {
        this.Cards = new List<Card>();
        this.suitAmount = new Dictionary<Suit, int>();
    }

    public CardDeckL4(List<Card> cards) : this()
    {
        Cards = cards;
    }

    public void Add(Card card)
    {
        Cards.Add(card);
    }

    public void Add(List<Card> cards)
    {
        Cards.AddRange(cards);
    }

    public int GetScore()
    {
        int score = 0;
        foreach (var card in Cards)
            score += (int)card.cardRank;
        return score;
    }

    CardDeckPokerType GetPockerType()
    {
        if (Cards.Count < 5)
            return CardDeckPokerType.None;

        foreach (var card in Cards)
        {
            if (!suitAmount.ContainsKey(card.cardSuit))
            {
                suitAmount.Add(card.cardSuit, 1);
            }
            else
            {
                suitAmount[card.cardSuit]++;
            }

            if (!rankAmount.ContainsKey(card.cardRank))
            {
                rankAmount.Add(card.cardRank, 1);
            }
            else
            {
                rankAmount[card.cardRank]++;
            }
        }
        return CardDeckPokerType.None;
    }
}