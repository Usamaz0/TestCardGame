using System;
using TMPro;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] private CardMatchHandler cardMatchCheck;
    int turns;
    int matches;
    int score;
    int accuracy;
    int combo =1;
    public event Action onTurnUpdated;
    public event Action onMatchesUpdated;

    public TextMeshProUGUI turnText,matchText,scoreText, accuracyText, comboText; // Reference to a UI Text component for displaying the score
    private void Start()
    {
        cardMatchCheck.onTurnUpdated +=  IncreaseTurns;
        cardMatchCheck.onCardMatahed += IncreaseMatches;
        cardMatchCheck.onCardMatahed += IncreaseScore;
        cardMatchCheck.onCardMatahed += IncreaseCombo;
        cardMatchCheck.onCardMismatahed += ResetCombo;
        UpdateScoreUI();
    }
    private void OnDisable()
    {
        cardMatchCheck.onTurnUpdated -= IncreaseTurns;
        cardMatchCheck.onCardMatahed -= IncreaseMatches;
        cardMatchCheck.onCardMatahed -= IncreaseScore;
        cardMatchCheck.onCardMatahed -= IncreaseCombo;
        cardMatchCheck.onCardMismatahed -= ResetCombo;
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
    void IncreaseScore()
    {
        score += (10*combo);
        UpdateScoreUI();
    }
    void IncreaseCombo()
    {
        combo++;
        UpdateScoreUI();
    }
    void ResetCombo()
    {
        combo = 1;
        UpdateScoreUI();
    }
    int GetTurns()
    {
        return turns/2;
    }
    int GetAccuracy()
    {
        if(GetTurns() == 0) return 0;
        return (int)(matches * 100) / GetTurns();
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
            matchText.text = "Match: " + matches; // Assuming GetScore() method exists in CardMatchHandler
        }
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // Assuming GetScore() method exists in CardMatchHandler
        }
        if (accuracyText != null)
        {
            accuracyText.text = "Accuracy: " + GetAccuracy() +"%"; // Assuming GetScore() method exists in CardMatchHandler
        }
        if(comboText != null)
        {
            comboText.text = "Combo: " + combo;
        }    
    }
}
