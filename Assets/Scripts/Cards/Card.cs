using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private Vector2Int _positionOnBoard;
    [SerializeField] private MoveCardEffect MoveCardEffect;
    public Vector2Int PositionOnBoard
    {
        get { return _positionOnBoard; }
        set { _positionOnBoard = value; }
    }

    protected virtual void OnMouseDown()
    {
        //if (EffectList.MoveCard)
        //{
        //    // Afficher Flèches
        //    StartCoroutine(MoveCardEffect._moveCardCoroutine());
        //}
    }
}
