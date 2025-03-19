using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public struct Slots
{
    public Card Card;
    public Transform Slot;
}


public class BoardController : MonoBehaviour
{
    [SerializeField] private List<Transform> _slots = new List<Transform>();
    [SerializeField] private List<Transform> _effectSlots = new List<Transform>();
    [SerializeField] private LevelDatabase _levelDatabase;
    [SerializeField] private List<GameObject> _effectBG = new List<GameObject>();
    
    
    [Header("Distribution")]
    [SerializeField] private Transform _handTransform;
    [SerializeField] private float _distrubDuration;
    
    //Private field
    private GameObject[] _effectGO = new GameObject[3];
    //private Card[,] _board = new Card[4, 3];
    //private Transform[,] _slotsTab = new Transform[4, 3];

    private Slots[,] _board = new Slots[4, 3];
    
    //Get
    //public Card[,] CardList => _board;
    public Slots[,] Board => _board;
    //public Transform[,] SlotsTab => _slotsTab;
    
    #region Clear
    
    
    public void ClearSlot(Card card)
    {
        InitSlotTab();  
        if (_board[card.PositionOnBoard.x, card.PositionOnBoard.y].Slot.childCount > 0)
            DestroyImmediate(_board[card.PositionOnBoard.x, card.PositionOnBoard.y].Slot.GetChild(0).gameObject);
        _board[card.PositionOnBoard.x, card.PositionOnBoard.y].Card = null;
    }

    public void ClearSlot(Vector2Int position)
    {
        InitSlotTab();  
        if (PositionInBounds(position))
        {
            if (_board[position.x, position.y].Slot.childCount > 0)
                DestroyImmediate(_board[position.x, position.y].Slot.GetChild(0).gameObject);
            _board[position.x, position.y].Card = null;
        }
    }
    
    public void ClearEffect()
    {
        foreach (var go in _effectGO)
        {
            if (go != null)
            {
                DestroyImmediate(go);
            }
        }
    }

    /// <summary>
    /// Clear board
    /// </summary>
    public void ResetBoard()
    {
        InitSlotTab();
        if (_board == null) return;

        foreach (var card in _slots)
        {
            if (card.childCount > 0)
            {
                ClearSlot(card.GetComponentInChildren<Card>());
            }
        }

        Destroy(GameManager.Instance.TutoParent.GetChild(0));
        
        ClearEffect();
    }
    
    
    
    #endregion

    #region Set
    
