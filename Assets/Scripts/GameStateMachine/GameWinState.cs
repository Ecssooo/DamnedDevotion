using UnityEngine;

public class GameWinState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        LevelManager.Instance.LoadWinMenu();
        GameManager.Instance.MonsterScore = 0;
        GameManager.Instance.Effect = Effects.NONE;
        
        if (LevelManager.Instance.CurrentLevel < GameManager.Instance.LevelDatabase.levelList.Count - 1 &&
            LevelManager.Instance.CurrentLevel + 1 > SaveSystem.Load())
        {
            SaveSystem.Save(LevelManager.Instance.CurrentLevel + 1);
        }
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override void ExitState(GameStateManager manager)
    {
        
    }
}
