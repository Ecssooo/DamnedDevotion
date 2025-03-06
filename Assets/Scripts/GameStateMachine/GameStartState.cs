using UnityEngine;

public class GameStartState : GameBaseState
{
    public override void EnterState(GameStateManager manager)
    {
        LevelManager.Instance.LoadMainScreen();
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlayMusic("Menu");
        GameManager.Instance.GameState = GameState.Playable;
        GameManager.Instance.StartTransition("Out");
    }

    public override void UpdateState(GameStateManager manager)
    {
        //
    }

    public override void ExitState(GameStateManager manager)
    {
    }
}
