using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SetGameScreen : SetScreen
{
    [FormerlySerializedAs("_board")] [SerializeField] private BoardController boardController;
    [SerializeField] private TextMeshProUGUI _actionPointTxt;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject BTN_Pause;
    
    public override void OnLoad()
    {
        _canvas.worldCamera = Camera.main;
        GameManager.Instance.BoardController = boardController;
        GameManager.Instance.ActionCount.ActionPointText = _actionPointTxt;

        var btn_Pause = BTN_Pause.GetComponent<Button>();
        btn_Pause.onClick.AddListener(() => {AudioManager.Instance.PlaySFX("button");});
        btn_Pause.onClick.AddListener(() => { GameStateManager.Instance.SwitchState(GameStateManager.Instance.GamePauseState) ;});
    }
}
