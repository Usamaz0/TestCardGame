using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource;

    [SerializeField] private Toggle[] toggles;

    [SerializeField] private int gameplaySceneIndex = 1;

    private void Start()
    {
        toggles[GameSaveLoad.modeType].isOn = true;
    }
    public void LoadGameplayScene()
    {
        m_AudioSource.Play();
        SceneManager.LoadScene(gameplaySceneIndex);
    }
    public void OnToggleChanged(int value)
    {
        m_AudioSource.Play();
        GameSaveLoad.modeType = value;
    }
}
