using UnityEngine;

public class Effect : MonoBehaviour
{


    [Header("Effect Selection")]
    [SerializeField] private Effects _effect;
    public Effects Effet { get => _effect; }


    // Variables Drag and Drop
    //protected CircleCollider2D _circleCollider2D;
    //private bool _isDragging = false;
    //private bool _hasDragged = false;
    //private Vector3 _lastMousePos;


    public virtual void DoEffect()
    {
        
    }
    private void OnMouseDown()
    {
        GameManager.Instance.Effect = this.Effet;
        //if (GameManager.Instance.Effect != Effects.NONE) GameManager.Instance.Effect = Effects.NONE;
        switch (GameManager.Instance.Effect)
        {
            case Effects.MOVE:
                if (EffectList.MoveCard)
                {
                    EffectList.MoveCard = false;
                    GameManager.Instance.Effect = Effects.NONE;
                    return;
                }
                EffectList.MoveCard = true;
                GameManager.Instance.Effect = Effects.MOVE;
                break;
            case Effects.SWAP:
                if (EffectList.SwapCard)
                {
                    EffectList.SwapCard = false;
                    GameManager.Instance.Effect = Effects.NONE;
                    return;
                }
                EffectList.SwapCard = true;
                GameManager.Instance.Effect = Effects.SWAP;
                break;
            case Effects.NONE:
                break;
        }
        Debug.Log(GameManager.Instance.Effect);
    }

    #region Drag and Drop

    //private void OnMouseDrag()
    //{
    //    _isDragging = true;
    //    //transform.position = Vector3.Lerp(transform.position, GetMousePos(), .25f);
    //    _lastMousePos = GetMousePos();
    //    _hasDragged = true;
    //}
    //private Vector2 GetMousePos()
    //{
    //    return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //}

    //protected virtual void OnMouseUp()
    //{
    //    _isDragging = false;
    //}

    #endregion
}
