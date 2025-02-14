using Unity.VisualScripting;
using UnityEngine;

public class Effect : MonoBehaviour
{


    [Header("Effect Selection")]
    [SerializeField] private Effects _effect;
    public Effects Effet { get => _effect; }

    private void Start()
    {
        if (_effect == Effects.SWAP) this.AddComponent<SwitchPower>();
    }

    private void OnMouseDown()
    {
        GameManager.Instance.Effect = this.Effet;
        //if (GameManager.Instance.Effect != Effects.NONE) GameManager.Instance.Effect = Effects.NONE;
        switch (GameManager.Instance.Effect)
        {
            case Effects.MOVE:
                if (!EffectList.MoveCard)
                {
                    EffectList.MoveCard = true;
                    EffectList.SwapCard = false;
                    GameManager.Instance.Effect = Effects.MOVE;
                    return;
                }
                EffectList.MoveCard = false;
                EffectList.SwapCard = false;
                GameManager.Instance.Effect = Effects.NONE;
                break;
            case Effects.SWAP:
                if (!EffectList.SwapCard)
                {
                    EffectList.SwapCard = true;
                    EffectList.MoveCard = false;
                    GameManager.Instance.Effect = Effects.SWAP;
                    return;
                }
                EffectList.SwapCard = false;
                EffectList.MoveCard = false;
                GameManager.Instance.Effect = Effects.NONE;
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
