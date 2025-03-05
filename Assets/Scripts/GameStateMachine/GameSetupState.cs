using UnityEngine;

public class GameSetupState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        if(LevelManager.Instance.CurrentLevel == 25) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQCw");
        GameManager.Instance.MonsterScore = 0;
        GameManager.Instance.ActionCount.InitActionPoint(GameManager.Instance.LevelDatabase.levelList[LevelManager.Instance.CurrentLevel].maxActionCount);
        GameManager.Instance.ActionCount.DisplayActionPoint();
        LevelManager.Instance.LoadLevel();
        GameManager.Instance.StartTransition("Out");
    }

    public override void UpdateState(GameStateManager manager)
    {
        if (!GameManager.Instance.ActionCount.ActionRemaining() && !manager.WaitForAction)
        {
            // manager.SwitchState(manager.GameActionState, true);
            LevelManager.Instance.LoadPopUp();
        }
    }

    public override void ExitState(GameStateManager manager)
    {
        GameStateManager.Instance.SetWaitForAction(true);
    }
}
