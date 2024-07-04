using OOP_ICT.Exceptions;
using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class PlayerTests
{
    [Fact]
    public void Constructor_ValidParameters_CreatesPlayer()
    {
        string playerName = "John Doe";
        int initialBalance = 100;
        
        Player player = new Player(playerName, new Bank(initialBalance));
        
        Assert.Equal(playerName, player.Name);
        Assert.Equal(initialBalance, player.Balance);
        Assert.NotNull(player.Cards);
    }
    
    [Fact]
    public void Constructor_EmptyName_ThrowsEmptyStringFieldException()
    {
        string playerName = "";
        
        var exception = Assert.Throws<EmptyStringFieldException>(() => new Player(playerName, new Bank(100)));
        Assert.Equal($"Field 'Name' cannot be empty", exception.Message);
    }
    
    [Fact]
    public void Constructor_InitialBalance_CreatesPlayerWithBank()
    {
        string playerName = "John Doe";
        int initialBalance = 100;
        
        Player player = new Player(playerName, initialBalance);
        
        Assert.Equal(playerName, player.Name);
        Assert.NotNull(player.Bank);
        Assert.Equal(initialBalance, player.Bank.Balance);
        Assert.NotNull(player.Cards);
    }

    [Fact]
    public void Balance_SetValidValue_UpdatesBankBalance()
    {
        string playerName = "John Doe";
        int initialBalance = 100;
        Player player = new Player(playerName, new Bank(initialBalance));
        int newBalance = 200;
        
        player.Balance = newBalance;
        
        Assert.Equal(newBalance, player.Bank.Balance);
    }

    [Fact]
    public void Balance_SetWithNoBank_ThrowsNullReferenceException()
    {
        string playerName = "John Doe";
        Player player = new Player(playerName, bank: null);
        
        var exception = Assert.Throws<NullReferenceException>(() => player.Balance = 100);
        Assert.Equal("Bank is not initialized", exception.Message);
    }
}
