using UnityEngine;

public class GameSetupState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        GameManager.Instance.Board.SetLevel(GameManager.Instance.LevelDatabase.levelList[LevelManager.Instance.CurrentLevel]);
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
