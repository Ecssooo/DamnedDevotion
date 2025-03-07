using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Database", menuName = "Databases/Level Database")]
public class LevelDatabase : ScriptableObject
{
    
    [Header("Card Prefabs")]
    public  GameObject humanPrefab;
    public  GameObject knightSwordPrefab;
    public  GameObject knightShieldPrefab;
    public  GameObject monsterPrefab;
    public  GameObject miniMonsterPrefab;
    public  GameObject cauldronPrefab;

    [Header("Effect Prefabs")]
    public GameObject moveEffectPrefab;
    public GameObject switchEffectPrefab;
    public GameObject invocationEffectPrefab;
    
    [Header("Levels list")]
    public List<Level> levelList = new ();
    
    public GameObject GetPrefab(CardType type)
    {
        switch (type)
        {
            case(CardType.NONE): return null;
            case(CardType.HUMAN): return humanPrefab;
            case(CardType.KNIGHTSWORD): return knightSwordPrefab;
            case(CardType.KNIGHTSHIELD): return knightShieldPrefab;
            case(CardType.MONSTER): return monsterPrefab;
            case(CardType.MINIMONSTER): return miniMonsterPrefab;
            case(CardType.CAULDRON): return cauldronPrefab;
        }
        return null;
    }

    public GameObject GetPrefab(Effects type)
    {
        switch (type)
        {
            case(Effects.NONE): return null;
            case(Effects.MOVE): return moveEffectPrefab;
            case(Effects.SWAP): return switchEffectPrefab;
            case(Effects.INVOKE): return invocationEffectPrefab;
        }
        return null;
    }
}
