using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListAction : MonoBehaviour
{
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
            Debug.Log("Doing : " + action._card);
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
        GameManager.Instance.ActionCount.Decrement(1);
        if (!GameManager.Instance.ActionCount.ActionRemaining())
        {
            StartCoroutine(StartListAction());
        }
    }
    public void RemoveLastAction()
    {
        _listActions.Remove(_listActions[^1]);
        GameManager.Instance.ActionCount.Increment(1);
    }

    public void ClearListAction()
    {
        _listActions.Clear();
    }
}
