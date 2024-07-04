using OOP_ICT.Exceptions;
using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class BankTests
{
    private string expectedExceptionStr = $"Field 'Balance' cannot be negative";

    [Fact]
    public void Constructor_PositiveBalance_SetsBalance()
    {
        int balance = 100;
        Bank bank = new Bank(balance);
        Assert.Equal(balance, bank.Balance);
    }

    [Fact]
    public void Constructor_NegativeBalance_ThrowsNegativeNumFieldException()
    {
        int initialBalance = -50;

        var exception = Assert.Throws<NegativeNumFieldException>(() => new Bank(initialBalance));
        Assert.Equal(expectedExceptionStr, exception.Message);
    }

    [Fact]
    public void SetBalance_NegativeValue_ThrowsNegativeNumFieldException()
    {
        Bank bank = new Bank(100);

        var exception = Assert.Throws<NegativeNumFieldException>(() => bank.Balance = -100);
        Assert.Equal(expectedExceptionStr, exception.Message);
    }

    [Fact]
    public void SetBalance_PositiveValue_SetsBalance()
    {
        int initialBalance = 100;
        Bank bank = new Bank(initialBalance);
        int newBalance = 150;

        bank.Balance = newBalance;

        Assert.Equal(newBalance, bank.Balance);
    }

    [Fact]
    public void CopyConstructor_CreatesDeepCopy()
    {
        Bank originalBank = new Bank(100);
        Bank copyBank = new Bank(originalBank);

        copyBank.Balance = 200; // Change balance in the copy

        Assert.NotEqual(copyBank.Balance, originalBank.Balance);
    }

    [Fact]
    public void WithdrawMoneyFromBalance()
    {
        Bank bank = new Bank(100);
        bank.Withdraw(20);
        Assert.Equal(80, bank.Balance);
        var exception = Assert.Throws<NegativeNumFieldException>(() => bank.Withdraw(81));
        Assert.Equal(expectedExceptionStr, exception.Message);
    }

    [Fact]
    public void AddMoneytoBalance()
    {
        Bank bank = new Bank(100);
        bank.Add(20);
        Assert.Equal(120, bank.Balance);
    }

    [Fact]
    public void IsBalanceEfficient()
    {
        int initialBalance = 100;
        Bank bank = new Bank(initialBalance);
        for (int i = 0; i < 100; i++)
        {
            if (initialBalance - i >= 0)
                Assert.True(bank.IsBalanceEfficient(i));
            else
                Assert.False(bank.IsBalanceEfficient(i));
        }
    }
}