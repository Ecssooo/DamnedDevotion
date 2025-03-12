using UnityEngine;
using UnityEngine.UI;

public class SetOptionsScreen : SetScreen
{
    [SerializeField] private GameObject BTN_Music;
    [SerializeField] private GameObject BTN_SFX;
    [SerializeField] private GameObject BTN_Back;
    
    [SerializeField] private Canvas _canvas;

    public override void OnLoad()
    {
        _canvas.worldCamera = Camera.main;
        
        var btn_Music = BTN_Music.GetComponent<Button>();
        btn_Music.onClick.AddListener(() => { AudioManager.Instance.ToggleMusic(); });
        
        var btn_SFX = BTN_SFX.GetComponent<Button>();
        btn_Music.onClick.AddListener(() => { AudioManager.Instance.ToggleSFX(); });
        
        var btn_Back = BTN_Back.GetComponent<Button>();
        btn_Back.onClick.AddListener(() => { ScreenController.Instance.UnloadSecondScreen(); });
    }
}
