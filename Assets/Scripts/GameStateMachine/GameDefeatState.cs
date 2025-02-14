using UnityEngine;

public class GameDefeatState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        LevelManager.Instance.LoadDefeatMenu();
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
