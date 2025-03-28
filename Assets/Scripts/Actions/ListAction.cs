using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListAction : MonoBehaviour
{
    [SerializeField] private GameObject _moveEffectLeftPrefab;
    [SerializeField] private GameObject _moveEffectRightPrefab;
    [SerializeField] private GameObject _moveEffectUpPrefab;
    [SerializeField] private GameObject _moveEffectDownPrefab;
    [SerializeField] private GameObject _switchEffectPrefab;
    private bool HasAppliedEffect = false;
    private bool hasAppliedFirstCard = false;
    
    private bool HasFirstCardFreeToken = true;
    private bool HasSecondCardFreeToken = true;
    
    #region Instance

    private static ListAction _instance;

    public static ListAction Instance { get => _instance; }

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
    #endregion

    private List<Action> _listActions = new List<Action>();
    public List<Action> ListActions
    {
        get => _listActions;
        set => _listActions = value;
    }

    public IEnumerator StartListAction()
    {
        bool actionDone = true;
        GameManager.Instance.GameState = GameState.Busy;
        foreach (var action in _listActions)
        {
            actionDone = EffectActions.Instance.DoEffect(action);
            if (actionDone)
            {
                switch (action._effect)
                {
                    case (Effects.MOVE):
                        yield return new WaitForSeconds(GameManager.Instance.TimerList.MovementWait);
                        break;
                    case (Effects.SWAP):
                        yield return new WaitForSeconds(GameManager.Instance.TimerList.SwapWait);
                        break;
                    case (Effects.INVOKE):
                        yield return new WaitForSeconds(GameManager.Instance.TimerList.InvokeWait);
                        break;
                }

                if (action._effect == Effects.SWAP && (action._card.CardType == CardType.MINIMONSTER ||
                                                       action._card2.CardType == CardType.MINIMONSTER))
                    PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQBw");


                if (action._effect != Effects.INVOKE && action._card.CardType == CardType.MINIMONSTER)
                {
                    yield return new WaitForSeconds(GameManager.Instance.TimerList.BurnAnimationDuration);
                    action._card.Animator.SetTrigger("Burn");
                    yield return new WaitForSeconds(GameManager.Instance.TimerList.BurnAnimationDuration);
                    GameManager.Instance.BoardController.ClearSlot(action._card.PositionOnBoard);
                }

                if (action._card2 != null)
                {
                    if (action._card2.CardType == CardType.MINIMONSTER)
                    {
                        action._card2.Animator.SetTrigger("Burn");
                        yield return new WaitForSeconds(GameManager.Instance.TimerList.BurnAnimationDuration);
                        GameManager.Instance.BoardController.ClearSlot(action._card2.PositionOnBoard);
                    }
                }
            }

            yield return new WaitForSeconds(GameManager.Instance.TimerList.ImpossibleActionWait);
        }
        GameManager.Instance.BoardController.StartEndAction();
        GameManager.Instance.GameState = GameState.Playable;
    }

    public void AddAction(Action action)
    {
        if (!GameManager.Instance.ActionCount.ActionRemaining()) return;
        if (!CanBothCardReceiveTokens(action)) return;
        // Conditions to prevent adding action due to card type

        switch (action._effect)
        {
            case Effects.MOVE:
                if (action._card.CardType == CardType.CAULDRON ||
                    action._card.CardType == CardType.MONSTER ||
                    action._card.CardType == CardType.KNIGHTSHIELD)
                    return;
                break;
            case Effects.SWAP:
                if (action._card.CardType == CardType.CAULDRON ||
                    action._card2.CardType == CardType.CAULDRON)
                    return;
                break;
            case Effects.INVOKE:
                _listActions.Add(action);
                return;
        }


        // Add action to Card 

        foreach (var slot in action._card.ActionSlots)
        {
            if (slot.childCount == 0 && !HasAppliedEffect)
            {
                switch (action._effect)
                {
                    case Effects.SWAP:
                        GameObject newSwapAction = Instantiate(_switchEffectPrefab, slot);
                        hasAppliedFirstCard = true;
                        newSwapAction.transform.localScale = Vector3.one / 3;
                        newSwapAction.transform.localPosition = Vector3.zero;
                        _listActions.Add(action);
                        GameManager.Instance.ActionCount.Decrement(1);
                        HasAppliedEffect = true;
                        break;
                    case Effects.MOVE:
                        GameObject newMoveAction = null;
                        switch (action._direction)
                        {
                            case Direction.UP:
                                newMoveAction = Instantiate(_moveEffectUpPrefab, slot);
                                break;
                            case Direction.DOWN:
                                newMoveAction = Instantiate(_moveEffectDownPrefab, slot);
                                break;
                            case Direction.LEFT:
                                newMoveAction = Instantiate(_moveEffectLeftPrefab, slot);
                                break;
                            case Direction.RIGHT:
                                newMoveAction = Instantiate(_moveEffectRightPrefab, slot);
                                break;
                        }

                        newMoveAction.transform.localScale = Vector3.one / 3;
                        newMoveAction.transform.localPosition = Vector3.zero;
                        _listActions.Add(action);
                        GameManager.Instance.ActionCount.Decrement(1);
                        HasAppliedEffect = true;
                        break;
                }

            }
        }

        
        if (action._card2 != null && hasAppliedFirstCard)
        {
            HasAppliedEffect = false;
            foreach (var slot in action._card2.ActionSlots)
            {
                if (slot.childCount == 0 && !HasAppliedEffect)
                {
                    GameObject newSwapAction = Instantiate(_switchEffectPrefab, slot);
                    newSwapAction.transform.localScale = Vector3.one / 3;
                    newSwapAction.transform.localPosition = Vector3.zero;
                    HasAppliedEffect = true;
                }
            }
        }
        HasAppliedEffect = false;
        hasAppliedFirstCard = false;
    }
    public void RemoveLastAction()
    {
        if (GameManager.Instance.GameState != GameState.Playable || ScreenController.Instance.CurrentSecondScreenActive == SecondScreenActive.PopUp) return;
        if (GameManager.Instance.GameState == GameState.Playable)
        {
            GameStateManager.Instance.WaitForAction = true;
            if (_listActions.Count == 0) return;

            Debug.Log(_listActions[^1]._card.CardType);
            if (_listActions[^1]._card.CardType == CardType.MINIMONSTER && _listActions[^1]._effect == Effects.INVOKE)
            {
                Debug.Log("suppressing minimonster");
                GameManager.Instance.BoardController.ClearSlot(_listActions[^1]._card.PositionOnBoard);
                _listActions.RemoveAt(_listActions.Count - 1);
                GameManager.Instance.ActionCount.Increment(1);
                GameStateManager.Instance.WaitForAction = false;
                return;
            }

            //Remove icon from last action
            GameObject SlotToRemove = null;
            GameObject SlotToRemove2 = null;
            foreach (var LastSlot in _listActions[^1]._card.ActionSlots)
            {
                if (LastSlot.childCount > 0)
                {
                    SlotToRemove = LastSlot.GetChild(0).gameObject;
                }
            }

            if (_listActions[^1]._card2 != null)
            {
                foreach (var LastSlot in _listActions[^1]._card2.ActionSlots)
                {
                    if (LastSlot.childCount > 0)
                    {
                        SlotToRemove2 = LastSlot.GetChild(0).gameObject;
                    }
                }
            }

            Destroy(SlotToRemove.gameObject);
            if (SlotToRemove2 != null) Destroy(SlotToRemove2.gameObject);
            SlotToRemove = null;
            SlotToRemove2 = null;
            

            _listActions.RemoveAt(_listActions.Count - 1);
            GameManager.Instance.ActionCount.Increment(1);
            GameStateManager.Instance.WaitForAction = false;
        }
    }

    private bool CanBothCardReceiveTokens(Action action)
    {
        HasFirstCardFreeToken = false;
        HasSecondCardFreeToken = false;
        foreach (var slot in action._card.ActionSlots)
        {
            if (slot.childCount == 0 && !HasFirstCardFreeToken)
            {
                HasFirstCardFreeToken = true;
            }
        }
        if (action._card2 != null)
        {
            foreach (var slot in action._card2.ActionSlots)
            {
                if (slot.childCount == 0 && !HasSecondCardFreeToken)
                    HasSecondCardFreeToken = true;
            }
        }
        else
        {
            HasSecondCardFreeToken = true;
        }

        if (HasFirstCardFreeToken && HasSecondCardFreeToken)
            return true;

        return false;
    }
    
    public void ClearListAction()
    {
        _listActions.Clear();
    }

    public void StartListActionCoroutine() { StartCoroutine(StartListAction()); }
}
