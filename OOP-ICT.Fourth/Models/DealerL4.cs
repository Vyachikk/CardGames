using OOP_ICT.Exceptions;
using OOP_ICT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ICT.Fourth.Models
{
    public class DealerL4 : Dealer
    {
        private List<Card> _inHandCards;

        public ReadOnlyCollection<Card> InHandCards
        {
            get { return _inHandCards.AsReadOnly(); }
        }

        public DealerL4(CardDeck deck) : base(deck)
        {
            _inHandCards = new List<Card>();
        }

        public DealerL4() : this(new CardDeck())
        {
        }

        public void InitCards()
        {
            ShuffleDeck();
        }

        public void TakeCards(int amount)
        {
            if (amount < 0)
                throw new NegativeNumArgumentException("amount");
            _inHandCards.AddRange(GiveCards(amount));
        }

        public void ClearCards()
        {
            _inHandCards.Clear();
        }
    }
}
