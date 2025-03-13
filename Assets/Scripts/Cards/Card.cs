using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Cards params")]
    [SerializeField] CardType _cardType;
    [Tooltip("None if anything other than KnightSword")]
    [SerializeField] private int _foodValue;
    [SerializeField] private bool _canMove;
    [SerializeField] private bool _canSwap;
    
    private Direction _direction;
    private Direction _attackDirection;
    private Vector2Int _positionOnBoard;

    [Header("Component")]
    [SerializeField] private Animator _animator;
    
    [Header("Feedback")]
    [SerializeField] private List<Transform> _actionSlots = new List<Transform>();
    [SerializeField] SpriteRenderer _darkenedEffect;
    [SerializeField] private TextMeshProUGUI _monsterScoreTXT;
    
    #region Getters / Setters
    public CardType CardType { get { return _cardType; } }

    public Direction Direction { get => _direction; }

    public Direction AttackDirection { get => _attackDirection; set => _attackDirection = value; }

    public int FoodValue { get => _foodValue; }

    public Vector2Int PositionOnBoard { get { return _positionOnBoard; } set { _positionOnBoard = value; } }

    public List<Transform> ActionSlots { get { return _actionSlots; } set { _actionSlots = value; } }
    public Animator Animator { get { return _animator; } }
    #endregion

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
                if (EffectActions.Instance.SwapFirstCard == null)
                {
                    EffectActions.Instance.SwapFirstCard = card;
                }
                else
                {
                    if (EffectActions.Instance.SwapFirstCard == card) return;
                    EffectActions.Instance.SwapSecondCard = card;
                    Action switchAction = EffectActions.Instance.CreateAction(EffectActions.Instance.SwapFirstCard,
                        EffectActions.Instance.SwapSecondCard);

                    ListAction.Instance.AddAction(switchAction);

                    //EffectActions.Instance.DoEffect(switchAction);
                    EffectActions.Instance.SwapFirstCard = null;
                    EffectActions.Instance.SwapSecondCard = null;
                }
                break;
        }
    }

    public void DoEndOfTurnActions() // To use at end of turn to make Knights attack
    {
        switch (CardType)
        {
            case CardType.HUMAN:
                break;
            case CardType.KNIGHTSWORD:
                Card target = GameManager.Instance.BoardController.GetCardClose(this.PositionOnBoard, this._attackDirection);
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
                yield return new WaitForSeconds(GameManager.Instance.TimerList.HitAniamtionDuration);
                GameManager.Instance.BoardController.ClearSlot(this);
                GameManager.Instance.MonsterScore += _foodValue;
                GameManager.Instance.HumanKill++;
                AudioManager.Instance.PlaySFX("death");
                break;
            case(CardType.KNIGHTSHIELD):
                _animator.SetTrigger("Hit");
                AudioManager.Instance.PlaySFX("swordClang");
                yield return new WaitForSeconds(GameManager.Instance.TimerList.HitAniamtionDuration);
                PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQDg");
                break;
            case(CardType.MONSTER):
                _animator.SetTrigger("Hit");
                AudioManager.Instance.PlaySFX("swordHit");
                yield return new WaitForSeconds(GameManager.Instance.TimerList.BurnAnimationDuration);
                GameManager.Instance.MonsterScore = 0;
                GameStateManager.Instance.SwitchState(GameStateManager.Instance.GameDefeatState);
                GameManager.Instance.BoardController.ClearSlot(this);
                break;
            case(CardType.MINIMONSTER):
                _animator.SetTrigger("Hit");
                yield return new WaitForSeconds(GameManager.Instance.TimerList.BurnAnimationDuration);
                GameManager.Instance.BoardController.ClearSlot(this);
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
            if (GameManager.Instance.Effect == Effects.INVOKE) { _animator.SetBool("Dark", true); }
            else { _animator.SetBool("Dark", false); }
        }
        else
        {
            switch (GameManager.Instance.Effect)
            {
                case(Effects.MOVE): if(!_canMove) _darkenedEffect.gameObject.SetActive(true); break;
                case(Effects.SWAP): if(!_canSwap) _darkenedEffect.gameObject.SetActive(true); break;
                case(Effects.INVOKE): _darkenedEffect.gameObject.SetActive(true); break;
                case(Effects.NONE): _darkenedEffect.gameObject.SetActive(false); break;
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
