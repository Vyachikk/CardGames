using OOP_ICT.Fifth.Enums;
using OOP_ICT.Fifth.Exceptions;
using System.Text.Json.Serialization;

namespace OOP_ICT.Fifth.Models
{
    public class Player
    {
        public string Name { get; set; }
        [JsonIgnore]
        public List<Card> Hand { get; private set; }
        [JsonIgnore]
        public bool IsActive { get; internal set; }
        [JsonIgnore]
        public int BetAmount { get; internal set; }
        public int Chips { get; private set; }
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public static List<Player> AllPlayers { get; } = new List<Player>();

        public Player(string name, int startingChips)
        {
            Name = name;
            Hand = new List<Card>();
            IsActive = true;
            BetAmount = 0;
            Chips = startingChips;
            MatchesPlayed = 0;
            Wins = 0;
            Losses = 0;

            AllPlayers.Add(this);
        }

        [JsonConstructor]
        public Player(string name, int chips, int matchesPlayed, int wins, int losses)
        {
            Name = name;
            Chips = chips;
            MatchesPlayed = matchesPlayed;
            Wins = wins;
            Losses = losses;
            Hand = new List<Card>();
            IsActive = true;
        }

        public void AddCardToHand(Card card)
        {
            Hand.Add(card);
        }

        public void AddChips(int winnings)
        {
            if (winnings < 0)
                throw new InvalidBetExeptions(Name, "Cannot add negative winnings.");

            Chips += winnings;
            Wins += winnings;
        }

        public void RemoveChips(int bet)
        {
            if (bet < 0)
                throw new InvalidBetExeptions(Name, "Cannot place a negative bet.");

            if (bet <= Chips)
            {
                Chips -= bet;
                Losses += bet;
            }
            else
                throw new InvalidBetExeptions(Name, "Not enough chips for this bet.");
        }
    }
}