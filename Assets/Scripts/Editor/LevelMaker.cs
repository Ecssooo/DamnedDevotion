using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class LevelMaker : EditorWindow
{
    private BoardController _boardController;
    private LevelDatabase _levelDatabase;

    private int levelId;
    private int maxScore = 0;
    private int maxAction = 0;

    private int levelToLoad = 0;
    
    public struct PersoWrapper
    {
        public CardType type;
        public Direction axis;
    }

    public PersoWrapper[] persos = new PersoWrapper[12];


    private GameObject[] slots = new GameObject[12];

    private Vector2Int[] positionSlot = new Vector2Int[12]
    {
        new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2),
        new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(1, 2),
        new Vector2Int(2, 0), new Vector2Int(2, 1), new Vector2Int(2, 2),
        new Vector2Int(3, 0), new Vector2Int(3, 1), new Vector2Int(3, 2),
    };

    private Vector2 scrollPos;

    private bool _moveEnabled;
    private bool _switchEnabled;
    private bool _invocEnabled;

    private GameObject _tutoPrefab;
    
    
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
        _boardController = EditorGUILayout.ObjectField("Board", _boardController, typeof(BoardController), true) as BoardController;
        _levelDatabase =
            EditorGUILayout.ObjectField("Level Database", _levelDatabase, typeof(LevelDatabase), true) as LevelDatabase;
        //Level info
        EditorGUILayout.Space();
        GUILayout.Label("Settings", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        levelId = EditorGUILayout.IntField("Id", levelId);
        maxAction = EditorGUILayout.IntField("maxAction", maxAction);
        maxScore = EditorGUILayout.IntField("maxScore", maxScore);
 
        EditorGUILayout.Space();
        GUILayout.Label("Prefabs", EditorStyles.boldLabel);

        EditorGUILayout.Space();
        GUILayout.Label("Board", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        for (int i = 0; i < persos.Length; i++)
        {
            var pw = persos[i];

            if (i % 3 == 0)
                EditorGUILayout.BeginHorizontal();

            if (pw.type == CardType.KNIGHTSWORD)
            {
                EditorGUILayout.BeginHorizontal(GUILayout.Width((position.width / 3.1f)));
                pw.type = (CardType)EditorGUILayout.EnumPopup(pw.type);
                pw.axis = (Direction)EditorGUILayout.EnumPopup(pw.axis);

                EditorGUILayout.EndHorizontal();
            }
            else
                pw.type = (CardType)EditorGUILayout.EnumPopup(pw.type, GUILayout.Width((position.width / 3.1f)));


            if (i % 3 == 2)
                EditorGUILayout.EndHorizontal();

            persos[i] = pw;
        }

        _moveEnabled = EditorGUILayout.Toggle("Move", _moveEnabled);
        _switchEnabled = EditorGUILayout.Toggle("Switch", _switchEnabled);
        _invocEnabled = EditorGUILayout.Toggle("Invocation", _invocEnabled);
        
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

        _tutoPrefab = EditorGUILayout.ObjectField("Tuto", _tutoPrefab, typeof(GameObject), true) as GameObject;
        
        EditorGUILayout.EndScrollView();
    }

    /// <summary>
    /// Instantiate card in Scene
    /// </summary>
    void ApplyLevel()
    {
        _boardController.ResetBoard();
        _boardController.InitSlotTab();
        int index = 0;
        foreach (var perso in persos)
        {
            SpawnPrefab(perso, index);
            index++;
        }
    }

    void SpawnPrefab(PersoWrapper cardParams, int index)
    {
        CardParams card = new();
        card.cardType = cardParams.type;
        card.positionOnBoard = positionSlot[index];
        card.direction = cardParams.axis;
        _boardController.EditorSetSlots(card);
    }

    /// <summary>
    /// Save a new level in level database
    /// </summary>
    void SaveLevel()
    {
        levelId = _levelDatabase.levelList.Count;
        
        List<CardParams> newCardList = new();
        int index = 0;

        foreach (var pw in persos)
        {
            CardParams newCard = new CardParams();

            newCard.cardType = pw.type;
            newCard.direction = pw.axis;
            newCard.positionOnBoard = positionSlot[index];

            newCardList.Add(newCard);
            index++;
        }

        bool[] effect = new[] { _moveEnabled, _switchEnabled, _invocEnabled };
        
        Level newLevel = new Level(levelId, newCardList, maxAction, maxScore, effect, _tutoPrefab);
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
            _boardController.EditorSetLevel(_levelDatabase.levelList[levelToLoad]);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Debug.LogWarning("Level doesn't exist" + ex);
        }
        
        int index = 0;
        foreach (var card in _levelDatabase.levelList[levelToLoad].CardsList)
        {
            PersoWrapper newPW = new PersoWrapper();
            newPW.type = card.cardType;
            newPW.axis = card.direction;
            persos[index] = newPW;
            index++;
        }
    }

    /// <summary>
    /// Reset board
    /// </summary>
    void ResetBoard()
    {
        _boardController.ResetBoard();
        for (int i = 0; i < persos.Length; i++)
        {
            persos[i].type = CardType.NONE;
        }
    }
}
