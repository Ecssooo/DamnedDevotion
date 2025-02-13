using UnityEngine;


public class GameLevelState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        LevelManager.Instance.InitLevel(SaveSystem.Load());
    }

    public override void UpdateState(GameStateManager manager)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(GameStateManager manager)
    {
        throw new System.NotImplementedException();
    }
}
