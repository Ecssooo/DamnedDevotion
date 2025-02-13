using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectActions : MonoBehaviour
{
    private static EffectActions _instance;

    public static EffectActions Instance { get => _instance; }


    public Card _swapFirstCard;

    public void DoEffect(Action action)
    {
        if (GameManager.Instance.Effect == Effects.None) return;

        switch (action._effect)
        {
            case Effects.Move:
                if (action._card.CompareTag("Cauldron") || action._card.CompareTag("Monster")) return;
                Vector2Int newPos = GameManager.Instance.Board.GetPositionNextTo(action._card.PositionOnBoard, action._direction);
                Debug.Log("newPos is : " + newPos);
                GameManager.Instance.Board.MoveCard(action._card, newPos);
                break;
            case Effects.Swap:
                break;
        }
    }

    public Action CreateAction(Card card)
    {
        Action action = new Action();
        action._card = card;
        action._effect = GameManager.Instance.Effect;
        action._direction = card.Direction;
        return action;
    }
    public Action CreateAction(Card card, Card card2)
    {
        Action action = new Action();
        action._card = card;
        action._card2 = card2;
        action._effect = GameManager.Instance.Effect;
        action._direction = card.Direction;
        return action;
    }
}
