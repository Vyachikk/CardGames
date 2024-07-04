namespace OOP_ICT.Fifth.Enums
{
    class HandEvaluator
    {
        public enum HandRanking
        {
            HighCard,
            OnePair,
            TwoPairs,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush
        }

        public static HandRanking EvaluateHand(List<Card> hand)
        {
            List<Card> sortedHand = hand.OrderByDescending(card => card.Rank).ToList();

            if (IsRoyalFlush(sortedHand)) return HandRanking.RoyalFlush;
            if (IsStraightFlush(sortedHand)) return HandRanking.StraightFlush;
            if (IsFourOfAKind(sortedHand)) return HandRanking.FourOfAKind;
            if (IsFullHouse(sortedHand)) return HandRanking.FullHouse;
            if (IsFlush(sortedHand)) return HandRanking.Flush;
            if (IsStraight(sortedHand)) return HandRanking.Straight;
            if (IsThreeOfAKind(sortedHand)) return HandRanking.ThreeOfAKind;
            if (IsTwoPair(sortedHand)) return HandRanking.TwoPairs;
            if (IsOnePair(sortedHand)) return HandRanking.OnePair;

            return HandRanking.HighCard; // Default to high card
        }

        private static bool IsRoyalFlush(List<Card> hand)
        {
            // Check for a royal flush (10, J, Q, K, A of the same suit)
            return IsStraightFlush(hand) && hand.Last().Rank == CardRank.Ace;
        }

        private static bool IsStraightFlush(List<Card> hand)
        {
            // Check for a straight flush (consecutive cards of the same suit)
            return IsStraight(hand) && IsFlush(hand);
        }

        private static bool IsFourOfAKind(List<Card> hand)
        {
            // Check for four cards of the same rank
            return hand.GroupBy(card => card.Rank).Any(group => group.Count() == 4);
        }

        private static bool IsFullHouse(List<Card> hand)
        {
            // Check for a full house (three of a kind and a pair)
            return IsThreeOfAKind(hand) && IsOnePair(hand);
        }

        private static bool IsFlush(List<Card> hand)
        {
            // Check for all cards of the same suit
            return hand.GroupBy(card => card.Suit).Count() == 1;
        }

        private static bool IsStraight(List<Card> hand)
        {
            // Check for consecutive ranks
            for (int i = 1; i < hand.Count; i++)
            {
                if (hand[i].Rank != hand[i - 1].Rank + 1)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsThreeOfAKind(List<Card> hand)
        {
            // Check for three cards of the same rank
            return hand.GroupBy(card => card.Rank).Any(group => group.Count() == 3);
        }

        private static bool IsTwoPair(List<Card> hand)
        {
            // Check for two pairs
            return hand.GroupBy(card => card.Rank).Count(group => group.Count() == 2) == 2;
        }

        private static bool IsOnePair(List<Card> hand)
        {
            // Check for a single pair
            return hand.GroupBy(card => card.Rank).Any(group => group.Count() == 2);
        }
    }
}
