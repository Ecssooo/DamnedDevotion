using UnityEngine;

public class GameActionState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        GameManager.Instance.Board.StartEndAction();
    }

    public override void UpdateState(GameStateManager manager)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            manager.SwitchState(manager.GameWinState);
        }
    }

    public override void ExitState(GameStateManager manager)
    {
        //
    }
}
