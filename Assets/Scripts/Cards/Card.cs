using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private Vector2Int _positionOnBoard;
    public Vector2Int PositionOnBoard
    {
        get { return _positionOnBoard; }
        set { _positionOnBoard = value; }
    }


    void Start()
    {
        PositionOnBoard = new Vector2Int((int)transform.position.x, (int)transform.position.y);
    }
}
