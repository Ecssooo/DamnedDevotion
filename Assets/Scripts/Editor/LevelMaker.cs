using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private GameObject humanPrefab;
    private GameObject knightSwordPrefab;
    private GameObject knightShieldPrefab;
    private GameObject monsterPrefab;
    private GameObject miniMonsterPrefab;
    private GameObject cauldronPrefab;

    public enum OPTIONS
    {
        NONE,
        HUMAN,
        KNIGHTSWORD,
        KNIGHTSHIELD,
        MONSTER,
        MINIMONSTER,
        CAULDRON,
    }
    
    public List<OPTIONS> ops = new(12){ 
        OPTIONS.NONE,OPTIONS.NONE, OPTIONS.NONE,
        OPTIONS.NONE,OPTIONS.NONE,OPTIONS.NONE,
        OPTIONS.NONE,OPTIONS.NONE,OPTIONS.NONE,
        OPTIONS.NONE,OPTIONS.NONE,OPTIONS.NONE,
    };
    
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

    private Vector2 scrollPos;
    
    [MenuItem("Tools/LevelMaker")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelMaker));
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.MaxHeight(position.height));
        GUILayout.Label("LevelMaker", EditorStyles.boldLabel);

        //Setup
        board = EditorGUILayout.ObjectField("Board", board, typeof(Board), true) as Board;
        _levelDatabase = EditorGUILayout.ObjectField("Level Database", _levelDatabase, typeof(LevelDatabase), true) as LevelDatabase;
        //Level info
        EditorGUILayout.Space();
        GUILayout.Label("Settings",EditorStyles.boldLabel);
        EditorGUILayout.Space();
        
        levelId = EditorGUILayout.IntField("Id", levelId);
        maxAction = EditorGUILayout.IntField("maxAction", maxAction);
        maxScore = EditorGUILayout.IntField("maxScore", maxScore);

        EditorGUILayout.Space();
        GUILayout.Label("Prefabs",EditorStyles.boldLabel);
        EditorGUILayout.Space();
        
        humanPrefab = EditorGUILayout.ObjectField("Human", humanPrefab, typeof(GameObject), false) as GameObject;
        knightSwordPrefab = EditorGUILayout.ObjectField("Knight Sword", knightSwordPrefab, typeof(GameObject), false) as GameObject;
        knightShieldPrefab = EditorGUILayout.ObjectField("Knight Shield",knightShieldPrefab, typeof(GameObject), false) as GameObject;
        monsterPrefab = EditorGUILayout.ObjectField("Monster", monsterPrefab, typeof(GameObject), false) as GameObject;
        miniMonsterPrefab = EditorGUILayout.ObjectField("Mini Monster", miniMonsterPrefab, typeof(GameObject), false) as GameObject;
        cauldronPrefab = EditorGUILayout.ObjectField("Cauldron", cauldronPrefab, typeof(GameObject), false) as GameObject;
        
        EditorGUILayout.Space();
        GUILayout.Label("Board",EditorStyles.boldLabel);
        EditorGUILayout.Space();
        int index = 0;
        for (int i = 1; i <= 4; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < 3; j++)
            {
                ops[index] = (OPTIONS)EditorGUILayout.EnumPopup(ops[index]);
                index++;
            }
            EditorGUILayout.EndHorizontal();
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
        
        EditorGUILayout.EndScrollView();
    }
    
    /// <summary>
    /// Instantiate card in Scene
    /// </summary>
    void ApplyLevel()
    {
        board.InitSlotTab();
        int index = 0;
        // foreach (var card in slots)
        // {
        //     index++;
        //     if (card == null) continue;
        //     var GO = Instantiate(card, board.transform);
        //     var cardGO = GO.GetComponent<Card>();
        //     cardGO.PositionOnBoard = positionSlot[index];
        //     board.SetSlots(cardGO);
        // }

        foreach (var op in ops)
        {
            switch (op)
            {
                case(OPTIONS.HUMAN):
                    var GO = Instantiate(humanPrefab, board.transform);
                    var cardGO = GO.GetComponent<Card>();
                    cardGO.PositionOnBoard = positionSlot[index];
                    board.SetSlots(cardGO);
                    break;
                case(OPTIONS.KNIGHTSWORD):
                    var GO1 = Instantiate(knightSwordPrefab, board.transform);
                    var cardGO1 = GO1.GetComponent<Card>();
                    cardGO1.PositionOnBoard = positionSlot[index];
                    board.SetSlots(cardGO1);
                    break;
                case(OPTIONS.KNIGHTSHIELD):
                    var GO3 = Instantiate(knightShieldPrefab, board.transform);
                    var cardGO3 = GO3.GetComponent<Card>();
                    cardGO3.PositionOnBoard = positionSlot[index];
                    board.SetSlots(cardGO3);
                    break;
                case(OPTIONS.MONSTER):
                    var GO2 = Instantiate(monsterPrefab, board.transform);
                    var cardGO2 = GO2.GetComponent<Card>();
                    cardGO2.PositionOnBoard = positionSlot[index];
                    board.SetSlots(cardGO2);
                    break;
                case(OPTIONS.MINIMONSTER):
                    var GO4 = Instantiate(monsterPrefab, board.transform);
                    var cardGO4 = GO4.GetComponent<Card>();
                    cardGO4.PositionOnBoard = positionSlot[index];
                    board.SetSlots(cardGO4);
                    break;
                case(OPTIONS.CAULDRON):
                    var GO5 = Instantiate(cauldronPrefab, board.transform);
                    var cardGO5 = GO5.GetComponent<Card>();
                    cardGO5.PositionOnBoard = positionSlot[index];
                    board.SetSlots(cardGO5);
                    break;
            }
            index++;
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

        for (int i = 0; i < ops.Count; i++)
        {
            switch (ops[i])
            {
                case(OPTIONS.HUMAN): slots[i] = humanPrefab; break;
                case(OPTIONS.KNIGHTSWORD): slots[i] = knightSwordPrefab; break;
                case(OPTIONS.KNIGHTSHIELD): slots[i] = knightShieldPrefab; break;
                case(OPTIONS.MINIMONSTER): slots[i] = miniMonsterPrefab; break;
                case(OPTIONS.MONSTER): slots[i] = monsterPrefab; break;
                case(OPTIONS.CAULDRON): slots[i] = cauldronPrefab; break;
                case(OPTIONS.NONE): slots[i] = null; break;
            }
        }
        
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
        try
        {
            board.SetLevel(_levelDatabase.levelList[levelToLoad]);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Debug.LogWarning("Level doesn't exist");
        }
    }

    /// <summary>
    /// Reset board
    /// </summary>
    void ResetBoard()
    {
        board.ResetBoard();
    }
}
