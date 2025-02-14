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

    //public void OnDie()
    //{
    //    // add _value to GameManager
    //    Debug.Log("Human died");
    //    _value = 0;
    //}
}
