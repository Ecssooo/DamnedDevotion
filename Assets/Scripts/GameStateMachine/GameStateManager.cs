using System;
using System.Collections;
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
    private GameLevelSelectState _gameLevelSelectState = new();
    private GameActionState _gameActionState = new();
    private GameDefeatState _gameDefeatStateState = new();
    private GameWinState _gameWinState = new();
    private GamePauseState _gamePauseState = new();
    private GameStartState _gameStartState = new();

    #region Getter
    
    public GameBaseState CurrentState => _currentState;
    public GameSetupState GameSetupState => _gameSetupState;
    public GameLevelSelectState GameLevelSelectState => _gameLevelSelectState;
    public GameActionState GameActionState => _gameActionState;
    public GameDefeatState GameDefeatStateState => _gameDefeatStateState;
    public GameWinState GameWinState => _gameWinState;
    public GamePauseState GamePauseState => _gamePauseState;
    public GameStartState GameStartState => _gameStartState;

    #endregion
    
    private bool waitForAction;
    public bool WaitForAction { get => waitForAction; }
    
    
    
    private void Start()
    {
        _currentState = _gameStartState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state, bool doExit = true, bool doEnter = true)
    {
        if(doExit) _currentState.ExitState(this);
        _currentState = state;
        if(doEnter)_currentState.EnterState(this);
    } 
    
    #region Premade Change State

    #region Start State
    
    public void StateStart()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gameStartState);
    }
    
    public void StateStartAnyGameState()
    {
        SwitchState(_gameStartState);
    }
    
    #endregion 
    
    #region Game State
    
    
    public void StateSetup() {
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gameSetupState);
    }
    
    public void StateSetupAnyGameState(bool doEnter)
    {
        SwitchState(_gameSetupState, true, doEnter);
    }
    
    public void StateAction(){
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gameActionState);
    }
    
    public void StatePause()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gamePauseState, true, true);
    }

    
    #endregion
    
    #region Level Selector State
    
    public void StateMenu()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gameLevelSelectState);
    }
    
    public void StateLevelAnyGameState()
    {
        SwitchState(_gameLevelSelectState);
    }

    
    
    #endregion

    #region Win/Defeat State
    
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
    
    public void SetWaitForAction(bool value)
    {
        waitForAction = value;
    }
    #endregion
}
