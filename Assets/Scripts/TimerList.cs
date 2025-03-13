using UnityEngine;

[CreateAssetMenu(fileName = "Timer", menuName = "Timer/TimerList")]
public class TimerList : ScriptableObject
{
    [Header("Animations")]
    [SerializeField] private float _movementAnimationDuration;
    [SerializeField] private float _burnAnimationDuration;
    [SerializeField] private float _hitAnimationDuration;


    [SerializeField] private float _swapDelay;

    [Header("Delay between actions")]
    [SerializeField] private float _movementWait;
    [SerializeField] private float _swapWait;
    [SerializeField] private float _invokeWait;
    [SerializeField] private float _cauldronWait;
    [SerializeField] private float _impossibleActionWait;
    
    [Header("Delay for end actions")]
    [SerializeField] private float _endActionWait;
    [SerializeField] private float _beforeEndActionWait;
    [SerializeField] private float _afterEndActionWait;

    
    #region Getter

    public float MovementAnimationDuration => _movementAnimationDuration;
    public float BurnAnimationDuration => _burnAnimationDuration;
    public float HitAniamtionDuration => _hitAnimationDuration;
    public float SwapDelay => _swapDelay;
    
    
    
    public float MovementWait => _movementWait;
    public float SwapWait => _swapWait;
    public float InvokeWait => _invokeWait;
    public float CauldronWait => _cauldronWait;
    public float ImpossibleActionWait => _impossibleActionWait;

    
    public float EndActionWait => _endActionWait;
    public float BeforeEndActionWait => _beforeEndActionWait;
    public float AfterEndActionWait => _afterEndActionWait;
    #endregion
}
