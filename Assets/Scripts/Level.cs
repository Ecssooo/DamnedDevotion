using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CardParams
{
    public CardType cardType;
    public Direction direction;
    public Vector2Int positionOnBoard;
}

[Serializable]
public class Level
{
    public int level;
    public List<CardParams> CardsList;
    public int maxActionCount;
    public int maxScore;
    public List<GameObject> effects;

    public Level(int _levelindex,List<CardParams> cardsList, int _maxAction, int _maxScore)
    {
        level = _levelindex;
        CardsList = cardsList;
        maxActionCount = _maxAction;
        maxScore = _maxScore;
        effects = new List<GameObject>();
    }
}