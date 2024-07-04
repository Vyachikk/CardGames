using OOP_ICT.Exceptions;

namespace OOP_ICT.Second.Models;

public class BlackJackCasinoL2
{
    private string _name;

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

    public Bank Bank { get; set; }

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

    public BlackJackCasinoL2(string name, Bank bank)
    {
        Bank = new Bank(bank);
        Name = name;
    }

    public BlackJackCasinoL2(string name, int balance) : this(name, new Bank(balance))
    {
    }


    public void PlayerPlaceBet(PlayerCasinoL2 player, int bet)

    {
        if (!player.Bank.IsBalanceEfficient(bet))
            throw new NegativeNumFieldException("Bet");
        player.Bet = bet;
    }

    public void CountWinOrLoss(PlayerCasinoL2 playerCasinoL2, int playerScore, int dealerScore)
    {
        if (playerScore > dealerScore)
        {
            playerCasinoL2.Bank.Balance += playerCasinoL2.Bet;
            Bank.Balance -= playerCasinoL2.Bet;
        }

        if (playerScore < dealerScore)
        {
            playerCasinoL2.Bank.Balance -= playerCasinoL2.Bet;
            Bank.Balance += playerCasinoL2.Bet;
        }
    }
}