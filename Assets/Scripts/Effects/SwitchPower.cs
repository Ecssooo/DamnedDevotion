using UnityEngine;

public class SwitchPower : MonoBehaviour
{
    private Card[] switches;
    private bool areSwitchesActive = false;

    void Start()
    {
        switches = FindObjectsOfType<Card>();

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
