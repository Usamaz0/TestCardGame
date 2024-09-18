using UnityEngine;
using UnityEngine.UI; // For UI components

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GridManager gridManager; // Reference to the GridManager
    public Text scoreText; // Reference to a UI Text component for displaying the score
    public GameObject gameCompletePanel; // Panel to display when the game is complete

    private CardMatchHandler _cardMatchHandler;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (gridManager == null || scoreText == null || gameCompletePanel == null)
        {
            Debug.LogError("GameManager is missing references. Please assign all fields in the Inspector.");
            return;
        }

        _cardMatchHandler = gridManager.GetComponent<CardMatchHandler>();

        if (_cardMatchHandler == null)
        {
            Debug.LogError("CardMatchHandler component is missing from GridManager!");
            return;
        }

        _cardMatchHandler.SetTotalPairs(gridManager.rows * gridManager.cols / 2); // Initialize total pairs

        // Initialize UI
        gameCompletePanel.SetActive(false); // Hide the game complete panel initially
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        // Update the score displayed on the UI
        if (_cardMatchHandler != null)
        {
            scoreText.text = "Score: " + _cardMatchHandler.GetScore(); // Assuming GetScore() method exists in CardMatchHandler
        }
    }

    public void OnGameComplete()
    {
        // Show the game complete panel
        if (gameCompletePanel != null)
        {
            gameCompletePanel.SetActive(true);
        }
    }
}
