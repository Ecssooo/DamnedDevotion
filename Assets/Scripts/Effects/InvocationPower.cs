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

    void OnMouseDown()
    {
        if (scriptToActivate != null)
        {
            scriptToActivate.enabled = !scriptToActivate.enabled;
        }
        if (GameManager.Instance.Effect == Effects.INVOKE)
        {
            GameManager.Instance.Effect = Effects.NONE;
        }
        else
        {
            StartCoroutine(scriptToActivate.InvokeMiniMonster());
        }

        
    }
}
