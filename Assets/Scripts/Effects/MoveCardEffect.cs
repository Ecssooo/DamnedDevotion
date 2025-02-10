using System.Collections;
using UnityEngine;

public class MoveCardEffect : MonoBehaviour
{
    private Vector2 _firstMousePos;
    private Vector2 _lastMousePos;
    private Vector2 _moveCardDir;
    protected Collider2D _collider2D;

    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
    }
    public IEnumerator _moveCardCoroutine()
    {
        _firstMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        _lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(_lastMousePos.y - _firstMousePos.y, _lastMousePos.x - _firstMousePos.x) * Mathf.Rad2Deg;
        switch(angle){
            case float a when a >= -45 && a < 45:
                _moveCardDir = Vector2.right;
                break;
            case float a when a >= 45 && a < 135:
                _moveCardDir = Vector2.up;
                break;
            case float a when a >= 135 || a < -135:
                _moveCardDir = Vector2.left;
                break;
            case float a when a >= -135 && a < -45:
                _moveCardDir = Vector2.down;
                break;
            }
        this._collider2D.enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _moveCardDir);
        this._collider2D.enabled = true;

        Debug.Log(_moveCardDir);
    }
}
