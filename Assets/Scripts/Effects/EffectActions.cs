using System.Collections;
using UnityEngine;

public class EffectActions : MonoBehaviour
{
    #region Instance
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
        DontDestroyOnLoad(this);
    }
    
    #endregion
    private Vector2 _firstMousePos;
    private Vector2 _lastMousePos;
    private Direction _moveCardDir;

    private Card _swapFirstCard;
    private Card _swapSecondCard;
    
    
    
    public Card SwapFirstCard { get => _swapFirstCard; set => _swapFirstCard = value; }
    public Card SwapSecondCard{ get => _swapSecondCard; set => _swapSecondCard = value; }

    public bool DoEffect(Action action)
    {
        bool actionDone = false;
        if (action._card == null) return false;
        switch (action._effect)
        {
            case Effects.MOVE:
                if (action._card.CardType == CardType.CAULDRON || action._card.CardType == CardType.MONSTER || action._card.CardType == CardType.KNIGHTSHIELD) return false;
                Vector2Int newPos = GameManager.Instance.BoardController.GetPositionNextTo(action._card.PositionOnBoard, action._direction);
                StartCoroutine(GameManager.Instance.BoardController.MoveCard(action._card, newPos, b => { actionDone = b;} ));
                PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQBA");
                break;
            case Effects.SWAP:
                if (action._card2 == null) return false;
                StartCoroutine(GameManager.Instance.BoardController.SwitchCard(action._card, action._card2, b => { actionDone = b;}));
                PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQBQ");
                break;
            case Effects.INVOKE:
                return false;
        }
        return actionDone;
    }

    private IEnumerator DestroyCard(Card card)
    {
        yield return new WaitForSeconds(.3f);
        Destroy(card.gameObject);
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

    public Action CreateAction(Card card, Vector2Int position)
    {
        Action action = new Action();
        action._card = card;
        action._effect = GameManager.Instance.Effect;
        action._direction = card.Direction;
        action._position = position;
        return action;
    }
}
