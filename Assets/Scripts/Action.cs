using UnityEngine;

public struct Action
{
    public Effect _effect;
    public Card _card;

    public Action(Effect effect, Card card)
    {
        _effect = effect;
        _card = card;
    }
}
