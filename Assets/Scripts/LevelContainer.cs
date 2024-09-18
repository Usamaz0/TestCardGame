using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainer : MonoBehaviour
{
  
    [SerializeField] private LevelInfo[] veryEasylevelsInfo, easyLevelsInfo, mediumLevelsInfo,
        hardLevelsInfo, veryHardLevelsInfo;

    public int currentMode;
    private void Awake()
    {
        currentMode = GetModeType();
        if (GetCurrentLevel() >= GetTotalLevelsInMode())
        {
            GameSaveLoad.SetCurrentLevel(GetModeType(), 0);
        }
    }
    public int GetLevelGridRowCount()
    {
        return GetModeType() switch
        {
            0 => veryEasylevelsInfo[GetCurrentLevel()].gridRowCount,
            1 => easyLevelsInfo[GetCurrentLevel()].gridRowCount,
            2 => mediumLevelsInfo[GetCurrentLevel()].gridRowCount,
            3 => hardLevelsInfo[GetCurrentLevel()].gridRowCount,
            _ => veryHardLevelsInfo[GetCurrentLevel()].gridRowCount
        };
    }
    public int GetLevelGridColumnCount()
    {
        return GetModeType() switch
        {
            0 => veryEasylevelsInfo[GetCurrentLevel()].gridColumnCount,
            1 => easyLevelsInfo[GetCurrentLevel()].gridColumnCount,
            2 => mediumLevelsInfo[GetCurrentLevel()].gridColumnCount,
            3 => hardLevelsInfo[GetCurrentLevel()].gridColumnCount,
            _ => veryHardLevelsInfo[GetCurrentLevel()].gridColumnCount
        };
    }
    public int GetCurrentLevel()
    {
        return GameSaveLoad.GetCurrentLevel(GetModeType());
    }
    public int GetModeType()
    {
        return GameSaveLoad.modeType;
    }
    public void IncreaseLevel()
    {
        GameSaveLoad.SetCurrentLevel(GetModeType(), GetCurrentLevel() + 1);
        if (GetCurrentLevel() >= GetTotalLevelsInMode())
        {
            GameSaveLoad.SetCurrentLevel(GetModeType(), 0);
        }
    }

    int GetTotalLevelsInMode()
    {
        return GetModeType() switch
        {
            0 => veryEasylevelsInfo.Length,
            1 => easyLevelsInfo.Length,
            2 => mediumLevelsInfo.Length,
            3 => hardLevelsInfo.Length,
            _ => veryHardLevelsInfo.Length
        };
    }
}
