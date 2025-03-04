using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Effects _effect;
    public Effects Effect
    {
        get => _effect;
        set => _effect = value;
    }
    #region Instance

    private static GameManager _instance;

    public static GameManager Instance { get => _instance; }

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

    private void Start()
    {
        EffectList.Effects = Effects.NONE;
        EffectList.MoveCard = false;
        EffectList.SwapCard = false;

    }
    #endregion

    [SerializeField] private Board _board;
    public Board Board { get => _board; }

    [SerializeField] private LevelDatabase _levelDatabase;
    public LevelDatabase LevelDatabase {get => _levelDatabase; }

    [SerializeField] private ListAction _listAction;
    public ListAction ListActions { get => _listAction; }

    [SerializeField] private ActionCount _actionCount;
    public ActionCount ActionCount { get => _actionCount; }

    private int _monsterScore;
    public int MonsterScore { get => _monsterScore; set => _monsterScore = value; }


    [SerializeField] private List<GameObject> _effectBG = new List<GameObject>();

    private GameState _gameState;
    public GameState GameState { get => _gameState; set => _gameState = value; }

    private List<Card> _miniMonsterCards = new List<Card>();
    public List<Card> MiniMonsterCards { get => _miniMonsterCards; set => _miniMonsterCards = value; }

    private int humanKill;
    public int HumanKill { get => humanKill; set => humanKill = value; }
    
    private void Update()
    {
        if (GameState == GameState.Busy) Effect = Effects.NONE;
        UpdateEffectUI();
    }

    private void UpdateEffectUI()
    {
        switch (_effect)
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

        if (humanKill >= 20) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQDw");
    }
}