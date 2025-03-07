using UnityEngine;
using UnityEngine.UI;

public class SetPopUpScreen : SetScreen
{
    [SerializeField] private GameObject BTN_back;
    [SerializeField] private GameObject BTN_Yes;

    [SerializeField] private Canvas _canvas;
    
    
    public override void OnLoad()
    {
        _canvas.worldCamera = Camera.main;
        
        var btn_Back = BTN_back.GetComponent<Button>();
        btn_Back.onClick.AddListener(()=> {GameStateManager.Instance.SwitchState(GameStateManager.Instance.GameSetupState, true, false);});
        btn_Back.onClick.AddListener(()=>{ScreenController.Instance.UnloadSecondScreen();});

        var btn_Yes = BTN_Yes.GetComponent<Button>();
        btn_Yes.onClick.AddListener((() => {GameStateManager.Instance.StateAction();}));
        btn_Yes.onClick.AddListener((() => {Debug.Log("Click");}));
        btn_Back.onClick.AddListener(()=>{ScreenController.Instance.UnloadSecondScreen();});
    }
}
