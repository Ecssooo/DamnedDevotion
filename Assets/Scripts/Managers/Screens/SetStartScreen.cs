using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SetStartScreen : SetScreen
{
    [Header("Canva")]
    [SerializeField] private Canvas _canvas;
    
    [Header("Buttons")]
    [SerializeField] private GameObject BTN_Play;
    [SerializeField] private GameObject BTN_Achievements;
    [SerializeField] private GameObject BTN_Options;
    [SerializeField] private GameObject BTN_Credit;
    [SerializeField] private GameObject BTN_Login;
    
    public override void OnLoad()
    {
        _canvas.worldCamera = Camera.main;
        
        var btn_Play = BTN_Play.GetComponent<Button>();
        btn_Play.onClick.AddListener(() => { AudioManager.Instance.PlaySFX("button"); });
        btn_Play.onClick.AddListener(() => { GameStateManager.Instance.StateMenu(); });

        var btn_Achievements = BTN_Achievements.GetComponent<Button>();
        btn_Achievements.onClick.AddListener(() => { AudioManager.Instance.PlaySFX("button"); });
        btn_Achievements.onClick.AddListener(() => { PlayGamesController.Instance.ShowAchievements();});

        var btn_Options = BTN_Options.GetComponent<Button>();
        btn_Options.onClick.AddListener(() => { AudioManager.Instance.PlaySFX("button"); });
        btn_Options.onClick.AddListener(() => { ScreenController.Instance.CoroutineLoadScreen(SecondScreenActive.Options);});

        var btn_Credits = BTN_Credit.GetComponent<Button>();
        btn_Credits.onClick.AddListener(() => { AudioManager.Instance.PlaySFX("button"); });
        btn_Credits.onClick.AddListener (() => { ScreenController.Instance.CoroutineLoadScreen(SecondScreenActive.Credits); });

        var btn_Login = BTN_Login.GetComponent<Button>();
        btn_Login.onClick.AddListener(() => { AudioManager.Instance.PlaySFX("button"); });
        btn_Login.onClick.AddListener(() => { PlayGamesController.Instance.Connect(); });
    }
}
