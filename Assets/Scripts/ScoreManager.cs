using UnityEngine;

public interface IScoreObserver
{
    void UpdateScore(int points);
}

public class ScoreManager : MonoBehaviour, IScoreObserver
{
    private int _score;
    private int _matchCount;

    public void UpdateScore(int points)
    {
        _score += points;
        _matchCount++;

        // Trigger UI updates (assuming a UI system is in place)
        Debug.Log("Score updated: " + _score);
    }
    public int GetScore()
    {
        return _score;
    }
}
