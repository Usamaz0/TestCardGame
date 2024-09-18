using UnityEngine;

public interface ICardFactory
{
    Card CreateCard(int id, Vector3 position);
}
public interface ICardMatchHandler
{
    void OnCardFlipped(Card card); // Called when a card is flipped

    void OnCardFilledAction();
}

public class CardFactory : ICardFactory
{
    private GameObject _cardPrefab;

    public CardFactory(GameObject cardPrefab)
    {
        _cardPrefab = cardPrefab;
    }

    public Card CreateCard(int id, Vector3 position)
    {
        GameObject cardObject = GameObject.Instantiate(_cardPrefab, position, Quaternion.identity);
        Card card = cardObject.GetComponent<Card>();
        return card;
    }
}
