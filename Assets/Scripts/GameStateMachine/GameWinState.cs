using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameWinState : GameBaseState
{
    public override IEnumerator EnterState(GameStateManager manager)
    {
        //Achievement
        if (LevelManager.Instance.CurrentLevel == 0) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQAQ");
        if (LevelManager.Instance.CurrentLevel == 9) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQAg"); 
        if (LevelManager.Instance.CurrentLevel == 14) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQAw");

        //Audio
        AudioManager.Instance.PlaySFX("win");
        
        //LevelManager.Instance.LoadWinMenu();
        ScreenController.Instance.CoroutineLoadScreen(SecondScreenActive.Win);
        yield return null;
        //Reset level
        GameManager.Instance.MonsterScore = 0;
        GameManager.Instance.Effect = Effects.NONE;
        
        //Save
        if (LevelManager.Instance.CurrentLevel < GameManager.Instance.LevelDatabase.levelList.Count - 1 &&
            LevelManager.Instance.CurrentLevel + 1 > SaveSystem.Load())
        {
            SaveSystem.Save(LevelManager.Instance.CurrentLevel + 1);
        }
        LevelManager.Instance.CurrentLevel++;
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override IEnumerator ExitState(GameStateManager manager)
    {
        yield return new WaitForNextFrameUnit();
    }
}
