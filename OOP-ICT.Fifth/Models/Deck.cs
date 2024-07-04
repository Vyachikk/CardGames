using OOP_ICT.Fifth.Enums;
using OOP_ICT.Fifth.Exeptions;

namespace OOP_ICT.Fifth.Models
{
    class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();

            foreach (var suit in Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>())
            {
                foreach (var rank in Enum.GetValues(typeof(CardRank)).Cast<CardRank>())
                {
                    cards.Add(new Card(rank, suit));
                }
            }
        }

        public void Shuffle()
        {
            if (cards == null || cards.Count <= 1)
                throw new DeckException("Deck is empty or contains only one card. Cannot shuffle.");

            Random random = new Random();
            cards = cards.OrderBy(card => random.Next()).ToList();
        }

        public Card DrawCard()
        {
            if (cards.Count > 0)
            {
                Card drawnCard = cards[0];
                cards.RemoveAt(0);
                return drawnCard;
            }
            else
                throw new DeckException("Deck is empty. Cannot draw a card.");
        }
    }
}
