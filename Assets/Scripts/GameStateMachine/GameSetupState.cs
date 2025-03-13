using System.Collections;
using Unity.VisualScripting;

public class GameSetupState : GameBaseState
{
    public override IEnumerator EnterState(GameStateManager manager)
    {
        //Achievement
        if(LevelManager.Instance.CurrentLevel == 25) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQCw");
        
        manager.WaitForAction = true;
        
        ScreenController.Instance.UnloadSecondScreen();
        ScreenController.Instance.CoroutineLoadScreen(MainScreenActive.Board);
        
        yield return new WaitForNextFrameUnit();
        
        //Init Score / Actions
        GameManager.Instance.MonsterScore = 0;
        
        GameManager.Instance.ActionCount.InitActionPoint(GameManager.Instance.LevelDatabase.levelList[LevelManager.Instance.CurrentLevel].maxActionCount);
        GameManager.Instance.ActionCount.DisplayActionPoint();
        
        
        LevelManager.Instance.LoadLevel();
        manager.WaitForAction = false;
    }

    public override void UpdateState(GameStateManager manager)
    {
        if (!GameManager.Instance.ActionCount.ActionRemaining() && !manager.WaitForAction)
        {
            // LevelManager.Instance.LoadPopUp();
            ScreenController.Instance.CoroutineLoadScreen(SecondScreenActive.PopUp);
            manager.WaitForAction = true;
        }
    }

    public override IEnumerator ExitState(GameStateManager manager)
    {
        yield return new WaitForNextFrameUnit();
    }
}
