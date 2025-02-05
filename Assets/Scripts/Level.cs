using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int level;
    public List<Vector2> cardPositions;
    public int maxActionCount;
    public int maxScore;
    public List<GameObject> effects;

    public Level()
    {
        cardPositions = new List<Vector2>();
        effects = new List<GameObject>();
    }
}