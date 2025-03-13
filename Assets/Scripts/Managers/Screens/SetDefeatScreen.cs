using UnityEngine;
using UnityEngine.UI;

public class SetDefeatScreen : SetScreen
{
     [SerializeField] private GameObject BTN_Retry;
        [SerializeField] private GameObject BTN_Back;
    
        [SerializeField] private Canvas _canvas;
    
        public override void OnLoad()
        {
            _canvas.worldCamera = Camera.main;
            
            var btn_Retry = BTN_Retry.GetComponent<Button>();
            btn_Retry.onClick.AddListener(() => {AudioManager.Instance.PlaySFX("button");});
            btn_Retry.onClick.AddListener(() => { GameStateManager.Instance.StateSetupAnyGameState(true); });
            
            var btn_Back = BTN_Back.GetComponent<Button>();
            btn_Back.onClick.AddListener(() => {AudioManager.Instance.PlaySFX("button");});
            btn_Back.onClick.AddListener(() => { GameStateManager.Instance.StateLevelSelector(); });
            
        }
}
