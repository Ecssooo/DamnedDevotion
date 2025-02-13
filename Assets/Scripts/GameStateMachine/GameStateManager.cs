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

    public GameBaseState CurrentState => _currentState;

    public GameSetupState GameSetupState => _gameSetupState;

    public GameLevelState GameLevelState => _gameLevelState;

    private void Start()
    {
        _currentState = _gameLevelState;
        _currentState.EnterState(this);
    }

    public void SwitchState(GameBaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }
}
