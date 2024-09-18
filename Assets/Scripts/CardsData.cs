using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CardsData", menuName = "ScriptableObjects/CardsData", order = 1)]
public class CardsData : ScriptableObject
{
    public Sprite[] cardSprites; // Array of sprites for different card types
    public Sprite cardBackSprite; // Sprite for the card back
}
