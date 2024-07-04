using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using OOP_ICT.Third.Services;

namespace OOP_ICT.Third.Models;

public class PlayerCasinoL3 : PlayerCasinoL2
{
    public PlayerCasinoL3(string name, int balance, int bet = 0) : base(name, balance, bet)
    {
    }

    public PlayerCasinoL3(string player1) : base("PlayerCasinoL3", 0, 0)
    {
    }

    public PlayerCasinoL3(string name, Bank bank, int bet = 0) : base(name, bank, bet)
    {
    }

    public PlayerCasinoL3(PlayerCasinoL3 other) : base(other.Name, other.Balance, other.Bet)
    {
        this.Cards = other.Cards.Select(card => new Card(card)).ToList();
    }

    public int MakeBet()
    {
        Random random = new Random();
        return Balance > 0 ? random.Next(1, (Balance + 1)/2) : 0;
    }

    public bool IsEnough()
    {
        return Cards != null && CardService.CardCount(this.Cards) >= 17;
    }

    public override bool Equals(object obj)
    {
        var other = obj as PlayerCasinoL3;
        if (other == null)
            return false;

        return Balance == other.Balance &&
               Bet == other.Bet &&
               Name == other.Name;
    }

    public override int GetHashCode()
    {
        unchecked 
        {
            int hash = 17;
            hash = hash * 23 + Balance.GetHashCode();
            hash = hash * 23 + Bet.GetHashCode();
            hash = hash * 23 + (Name != null ? Name.GetHashCode() : 0);
            return hash;
        }
    }
}