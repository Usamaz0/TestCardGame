using UnityEngine;

public class GameSaveLoad : MonoBehaviour
{
    private const string ScoreKey = "Score";

    public void SaveGame(int score)
    {
        PlayerPrefs.SetInt(ScoreKey, score);
        PlayerPrefs.Save();
    }

    public int LoadGame()
    {
        return PlayerPrefs.GetInt(ScoreKey, 0);
    }
}
