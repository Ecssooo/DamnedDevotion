using UnityEngine;


public class GameLevelSelectState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        ScreenController.Instance.LoadScreen(MainScreenActive.LevelSelect);
        LevelManager.Instance.InitLevel(SaveSystem.Load());

        LevelManager.Instance.UpdateLocker();
        LevelManager.Instance.UpdateEffectIcons();
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override void ExitState(GameStateManager manager)
    {
        AudioManager.Instance.PlayMusic("Level");
    }
}
