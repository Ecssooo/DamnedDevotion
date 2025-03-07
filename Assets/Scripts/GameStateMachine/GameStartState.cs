using UnityEngine;

public class GameStartState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        //Screen
        ScreenController.Instance.LoadScreen(MainScreenActive.Start);
        
        //Audio
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlayMusic("Menu");
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override void ExitState(GameStateManager manager)
    {

    }
}
