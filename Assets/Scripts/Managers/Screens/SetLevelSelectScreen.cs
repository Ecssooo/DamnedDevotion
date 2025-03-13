using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetLevelSelectScreen : SetScreen
{
    [Header("Canva")]
    [SerializeField] private Canvas _canvas;
    
    
    [Header("Button")]
    [SerializeField] private GameObject BTN_LoadLevel;
    [SerializeField] private GameObject BTN_NextLevel;
    [SerializeField] private GameObject BTN_PreviousLevel;
    [SerializeField] private GameObject BTN_StartScreen;

    [Header("Icons parents")]
    [SerializeField] private GameObject _moveParents;
    [SerializeField] private GameObject _swapParents;
    [SerializeField] private GameObject _invokeParents;

    [Header("Level button")] 
    [SerializeField] private TextMeshProUGUI _number;
    [SerializeField] private GameObject _locker;

    public override void OnLoad()
    {
        _canvas.worldCamera = Camera.main;
        
        //Buttons
        var btn_LoadLevel = BTN_LoadLevel.GetComponent<Button>();
        btn_LoadLevel.onClick.AddListener(() => { AudioManager.Instance.PlaySFX("button"); });
        btn_LoadLevel.onClick.AddListener(() => { GameStateManager.Instance.StateSetup(); });

        var btn_NextLevel = BTN_NextLevel.GetComponent<Button>();
        btn_NextLevel.onClick.AddListener(() => { AudioManager.Instance.PlaySFX("button"); });
        btn_NextLevel.onClick.AddListener(() => { LevelManager.Instance.IncreaseLevel(); });

        var btn_PreviousLevel = BTN_PreviousLevel.GetComponent<Button>();
        btn_PreviousLevel.onClick.AddListener(() => { AudioManager.Instance.PlaySFX("button"); });
        btn_PreviousLevel.onClick.AddListener(() => { LevelManager.Instance.DecreaseLevel(); });

        var btn_StartScreen = BTN_StartScreen.GetComponent<Button>();
        btn_StartScreen.onClick.AddListener(() => { AudioManager.Instance.PlaySFX("button"); });
        btn_StartScreen.onClick.AddListener(() => { GameStateManager.Instance.StateStartAnyGameState(); });
        
        LevelManager.Instance.MoveParent = _moveParents;
        LevelManager.Instance.SwapParent = _swapParents;
        LevelManager.Instance.InvokeParent = _invokeParents;
        LevelManager.Instance.Number = _number;
        LevelManager.Instance.Locker = _locker;
   }
}
