using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class LevelMaker : EditorWindow
{
    private Board board;
    private LevelDatabase _levelDatabase;
    
    private int levelId = 0;
    private int maxScore = 0;
    private int maxAction = 0;

    private int levelToLoad = 0;

    private List<GameObject> slots = new List<GameObject>() { 
        null, null, null,
        null,null,null,
        null,null,null,
        null,null,null,
    };

    private List<Vector2Int> positionSlot = new List<Vector2Int>()
    {
        new Vector2Int(0,0), new Vector2Int(0,1), new Vector2Int(0,2),
        new Vector2Int(1,0), new Vector2Int(1,1), new Vector2Int(1,2),
        new Vector2Int(2,0), new Vector2Int(2,1), new Vector2Int(2,2),
        new Vector2Int(3,0), new Vector2Int(3,1), new Vector2Int(3,2),
    };
    
    
    [MenuItem("Tools/LevelMaker")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelMaker));
    }

    private void OnGUI()
    {
        GUILayout.Label("LevelMaker", EditorStyles.boldLabel);

        //Setup
        board = EditorGUILayout.ObjectField("Board", board, typeof(Board), true) as Board;
        _levelDatabase = EditorGUILayout.ObjectField("Level Database", _levelDatabase, typeof(LevelDatabase), true) as LevelDatabase;
        EditorGUILayout.Space();
        //Level info
        levelId = EditorGUILayout.IntField("Id", levelId);
        maxAction = EditorGUILayout.IntField("maxAction", maxAction);
        maxScore = EditorGUILayout.IntField("maxScore", maxScore);

        EditorGUILayout.Space(20);
        //Slots for Card
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i] = EditorGUILayout.ObjectField($"Slot {i + 1}", slots[i], typeof(GameObject), false) as GameObject;
        }

        
        EditorGUILayout.Space(20);
        //Buttons
        if (GUILayout.Button("ApplyLevel"))
        {
            ApplyLevel();
        }
        if (GUILayout.Button("SaveLevel"))
        {
            SaveLevel();
        }
        levelToLoad = EditorGUILayout.IntField("levelToLoad", levelToLoad);
        if (GUILayout.Button("LoadLevel"))
        {
            LoadLevel();
        }

        if (GUILayout.Button("Reset board"))
        {
            ResetBoard();
        }
    }
    
    /// <summary>
    /// Instantiate card in Scene
    /// </summary>
    void ApplyLevel()
    {
        board.InitSlotTab();
        int index = -1;
        foreach (var card in slots)
        {
            index++;
            if (card == null) continue;
            var GO = Instantiate(card, board.transform);
            var cardGO = GO.GetComponent<Card>();
            cardGO.PositionOnBoard = positionSlot[index];
            board.SetSlots(cardGO);
        }
    }

    /// <summary>
    /// Save a new level in level database
    /// </summary>
    void SaveLevel()
    {
        List<Vector2Int> allPosition = new List<Vector2Int>(){ 
            new (-1,-1),new (-1,-1),new (-1,-1),
            new (-1,-1),new (-1,-1),new (-1,-1),
            new (-1,-1),new (-1,-1),new (-1,-1),
            new (-1,-1),new (-1,-1),new (-1,-1),
        };
        List<GameObject> newCardList = new();
        int index = 0;
        foreach (var card in slots)
        {
            if (card != null) allPosition[index] = positionSlot[index];
            newCardList.Add(card);
            index++;
        }
        
        Level newLevel = new Level(levelId, newCardList, allPosition, maxAction, maxScore);
        _levelDatabase.levelList.Add(newLevel);
    }

    
    /// <summary>
    /// Load level in function of levelToLoad
    /// </summary>
    void LoadLevel()
    {
        ResetBoard();
        board.SetLevel(_levelDatabase.levelList[levelToLoad]);
    }

    /// <summary>
    /// Reset board
    /// </summary>
    void ResetBoard()
    {
        board.ResetBoard();
    }
}
