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
     private  void OnMouseDown() {

        switch(GameManager.Instance.Effect)
        {
            case Effects.None:
                break;
            case Effects.Move:
                Action moveAction = EffectActions.Instance.CreateAction(this);
                EffectActions.Instance.DoEffect(moveAction);
                break;
            case Effects.Switch:
                Card card = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Card>();
                if (card == null) return;
                if (EffectActions.Instance._swapFirstCard == null)
                {
                    EffectActions.Instance._swapFirstCard = card;
                }
                else
                {
                    EffectActions.Instance._swapSecondCard = card;
                }
                Action switchAction = EffectActions.Instance.CreateAction(EffectActions.Instance._swapFirstCard, EffectActions.Instance._swapSecondCard);
                EffectActions.Instance.DoEffect(switchAction);
                break;
        }

        //Action action = EffectActions.Instance.CreateAction(this);
        //EffectActions.Instance.DoEffect(action);  

        //DoEndOfTurnActions();

        Collider2D effectClicked = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //EffectActions.Instance.StartGetActionCoroutine(effectClicked, (action) => { });
        
    }
    private void DoEndOfTurnActions() // To use at end of turn to make Knights attack
    {
        switch(CardType)
        {
            case CardType.HUMAN:
                break;
            case CardType.KNIGHTSWORD:
                Card target = GameManager.Instance.Board.GetCardClose(this.PositionOnBoard, this._direction);
                Debug.Log(target);
                target.OnDie();
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
        Debug.Log("Mog Fed");
    }
}
