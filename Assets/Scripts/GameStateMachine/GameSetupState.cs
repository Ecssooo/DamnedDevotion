using UnityEngine;

public class GameSetupState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        //Achievement
        if(LevelManager.Instance.CurrentLevel == 25) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQCw");
        
        //Init Score / Actions
        GameManager.Instance.MonsterScore = 0;
        
        ScreenController.Instance.LoadScreen(MainScreenActive.Board);
        
        GameManager.Instance.ActionCount.InitActionPoint(GameManager.Instance.LevelDatabase.levelList[LevelManager.Instance.CurrentLevel].maxActionCount);
        GameManager.Instance.ActionCount.DisplayActionPoint();
        
        
        LevelManager.Instance.LoadLevel();
    }

    public override void UpdateState(GameStateManager manager)
    {
        if (!GameManager.Instance.ActionCount.ActionRemaining() && !manager.WaitForAction)
        {
            // LevelManager.Instance.LoadPopUp();
            ScreenController.Instance.LoadScreen(SecondScreenActive.PopUp);
            GameStateManager.Instance.SetWaitForAction(true);
        }
    }

    public override void ExitState(GameStateManager manager)
    {
        
    }
}
