using UnityEngine;

public class InvocationPower : MonoBehaviour
{
    public MonoBehaviour scriptToActivate;

    void Start()
    {
        if (scriptToActivate != null)
        {
            scriptToActivate.enabled = false;
        }
    }

    void OnMouseDown()
    {
        if (scriptToActivate != null)
        {
            scriptToActivate.enabled = !scriptToActivate.enabled;
        }
    }
}
