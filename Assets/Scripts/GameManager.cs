using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelCountText;
    [SerializeField] private TextMeshProUGUI ActionCountText;

    private int actionCount;

    private int mogscore;

    private Vector2 cardposition;

    private bool Win = false;

    void Start()
    {
        UpdateLevelCountText();
    }

    void Update()
    {

    }

    private void UpdateLevelCountText()
    {
        Level level = new Level();
        if (Win==true)
        {
            level.level++;
        }
        if (levelCountText != null)
        {
            levelCountText.text = "Level: " + level.level;
        }
    }

    public void Reset()
    {
        Level level = new Level();
        actionCount = level.maxActionCount;
        mogscore = level.maxScore;
    }
}