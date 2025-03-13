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
    private GameDefeatState _gameDefeatState = new();
    private GameWinState _gameWinState = new();
    private GamePauseState _gamePauseState = new();
    private GameStartState _gameStartState = new();

    #region Getter
    
    public GameBaseState CurrentState => _currentState;
    public GameSetupState GameSetupState => _gameSetupState;
    public GameLevelSelectState GameLevelSelectState => _gameLevelSelectState;
    public GameActionState GameActionState => _gameActionState;
    public GameDefeatState GameDefeatState => _gameDefeatState;
    public GameWinState GameWinState => _gameWinState;
    public GamePauseState GamePauseState => _gamePauseState;
    public GameStartState GameStartState => _gameStartState;

    #endregion
    
    private bool _waitForAction;
    public bool WaitForAction { get => _waitForAction; set => _waitForAction = value; }
    
    
    
    private void Start()
    {
        _currentState = _gameStartState;
        StartCoroutine(_currentState.EnterState(this));
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state, bool doExit = true, bool doEnter = true)
    {
        if (doExit) StartCoroutine(_currentState.ExitState(this));
        _currentState = state;
        if(doEnter) StartCoroutine (_currentState.EnterState(this));
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
    
    //Player choose actions
    public void StateSetup() {
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gameSetupState);
    }
    public void StateSetupAnyGameState(bool doEnter){ SwitchState(_gameSetupState, true, doEnter); }
    
    //Game do all actions
    public void StateAction(){
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gameActionState);
    }
    public void StateActionAnyGameState() { SwitchState(_gameActionState); }
    
    //Pause screen
    public void StatePause()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gamePauseState, true, true);
    }
    public void StatePauseAnyGameState() { SwitchState(_gamePauseState); }

    
    #endregion
    
    #region Level Selector State
    
    public void StateLevelSelector()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gameLevelSelectState);
    }
    
    public void StateLevelSelectorAnyGameState() { SwitchState(_gameLevelSelectState); }

    
    
    #endregion

    #region Win/Defeat State
    
    // Level win after action phase
    public void StateWin()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gameWinState);
    }
    public void StateWinAnyGameState() => SwitchState(_gameWinState);
    
    
    // Level defeat after action phase
    public void StateDefeat()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            SwitchState(_gameDefeatState);
    }
    public void StateDefeatAnyGameState() => SwitchState(_gameDefeatState);
    
    #endregion
    #endregion
}
