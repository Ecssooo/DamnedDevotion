using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Database", menuName = "Databases/Level Database")]
public class LevelDatabase : ScriptableObject
{
    public  GameObject humanPrefab;
    public  GameObject knightSwordPrefab;
    public  GameObject knightShieldPrefab;
    public  GameObject monsterPrefab;
    public  GameObject miniMonsterPrefab;
    public  GameObject cauldronPrefab;

    
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
}
