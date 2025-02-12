using System.Collections;
using UnityEngine;

public class EffectActions : MonoBehaviour
{
    private static EffectActions _instance;

    public static EffectActions Instance { get => _instance; }

    private Vector2 _firstMousePos;
    private Vector2 _lastMousePos;
    private Direction _moveCardDir;
    protected Collider2D _collider2D;
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

    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
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
        Card card = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Card>();
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


        Debug.Log(_moveCardDir);
        action._direction = _moveCardDir;

        if (card != null) action._card = card;
        callback?.Invoke(action);
    }
    public void StartGetActionCoroutine(Collider2D effectClicked, System.Action<Action> callback)
    {
        StartCoroutine(GetActionCoroutine(effectClicked, (action) =>
        {
            Debug.Log(action._effect);
            Debug.Log(action._card);
            Debug.Log(action._direction);
            DoEffect(action);
            EffectList.Effects = Effects.None;
        }));
    }

    public void DoEffect(Action action)
    {
        switch (action._effect)
        {
            case Effects.Move:
                if (action._card.CompareTag("Cauldron") || action._card.CompareTag("Monster")) return;
                Debug.Log(action._card.PositionOnBoard);
                Vector2Int newPos = GameManager.Instance.Board.GetPositionNextTo(action._card.PositionOnBoard, action._direction);
                Debug.Log(newPos);

                Debug.Log("newPos is free");
                GameManager.Instance.Board.MoveCard(action._card, newPos);

                break;
            case Effects.Swap:
                break;
        }
    }
}
