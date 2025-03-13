using Unity.VisualScripting;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [Header("Effect Selection")]
    [SerializeField] private Effects _effect;
    public Effects Effet { get => _effect; }

    [SerializeField] private Invocation _invocationScript;
    
    private void OnMouseDown()
    {
        if (GameManager.Instance.GameState == GameState.Playable)
        {
            switch (_effect)
            {
                case Effects.MOVE:
                    if (GameManager.Instance.Effect == Effects.MOVE)
                    {
                        GameManager.Instance.Effect = Effects.NONE;
                    }
                    else
                    {
                        GameManager.Instance.Effect = Effects.MOVE;
                    }
                    break;
                case Effects.SWAP:
                    if (GameManager.Instance.Effect == Effects.SWAP)
                    {
                        GameManager.Instance.Effect = Effects.NONE;
                        EffectActions.Instance.SwapFirstCard = null;
                        EffectActions.Instance.SwapSecondCard = null;
                    }
                    else
                    {
                        GameManager.Instance.Effect = Effects.SWAP;
                    }
                    break;
                case Effects.INVOKE:
                    if (GameManager.Instance.Effect == Effects.INVOKE)
                    {
                        GameManager.Instance.Effect = Effects.NONE;
                        _invocationScript.enabled = false;
                    }
                    else
                    {
                        GameManager.Instance.Effect = Effects.INVOKE;
                        _invocationScript.enabled = true;
                        StartCoroutine(_invocationScript.InvokeMiniMonster());
                    }
                    break;
            }
        }
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
