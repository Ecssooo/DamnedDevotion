using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class GameBaseState
{
    [SerializeField] protected SpriteRenderer _fonduNoir;
    
    public abstract IEnumerator EnterState(GameStateManager manager);

    public abstract void UpdateState(GameStateManager manager);

    public abstract IEnumerator ExitState(GameStateManager manager);


    
}
