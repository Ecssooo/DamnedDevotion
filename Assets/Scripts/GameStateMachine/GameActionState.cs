using UnityEngine;

public class GameActionState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        //GameManager.Instance.Board.StartEndAction();
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override void ExitState(GameStateManager manager)
    {
        GameManager instance = GameManager.Instance;
        
        if (instance.MonsterScore >= 
            instance.LevelDatabase.levelList[LevelManager.Instance.CurrentLevel].maxScore)
        {
            manager.SwitchState(manager.GameWinState);
        }
        else
        {
            manager.SwitchState(manager.GameDefeatStateState);
        }
    }
}
