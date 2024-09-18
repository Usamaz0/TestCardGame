using UnityEngine;

public static class GameSaveLoad
{
    public static int modeType
    {
        get { return PlayerPrefs.GetInt("modeType", 0); }
        set { PlayerPrefs.SetInt("modeType", value); }
    }
    public static int GetCurrentLevel(int modeType)
    {
        return PlayerPrefs.GetInt("currentLevel" + modeType, 0);
    }
    public static void SetCurrentLevel(int modeType,int value)
    {
        PlayerPrefs.SetInt("currentLevel" + modeType, value); 
    }
}
