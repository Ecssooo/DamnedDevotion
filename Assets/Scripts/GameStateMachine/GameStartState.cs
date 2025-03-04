using UnityEngine;

public class GameStartState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        LevelManager.Instance.LoadMainScreen();
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override void ExitState(GameStateManager manager)
    {
        //
    }
}
