using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab; // Prefab for the card
    [SerializeField] private Transform gridParent; // Parent object where cards will be instantiated

    [SerializeField] private float xSpacing = 1.5f; // Horizontal spacing between cards
    [SerializeField] private float ySpacing = 1.5f; // Vertical spacing between cards
    [SerializeField] private Camera mainCamera; // Reference to the main camera

    [SerializeField]
    private CardMatchHandler _cardMatchHandler;
    [SerializeField]
    private LevelContainer levelContainer;

    private int rows = 2;
    private int cols = 2;
    private void Awake()
    {
        rows = levelContainer.GetLevelGridRowCount();
        cols = levelContainer.GetLevelGridColumnCount();
    }
    private void Start()
    {
        if (_cardMatchHandler == null)
        {
            Debug.LogError("CardMatchHandler component is missing from the GridManager!");
            return;
        }
        _cardMatchHandler.SetTotalPairs(GetTotalPairs()); // Initialize total pairs


        int totalPairs = GenerateGrid();
        _cardMatchHandler.SetTotalPairs(totalPairs / 2); // Set total pairs in the match handler

        AdjustGridToFitCamera(); // Adjust the grid to fit the camera view
    }
    public int GetTotalPairs()
    {
        return ((rows * cols) / 2);
    }
    // Method to adjust the grid to fit within the camera view and center it
    private void AdjustGridToFitCamera()
    {
        float screenAspect = mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize * 2.0f;
        float screenWidth = screenHeight * screenAspect;

        float gridWidth = cols * xSpacing;
        float gridHeight = rows * ySpacing;

        // Calculate the scale factor to fit the grid inside the camera view
        float widthScale = screenWidth / gridWidth;
        float heightScale = screenHeight / gridHeight;
        float scale = Mathf.Min(widthScale, heightScale);

        // Apply the calculated scale
        gridParent.localScale = Vector3.one * scale;

        // Calculate the grid center position (grid's midpoint should align with camera center)
        float totalGridWidth = gridWidth * scale;
        float totalGridHeight = gridHeight * scale;

        // Set grid's position directly at the center of the camera view
        float gridCenterX = (cols - 1) * xSpacing * scale / 2f;
        float gridCenterY = (rows - 1) * ySpacing * scale / 2f;

        // Adjust gridParent's position so that it's centered within the camera
        gridParent.localPosition = new Vector3(-gridCenterX, gridCenterY, 0);
        mainCamera.orthographicSize = mainCamera.orthographicSize+1;
    }




    private int GenerateGrid()
    {
        int totalCards = rows * cols;
        if (totalCards % 2 != 0)
        {
            Debug.LogError("Total number of cards must be even.");
            return 0;
        }

        // Generate pairs of unique card IDs
        List<int> cardIDs = GenerateCardIDs(totalCards / 2);
        ShuffleList(cardIDs);

        int cardIndex = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (cardIndex >= totalCards) return totalCards;

                // Instantiate the card prefab and position it
                GameObject newCard = Instantiate(cardPrefab, gridParent);
                newCard.transform.localPosition = new Vector3(col * xSpacing, -row * ySpacing, 0); // Use negative Y for rows

                Card card = newCard.GetComponent<Card>();
                if (card != null)
                {
                    // Initialize the card with a unique ID and card match handler
                    card.Initialize(cardIDs[cardIndex], _cardMatchHandler);
                }
                else
                {
                    Debug.LogError("Card prefab is missing the Card component!");
                }

                cardIndex++;
            }
        }

        return totalCards;
    }

    private List<int> GenerateCardIDs(int numPairs)
    {
        List<int> cardIDs = new List<int>();
        for (int i = 0; i < numPairs; i++)
        {
            cardIDs.Add(i);
            cardIDs.Add(i);
        }

        return cardIDs;
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

  
}
