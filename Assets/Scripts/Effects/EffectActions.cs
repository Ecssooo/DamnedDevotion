using System.Collections;
using UnityEngine;

public class EffectActions : MonoBehaviour
{
    #region Action
    private static EffectActions _instance;

    public static EffectActions Instance { get => _instance; }

    public void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private IEnumerator GetActionCoroutine(Collider2D effectClicked, System.Action<Action> callback)
    {
        Action action = new Action();
        if (effectClicked == null)
        {
            yield break;
        }
        Debug.Log(effectClicked);

        Effects effect = EffectList.Effects;

        action._effect = effect;

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        Card card = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Card>() ?? throw new System.Exception("No card found");
        #region experimental

        _firstMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        _lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(_lastMousePos.y - _firstMousePos.y, _lastMousePos.x - _firstMousePos.x) * Mathf.Rad2Deg;
        switch (angle)
        {
            case float a when a >= -45 && a < 45:
                _moveCardDir = Direction.Right;
                break;
            case float a when a >= 45 && a < 135:
                _moveCardDir = Direction.Top;
                break;
            case float a when a >= 135 || a < -135:
                _moveCardDir = Direction.Left;
                break;
            case float a when a >= -135 && a < -45:
                _moveCardDir = Direction.Down;
                break;
        }
        //this._collider2D.enabled = false;
        ////RaycastHit2D hit = Physics2D.Raycast(transform.position, _moveCardDir);
        //this._collider2D.enabled = true;

        Debug.Log(_moveCardDir);
        action._direction = _moveCardDir;

        #endregion


        action._card = card;
        callback?.Invoke(action);
    }
    public void StartGetActionCoroutine(Collider2D effectClicked, System.Action<Action> callback)
    {
        StartCoroutine(GetActionCoroutine(effectClicked, (action) =>
        {
            Debug.Log(action._effect);
            Debug.Log(action._card);
            Debug.Log(action._direction);
        }));
    }
    #endregion

    private Vector2 _firstMousePos;
    private Vector2 _lastMousePos;
    private Direction _moveCardDir;
    protected Collider2D _collider2D;

    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
    }
}
