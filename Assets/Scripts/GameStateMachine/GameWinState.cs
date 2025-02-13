using UnityEngine;

public class GameWinState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        LevelManager.Instance.LoadWinMenu();
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override void ExitState(GameStateManager manager)
    {
        SaveSystem.Save(LevelManager.Instance.CurrentLevel + 1);
    }
}
