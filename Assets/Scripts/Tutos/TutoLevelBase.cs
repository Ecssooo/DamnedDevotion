using System.Collections;
using UnityEngine;

public class TutoLevelBase : MonoBehaviour
{
    private int action;

    private Coroutine _coroutine;
    
    public void Update()
    {
        if(_coroutine == null) _coroutine = StartCoroutine(DoTuto());
    }
    
    public virtual IEnumerator DoTuto()
    {
        yield break;
    }
}
