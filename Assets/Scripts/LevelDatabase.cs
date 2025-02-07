using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Database", menuName = "Databases/Level Database")]
public class LevelDatabase : ScriptableObject
{
    public List<Level> levelList = new ();
}
