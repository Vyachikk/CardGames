using OOP_ICT.Exceptions;
using OOP_ICT.Models;

namespace OOP_ICT.Second.Models;

public class Player
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

    public List<Card> Cards { set; get; }

    public Player(string name, Bank bank)
    {
        Name = name;
        Bank = bank;
        Cards = new List<Card>();
    }

    public Player(string name, int balance) : this(name, new Bank(balance))
    {
    }
}