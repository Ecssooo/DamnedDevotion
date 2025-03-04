using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    #region Instance
    private static LevelManager _instance;

    public static LevelManager Instance { get => _instance; }

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

    private int _currentLevel;
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }

    #region UpdateLevel

    public void IncreaseLevel()
    {
        _currentLevel++;
        if (_currentLevel > GameManager.Instance.LevelDatabase.levelList.Count - 1)
            _currentLevel = GameManager.Instance.LevelDatabase.levelList.Count - 1;
        UpdateUI();
    }

    public void DecreaseLevel()
    { 
        _currentLevel--; 
        if (_currentLevel < 0)
            _currentLevel = 0;
        UpdateUI();
    }

    public void InitLevel(int value)
    {
        if (value < 0) SaveSystem.InitSave();
        
        _currentLevel = value; 
        UpdateUI();
    }
    #endregion
    
    #region UI

    [SerializeField] private GameObject _mainScreen;
    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private GameObject _defeatScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _endScreen;

    [Header("Game")] 
    [SerializeField] private GameObject _popUp;
    
    [Header("Level Selector")]
    [SerializeField] private GameObject _levelSelectorScreen;
    [SerializeField] private TextMeshProUGUI TXT_number;
    [SerializeField] private GameObject _locker;
    
    [SerializeField] private GameObject _moveLS;
    [SerializeField] private GameObject _swapLS;
    [SerializeField] private GameObject _invokeLS;
    
    
    void UpdateUI()
    {
        TXT_number.text = (_currentLevel+1).ToString();
        if (_currentLevel > SaveSystem.Load())
        {
            _locker.SetActive(true);
        }
        else
        {
            _locker.SetActive(false);
        }

        _moveLS.SetActive(false);
        _swapLS.SetActive(false);
        _invokeLS.SetActive(false);
        
        Level level = GameManager.Instance.LevelDatabase.levelList[_currentLevel];
        for (int i = 0; i < level.effects.Length; i++)
        {
            if (level.effects[i])
            {
                switch (i)
                {
                    case(0): _moveLS.SetActive(true); break;
                    case(1): _swapLS.SetActive(true); break;
                    case(2): _invokeLS.SetActive(true); break;
                }
            }
        }
    }

    public void LoadMenu()
    {
        _game.SetActive(false);
        GameManager.Instance.Board.ResetBoard();
        _defeatScreen.SetActive(false);
        _winScreen.SetActive(false);
        _endScreen.SetActive(false);
        _mainScreen.SetActive(false);
        _pauseScreen.SetActive(false);
        _levelSelectorScreen.SetActive(true);
    }
    
    public void LoadDefeatMenu()
    {
        _defeatScreen.SetActive(true);
        _pauseScreen.SetActive(false);
    }
    
    public void LoadWinMenu()
    {
        if (LevelManager.Instance.CurrentLevel == GameManager.Instance.LevelDatabase.levelList.Count-1)
        {
            _endScreen.SetActive(true);
            _pauseScreen.SetActive(false);
        }
        else
        {
            _pauseScreen.SetActive(false);
            _winScreen.SetActive(true);
        }
    }
    
    public void LoadMainScreen()
    {
        _mainScreen.SetActive(true);
        _pauseScreen.SetActive(false);
        _game.SetActive(false);
        _levelSelectorScreen.SetActive(false);
        _winScreen.SetActive(false);
        _defeatScreen.SetActive(false);
    }
    
    #endregion

    #region Game
    
    public void LoadLevel()
    {
        _levelSelectorScreen.SetActive(false);
        _game.SetActive(true);
        _winScreen.SetActive(false);
        _defeatScreen.SetActive(false);
        StartCoroutine(GameManager.Instance.Board.SetLevel(GameManager.Instance.LevelDatabase.levelList[_currentLevel]));

        EffectList.MoveCard = false;
        EffectList.SwapCard = false;
        GameManager.Instance.Effect = Effects.NONE;

        ListAction.Instance.ListActions.Clear();
    }

    public void LoadPopUp()
    {
        _popUp.SetActive(true);
    }
   
    #endregion

    private void Start()
    {
        SaveSystem.InitSave();
        InitLevel(SaveSystem.Load());
        UpdateUI();
    }

    private void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveSystem.Save(0);
            Debug.Log("Saved");
        }
        #endif
    }
    

}
