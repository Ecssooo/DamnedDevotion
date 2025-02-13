using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectActions : MonoBehaviour
{
    private static EffectActions _instance;

    public static EffectActions Instance { get => _instance; }


    public Card _swapFirstCard;
    public Card _swapSecondCard;

    public void DoEffect(Action action)
    {
        Debug.Log(GameManager.Instance.Effect);
        if (GameManager.Instance.Effect == Effects.NONE) return;

        switch (action._effect)
        {
            case Effects.MOVE:
                if (action._card.CompareTag("Cauldron") || action._card.CompareTag("Monster")) return;
                Vector2Int newPos = GameManager.Instance.Board.GetPositionNextTo(action._card.PositionOnBoard, action._direction);
                Debug.Log("newPos is : " + newPos);
                GameManager.Instance.Board.MoveCard(action._card, newPos);
                break;
            case Effects.SWAP:
                GameManager.Instance.Board.SwitchCard(action._card, action._card2);
                Debug.Log("Swapping Cards");
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
