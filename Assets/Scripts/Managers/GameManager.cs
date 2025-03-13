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


    [SerializeField] private LevelDatabase _levelDatabase;
    
    [SerializeField] private TimerList _timerList;

    [SerializeField] private ActionCount _actionCount;

    //Private field
    private Board _board;
    private int humanKill;
    private int _monsterScore;
    private GameState _gameState;
    private Effects _effect;
    
    #region Getter / Setter
    
    //SF field
    public LevelDatabase LevelDatabase {get => _levelDatabase; }
    public TimerList TimerList { get => _timerList; }
    public ActionCount ActionCount { get => _actionCount; }

    
    //Private field
    public Board Board { get => _board; set => _board = value; }
    public int HumanKill { get => humanKill; set => humanKill = value; }
    public int MonsterScore { get => _monsterScore; set => _monsterScore = value; }
    public GameState GameState { get => _gameState; set => _gameState = value; }
    public Effects Effect { get => _effect; set => _effect = value; }
    
    #endregion

    [Header("TEMP")]
    [SerializeField] private Button _buttonReady;
    public Button ButtonReady { get => _buttonReady; }
    
    private void Update()
    {
        if (GameState == GameState.Busy) Effect = Effects.NONE;
        //UpdateEffectUI();
        
        if (humanKill >= 20) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQDw");
    }
    
}