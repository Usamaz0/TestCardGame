using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelInfo : ScriptableObject
{
    [Header("Grid Size")]
    public int gridRowCount = 2;
    public int gridColumnCount = 2;
}
