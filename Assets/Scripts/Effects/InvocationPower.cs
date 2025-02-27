using UnityEngine;

public class InvocationPower : MonoBehaviour
{
    public Invocation scriptToActivate;

    void Start()
    {
        if (scriptToActivate != null)
        {
            scriptToActivate.enabled = false;
        }
        scriptToActivate.Board = GameManager.Instance.Board;
    }

    // void OnMouseDown()
    // {
    //     if (scriptToActivate != null)
    //     {
    //         scriptToActivate.enabled = !scriptToActivate.enabled;
    //     }
    //     StartCoroutine(scriptToActivate.InvokeMiniMonster());
    // }
}
