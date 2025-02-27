using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    #region Instance
    private static GameStateManager _instance;

    public static GameStateManager Instance { get => _instance; }

    public void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    
    
    private GameBaseState _currentState;
    
    private GameSetupState _gameSetupState = new();
    private GameLevelState _gameLevelState = new();
    private GameActionState _gameActionState = new();
    private GameDefeatState _gameDefeatStateState = new();
    private GameWinState _gameWinState = new();



    public GameBaseState CurrentState => _currentState;
    public GameSetupState GameSetupState => _gameSetupState;
    public GameLevelState GameLevelState => _gameLevelState;
    public GameActionState GameActionState => _gameActionState;
    public GameDefeatState GameDefeatStateState => _gameDefeatStateState;
    public GameWinState GameWinState => _gameWinState;


    private void Start()
    {
        _currentState = _gameLevelState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state, bool doExit = true)
    {
        if(doExit) _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }
    
    #region TEMP

    public void StateMenu()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gameLevelState);
    }

    public void StateSetup()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gameSetupState, true);
    }
    public void StateAction(){
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gameActionState);
    }

    public void StateWin()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gameWinState);
    }

    public void StateDefeat()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gameDefeatStateState);
    }
    
    
    #endregion
}
