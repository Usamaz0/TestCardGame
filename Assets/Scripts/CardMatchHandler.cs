using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class CardMatchHandler : MonoBehaviour, ICardMatchHandler
{
    private List<Card> _flippedCards = new List<Card>();
    private int _matchedPairs = 0;
    private int _totalPairs;

    public event Action onTurnUpdated;
    public event Action onCardMatahed;
    public event Action onCardMismatahed;
    public event Action onLevelComplete;

    public AudioClip cardClickSound;
    public AudioClip cardMatchSound;
    public AudioClip cardMismatchSound;
    public AudioClip gameCompleteSound;

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on GridManager!");
            return;
        }
    }
    public void OnCardFlipped(Card card)
    {
        _flippedCards.Add(card);
        if (_flippedCards.Count == 2)
        {
            StartCoroutine(CheckMatchCoroutine());
        }
    }

    private IEnumerator CheckMatchCoroutine()
    {
        yield return new WaitForSeconds(0.5f); // Wait for flip animation

        if (_flippedCards[0].CardID == _flippedCards[1].CardID)
        {
            _matchedPairs++;
            foreach (Card card in _flippedCards)
            {
                onCardMatahed?.Invoke();
                card.MatchFound(); // Notify each card of a successful match
                HandleCardMatch();
            }
        }
        else
        {
            foreach (Card card in _flippedCards)
            {
                onCardMismatahed?.Invoke();
                card.ResetCard(); // Reset cards that did not match
                HandleCardMismatch();
            }
        }

        _flippedCards.Clear();
        CheckGameCompletion();
    }

    private void CheckGameCompletion()
    {
        if (_matchedPairs >= _totalPairs)
        {
            onLevelComplete?.Invoke();
            HandleGameComplete();
        }
    }

    public void SetTotalPairs(int totalPairs)
    {
        _totalPairs = totalPairs;
    }
    private void HandleCardClick()
    {
        audioSource.PlayOneShot(cardClickSound);
    }

    private void HandleCardMatch()
    {
        audioSource.PlayOneShot(cardMatchSound);
    }

    private void HandleCardMismatch()
    {
        audioSource.PlayOneShot(cardMismatchSound);
    }

    // Call this when the game is complete
    public void HandleGameComplete()
    {
        audioSource.PlayOneShot(gameCompleteSound);
    }

    public void OnCardFilledAction()
    {
        onTurnUpdated?.Invoke();
        HandleCardClick();
    }
}
