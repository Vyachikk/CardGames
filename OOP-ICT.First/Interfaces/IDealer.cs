using OOP_ICT.Models;

namespace OOP_ICT.Interfaces;

public interface IDealer
{
    void ShuffleDeck();
    IReadOnlyList<Card> SeeCards();
    List<Card> GiveCards(int n);
}