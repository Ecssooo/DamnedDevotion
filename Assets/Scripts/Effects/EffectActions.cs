using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectActions : MonoBehaviour
{
    private static EffectActions _instance;

    public static EffectActions Instance { get => _instance; }

    private Vector2 _firstMousePos;
    private Vector2 _lastMousePos;
    private Direction _moveCardDir;
    protected Collider2D _collider2D;


    public struct UndoWrapper
    {
        public System.Action<bool, int> act;
        public int value;
    }

    public List<UndoWrapper> _acts = new();
    
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

        UndoWrapper uw = new()
        {
            act = (bool b, int i) => PlayerAction(true, i),
            value = 1
        };
        _acts.Add(uw);
    }

    public void OnUndo()
    {
        _acts[^1].act.Invoke(true, _acts[^1].value);
        _acts.Remove(_acts[^1]);
    }

    public void PlayerAction(bool isUndo, int value)
    {

        if(isUndo) {
            Debug.Log("Undo");
            RemoveToken(value);
        }
        else
        {
            Debug.Log("Add");
            AddToken(0);
        }
    }

    private void AddToken(int idToken)
    {
        
    }
    
    private void RemoveToken(int idToken)
    {

    }

    private IEnumerator GetActionCoroutine(Collider2D effectClicked, System.Action<Action> callback)
    {
        Action action = new Action();
        if (effectClicked == null)
        {
            yield break;
        }

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
                _moveCardDir = Direction.RIGHT;
                break;
            case float a when a >= 45 && a < 135:
                _moveCardDir = Direction.UP;
                break;
            case float a when a >= 135 || a < -135:
                _moveCardDir = Direction.LEFT;
                break;
            case float a when a >= -135 && a < -45:
                _moveCardDir = Direction.DOWN;
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
                Vector2Int newPos = GameManager.Instance.Board.GetPositionNextTo(action._card.PositionOnBoard, action._direction);

                Debug.Log("newPos is : " + newPos);
                GameManager.Instance.Board.MoveCard(action._card, newPos);

                break;
            case Effects.Swap:
                break;
        }
    }
}
