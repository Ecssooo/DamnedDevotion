using UnityEditor.Build.Content;
using UnityEngine;

public class Human : Card
{
    [SerializeField] private int _value;

    //private GameManager gameManager;

    private void Start()
    {
        // find GameManager
    }

    public void OnDie()
    {
        // add _value to GameManager
        _value = 0;
    }
}
