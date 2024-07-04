using OOP_ICT.Enums;
using OOP_ICT.Fourth.Models;
using OOP_ICT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OOP_ICT.Fourth.Tests
{
    public class PlayerPokerTests
    {
        [Fact]
        public void MakeBet_WithPositiveBalance_ShouldReturnBetWithinRange()
        {
            // Arrange
            var player = new PlayerPoker("Player", 100);

            // Act
            var bet = player.MakeBet();

            // Assert
            Assert.InRange(bet, 1, 50); // Assuming the maximum bet is half of the balance
        }

        [Fact]
        public void MakeBet_WithZeroBalance_ShouldReturnZeroBet()
        {
            // Arrange
            var player = new PlayerPoker("Player", 0);

            // Act
            var bet = player.MakeBet();

            // Assert
            Assert.Equal(0, bet);
        }

        [Fact]
        public void IsEnough_WithCardCountBelow17_ShouldReturnFalse()
        {
            // Arrange
            var player = new PlayerPoker("Player", 100);
            player.Cards = new System.Collections.Generic.List<Card>
        {
            new Card(Rank.Ace, Suit.Hearts),
            new Card(Rank.Two, Suit.Diamonds),
            // Add more cards if necessary to make the count below 17
        };

            // Act
            var isEnough = player.IsEnough();

            // Assert
            Assert.False(isEnough);
        }

        [Fact]
        public void IsEnough_WithCardCountEqualOrAbove17_ShouldReturnTrue()
        {
            // Arrange
            var player = new PlayerPoker("Player", 100);
            player.Cards = new System.Collections.Generic.List<Card>
        {
            new Card(Rank.Ace, Suit.Hearts),
            new Card(Rank.Ten, Suit.Diamonds),
            // Add more cards if necessary to make the count 17 or above
        };

            // Act
            var isEnough = player.IsEnough();

            // Assert
            Assert.True(isEnough);
        }

        [Fact]
        public void Equals_WithSameValues_ShouldReturnTrue()
        {
            // Arrange
            var player1 = new PlayerPoker("Player", 100, 10);
            var player2 = new PlayerPoker("Player", 100, 10);

            // Act
            var result = player1.Equals(player2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_WithDifferentValues_ShouldReturnFalse()
        {
            // Arrange
            var player1 = new PlayerPoker("Player1", 100, 10);
            var player2 = new PlayerPoker("Player2", 200, 20);

            // Act
            var result = player1.Equals(player2);

            // Assert
            Assert.False(result);
        }
    }
}
