using OOP_ICT.Second.Models;
using Xunit;
using OOP_ICT.Second.Factories;
namespace OOP_ICT.Second.Tests;

public class FactoryTests
{
    [Fact]
    public void TestFactories_CreateBankAccount()
    {
        int BankBalance = 1000;
        
        Bank bankTests = CasinoBankFactory.CreateBankAccount(BankBalance);
        
        Assert.Equal(BankBalance, bankTests.Balance);
    }
    [Fact]
    public void TestFactories_CreateNewPlayer()
    {
        string playerName = "Jonny boy";
        int initialBalance = 100;
          
        PlayerCasinoL2 player = PlayerFactory.CreateNewPlayer(playerName, initialBalance);
        Assert.Equal(playerName, player.Name);
        Assert.Equal(initialBalance, player.Bank.Balance);
    }
}