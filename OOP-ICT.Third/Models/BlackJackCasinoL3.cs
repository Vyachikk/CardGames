using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using OOP_ICT.Third.Services;
using OOP_ICT.Third.Exceptions;
using Newtonsoft.Json;

namespace OOP_ICT.Third.Models;

public class BlackJackCasinoL3 : BlackJackCasinoL2
{
    private bool isStarted;
    private DealerL3 _dealerL3;
    private const int lowWinScore = 17;
    private const int highWinScore = 21;

    public List<PlayerCasinoL3> CurrentPlayers
    {
        set;
        get;
    }
    public DealerL3 Dealer
    {
        get { return _dealerL3; }
    }
    
    public BlackJackCasinoL3():this("BlackJackCasinoL3", 0)
    {
        
    }

    public BlackJackCasinoL3(string name, Bank bank) : base(name, bank)
    {
        Init();
    }

    public BlackJackCasinoL3(string name, int balance) : base(name, balance)
    {
        Init();
    }

    public void Init()
    {
        isStarted = false;
        _dealerL3 = new DealerL3();
    }

    public bool IsEnoughCards(List<PlayerCasinoL3> players)
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

    public void RemoveCards(List<PlayerCasinoL3> players)
    {
        if (players == null)
            throw new ArgumentNullException();
        foreach (var player in players)
        {
            player.Cards.Clear();
        }
    }

    public void MakeBets(List<PlayerCasinoL3> players)
    {
        if (players == null)
            throw new NullReferenceException();
        for (int i = 0; i < players.Count(); i++)
            players[i].Bet = players[i].MakeBet();
    }

    public void GiveCards(DealerL3 dealer, List<PlayerCasinoL3> players)
    {
        if (players == null || dealer == null)
            throw new NullReferenceException();
        for (int i = 0; i < players.Count; i++)
            while (!players[i].IsEnough())
                players[i].Cards.AddRange(dealer.GiveCards(1));
    }

    public void CalculateWinnings(DealerL3 dealer, List<PlayerCasinoL3> players)
    {
        if (players == null || dealer == null)
            throw new NullReferenceException();
        int dealerScore = CardService.CardCount(new List<Card>(dealer.InHandCards));
        if (dealerScore < lowWinScore)
            throw new ArgumentOutOfRangeException("dealerScore", dealerScore, "Dealer doesn't have enough score");
        for (int i = 0; i < players.Count; i++)
        {
            int playersScore = CardService.CardCount(players[i].Cards);
            CountWinOrLoss(players[i], playersScore, dealerScore);
        }
    }

    public void Start(List<PlayerCasinoL3> players, bool removeCards = true)
    {
        CurrentPlayers = players;
        if (removeCards)
            RemoveCards(players);

        #region Checks for start

        if (players == null)
            throw new InvalidPlayersListException("Can't start game without players!");
        if (Balance == 0)
            throw new ArgumentOutOfRangeException(nameof(Balance), "Casino cannot pay winnings!");
        foreach (var player in players)
        {
            if (player.Balance == 0)
                throw new ArgumentOutOfRangeException(nameof(player.Balance), "Player cannot pay debt!");
        }

        #endregion

        #region Main game loop

        _dealerL3 = new DealerL3();
        Dealer.TakeCards(2);
        isStarted = players.Count > 0 && IsEnoughCards(players);
        MakeBets(players);
        while (isStarted)
        {
            isStarted = CardService.CardCount(new List<Card>(Dealer.InHandCards)) < lowWinScore;
            GiveCards(Dealer, players);
            if (isStarted)
                Dealer.TakeCards(1);
        }

        #endregion

        CalculateWinnings(Dealer, players);
        if (removeCards)
            RemoveCards(players);
        //CurrentPlayers = null;
    }

    public static bool CheckCardSumIsMoreThan21Point(List<Card> cards)
    {
        return CardService.CardCount(cards) > highWinScore;
    }  
}