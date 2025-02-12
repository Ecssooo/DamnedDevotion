using UnityEngine;

public class Effect : MonoBehaviour
{
    protected CircleCollider2D _circleCollider2D;
    private Vector3 _lastMousePos;
    [SerializeField] protected Effects _effect;
    public Effects Effet { get => _effect; }

    private bool _isDragging = false;
    private bool _hasDragged = false;

    private void Start()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate()
    {
        if (!_isDragging && _hasDragged)
        {
            //transform.position = Vector3.Lerp(transform.position, _lastMousePos, .25f);
        }
    }

    public virtual void DoEffect()
    {
        
    }
    private void OnMouseDown()
    {
        switch (_effect)
        {
            case Effects.Move:
                if (EffectList.MoveCard)
                {
                    EffectList.MoveCard = false;
                    EffectList.Effects = Effects.None;
                    return;
                }
                EffectList.MoveCard = true;
                EffectList.Effects = Effects.Move;
                break;
            case Effects.Swap:
                EffectList.SwapCard = true;
                EffectList.Effects = Effects.Swap;
                break;
        }
    }

    #region Drag and Drop

    private void OnMouseDrag()
    {
        _isDragging = true;
        //transform.position = Vector3.Lerp(transform.position, GetMousePos(), .25f);
        _lastMousePos = GetMousePos();
        _hasDragged = true;
    }
    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected virtual void OnMouseUp()
    {
        _isDragging = false;
    }

    #endregion
}
