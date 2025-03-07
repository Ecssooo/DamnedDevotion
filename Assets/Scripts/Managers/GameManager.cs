using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
    #endregion

    private void Start()
    {
        EffectList.Effects = Effects.NONE;
        EffectList.MoveCard = false;
        EffectList.SwapCard = false;

    }

    [SerializeField] private Board _board;
    public Board Board { get => _board; set => _board = value; }

    [SerializeField] private LevelDatabase _levelDatabase;
    public LevelDatabase LevelDatabase {get => _levelDatabase; }

    [SerializeField] private Effects _effect;
    public Effects Effect { get => _effect; set => _effect = value; }
    
    [SerializeField] private ActionCount _actionCount;
    public ActionCount ActionCount { get => _actionCount; }

    private int _monsterScore;
    public int MonsterScore { get => _monsterScore; set => _monsterScore = value; }
    

    private GameState _gameState;
    public GameState GameState { get => _gameState; set => _gameState = value; }

    private List<Card> _miniMonsterCards = new List<Card>();
    public List<Card> MiniMonsterCards { get => _miniMonsterCards; set => _miniMonsterCards = value; }

    private int humanKill;
    public int HumanKill { get => humanKill; set => humanKill = value; }

    [SerializeField] private Button _buttonReady;
    public Button ButtonReady { get => _buttonReady; }
    
    private void Update()
    {
        if (GameState == GameState.Busy) Effect = Effects.NONE;
        //UpdateEffectUI();
        
        if (humanKill >= 20) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQDw");
    }
    
}