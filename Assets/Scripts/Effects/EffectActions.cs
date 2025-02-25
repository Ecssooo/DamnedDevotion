using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectActions : MonoBehaviour
{
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

    private Vector2 _firstMousePos;
    private Vector2 _lastMousePos;
    private Direction _moveCardDir;

    public Card _swapFirstCard;
    public Card _swapSecondCard;

    public void DoEffect(Action action)
    {
        //Debug.Log("Instance effect : " + GameManager.Instance.Effect);
        if (GameManager.Instance.Effect == Effects.NONE /*|| GameStateManager.Instance.CurrentState.GetType() != GameStateManager.Instance.GameSetupState.GetType()*/)
        {
            Debug.Log("GameStateError");
            return;
        }
        if (action._card == null) return;
        switch (action._effect)
        {
            case Effects.MOVE:
                if (action._card.CompareTag("Cauldron") || action._card.CompareTag("Monster") || action._card.CompareTag("ShieldedKnight")) return;
                Vector2Int newPos = GameManager.Instance.Board.GetPositionNextTo(action._card.PositionOnBoard, action._direction);
                //Debug.Log("newPos is : " + newPos);
                StartCoroutine(GameManager.Instance.Board.MoveCard(action._card, newPos));
                //GameManager.Instance.ActionCount.Decrement(1);
                break;
            case Effects.SWAP:
                if (action._card2 == null) return;
                GameManager.Instance.Board.SwitchCard(action._card, action._card2);
                //Debug.Log("Swapping Cards");
                //GameManager.Instance.ActionCount.Decrement(1);
                break;
            case Effects.INVOKE:
                Invocation invocation = FindObjectOfType<Invocation>();
                //invocation?.PlaceCardAtMousePosition();
                break;
        }
    }

    public IEnumerator MoveCardCoroutine(System.Action<Direction> callback)
    {
        Direction _moveCardDir = Direction.NONE;
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
        callback?.Invoke(_moveCardDir);
    }

    public Action CreateAction(Card card)
    {
        Action action = new Action();
        action._card = card;
        action._effect = GameManager.Instance.Effect;
        action._direction = card.Direction;
        return action;
    }
    public Action CreateAction(Card card, Card card2)
    {
        Action action = new Action();
        action._card = card;
        action._card2 = card2;
        action._effect = GameManager.Instance.Effect;
        action._direction = card.Direction;
        return action;
    }
}
