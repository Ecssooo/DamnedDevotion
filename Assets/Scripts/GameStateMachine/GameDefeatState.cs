using UnityEngine;

public class GameDefeatState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        AudioManager.Instance.PlaySFX("lose");
        LevelManager.Instance.LoadDefeatMenu();
        GameManager.Instance.MonsterScore = 0;
        GameManager.Instance.Effect = Effects.NONE;
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override void ExitState(GameStateManager manager)
    {
    }
}
