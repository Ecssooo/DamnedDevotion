using UnityEngine;

public struct Action
{
    public Effects _effect;
    public Card _card;
    public Card _card2;
    public Direction _direction;
    public Vector2Int _position;

    public Action(Effects effect, Card card, Card card2, Direction direction, Vector2Int position)
    {
        _effect = effect;
        _card = card;
        _card2 = card2;
        _direction = direction;
        _position = position;
    }
}
