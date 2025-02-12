using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private Vector2Int _positionOnBoard;
    //[SerializeField] private MoveCardEffect MoveCardEffect;
    public Vector2Int PositionOnBoard
    {
        get { return _positionOnBoard; }
        set { _positionOnBoard = value; }
    }

    [SerializeField] private Direction _direction;

    public Direction Direction
    {
        get => _direction;
        set => _direction = value;
    }


    // protected virtual void OnMouseDown()
    // {
    //     if (EffectList.MoveCard)
    //     {
    //         // Afficher Flèches
    //         StartCoroutine(MoveCardEffect._moveCardCoroutine());
    //     }
    // }
}
