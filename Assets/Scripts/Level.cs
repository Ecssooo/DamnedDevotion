using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int level;
    public List<Card> CardsList;
    public int maxActionCount;
    public int maxScore;
    public List<GameObject> effects;

    public Level()
    {
        CardsList = new List<Card>();
        effects = new List<GameObject>();
    }
}