    /// <summary>
    /// Transform slot list into 2D array
    /// </summary>
    public void InitSlotTab()
    {
        int index = 0;

        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                _board[i, j].Slot = _slots[index];
                index++;
            }
        }
    }

    
    /// <summary>
    /// Add a Card to a slot in board
    /// </summary>
    /// <param name="card">Cards type, Can be null</param>
    public IEnumerator SetSlots(CardParams cardParams)
    {
        InitSlotTab();

        var prefab = _levelDatabase.GetPrefab(cardParams.cardType);
        if (prefab == null) yield break;

        var GO = Instantiate(prefab, _handTransform);
        var card = GO.GetComponent<Card>();
        if (card == null) yield break;
        card.PositionOnBoard = cardParams.positionOnBoard;
        card.AttackDirection = cardParams.direction;
        if (PositionInBounds(card.PositionOnBoard))
        {
            _board[card.PositionOnBoard.x, card.PositionOnBoard.y].Card = card;
            if(card.CardType == CardType.KNIGHTSWORD) SetKnightDirection(card);
            card.transform.DOMove(_board[card.PositionOnBoard.x, card.PositionOnBoard.y].Slot.position, _distrubDuration);
            AudioManager.Instance.PlaySFX("mixing");
            yield return new WaitForSeconds( _distrubDuration);
            card.transform.parent = _board[card.PositionOnBoard.x, card.PositionOnBoard.y].Slot;
            card.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void SetSlots(Card card)
    {
        if (PositionInBounds(card.PositionOnBoard))
        {
            _board[card.PositionOnBoard.x, card.PositionOnBoard.y].Card = card;
            card.transform.parent = _board[card.PositionOnBoard.x, card.PositionOnBoard.y].Slot;
            card.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
    
    public void EditorSetSlots(CardParams cardParams)
    {
        InitSlotTab();

        var prefab = _levelDatabase.GetPrefab(cardParams.cardType);
        if (prefab == null) return;

        var GO = Instantiate(prefab, _handTransform);
        var card = GO.GetComponent<Card>();
        if (card == null) return;
        card.PositionOnBoard = cardParams.positionOnBoard;
        card.AttackDirection = cardParams.direction;
        if (PositionInBounds(card.PositionOnBoard))
        {
            _board[card.PositionOnBoard.x, card.PositionOnBoard.y].Card = card;
            if(card.CardType == CardType.KNIGHTSWORD) SetKnightDirection(card);
            card.transform.parent = _board[card.PositionOnBoard.x, card.PositionOnBoard.y].Slot;
            card.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
    
    public void SetEffect(int index)
    {
        switch (index)
        {
            case(0): _effectGO[0] = Instantiate(_levelDatabase.moveEffectPrefab, _effectSlots[0]); break;
            case(1): _effectGO[1] = Instantiate(_levelDatabase.switchEffectPrefab,_effectSlots[1]); break;
            case(2): _effectGO[2] = Instantiate(_levelDatabase.invocationEffectPrefab, _effectSlots[2]); break;
        }
    }
    
    public void SetKnightDirection(Card card)
    {
        if (card == null) return;
        switch (card.AttackDirection)
        {
            case(Direction.UP): card.Animator.SetBool("Up", true); break;
            case(Direction.RIGHT): card.Animator.SetBool("Right", true); break;
            case(Direction.DOWN): card.Animator.SetBool("Down", true); break;
            case(Direction.LEFT): card.Animator.SetBool("Left", true); break;
        }
    }

    public void SetTuto(Level level)
    {
        if (level.tutoPrefab != null) Instantiate(level.tutoPrefab, GameManager.Instance.TutoParent);
    }

    [SerializeField] private TextMeshProUGUI _levelNumberTXT;
    /// <summary>
    /// Setup card on board
    /// </summary>
    /// <param name="level">Level to setup</param>
    public IEnumerator SetLevel(Level level)
    {
        GameManager.Instance.GameState = GameState.Busy;
        InitSlotTab();
        // ResetBoard();
        for (int i = 0; i < 3; i++)
        {
            if (level.effects[i])
            {
                SetEffect(i);
            }
        }

        _levelNumberTXT.text = "Level   " + (level.level + 1).ToString();
        
        foreach (var card in level.CardsList)
        {
            if (card.cardType != CardType.NONE)
            {
                StartCoroutine(SetSlots(card));
                yield return new WaitForSeconds( _distrubDuration - 0.1f);
            }
        }
        SetCollider();
        SetTuto(level);
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.GameState = GameState.Playable;
    }
    
    public void EditorSetLevel(Level level)
    {
        InitSlotTab();
        ResetBoard();
        for (int i = 0; i < 3; i++)
        {
            if (level.effects[i])
            {
                SetEffect(i);
            }
        }
        
        foreach (var card in level.CardsList)
        {
            if (card.cardType != CardType.NONE)
            {
                EditorSetSlots(card);
            }
        }
        SetCollider();
    }

    public void SetCollider()
    {
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                if (!SlotEmpty(new(i, j)))
                {
                    _board[i, j].Slot.GetComponent<BoxCollider2D>().enabled = false;
                }
                else
                {
                    _board[i, j].Slot.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
    }
    
    private void UpdateEffectUI()
    {
        switch (GameManager.Instance.Effect)
        {
            case(Effects.MOVE): 
                _effectBG[0].SetActive(true);
                _effectBG[1].SetActive(false);
                _effectBG[2].SetActive(false);
                break;
            case(Effects.SWAP): 
                _effectBG[0].SetActive(false);
                _effectBG[1].SetActive(true);
                _effectBG[2].SetActive(false);
                break;
            case(Effects.INVOKE): 
                _effectBG[0].SetActive(false);
                _effectBG[1].SetActive(false);
                _effectBG[2].SetActive(true);
                break;
            case(Effects.NONE): 
                _effectBG[0].SetActive(false);
                _effectBG[1].SetActive(false);
                _effectBG[2].SetActive(false);
                break;
        }
    }
    #endregion
    
    #region Test

    /// <summary>
    ///  Check if position is in board
    /// </summary>
    /// <param name="position">Position to check</param>
    /// <returns>Is in board</returns>
    bool PositionInBounds(Vector2Int position)
    {
        return position.x < _board.GetLength(0) &&
               position.x >= 0 &&
               position.y < _board.GetLength(1) &&
               position.y >= 0;
    }

    
    /// <summary>
    /// Get card in slot close
    /// </summary>
    /// <param name="position">Initial position</param>
    /// <param name="direction">Direction</param>
    /// <returns></returns>
    public Card GetCardClose(Vector2Int position, Direction direction)
    {
        InitSlotTab();
        Card card = null;
        switch (direction)
        {
            case (Direction.RIGHT):
                if (PositionInBounds(position + new Vector2Int(0, 1)))
                {
                    card = _board[position.x + 0, position.y + 1].Card;
                }

                break;
            case (Direction.LEFT):
                if (PositionInBounds(position + new Vector2Int(0, -1)))
                {
                    card = _board[position.x, position.y - 1].Card;
                }

                break;
            case (Direction.UP):
                if (PositionInBounds(position + new Vector2Int(-1, 0)))
                {
                    card = _board[position.x - 1, position.y].Card;
                }

                break;
            case (Direction.DOWN):
                if (PositionInBounds(position + new Vector2Int(1, 0)))
                {
                    card = _board[position.x + 1, position.y].Card;
                }

                break;
        }

        return card;
    }

    /// <summary>
    ///  Check if slot is empty
    /// </summary>
    /// <param name="newPos"></param>
    /// <returns></returns>
    public bool SlotEmpty(Vector2Int newPos)
    {
        InitSlotTab();
        return _board[newPos.x, newPos.y].Card == null;
    }

    /// <summary>
    ///  Get position
    /// </summary>
    /// <param name="position">Initial position</param>
    /// <param name="direction">Direction position you want</param>
    /// <returns></returns>
    public Vector2Int GetPositionNextTo(Vector2Int position, Direction direction)
    {
        InitSlotTab();
        switch (direction)
        {
            case (Direction.RIGHT):
                if (PositionInBounds(position + new Vector2Int(0, 1)))
                {
                    return position + new Vector2Int(0, 1);
                }

                break;
            case (Direction.LEFT):
                if (PositionInBounds(position + new Vector2Int(0, -1)))
                {
                    return position + new Vector2Int(0, -1);
                }

                break;
            case (Direction.UP):
                if (PositionInBounds(position + new Vector2Int(-1, 0)))
                {
                    return position + new Vector2Int(-1, 0);
                }

                break;
            case (Direction.DOWN):
                if (PositionInBounds(position + new Vector2Int(1, 0)))
                {
                    return position + new Vector2Int(1, 0);
                }

                break;
        }

        return position;
    }


    
    #endregion
    
    #region Action
    /// <summary>
    ///  Move card to slots
    /// </summary>
    /// <param name="card"></param>
    /// <param name="newPos"></param>
    public IEnumerator MoveCard(Card card, Vector2Int newPos, System.Action<bool> callback)
    {
        InitSlotTab();

        Card cardOnTarget = _board[newPos.x, newPos.y].Card;
        if (PositionInBounds(newPos) && SlotEmpty(newPos))
        {
            _board[card.PositionOnBoard.x, card.PositionOnBoard.y].Card = null;
            card.PositionOnBoard = newPos;
            DOTween.Init();
            card.transform.DOMove(_board[card.PositionOnBoard.x, card.PositionOnBoard.y].Slot.position, GameManager.Instance.TimerList.MovementAnimationDuration);
            AudioManager.Instance.PlaySFX("swipe");
            callback(true);
            yield return new WaitForSeconds(GameManager.Instance.TimerList.MovementAnimationDuration + Time.deltaTime);
            SetSlots(card);
        } 
        else if (card.CardType == CardType.KNIGHTSWORD && cardOnTarget.CardType == CardType.CAULDRON)
        {
            _board[card.PositionOnBoard.x, card.PositionOnBoard.y].Card = null;
            card.PositionOnBoard = newPos;
            DOTween.Init();
            card.transform.DOMove(_board[card.PositionOnBoard.x, card.PositionOnBoard.y].Slot.position, GameManager.Instance.TimerList.MovementAnimationDuration);
            AudioManager.Instance.PlaySFX("swipe");
            yield return new WaitForSeconds(GameManager.Instance.TimerList.MovementAnimationDuration + Time.deltaTime);
            AudioManager.Instance.PlaySFX("death");
            card.Animator.SetTrigger("Burn");
            PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQCQ");
            callback(true);

            yield return new WaitForSeconds(GameManager.Instance.TimerList.CauldronWait);
            GameManager.Instance.MonsterScore += card.FoodValue;
            Destroy(card.gameObject);
        } else
        {
            card.Animator.SetBool("Die" , false);
            card.Animator.SetTrigger("Hit");
            callback(false);
        }
    }

    /// <summary>
    /// Switch card position
    /// </summary>
    /// <param name="c1">First card</param>
    /// <param name="c2">Second card</param>
    public IEnumerator SwitchCard(Card c1, Card c2, System.Action<bool> callback)
    {
        InitSlotTab();

        if (c1 == null || c2 == null)
        {
            yield break;
            callback(false);
        }
        Vector2Int temp = c1.PositionOnBoard;
        c1.PositionOnBoard = c2.PositionOnBoard;
        c2.PositionOnBoard = temp;
        _board[c1.PositionOnBoard.x, c1.PositionOnBoard.y].Card = null;
        _board[c2.PositionOnBoard.x, c2.PositionOnBoard.y].Card = null;

        if (c1.Animator != null)
        {
            c1.Animator.SetTrigger("Swap");
        }
        if (c2.Animator != null)
        {
            c2.Animator.SetTrigger("Swap");
        }
        AudioManager.Instance.PlaySFX("teleport");

        callback(true);
        yield return new WaitForSeconds(GameManager.Instance.TimerList.SwapDelay);
        
        SetSlots(c1);
        SetSlots(c2);
    }

    public IEnumerator DoAllEndAction()
    {
        yield return new WaitForSeconds(GameManager.Instance.TimerList.BeforeEndActionWait); 
        foreach (var slot in _board)
        {
            if(slot.Card != null) slot.Card.DoEndOfTurnActions();
        }

        yield return new WaitForSeconds(GameManager.Instance.TimerList.AfterEndActionWait);
        StartCoroutine(GameStateManager.Instance.CurrentState.ExitState(GameStateManager.Instance));
    }

    public void StartEndAction()
    {
        StartCoroutine(DoAllEndAction());
    }


    private void Start()
    {
        InitSlotTab();
    }
    
    #endregion

    private void Update()
    {
        UpdateEffectUI();
    }
}