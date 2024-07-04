using OOP_ICT.Fifth.Enums;
using OOP_ICT.Fifth.Exeptions;

namespace OOP_ICT.Fifth.Models
{
    public class Dealer
    {
        private Deck deck;
        private List<Card> communityCards;

        public Dealer()
        {
            deck = new Deck();
            communityCards = new List<Card>();
        }

        public List<Card> GetCommunityCards { get { return communityCards; } }

        public void DealCards(List<Player> players)
        {
            if (deck == null)
            {
                throw new DealerException("Dealer's deck is null. Cannot deal cards.");
            }

            deck.Shuffle();

            foreach (var player in players)
            {
                if (player == null)
                {
                    throw new DealerException("Cannot deal cards to a null player.");
                }

                player.MatchesPlayed++;
                player.AddCardToHand(deck.DrawCard());
                player.AddCardToHand(deck.DrawCard());
            }
        }

        public void DealFlop()
        {
            if (deck == null)
            {
                throw new DealerException("Dealer's deck is null. Cannot deal the flop.");
            }

            for (int i = 0; i < 3; i++)
            {
                communityCards.Add(deck.DrawCard());
            }
        }

        public void DealTurn()
        {
            if (deck == null)
            {
                throw new DealerException("Dealer's deck is null. Cannot deal the turn.");
            }

            communityCards.Add(deck.DrawCard());
        }

        public void DealRiver()
        {
            if (deck == null)
            {
                throw new DealerException("Dealer's deck is null. Cannot deal the river.");
            }

            communityCards.Add(deck.DrawCard());
        }
    }
}
