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
    
    private IEnumerator StartListAction()
    {
        foreach (var action in _listActions)
        {
            EffectActions.Instance.DoEffect(action);
            yield return new WaitForSeconds(.5f);
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
        _listActions.Add(action);

        // Add action to Card 

        foreach (var slot in action._card.ActionSlots)
        {
            if (slot.childCount == 0 && !HasAppliedEffect)
            {
                switch (action._effect)
                {
                    case Effects.MOVE:
                        GameObject newMoveAction = Instantiate(_moveEffectPrefab, slot);
                        newMoveAction.transform.localScale = Vector3.one / 3;
                        newMoveAction.transform.localPosition = Vector3.zero;
                        HasAppliedEffect = true;
                        break;
                    case Effects.SWAP:
                        GameObject newSwapAction = Instantiate(_switchEffectPrefab, slot);
                        newSwapAction.transform.localScale = Vector3.one / 3;
                        newSwapAction.transform.localPosition = Vector3.zero;
                        HasAppliedEffect = true;
                        break;
                }

            }
        }

        GameManager.Instance.ActionCount.Decrement(1);
        HasAppliedEffect = false;
        if (!GameManager.Instance.ActionCount.ActionRemaining())
        {
            StartCoroutine(StartListAction());
        }
    }
    public void RemoveLastAction()
    {
        //Remove icon from last action
        GameObject SlotToRemove = null;
        foreach (var LastSlot in _listActions[^1]._card.ActionSlots)
        {
            if (LastSlot.childCount > 0)
            {
                SlotToRemove = LastSlot.GetChild(0).gameObject;
            }
        }
        Destroy(SlotToRemove.gameObject);
        Debug.Log("last card action : " + _listActions[^1]._card);
        SlotToRemove = null;

        _listActions.RemoveAt(_listActions.Count - 1); // Remove le dernier index
        //_listActions.Remove(_listActions[^1]);
        Debug.Log(_listActions.Count);
        GameManager.Instance.ActionCount.Increment(1);
    }

    public void ClearListAction()
    {
        _listActions.Clear();
    }
}
