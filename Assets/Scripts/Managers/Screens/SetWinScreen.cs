using UnityEngine;
using UnityEngine.UI;

public class SetWinScreen : SetScreen
{
    [SerializeField] private GameObject BTN_Next;
    [SerializeField] private GameObject BTN_Back;

    [SerializeField] private Canvas _canvas;

    public override void OnLoad()
    {
        _canvas.worldCamera = Camera.main;
        
        var btn_Next = BTN_Next.GetComponent<Button>();
        btn_Next.onClick.AddListener(() => {AudioManager.Instance.PlaySFX("button");});
        btn_Next.onClick.AddListener(() => { GameStateManager.Instance.StateSetupAnyGameState(true); });
        GameManager.Instance.NextButton = BTN_Next;
        
        
        var btn_Back = BTN_Back.GetComponent<Button>();
        btn_Back.onClick.AddListener(() => {AudioManager.Instance.PlaySFX("button");});
        btn_Back.onClick.AddListener(() => { GameStateManager.Instance.StateLevelSelector(); });
        
    }
}
