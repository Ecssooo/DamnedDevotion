using UnityEngine;

public class GameSetupState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        if(LevelManager.Instance.CurrentLevel == 25) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQCw");

        GameManager.Instance.ActionCount.InitActionPoint(GameManager.Instance.LevelDatabase.levelList[LevelManager.Instance.CurrentLevel].maxActionCount);
        GameManager.Instance.ActionCount.DisplayActionPoint();
        LevelManager.Instance.LoadLevel();
    }

    public override void UpdateState(GameStateManager manager)
    {
        if (!GameManager.Instance.ActionCount.ActionRemaining())
        {
            manager.SwitchState(manager.GameActionState, true);
        }
    }

    public override void ExitState(GameStateManager manager)
    {
        
    }
}
