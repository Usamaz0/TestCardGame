using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private int mainMenuSceneIndex =0;
    [SerializeField] private GridManager gridManager; // Reference to the GridManager
    [SerializeField] private GameObject gameCompletePanel; // Panel to display when the game is complete
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
        LoadGameplayScene();
    }
    public void RestartLevel()
    {
        LoadGameplayScene();
    }
    void LoadGameplayScene()
    {
        m_AudioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadMainMenuScene()
    {
        m_AudioSource.Play();
        SceneManager.LoadScene(mainMenuSceneIndex);
    }
}
