using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GridManager gridManager; // Reference to the GridManager
    public GameObject gameCompletePanel; // Panel to display when the game is complete

    private CardMatchHandler _cardMatchHandler;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _cardMatchHandler = gridManager.GetComponent<CardMatchHandler>();

        if (_cardMatchHandler == null)
        {
            Debug.LogError("CardMatchHandler component is missing from GridManager!");
            return;
        }

        _cardMatchHandler.SetTotalPairs(gridManager.rows * gridManager.cols / 2); // Initialize total pairs

        // Initialize UI
        gameCompletePanel.SetActive(false); // Hide the game complete panel initially
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
