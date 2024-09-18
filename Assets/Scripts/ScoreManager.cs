using System;
using TMPro;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] private CardMatchHandler cardMatchCheck;
    int turns;
    int matches;
    int score;
    public event Action onTurnUpdated;
    public event Action onMatchesUpdated;

    public TextMeshProUGUI turnText,matchText,scoreText; // Reference to a UI Text component for displaying the score
    private void Start()
    {
        cardMatchCheck.onTurnUpdated +=  IncreaseTurns;
        cardMatchCheck.onCardMatahed += IncreaseMatches;
    }
    private void OnDisable()
    {
        cardMatchCheck.onTurnUpdated -= IncreaseTurns;
        cardMatchCheck.onCardMatahed -= IncreaseMatches;
    }
    void IncreaseTurns()
    {
        turns++;
        onTurnUpdated?.Invoke();
        UpdateScoreUI();
    }
    void IncreaseMatches()
    {
        matches++;
        onMatchesUpdated?.Invoke();
        UpdateScoreUI();
    }
    public int GetTurns()
    {
        return turns;
    }
    public int GetMatches()
    {
        return matches;
    }
    public int GetScore()
    {
        if(turns == 0) return 0;
        return (int)(matches * 100) / turns;
    }
    public void UpdateScoreUI()
    {
        // Update the score displayed on the UI
        if (turnText != null)
        {
            turnText.text = "Turn: " + GetTurns(); // Assuming GetScore() method exists in CardMatchHandler
        }
        if (matchText != null)
        {
            matchText.text = "Match: " + GetMatches(); // Assuming GetScore() method exists in CardMatchHandler
        }
        if (scoreText != null)
        {
            scoreText.text = "Score: " + GetScore(); // Assuming GetScore() method exists in CardMatchHandler
        }
    }
}
