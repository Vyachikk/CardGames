using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class BlackajckCasinoTests
{
    [Fact]
    public void TestPlayerBets_AreLossCounted()
    {
        int initialBalance = 1000;
        int bet = 20;
        BlackJackCasinoL2 casinoL2 = new BlackJackCasinoL2("Black Jack Casino", initialBalance);
        PlayerCasinoL2 player = new PlayerCasinoL2("Jonny boy", initialBalance);

        casinoL2.PlayerPlaceBet(player, bet);
        casinoL2.CountWinOrLoss(player, 17, 21);

        Assert.Equal(initialBalance - bet, player.Balance);
        Assert.Equal(initialBalance + bet, casinoL2.Balance);
    }

    [Fact]
    public void TestPlayerBets_AreWinCounted()
    {
        int initialBalance = 1000;
        int bet = 20;
        BlackJackCasinoL2 casinoL2 = new BlackJackCasinoL2("Black Jack Casino", initialBalance);
        PlayerCasinoL2 player = new PlayerCasinoL2("Jonny boy", initialBalance);

        casinoL2.PlayerPlaceBet(player, bet);
        casinoL2.CountWinOrLoss(player, 21, 17);

        Assert.Equal(initialBalance + bet, player.Balance);
        Assert.Equal(initialBalance - bet, casinoL2.Balance);
    }
}