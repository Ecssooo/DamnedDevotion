using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListAction : MonoBehaviour
{
    [SerializeField] private GameObject _moveEffectPrefab;
    [SerializeField] private GameObject _switchEffectPrefab;
    [SerializeField] private GameObject _invocationEffectPrefab;
    private bool HasAppliedEffect = false;

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
        foreach (var action in _listActions)
        {
            EffectActions.Instance.DoEffect(action);
            yield return new WaitForSeconds(1f);
        }
        GameManager.Instance.Board.StartEndAction();
        //yield return new WaitForSeconds(1);
        //foreach (var card in _board)
        //{
        //    if (card != null) card.DoEndOfTurnActions();
        //}
        //GameStateManager.Instance.SwitchState(GameStateManager.Instance.GameWinState);
    }

    public void AddAction(Action action)
    {
        if (!GameManager.Instance.ActionCount.ActionRemaining()) return;
        // Conditions to prevent adding action due to card type

        switch (action._effect)
        {
            case Effects.MOVE:
                if (action._card.CompareTag("Cauldron") ||
                    action._card.CompareTag("Monster") ||
                    action._card.CompareTag("ShieldedKnight"))
                    return;
                break;
            case Effects.SWAP:
                if (action._card.CompareTag("Cauldron") ||
                    action._card2.CompareTag("Cauldron"))
                    return;
                break;
        }

        // Add action to Card 

        foreach (var slot in action._card.ActionSlots)
        {
            if (slot.childCount == 0 && !HasAppliedEffect)
            {
                switch (action._effect)
                {
                    case Effects.MOVE:
                        GameObject newMoveAction = Instantiate(_moveEffectPrefab, slot);
                        var moveEffect = newMoveAction.GetComponent<Effect>();
                        if (moveEffect != null)
                        {
                            moveEffect.enabled = false;
                        }
                        var moveCollider = newMoveAction.GetComponent<CircleCollider2D>();
                        if (moveCollider != null)
                        {
                            moveCollider.enabled = false;
                        }
                        newMoveAction.transform.localScale = Vector3.one / 3;
                        newMoveAction.transform.localPosition = Vector3.zero;
                        _listActions.Add(action);
                        GameManager.Instance.ActionCount.Decrement(1);
                        HasAppliedEffect = true;
                        break;
                    case Effects.SWAP:
                        GameObject newSwapAction = Instantiate(_switchEffectPrefab, slot);
                        var swapEffect = newSwapAction.GetComponent<Effect>();
                        if (swapEffect != null)
                        {
                            swapEffect.enabled = false;
                        }
                        var swapCollider = newSwapAction.GetComponent<CircleCollider2D>();
                        if (swapCollider != null)
                        {
                            swapCollider.enabled = false;
                        }
                        var switchPower = newSwapAction.GetComponent<SwitchPower>();
                        if (switchPower != null)
                        {
                            switchPower.enabled = false;
                        }
                        newSwapAction.transform.localScale = Vector3.one / 3;
                        newSwapAction.transform.localPosition = Vector3.zero;
                        _listActions.Add(action);
                        GameManager.Instance.ActionCount.Decrement(1);
                        HasAppliedEffect = true;
                        break;
                }

            }
        }
        if (action._card2 != null)
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
        if (!GameManager.Instance.ActionCount.ActionRemaining())
        {
            StartCoroutine(StartListAction());
        }
    }
    public void RemoveLastAction()
    {
        if (_listActions.Count == 0) return;
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
    }

    public void ClearListAction()
    {
        _listActions.Clear();
    }
}
