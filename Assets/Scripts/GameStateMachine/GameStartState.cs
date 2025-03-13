using System.Collections;
using Unity.VisualScripting;

public class GameStartState : GameBaseState
{
    public override IEnumerator EnterState(GameStateManager manager)
    {
        //Screen
        ScreenController.Instance.CoroutineLoadScreen(MainScreenActive.Start);
        yield return new WaitForNextFrameUnit();
        //Audio
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlayMusic("Menu");
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
