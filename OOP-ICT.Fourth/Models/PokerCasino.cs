using OOP_ICT.Exceptions;
using OOP_ICT.Fourth.Enums;
using OOP_ICT.Fourth.Exceptions;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;


namespace OOP_ICT.Fourth.Models;

public class PokerCasino
{

    #region Поля

    private string _name;
    private bool isStarted;
    private DealerL4 _dealerL4;
    private const int lowWinScore = 17;
    private const int highWinScore = 21;

    #endregion

    #region Свойства
    public string Name
    {
        get => _name;
        set
        {
            if (String.IsNullOrEmpty(value))
                throw new EmptyStringFieldException("Name");
            _name = value;
        }
    }
    public Bank Bank = new Bank(1000);
    public int Balance
    {
        get { return Bank != null ? Bank.Balance : 0; }
        set
        {
            if (Bank == null)
                throw new NullReferenceException("Bank is not initialized");
            Bank.Balance = value;
        }
    }
    public List<PlayerPoker> CurrentPlayers
    {
        set;
        get;
    }

    public DealerL4 Dealer
    {
        get { return _dealerL4; }
    }

    #endregion

    #region Конструкторы

    public PokerCasino():this("Casino Royal",0)
    {
        
    }

    public PokerCasino(string name, int balance)
    {
        isStarted = false;
        _dealerL4 = new DealerL4();
        this.Name = name;
        Bank = new Bank(balance);
    }

    #endregion

    #region Методы

    public bool IsEnoughCards(List<PlayerPoker> players)
    {
        if (Dealer == null || players == null)
            return false;
        int sum = 0;
        foreach (var player in players)
        {
            if (player.Cards.Count() > 1)
                sum += 1;
            else
                sum += 2;
        }

        return Dealer.SeeCards().Count >= sum;
    }

    public void RemoveCards(List<PlayerPoker> players)
    {
        if (players == null)
            throw new ArgumentNullException();
        foreach (var player in players)
        {
            player.Cards.Clear();
        }
    }

    public void MakeBets(List<PlayerPoker> players)
    {
        if (players == null)
            throw new NullReferenceException();
        foreach (var player in players)
            player.Bet = player.MakeBet();
    }

    public void GiveCards(DealerL4 dealer, IEnumerable<PlayerPoker> players)
    {
        if (players == null || dealer == null)
            throw new NullReferenceException();

        foreach (var player in players)
            while (!player.IsEnough())
                player.Cards.AddRange(dealer.GiveCards(1));
    }

    public void CalculateWinnings(DealerL4 dealer, List<PlayerPoker> players)
    {
        if (players == null || dealer == null)
            throw new NullReferenceException();
        int dealerScore = CardService.CardCount(new List<Card>(dealer.InHandCards));
        HandType dealerHand = DetermineHandType(new List<Card>(dealer.InHandCards));
        if (dealerHand == HandType.HighCard && dealerScore < lowWinScore)
            throw new ArgumentOutOfRangeException("dealerScore", dealerScore, "Dealer doesn't have enough score");

        for (int i = 0; i < players.Count; i++)
        {
            int playersScore = CardService.CardCount(players[i].Cards);
            HandType playerHand = DetermineHandType(players[i].Cards);
            int result = CompareHands(dealerHand, playerHand);
            CountWinOrLoss(players[i], result);
        }
    }

    private int CompareHands(HandType dealerHand, HandType playerHand)
    {
        if (playerHand > dealerHand)
            return 1;
        else if (playerHand < dealerHand)
            return -1;
        else
        {
            List<Card> dealerCards = new List<Card>(Dealer.InHandCards);
            List<Card> playerCards = new List<Card>(CurrentPlayers[0].Cards);

            switch (dealerHand)
            {
                case HandType.RoyalFlush:
                case HandType.StraightFlush:
                case HandType.Straight:
                    return CompareHighestCard(dealerCards, playerCards);
                case HandType.FourOfAKind:
                case HandType.ThreeOfAKind:
                case HandType.FullHouse:
                    return CompareSameRankCards(dealerCards, playerCards, 3);
                case HandType.TwoPair:
                    return CompareTwoPair(dealerCards, playerCards);
                case HandType.Pair:
                    return CompareSameRankCards(dealerCards, playerCards, 2);
                case HandType.Flush:
                case HandType.HighCard:
                    return CompareHighestCard(dealerCards, playerCards);
                default:
                    return 0;
            }
        }
    }

    private int CompareHighestCard(List<Card> dealerCards, List<Card> playerCards)
    {
        int minCount = Math.Min(dealerCards.Count, playerCards.Count);
        for (int i = minCount - 1; i >= 0; i--)
        {
            if (dealerCards[i].Rank > playerCards[i].Rank)
                return -1;
            else if (dealerCards[i].Rank < playerCards[i].Rank)
                return 1;
        }
        return 0;
    }

    private int CompareSameRankCards(List<Card> dealerCards, List<Card> playerCards, int numberOfSameRank)
    {
        var dealerGroups = dealerCards.GroupBy(c => c.Rank);
        var playerGroups = playerCards.GroupBy(c => c.Rank);
        var dealerMaxRank = dealerGroups.FirstOrDefault(g => g.Count() == numberOfSameRank)?.Key ?? 0;
        var playerMaxRank = playerGroups.FirstOrDefault(g => g.Count() == numberOfSameRank)?.Key ?? 0;
        if (dealerMaxRank > playerMaxRank)
            return -1;
        else if (dealerMaxRank < playerMaxRank)
            return 1;
        else
            return CompareHighestCard(dealerCards, playerCards);
    }

