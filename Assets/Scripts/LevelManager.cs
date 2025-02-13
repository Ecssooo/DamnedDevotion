using System;
using TMPro;
using UnityEngine;

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
        if (_currentLevel > GameManager.Instance.LevelDatabase.levelList.Count)
            _currentLevel = GameManager.Instance.LevelDatabase.levelList.Count;
        UpdateUI();
    }

    public void DecreaseLevel()
    { 
        _currentLevel--; 
        if (_currentLevel <= 0)
            _currentLevel = 1;
        UpdateUI();
    }

    public void InitLevel(int value)
    {
        if (value > 0) SaveSystem.InitSave();
        
        _currentLevel = value; 
        UpdateUI();
    }
    #endregion
    
    #region UI

    [SerializeField] private GameObject _canva;
    [SerializeField] private TextMeshProUGUI TXT_number;
    [SerializeField] private GameObject _locker;
    void UpdateUI()
    {
        TXT_number.text = (_currentLevel).ToString();
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
        GameManager.Instance.Board.SetLevel(GameManager.Instance.LevelDatabase.levelList[_currentLevel-1]);
        GameManager.Instance.Board.gameObject.SetActive(true);
        GameStateManager.Instance.SwitchState(GameStateManager.Instance.GameSetupState);
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
            SaveSystem.Save(1);
        }
        
        #endif
    }
}
