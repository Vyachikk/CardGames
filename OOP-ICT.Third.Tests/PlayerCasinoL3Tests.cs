using OOP_ICT.Enums;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using OOP_ICT.Third.Models;

namespace OOP_ICT.Third.Tests;
using Xunit;
public class PlayerCasinoL3Tests
{
    [Fact]
    public void Constructor_InitialBalance_CreatesPlayerCasinoWithBankAndBet()
    {
        string playerName = "John Doe";
        int initialBalance = 100;
        int bet = 200;
        PlayerCasinoL3 player = new PlayerCasinoL3(playerName, initialBalance, bet);
        
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
        PlayerCasinoL3 player = new PlayerCasinoL3(playerName, new Bank(initialBalance));
        int newBalance = 200;
        
        player.Balance = newBalance;
        
        Assert.Equal(newBalance, player.Bank.Balance);
    }

    [Fact]
    public void Balance_SetWithNoBank_ThrowsNullReferenceException()
    {
        string playerName = "John Doe";
        PlayerCasinoL3 player = new PlayerCasinoL3(playerName, bank: null);
        
        var exception = Assert.Throws<NullReferenceException>(() => player.Balance = 100);
        Assert.Equal("Bank is not initialized", exception.Message);
    }

    [Fact]
    public void IsEnough_Test()
    {
        PlayerCasinoL3 player = new PlayerCasinoL3("TestPLayer", 0, 0);
        Assert.NotNull(player.Cards);
        player.Cards.Add(new Card(Rank.Two,Suit.Hearts));
        player.Cards.Add(new Card(Rank.Three,Suit.Spades));
        Assert.False(player.IsEnough());
        player.Cards.Clear();
        Assert.Empty(player.Cards);
        player.Cards.Add(new Card(Rank.Ten,Suit.Diamonds));
        player.Cards.Add(new Card(Rank.Seven,Suit.Hearts));
        Assert.True(player.IsEnough());
    }
}