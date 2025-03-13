using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetGameScreen : SetScreen
{
    [SerializeField] private Board _board;
    [SerializeField] private TextMeshProUGUI _actionPointTxt;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject BTN_Pause;
    
    public override void OnLoad()
    {
        _canvas.worldCamera = Camera.main;
        GameManager.Instance.Board = _board;
        GameManager.Instance.ActionCount.actionPointText = _actionPointTxt;

        var btn_Pause = BTN_Pause.GetComponent<Button>();
        btn_Pause.onClick.AddListener(() => {AudioManager.Instance.PlaySFX("button");});
        btn_Pause.onClick.AddListener(() => { GameStateManager.Instance.SwitchState(GameStateManager.Instance.GamePauseState) ;});
    }
}
