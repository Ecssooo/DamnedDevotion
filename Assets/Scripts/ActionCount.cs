using TMPro;
using UnityEngine;

public class ActionCount : MonoBehaviour
{
    private int action;

    public int ActionPoints { get => action; }

    public TextMeshProUGUI actionPointText;


    public void InitActionPoint(int max)
    {
        action = max;
    }

    public void Decrement(int value)
    {
        action -= value;
    }

    public void Increment(int value)
    {
        action += value;
    }

    public bool ActionRemaining()
    {
        return action > 0;
    }

    void displayActionPoint()
    {
        actionPointText.text = "Action Point :" + action;
    }
}
