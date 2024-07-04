using OOP_ICT.Second.Models;
using Xunit;
namespace OOP_ICT.Second.Tests;

public class PlayerCasinoTests
{
    [Fact]
    public void Constructor_InitialBalance_CreatesPlayerCasinoWithBankAndBet()
    {
        string playerName = "John Doe";
        int initialBalance = 100;
        int bet = 200;
        PlayerCasinoL2 player = new PlayerCasinoL2(playerName, initialBalance, bet);
        
        Assert.Equal(playerName, player.Name);
        Assert.NotNull(player.Bank);
        Assert.Equal(initialBalance, player.Bank.Balance);
        Assert.Equal(200, player.Bet);
    }

    [Fact]
    public void Balance_SetValidValue_UpdatesCasinoPlayerBankBalance()
    {
        string playerName = "John Doe";
        int initialBalance = 100;
        PlayerCasinoL2 player = new PlayerCasinoL2(playerName, new Bank(initialBalance));
        int newBalance = 200;
        
        player.Balance = newBalance;
        
        Assert.Equal(newBalance, player.Bank.Balance);
    }

    [Fact]
    public void Balance_SetWithNoBank_ThrowsNullReferenceException()
    {
        string playerName = "John Doe";
        PlayerCasinoL2 player = new PlayerCasinoL2(playerName, bank: null);
        
        var exception = Assert.Throws<NullReferenceException>(() => player.Balance = 100);
        Assert.Equal("Bank is not initialized", exception.Message);
    }
}