using UnityEngine;

public class GameSetupState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        GameManager.Instance.ActionCount.InitActionPoint(GameManager.Instance.LevelDatabase.levelList[LevelManager.Instance.CurrentLevel].maxActionCount);
        GameManager.Instance.ActionCount.DisplayActionPoint();
        LevelManager.Instance.LoadLevel();
        AudioManager.Instance.PlayMusic("Level");
    }

    public override void UpdateState(GameStateManager manager)
    {
        if (!GameManager.Instance.ActionCount.ActionRemaining())
        {
            manager.SwitchState(manager.GameActionState);
        }
    }

    public override void ExitState(GameStateManager manager)
    {
        
    }
}
