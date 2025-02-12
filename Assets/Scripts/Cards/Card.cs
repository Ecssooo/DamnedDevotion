using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private Vector2Int _positionOnBoard;
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

        // Afficher Flèches
        Collider2D effectClicked = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        EffectActions.Instance.StartGetActionCoroutine(effectClicked, (action) => { });
    //     }
    // }
}
