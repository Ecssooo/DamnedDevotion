using System.Collections;
using Unity.VisualScripting;

public class GameActionState : GameBaseState
{
    public override IEnumerator EnterState(GameStateManager manager)
    {

        //Achievement
        int move = 0;
        int swap = 0;
        int invoke = 0;
        foreach (var action in ListAction.Instance.ListActions)
        {
            switch (action._effect)
            {
                case(Effects.MOVE): move++; break;
                case(Effects.SWAP): swap++; break;
                case(Effects.INVOKE): invoke++; break;
            }
        }
        
        if(move >= 3 || swap >= 3 || invoke >= 3) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQDA");
        if(move >= 5 || swap >= 5 || invoke >= 5) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQDQ");
        yield return new WaitForNextFrameUnit();
        
        //Action
        ListAction.Instance.StartListActionCoroutine();
        
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override IEnumerator ExitState(GameStateManager manager)
    {
        GameManager instance = GameManager.Instance;
        
        if (instance.MonsterScore >= 
            instance.LevelDatabase.levelList[LevelManager.Instance.CurrentLevel].maxScore)
        {
            manager.SwitchState(manager.GameWinState, false);
        }
        else
        {
            manager.SwitchState(manager.GameDefeatState, false);
        }
        yield return null;
    }
}
