using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameDefeatState : GameBaseState
{
    public override IEnumerator EnterState(GameStateManager manager)
    {
        //Audio
        AudioManager.Instance.PlaySFX("lose");

        //LevelManager.Instance.LoadDefeatMenu();
        ScreenController.Instance.CoroutineLoadScreen(SecondScreenActive.Defeat);
        yield return new WaitForNextFrameUnit();
        //Reset level
        GameManager.Instance.MonsterScore = 0;
        GameManager.Instance.Effect = Effects.NONE;
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override IEnumerator ExitState(GameStateManager manager)
    {
        yield return null;
    }
}
