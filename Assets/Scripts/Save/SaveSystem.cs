using UnityEngine;

public static class SaveSystem
{
    
    /// <summary>
    ///  Create save slots for level in PlayerPrefs
    /// </summary>
    public static void InitSave()
    {
        if (PlayerPrefs.GetInt("Level") == null)
        {
            PlayerPrefs.SetInt("Level", 0);
        }
    }
    
    /// <summary>
    ///  Save max level in PlayerPrefs
    /// </summary>
    /// <param name="value">Max level</param>
    public static void Save(int value)
    {
        if (value < 0) return;
        PlayerPrefs.SetInt("Level", value);
    }

    /// <summary>
    ///  Load max level save
    /// </summary>
    /// <returns>Max level</returns>
    public static int Load()
    {
        return PlayerPrefs.GetInt("Level");
    }
}
