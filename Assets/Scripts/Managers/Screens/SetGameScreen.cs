using TMPro;
using UnityEngine;

public class SetGameScreen : SetScreen
{
    [SerializeField] private Board _board;
    [SerializeField] private TextMeshProUGUI _actionPointTxt;
    [SerializeField] private Canvas _canvas;
    
    public override void OnLoad()
    {
        GameManager.Instance.Board = _board;
        GameManager.Instance.ActionCount.actionPointText = _actionPointTxt;
        _canvas.worldCamera = Camera.main;
    }
}
