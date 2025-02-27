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

    #region UpdateLevel
    private int _currentLevel;
    public int CurrentLevel { get => _currentLevel;}

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

    [SerializeField] private GameObject _canva;
    [SerializeField] private GameObject _board;
    [SerializeField] private GameObject _defeatScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private TextMeshProUGUI TXT_number;
    [SerializeField] private GameObject _locker;
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
    }

    #endregion

    #region Game
    
    public void LoadLevel()
    {
        _canva.SetActive(false);
        _board.SetActive(true);
        GetComponent<TutoPop>().PopUp();
        StartCoroutine(GameManager.Instance.Board.SetLevel(GameManager.Instance.LevelDatabase.levelList[_currentLevel]));

        EffectList.MoveCard = false;
        EffectList.SwapCard = false;
        GameManager.Instance.Effect = Effects.NONE;

        ListAction.Instance.ListActions.Clear();
        // GameStateManager.Instance.SwitchState(GameStateManager.Instance.GameSetupState);
    }

    public void LoadMenu()
    {
        _board.SetActive(false);
        GameManager.Instance.Board.ResetBoard();
        _defeatScreen.SetActive(false);
        _winScreen.SetActive(false);
        _canva.SetActive(true);
    }
    
    public void LoadDefeatMenu()
    {
        _defeatScreen.SetActive(true);
    }
    
    public void LoadWinMenu()
    {
        _winScreen.SetActive(true);
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
