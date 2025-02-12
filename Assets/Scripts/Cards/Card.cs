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
     protected virtual void OnMouseDown() { 
        DoAction();
        // Afficher Flèches
        Collider2D effectClicked = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        EffectActions.Instance.StartGetActionCoroutine(effectClicked, (action) => { });
    }
    private void DoAction() // To use at end of turn to make Knights attack
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
