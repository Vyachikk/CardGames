using System.Diagnostics.CodeAnalysis;
using OOP_ICT.Exceptions;
using OOP_ICT.Second.Factories;

namespace OOP_ICT.Second.Models;

public class Bank
{
    private int _balance;

    public int Balance
    {
        get => _balance;
        set
        {
            if (value < 0)
                throw new NegativeNumFieldException("Balance", value);
            _balance = value;
        }
    }

    public Bank(int balance)
    {
        Balance = balance;
    }

    public Bank(Bank bank)
    {
        _balance = bank.Balance;
    }

    public Bank()
    {
        throw new NotImplementedException();
    }

    public void Withdraw(int amount)
    {
        if (amount < 0)
            throw new NegativeNumArgumentException("Amount");
        Balance -= amount;
    }

    public void Add(int amount)
    {
        if (amount < 0)
            throw new NegativeNumArgumentException("Amount");
        Balance += amount;
    }

    public bool IsBalanceEfficient(int amount)
    {
        if (amount < 0)
            throw new NegativeNumArgumentException("Amount");
        return Balance - amount >= 0;
    }
}