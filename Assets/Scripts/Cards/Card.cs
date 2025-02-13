using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] CardType _cardType;
    public CardType CardType
    {
        get { return _cardType; }
    }   

    [Tooltip("None if anything other than KnightSword")]
    [SerializeField] private Direction _direction;
    public Direction Direction
    {
        get => _direction;
        set => _direction = value;
    }
    
    [SerializeField] private Direction _attackDirection;
    public Direction AttackDirection
    {
        get => _attackDirection;
        set => _attackDirection = value;
    }

    [SerializeField] private int _foodValue;
    public int FoodValue
    {
        get => _foodValue;
    }

    [SerializeField] private Vector2Int _positionOnBoard;
    public Vector2Int PositionOnBoard
    {
        get { return _positionOnBoard; }
        set { _positionOnBoard = value; }
    }
    private void OnMouseDown()
    {

        switch (GameManager.Instance.Effect)
        {
            case Effects.NONE:
                break;
            case Effects.MOVE:
                StartCoroutine(EffectActions.Instance._moveCardCoroutine((direction) =>
                {
                    this._direction = direction;
                    Action moveAction = EffectActions.Instance.CreateAction(this);
                    EffectActions.Instance.DoEffect(moveAction);
                }));
                break;
            case Effects.SWAP:
                Card card = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Card>();
                if (card == null) return;
                if (EffectActions.Instance._swapFirstCard == null)
                {
                    EffectActions.Instance._swapFirstCard = card;
                    // Debug.Log("First Swap card Selected");
                }
                else
                {
                    EffectActions.Instance._swapSecondCard = card;
                    // Debug.Log("Second Swap card Selected");
                    Action switchAction = EffectActions.Instance.CreateAction(EffectActions.Instance._swapFirstCard, EffectActions.Instance._swapSecondCard);
                    EffectActions.Instance.DoEffect(switchAction);
                    EffectActions.Instance._swapFirstCard = null;
                    EffectActions.Instance._swapSecondCard = null;
                }
                break;
        }

        Collider2D effectClicked = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    public void DoEndOfTurnActions() // To use at end of turn to make Knights attack
    {
        switch(CardType)
        {
            case CardType.HUMAN:
                break;
            case CardType.KNIGHTSWORD:
                Card target = GameManager.Instance.Board.GetCardClose(this.PositionOnBoard, this._attackDirection);
                // Debug.Log(target);
                if(target != null)
                    target.OnDie();
                else
                    Debug.Log("Target null");
                break;
            case CardType.KNIGHTSHIELD:
                break;
            case CardType.MONSTER:
                break;
            case CardType.MINIMONSTER:
                break;
            case CardType.CAULDRON:
                break;
        }
    }

    public void OnDie()
    {
        //Transfer food value to GameManager
        _foodValue = 0;
        GameManager.Instance.Board.ClearSlot(this);
        Debug.Log("Mog Fed");
    }
}
