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
        DisplayActionPoint();
    }

    public void Decrement(int value)
    {
        action -= value;
        DisplayActionPoint();
    }

    public void Increment(int value)
    {
        action += value;
        DisplayActionPoint();
    }

    public bool ActionRemaining()
    {
        return action > 0;
    }

    public void DisplayActionPoint()
    {
        actionPointText.text = action.ToString();
    }
}
