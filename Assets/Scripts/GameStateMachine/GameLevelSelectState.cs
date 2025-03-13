using System.Collections;
using Unity.VisualScripting;


public class GameLevelSelectState : GameBaseState
{
    public override IEnumerator EnterState(GameStateManager manager)
    {
        ScreenController.Instance.UnloadSecondScreen();
        ScreenController.Instance.CoroutineLoadScreen(MainScreenActive.LevelSelect);
        
        yield return new WaitForNextFrameUnit();
        
        LevelManager.Instance.InitLevel(SaveSystem.Load());

        LevelManager.Instance.UpdateLocker();
        LevelManager.Instance.UpdateEffectIcons();
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override IEnumerator ExitState(GameStateManager manager)
    {
        AudioManager.Instance.PlayMusic("Level");
        yield return new WaitForNextFrameUnit();
    }
}
