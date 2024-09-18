using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainer : MonoBehaviour
{
  
    [SerializeField] private LevelInfo[] veryEasylevelsInfo, easyLevelsInfo, mediumLevelsInfo,
        hardLevelsInfo, veryHardLevelsInfo;


    public int GetLevelGridRowCount()
    {
        return GetModeType() switch
        {
            0 => veryEasylevelsInfo[GetCurrentLevel()].gridRowCount,
            1 => veryEasylevelsInfo[GetCurrentLevel()].gridRowCount,
            2 => veryEasylevelsInfo[GetCurrentLevel()].gridRowCount,
            3 => veryEasylevelsInfo[GetCurrentLevel()].gridRowCount,
            4 => veryEasylevelsInfo[GetCurrentLevel()].gridRowCount,
            _ => veryEasylevelsInfo[GetCurrentLevel()].gridRowCount
        };
    }
    public int GetLevelGridColumnCount()
    {
        return GetModeType() switch
        {
            0 => veryEasylevelsInfo[GetCurrentLevel()].gridColumnCount,
            1 => veryEasylevelsInfo[GetCurrentLevel()].gridColumnCount,
            2 => veryEasylevelsInfo[GetCurrentLevel()].gridColumnCount,
            3 => veryEasylevelsInfo[GetCurrentLevel()].gridColumnCount,
            4 => veryEasylevelsInfo[GetCurrentLevel()].gridColumnCount,
            _ => veryEasylevelsInfo[GetCurrentLevel()].gridColumnCount
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
            1 => veryEasylevelsInfo.Length,
            2 => veryEasylevelsInfo.Length,
            3 => veryEasylevelsInfo.Length,
            4 => veryEasylevelsInfo.Length,
            _ => veryEasylevelsInfo.Length
        };
    }
}
