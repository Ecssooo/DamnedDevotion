using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Board _board;
    public Board Board { get => _board; }

    private static GameManager _instance;

    public static GameManager Instance { get => _instance; }

    public void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Reset()
    {
        // Level level = new Level(new List<Card>());
        // actionCount = level.maxActionCount;
        // mogscore = level.maxScore;
    }
}