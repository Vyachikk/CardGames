using OOP_ICT.Exceptions;

namespace OOP_ICT.Second.Models;

public class PlayerCasinoL2 :Player
{
    private int _bet;
    public int Bet
    {
        get { return _bet;}
        set
        {
            if (value < 0)
                throw new NegativeNumFieldException("Bet");
            _bet = value;
        } 
    }
    public PlayerCasinoL2(string name, int balance,int bet=0):base(name,balance)
    {
        Bet = bet;
    }
    public PlayerCasinoL2(string name, Bank bank,int bet=0):base(name,bank)
    {
        Bet = bet;
    }
}