using UnityEngine;


public class GameLevelState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        LevelManager.Instance.InitLevel(SaveSystem.Load());
        LevelManager.Instance.LoadMenu();
        manager.SetInteratable(true);
        GameManager.Instance.StartTransition("Out");
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override void ExitState(GameStateManager manager)
    {
        AudioManager.Instance.PlayMusic("Level");
        GameManager.Instance.StartTransition("In");
    }
}
