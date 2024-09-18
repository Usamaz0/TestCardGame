using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GridManager gridManager; // Reference to the GridManager
    public GameObject gameCompletePanel; // Panel to display when the game is complete
    [SerializeField]
    private CardMatchHandler _cardMatchHandler;
    [SerializeField]
    private LevelContainer _levelContainer;

    private void OnEnable()
    {
        _cardMatchHandler.onLevelComplete += OnGameComplete;
    }
    private void OnDisable()
    {
        _cardMatchHandler.onLevelComplete -= OnGameComplete;
    }
    private void Start()
    {
        Application.targetFrameRate = 60;

        if (_cardMatchHandler == null)
        {
            Debug.LogError("CardMatchHandler component is missing from GridManager!");
            return;
        }
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
    public void PlayerNextLevel()
    {
        _levelContainer.IncreaseLevel();
        LoadScene();
    }
    public void RestartLevel()
    {
        LoadScene();
    }
    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
