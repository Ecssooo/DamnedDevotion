using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public int level;
    public List<GameObject> CardsList;
    public List<Vector2Int> positionCardsList;
    public int maxActionCount;
    public int maxScore;
    public List<GameObject> effects;

    public Level(int _levelindex,List<GameObject> cardsList, List<Vector2Int> _positionList, int _maxAction, int _maxScore)
    {
        level = _levelindex;
        CardsList = cardsList;
        positionCardsList = _positionList;
        maxActionCount = _maxAction;
        maxScore = _maxScore;
        effects = new List<GameObject>();
    }
}