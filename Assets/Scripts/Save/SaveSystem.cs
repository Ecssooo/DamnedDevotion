using UnityEngine;

public static class SaveSystem
{

    public static void InitSave()
    {
        if (PlayerPrefs.GetInt("Level") == null)
        {
            PlayerPrefs.SetInt("Level", 0);
        }
    }
    
    public static void Save(int value)
    {
        if (value < 0) return;
        PlayerPrefs.SetInt("Level", value);
    }

    public static int Load()
    {
        return PlayerPrefs.GetInt("Level");
    }
}
