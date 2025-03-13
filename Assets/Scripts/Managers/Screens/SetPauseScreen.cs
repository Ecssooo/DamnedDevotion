using UnityEngine;
using UnityEngine.UI;

public class SetPauseScreen : SetScreen
{
    [SerializeField] private GameObject BTN_Resume;
    [SerializeField] private GameObject BTN_Retry;
    [SerializeField] private GameObject BTN_LevelSelector;
    [SerializeField] private GameObject BTN_MainMenu;
    [SerializeField] private GameObject BTN_Music;
    [SerializeField] private GameObject BTN_SFX;
    
    [SerializeField] private Canvas _canvas;

    public override void OnLoad()
    {
        _canvas.worldCamera = Camera.main;

        var btn_resume = BTN_Resume.GetComponent<Button>();
        btn_resume.onClick.AddListener(() => {AudioManager.Instance.PlaySFX("button");});
        btn_resume.onClick.AddListener(() => { GameStateManager.Instance.StateSetupAnyGameState(false);});
        btn_resume.onClick.AddListener(() => { ScreenController.Instance.UnloadSecondScreen();});
        
        var btn_Retry = BTN_Retry.GetComponent<Button>();
        btn_Retry.onClick.AddListener(() => {AudioManager.Instance.PlaySFX("button");});
        btn_Retry.onClick.AddListener(() => {GameStateManager.Instance.StateSetupAnyGameState(true);});
        
        var btn_LevelSelector = BTN_LevelSelector.GetComponent<Button>();
        btn_LevelSelector.onClick.AddListener(() => {AudioManager.Instance.PlaySFX("button");});
        btn_LevelSelector.onClick.AddListener(() => {GameStateManager.Instance.StateLevelSelectorAnyGameState();});
        
        var btn_MainMenu = BTN_MainMenu.GetComponent<Button>();
        btn_MainMenu.onClick.AddListener(() => {AudioManager.Instance.PlaySFX("button");});
        btn_MainMenu.onClick.AddListener(() => {GameStateManager.Instance.StateStartAnyGameState();});
        
        var btn_SFX = BTN_SFX.GetComponent<Button>();
        btn_SFX.onClick.AddListener(() => { AudioManager.Instance.ToggleSFX(); });
        
        var btn_Music = BTN_Music.GetComponent<Button>();
        btn_Music.onClick.AddListener(() => { AudioManager.Instance.ToggleMusic(); });
        
    }
}
