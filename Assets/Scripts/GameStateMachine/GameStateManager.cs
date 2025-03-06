using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    private GamePauseState _gamePauseState = new();
    private GameStartState _gameStartState = new();


    public GameBaseState CurrentState => _currentState;
    public GameSetupState GameSetupState => _gameSetupState;
    public GameLevelState GameLevelState => _gameLevelState;
    public GameActionState GameActionState => _gameActionState;
    public GameDefeatState GameDefeatStateState => _gameDefeatStateState;
    public GameWinState GameWinState => _gameWinState;
    public GamePauseState GamePauseState => _gamePauseState;

    public GameStartState GameStartState => _gameStartState;

    private bool waitForAction;
    public bool WaitForAction { get => waitForAction; }
    
    [SerializeField] private Button _levelButton;
    
    private void Start()
    {
        _currentState = _gameStartState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public IEnumerator SwitchState(GameBaseState state, bool doExit = true, bool doEnter = true, float exitTime = 1f)
    {
        if(doExit) _currentState.ExitState(this);
        yield return new WaitForSeconds(exitTime);
        _currentState = state;
        if(doEnter)_currentState.EnterState(this);
    }

    public void SetInteratable(bool value)
    {
        _levelButton.interactable = value;;
    }
    
    #region TEMP

    public void StateMenu()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            StartCoroutine(SwitchState(_gameLevelState));
    }

    public void StateSetup(bool doEnter)
    {
        StartCoroutine(CoroutineStateSetup(doEnter));
    }
    
    public void StateAction(){
        if(GameManager.Instance.GameState == GameState.Playable)
            StartCoroutine(SwitchState(_gameActionState, true, true, 0f));
    }

    public void StateWin()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            StartCoroutine(SwitchState(_gameWinState));
    }

    public void StateDefeat()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            StartCoroutine(SwitchState(_gameDefeatStateState));
    }

    public void StateStart()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            StartCoroutine(SwitchState(_gameStartState));
    }

    public void StatePause()
    {
        if(GameManager.Instance.GameState == GameState.Playable)
            StartCoroutine(SwitchState(_gamePauseState, true, true));
    }

    public void StateSetupAnyGameState(bool doEnter)
    {
        StartCoroutine(SwitchState(_gameSetupState, true, doEnter));
    }

    public IEnumerator CoroutineStateSetup(bool doEnter)
    {
        yield return new WaitForSeconds(0.05f);  
        if(GameManager.Instance.GameState == GameState.Playable)
            StartCoroutine(SwitchState(_gameSetupState, true, doEnter));
    }
    
    public void StateLevelAnyGameState()
    {
        StartCoroutine(SwitchState(_gameLevelState));
    }

    public void StateStartAnyGameState()
    {
        StartCoroutine(SwitchState(_gameStartState, true, true, 0f));
    }
    public void SetWaitForAction(bool value)
    {
        waitForAction = value;
    }

    public void CoroutineSwitchState(GameBaseState _state, bool doExit = true, bool doEnter = true,
        float exitTime = 1f)
    {
        StartCoroutine(SwitchState(_state, doExit, doEnter, exitTime));
    }

    #endregion
}