    private int CompareTwoPair(List<Card> dealerCards, List<Card> playerCards)
    {
        var dealerGroups = dealerCards.GroupBy(c => c.Rank);
        var playerGroups = playerCards.GroupBy(c => c.Rank);
        var dealerPairs = dealerGroups.Where(g => g.Count() == 2).OrderByDescending(g => g.Key).ToList();
        var playerPairs = playerGroups.Where(g => g.Count() == 2).OrderByDescending(g => g.Key).ToList();

        if (dealerPairs.Count >= 2 && playerPairs.Count >= 2)
        {
            var dealerHighPair = dealerPairs[0].Key;
            var dealerLowPair = dealerPairs[1].Key;
            var playerHighPair = playerPairs[0].Key;
            var playerLowPair = playerPairs[1].Key;

            if (dealerHighPair > playerHighPair)
                return -1;
            else if (dealerHighPair < playerHighPair)
                return 1;
            else if (dealerLowPair > playerLowPair)
                return -1;
            else if (dealerLowPair < playerLowPair)
                return 1;
            else
                return CompareHighestCard(dealerCards, playerCards);
        }
        else
            return CompareSameRankCards(dealerCards, playerCards, 2);
    }

    public void Start(List<PlayerPoker> players, bool removeCards = true)
    {
        CurrentPlayers = players;
        if (players == null || players.Count == 0)
            throw new InvalidPlayersListException("Can't start game without players!");

        foreach (var player in players)
            if (player.Balance <= 0)
                throw new ArgumentOutOfRangeException(nameof(player.Balance), "Player cannot play with zero or negative balance!");

        if (Balance <= 0)
            throw new ArgumentOutOfRangeException(nameof(Balance), "Casino has insufficient funds!");

        _dealerL4 = new DealerL4();
        Dealer.InitCards();
        Dealer.TakeCards(2);

        isStarted = IsEnoughCards(players);
        MakeBets(players);

        while (isStarted)
        {
            isStarted = CardService.CardCount(new List<Card>(Dealer.InHandCards)) < lowWinScore;
            GiveCards(Dealer, players);
            if (isStarted)
                Dealer.TakeCards(1);
        }

        CalculateWinnings(Dealer, players);
        if (removeCards)
            RemoveCards(players);
        CurrentPlayers = null;
    }


    private static bool IsRoyalFlush(List<Card> cards)
    {
        return IsStraightFlush(cards) && cards.Any(c => c.Rank == 10);
    }

    private static bool IsStraightFlush(List<Card> cards)
    {
        return IsStraight(cards) && IsFlush(cards);
    }

    private static bool IsFourOfAKind(List<Card> cards)
    {
        return cards.GroupBy(c => c.Rank).Any(g => g.Count() == 4);
    }

    private static bool IsFullHouse(List<Card> cards)
    {
        return IsThreeOfAKind(cards) && IsPair(cards);
    }

    private static bool IsFlush(List<Card> cards)
    {
        return cards.GroupBy(c => c.Suit).Any(g => g.Count() == 5);
    }

    private static bool IsStraight(List<Card> cards)
    {
        var sortedCards = cards.OrderBy(c => c.Rank).ToList();
        for (int i = 0; i < sortedCards.Count - 1; i++)
        {
            if (sortedCards[i + 1].Rank - sortedCards[i].Rank != 1)
                return false;
        }
        return true;
    }

    private static bool IsThreeOfAKind(List<Card> cards)
    {
        return cards.GroupBy(c => c.Rank).Any(g => g.Count() == 3);
    }

    private static bool IsTwoPair(List<Card> cards)
    {
        var groups = cards.GroupBy(c => c.Rank);
        return groups.Count() == 3 && groups.All(g => g.Count() == 2);
    }

    private static bool IsPair(List<Card> cards)
    {
        return cards.GroupBy(c => c.Rank).Any(g => g.Count() == 2);
    }
    public static HandType DetermineHandType(List<Card> cards)
    {
        if (IsRoyalFlush(cards))
            return HandType.RoyalFlush;
        if (IsStraightFlush(cards))
            return HandType.StraightFlush;
        if (IsFourOfAKind(cards))
            return HandType.FourOfAKind;
        if (IsFullHouse(cards))
            return HandType.FullHouse;
        if (IsFlush(cards))
            return HandType.Flush;
        if (IsStraight(cards))
            return HandType.Straight;
        if (IsThreeOfAKind(cards))
            return HandType.ThreeOfAKind;
        if (IsTwoPair(cards))
            return HandType.TwoPair;
        if (IsPair(cards))
            return HandType.Pair;
        
        return HandType.HighCard;
    }

    public void CountWinOrLoss(PlayerPoker playerPoker, int result)
    {
        if (result > 0)
        {
            playerPoker.Bank.Balance += playerPoker.Bet;
            Bank.Balance -= playerPoker.Bet;
        }
        else if (result < 0)
        {
            playerPoker.Bank.Balance -= playerPoker.Bet;
            Bank.Balance += playerPoker.Bet;
        }
    }
    #endregion
}