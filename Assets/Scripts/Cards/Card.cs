using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] CardType _cardType;

    [SerializeField] SpriteRenderer _darkenedEffect;

    [SerializeField] private bool _canMove;
    [SerializeField] private bool _canSwap;

    public CardType CardType
    {
        get { return _cardType; }
    }

    [Tooltip("None if anything other than KnightSword")]
    [SerializeField]
    private Direction _direction;

    public Direction Direction
    {
        get => _direction;
        set => _direction = value;
    }

    [SerializeField] private Direction _attackDirection;

    public Direction AttackDirection
    {
        get => _attackDirection;
        set => _attackDirection = value;
    }

    [SerializeField] private int _foodValue;

    public int FoodValue
    {
        get => _foodValue;
    }

    [SerializeField] private Vector2Int _positionOnBoard;

    public Vector2Int PositionOnBoard
    {
        get { return _positionOnBoard; }
        set { _positionOnBoard = value; }
    }

    [SerializeField] private TextMeshProUGUI _monsterScoreTXT;


    [SerializeField] private List<Transform> _actionSlots = new List<Transform>();

    public List<Transform> ActionSlots
    {
        get { return _actionSlots; }
        set { _actionSlots = value; }
    }

    [SerializeField] private Animator _animator;

    public Animator Animator
    {
        get { return _animator; }
    }

    private void OnMouseDown()
    {
        switch (GameManager.Instance.Effect)
        {
            case Effects.NONE:
                break;
            case Effects.MOVE:
                StartCoroutine(EffectActions.Instance.MoveCardCoroutine((direction) =>
                {
                    this._direction = direction;
                    Action moveAction = EffectActions.Instance.CreateAction(this);
                    ListAction.Instance.AddAction(moveAction);
                }));
                break;
            case Effects.SWAP:
                Card card = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))
                    .GetComponent<Card>();
                if (card == null) return;
                if (EffectActions.Instance._swapFirstCard == null)
                {
                    EffectActions.Instance._swapFirstCard = card;
                }
                else
                {
                    if (EffectActions.Instance._swapFirstCard == card) return;
                    EffectActions.Instance._swapSecondCard = card;
                    Action switchAction = EffectActions.Instance.CreateAction(EffectActions.Instance._swapFirstCard,
                        EffectActions.Instance._swapSecondCard);

                    ListAction.Instance.AddAction(switchAction);

                    //EffectActions.Instance.DoEffect(switchAction);
                    EffectActions.Instance._swapFirstCard = null;
                    EffectActions.Instance._swapSecondCard = null;
                }

                break;
        }

        Collider2D effectClicked = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public void DoEndOfTurnActions() // To use at end of turn to make Knights attack
    {

        switch (CardType)
        {
            case CardType.HUMAN:
                break;
            case CardType.KNIGHTSWORD:
                Card target = GameManager.Instance.Board.GetCardClose(this.PositionOnBoard, this._attackDirection);
                if (target != null)
                {
                    if (target.CardType == CardType.MINIMONSTER) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQCg");
                    StartCoroutine(target.OnDie());
                }
                else AudioManager.Instance.PlaySFX("swordSlash");
                break;
            case CardType.KNIGHTSHIELD:
                break;
            case CardType.MONSTER:
                break;
            case CardType.MINIMONSTER:
                break;
            case CardType.CAULDRON:
                break;
        }
    }

    public IEnumerator OnDie()
    {
        switch (this._cardType)
        {
            case(CardType.HUMAN):
                _animator.SetBool("Die", true);
                _animator.SetTrigger("Hit");
                AudioManager.Instance.PlaySFX("swordHit");
                yield return new WaitForSeconds(0.7f);
                GameManager.Instance.Board.ClearSlot(this);
                GameManager.Instance.MonsterScore += _foodValue;
                GameManager.Instance.HumanKill++;
                AudioManager.Instance.PlaySFX("death");
                break;
            case(CardType.KNIGHTSHIELD):
                _animator.SetTrigger("Hit");
                AudioManager.Instance.PlaySFX("swordClang");
                yield return new WaitForSeconds(0.5f);
                PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQDg");
                break;
            case(CardType.MONSTER):
                _animator.SetTrigger("Hit");
                AudioManager.Instance.PlaySFX("swordHit");
                yield return new WaitForSeconds(0.5f);
                GameManager.Instance.MonsterScore = 0;
                GameStateManager.Instance.SwitchState(GameStateManager.Instance.GameDefeatState);
                GameManager.Instance.Board.ClearSlot(this);
                break;
            case(CardType.MINIMONSTER):
                _animator.SetTrigger("Hit");
                yield return new WaitForSeconds(0.5f);
                GameManager.Instance.Board.ClearSlot(this);
                break;
            case(CardType.NONE):
                AudioManager.Instance.PlaySFX("swordSlash");
                break;
        }
    }

    public void ShowMonsterScore()
    {
        if(LevelManager.Instance.CurrentLevel >= GameManager.Instance.LevelDatabase.levelList.Count) return;
        _monsterScoreTXT.text = GameManager.Instance.MonsterScore.ToString() + "/" +
                                GameManager.Instance.LevelDatabase.levelList[LevelManager.Instance.CurrentLevel]
                                    .maxScore;
    }
    
    private void LockCard()
    {
        if (_cardType == CardType.KNIGHTSWORD)
        {
            if (GameManager.Instance.Effect == Effects.INVOKE)
            {
                _animator.SetBool("Dark", true);
            }
            else
            {
                _animator.SetBool("Dark", false);
            }
        }
        else
        {
            switch (GameManager.Instance.Effect)
            {
                case(Effects.MOVE):
                    if(!_canMove) _darkenedEffect.gameObject.SetActive(true);
                    break;
                case(Effects.SWAP):
                    if(!_canSwap) _darkenedEffect.gameObject.SetActive(true);
                    break;
                case(Effects.INVOKE):
                    _darkenedEffect.gameObject.SetActive(true);
                    break;
                case(Effects.NONE):
                    _darkenedEffect.gameObject.SetActive(false);
                    break;
            }
        }
    }
    
    private void Update()
    {
        if (_cardType == CardType.MONSTER) ShowMonsterScore();

        if (GameManager.Instance.GameState == GameState.Playable)
        {
            LockCard();
        }
        else
        {
            if (_cardType == CardType.KNIGHTSWORD) _animator.SetBool("Dark", false);
            else _darkenedEffect.gameObject.SetActive(false);
        }
    }
}
