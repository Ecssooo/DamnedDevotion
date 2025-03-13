using System.Collections;
using Unity.VisualScripting;

public class GamePauseState : GameBaseState
{
    public override IEnumerator EnterState(GameStateManager manager)
    {
        GameManager.Instance.GameState = GameState.Busy;
        yield return new WaitForNextFrameUnit();
        ScreenController.Instance.CoroutineLoadScreen(SecondScreenActive.Pause);
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override IEnumerator ExitState(GameStateManager manager)
    {
        GameManager.Instance.GameState = GameState.Playable;
        ScreenController.Instance.UnloadSecondScreen();
        yield return new WaitForNextFrameUnit();
    }
}
