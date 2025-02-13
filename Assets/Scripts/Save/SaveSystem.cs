using UnityEngine;

public static class SaveSystem
{

    public static void InitSave()
    {
        if (PlayerPrefs.GetInt("Level") == 0)
        {
            PlayerPrefs.SetInt("Level", 1);
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
