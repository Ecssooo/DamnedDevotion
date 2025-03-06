using UnityEngine;

public class GameWinState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        AudioManager.Instance.PlaySFX("win");
        if (!GameManager.Instance.HasUsedUndo) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQCA");
        LevelManager.Instance.LoadWinMenu();
        GameManager.Instance.MonsterScore = 0;
        GameManager.Instance.Effect = Effects.NONE;
        
        if (LevelManager.Instance.CurrentLevel == 0) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQAQ");
        if (LevelManager.Instance.CurrentLevel == 9) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQAg"); 
        if (LevelManager.Instance.CurrentLevel == 14) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQAw");
        
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

    public override void ExitState(GameStateManager manager)
    {

    }
}
