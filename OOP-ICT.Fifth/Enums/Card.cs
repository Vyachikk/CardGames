namespace OOP_ICT.Fifth.Enums
{
    public enum CardSuit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum CardRank
    {
        Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
    }

    public class Card
    {
        public CardRank Rank { get; private set; }
        public CardSuit Suit { get; private set; }

        public Card(CardRank rank, CardSuit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }

        public string GetRankSymbol()
        {
            // Возвращаем символ ранга карты
            switch (Rank)
            {
                case CardRank.Two:
                case CardRank.Three:
                case CardRank.Four:
                case CardRank.Five:
                case CardRank.Six:
                case CardRank.Seven:
                case CardRank.Eight:
                case CardRank.Nine:
                case CardRank.Ten:
                    return ((int)Rank).ToString();
                case CardRank.Jack:
                    return "J";
                case CardRank.Queen:
                    return "Q";
                case CardRank.King:
                    return "K";
                case CardRank.Ace:
                    return "A";
                default:
                    return "";
            }
        }

        public string GetSuitSymbol()
        {
            // Возвращаем символ масти карты
            switch (Suit)
            {
                case CardSuit.Hearts:
                    return "[red]♥[/]";
                case CardSuit.Diamonds:
                    return "[red]♦[/]";
                case CardSuit.Clubs:
                    return "♣";
                case CardSuit.Spades:
                    return "♠";
                default:
                    return "";
            }
        }
    }
}
