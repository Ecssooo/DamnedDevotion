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
        // Debug.Log(_currentState.GetType());  
    }

    public void SwitchState(GameBaseState state)
    {
        //_currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }
    
    #region TEMP

    public void SwitchToMenu()
    {
        _currentState.ExitState(this);
        SwitchState(_gameLevelState);
    }
    
    #endregion
}
