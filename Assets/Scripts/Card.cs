using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{
    public int CardID { get; private set; }
    public CardsData cardsData;
    private SpriteRenderer cardRenderer; // Reference to the SpriteRenderer component

    private ICardMatchHandler _matchHandler;
    private bool _isFlipped = false;
    private bool _isAnimating = false;


    private void Awake()
    {
        cardRenderer = GetComponent<SpriteRenderer>();

        if (cardRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing on the card prefab!");
        }

        if (cardsData.cardSprites == null || cardsData.cardSprites.Length == 0)
        {
            Debug.LogError("Card sprites not assigned in the Inspector!");
        }

        // Initialize with card back sprite
        cardRenderer.sprite = cardsData.cardBackSprite;
    }

    public void Initialize(int id, ICardMatchHandler matchHandler)
    {
        CardID = id;
        _matchHandler = matchHandler;
        cardRenderer.sprite = cardsData.cardBackSprite;
    }

    private void OnMouseDown()
    {
        if (!_isAnimating && !_isFlipped)
        {
            StartCoroutine(FlipCardCoroutine());
        }
    }

    private IEnumerator FlipCardCoroutine()
    {
        _isAnimating = true;
        _matchHandler.OnCardFilledAction();
        // Flip to front
        yield return StartCoroutine(FlipCoroutine(Vector3.zero, Vector3.one, cardsData.cardSprites[CardID]));

        // Notify the match handler
        yield return new WaitForSeconds(0.2f);
        _matchHandler.OnCardFlipped(this);

        _isFlipped = true;
        _isAnimating = false;
    }

    public void ResetCard()
    {
        if (_isAnimating || !_isFlipped) return;

        StartCoroutine(FlipBackCoroutine());
    }

    private IEnumerator FlipBackCoroutine()
    {
        _isAnimating = true;
        Debug.Log("Flipping back card: " + gameObject.name);

        // Ensure the card's scale and rotation are set correctly before starting the flip back
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
        cardRenderer.sprite = cardsData.cardBackSprite;

        // Flip back to original state
        yield return StartCoroutine(FlipCoroutine(Vector3.one, new Vector3(1,1,1), cardsData.cardBackSprite));

        _isFlipped = false;
        _isAnimating = false;
        Debug.Log("Card reset: " + gameObject.name);
    }

    private IEnumerator FlipCoroutine(Vector3 startScale, Vector3 endScale, Sprite endSprite)
    {
        float duration = 0.5f;
        float elapsedTime = 0f;

        Quaternion startRotation = Quaternion.Euler(0, 0, 0);
        Quaternion endRotation = Quaternion.Euler(0, 180, 0); // Flip 180 degrees

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final values
        transform.localScale = endScale;
        transform.rotation = endRotation;
        cardRenderer.sprite = endSprite;

        Debug.Log($"Finished flip with scale {endScale} and rotation {endRotation}");
    }

    public void MatchFound()
    {
        StartCoroutine(HideCardCoroutine());
    }

    private IEnumerator HideCardCoroutine()
    {
        yield return new WaitForSeconds(0.2f); // Wait for flip animation

        gameObject.SetActive(false); // Hide the card when a match is found
    }
}
