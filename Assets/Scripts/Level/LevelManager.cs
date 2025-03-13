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
    
    [Header("Game")] 
    [SerializeField] private GameObject _popUp;
    
    [Header("Level Selector")]
    [SerializeField] private TextMeshProUGUI _number;
    [SerializeField] private GameObject _locker;
    
    [Header("Effect parents")]
    [SerializeField] private GameObject _moveParent;
    [SerializeField] private GameObject _swapParent;
    [SerializeField] private GameObject _invokeParent;
    
    #region Getters
    
    public TextMeshProUGUI Number { get => _number; set => _number = value; }
    public GameObject Locker { get => _locker; set => _locker = value;}
    public GameObject MoveParent { get => _moveParent; set => _moveParent = value; }

    public GameObject SwapParent { get => _swapParent; set => _swapParent = value; }

    public GameObject InvokeParent { get => _invokeParent; set => _invokeParent = value; }
    
    #endregion
    
    #region UpdateLevel

    public void IncreaseLevel()
    {
        _currentLevel++;
        if (_currentLevel > GameManager.Instance.LevelDatabase.levelList.Count - 1)
            _currentLevel = GameManager.Instance.LevelDatabase.levelList.Count - 1;
        UpdateLocker();
        UpdateEffectIcons();
    }

    public void DecreaseLevel()
    { 
        _currentLevel--; 
        if (_currentLevel < 0)
            _currentLevel = 0; 
        UpdateLocker();
        UpdateEffectIcons();
    }

    public void InitLevel(int value)
    {
        if (value < 0) SaveSystem.InitSave();
        
        _currentLevel = value; 
    }
    #endregion
    
    #region UI

    public void UpdateLocker()
    {
        _number.text = (_currentLevel+1).ToString();
        if (_currentLevel > SaveSystem.Load())
        {
            _locker.SetActive(true);
        }
        else
        {
            _locker.SetActive(false);
        }
    }
    
    public void  UpdateEffectIcons()
    {
        _moveParent.SetActive(false);
        _swapParent.SetActive(false);
        _invokeParent.SetActive(false);
        
        Level level = GameManager.Instance.LevelDatabase.levelList[_currentLevel];
        for (int i = 0; i < level.effects.Length; i++)
        {
            if (level.effects[i])
            {
                switch (i)
                {
                    case(0): _moveParent.SetActive(true); break;
                    case(1): _swapParent.SetActive(true); break;
                    case(2): _invokeParent.SetActive(true); break;
                }
            }
        }
    }
    
    
    #endregion

    #region Game
    
    public void LoadLevel()
    {
        StartCoroutine(GameManager.Instance.BoardController.SetLevel(GameManager.Instance.LevelDatabase.levelList[_currentLevel]));
        
        GameManager.Instance.Effect = Effects.NONE;

        ListAction.Instance.ListActions.Clear();
    }

    #endregion

    private void Start()
    {
        SaveSystem.InitSave();
        InitLevel(SaveSystem.Load());
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveSystem.Save(0);
            Debug.Log("Saved");
        }
    }
#endif
}
