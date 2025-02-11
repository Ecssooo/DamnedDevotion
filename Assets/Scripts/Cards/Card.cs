using Unity.VisualScripting;
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


    void Start()
    {
        PositionOnBoard = new Vector2Int((int)transform.position.x, (int)transform.position.y);
    }

    protected virtual void OnMouseDown()
    {
        if (EffectList.MoveCard)
        {
            // Afficher Flèches
            Collider2D effectClicked = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //StartCoroutine(MoveCardEffect.GetActionCoroutine(effectClicked, (action) =>
            //{
            //    Debug.Log(action._effect);
            //    Debug.Log(action._card);
            //    //GameManager.Instance.ListActions.ListActions.Add(action);
            //    //Debug.Log(GameManager.Instance.ListActions.ListActions);
            //}));
            EffectActions.Instance.StartGetActionCoroutine(effectClicked, (action) => { });
        }
    }
}
