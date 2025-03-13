using TMPro;
using UnityEngine;

public class ActionCount : MonoBehaviour
{
    private int action;

    public int ActionPoints { get => action; }

    private TextMeshProUGUI _actionPointText;
    public TextMeshProUGUI ActionPointText { get => _actionPointText; set => _actionPointText = value; }


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
        ActionPointText.text = action.ToString();
    }
}
