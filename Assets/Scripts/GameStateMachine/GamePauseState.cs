using UnityEngine;

public class GamePauseState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        GameManager.Instance.GameState = GameState.Busy;
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override void ExitState(GameStateManager manager)
    {
        GameManager.Instance.GameState = GameState.Playable;
    }
}
