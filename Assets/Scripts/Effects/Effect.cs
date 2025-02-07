using UnityEngine;

public class Effect : MonoBehaviour
{
    protected CircleCollider2D _circleCollider2D;
    private Vector3 _lastMousePos;

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
            transform.position = Vector3.Lerp(transform.position, _lastMousePos, .25f);
        }
    }

    #region Drag and Drop

    private void OnMouseDrag()
    {
        _isDragging = true;
        transform.position = Vector3.Lerp(transform.position, GetMousePos(), .25f);
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
