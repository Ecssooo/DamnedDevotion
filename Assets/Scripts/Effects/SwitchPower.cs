using UnityEngine;

public class SwitchPower : MonoBehaviour
{
    private Switch[] switches;
    private bool areSwitchesActive = false;

    void Start()
    {
        switches = FindObjectsOfType<Switch>();

        foreach (var switchScript in switches)
        {
            switchScript.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        areSwitchesActive = !areSwitchesActive;

        foreach (var switchScript in switches)
        {
            switchScript.enabled = areSwitchesActive;
        }
    }
}
