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

    [SerializeField] private LevelDatabase _levelDatabase;
    public LevelDatabase LevelDatabase { get => _levelDatabase; }

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


    // [SerializeField] private TextMeshProUGUI levelCountText;
    // [SerializeField] private TextMeshProUGUI ActionCountText;
    //
    // private int actionCount;
    //
    // private int mogscore;
    //
    // private Vector2 cardposition;
    //
    // private bool Win = false;
    //
    // void Start()
    // {
    //     UpdateLevelCountText();
    // }
    //
    // void Update()
    // {
    //
    // }
    //
    // private void UpdateLevelCountText()
    // {
    //     // Level level = new Level(new List<Card>());
    //     if (Win==true)
    //     {
    //         //level.level++;
    //     }
    //     if (levelCountText != null)
    //     {
    //         // levelCountText.text = "Level: " + level.level;
    //     }
    // }
    //
    // private void Reset()
    // {
    //     // Level level = new Level(new List<Card>());
    //     // actionCount = level.maxActionCount;
    //     // mogscore = level.maxScore;
    // }
}