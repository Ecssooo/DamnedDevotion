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

    [SerializeField] private float _moveEffectDuration;
    [SerializeField] private float _swapEffectDuration;
    [SerializeField] private float _invokeEffectDuration;
    
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
        GameManager.Instance.GameState = GameState.Busy;
        foreach (var action in _listActions)
        {
            EffectActions.Instance.DoEffect(action);
            switch (action._effect)
            {
                case(Effects.MOVE):
                    yield return new WaitForSeconds(_moveEffectDuration);
                    break;
                case(Effects.SWAP) :
                    yield return new WaitForSeconds(_swapEffectDuration);
                    break;
                case(Effects.INVOKE):
                    yield return new WaitForSeconds(_invokeEffectDuration);
                    break;
            }
            
            if(action._effect == Effects.SWAP && (action._card.CardType == CardType.MINIMONSTER || action._card2.CardType == CardType.MINIMONSTER))
                PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQBw");

            
            if (action._card.CardType == CardType.MINIMONSTER)
            {
                action._card.Animator.SetTrigger("Burn");
                yield return new WaitForSeconds(0.3f);
                GameManager.Instance.Board.ClearSlot(action._card.PositionOnBoard);
            }
            if (action._card2 != null)
            {
                if(action._card2.CardType == CardType.MINIMONSTER)
                {
                    action._card2.Animator.SetTrigger("Burn");
                    yield return new WaitForSeconds(0.3f);
                    GameManager.Instance.Board.ClearSlot(action._card2.PositionOnBoard);
                }
            }
        }
        GameManager.Instance.Board.StartEndAction();
        GameManager.Instance.GameState = GameState.Playable;
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
                    case Effects.MOVE:
                        GameObject newMoveAction = new GameObject();
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

                        //Deactivate the effect and collider
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
    }
    public void RemoveLastAction()
    {
        if (GameManager.Instance.GameState != GameState.Playable) return;
        if (GameManager.Instance.GameState == GameState.Playable)
        {

            if (_listActions.Count == 0) return;

            Debug.Log(_listActions[^1]._card.CardType);
            if (_listActions[^1]._card.CardType == CardType.MINIMONSTER)
            {
                Debug.Log("suppressing minimonster");
                GameManager.Instance.Board.ClearSlot(_listActions[^1]._card.PositionOnBoard);
                _listActions.RemoveAt(_listActions.Count - 1);
                GameManager.Instance.ActionCount.Increment(1);
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
        }
    }

    public void ClearListAction()
    {
        _listActions.Clear();
    }

    public void StartListActionCoroutine() { StartCoroutine(StartListAction()); }
}
