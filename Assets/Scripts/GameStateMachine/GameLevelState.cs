using UnityEngine;


public class GameLevelState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        LevelManager.Instance.InitLevel(SaveSystem.Load());
        LevelManager.Instance.LoadMenu();
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
