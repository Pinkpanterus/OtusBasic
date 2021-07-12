using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    enum Screen
    {
        Main,
        Settings,
        LevelChoice
    }

    public CanvasGroup mainScreen;
    public CanvasGroup settingsScreen;
    public CanvasGroup levelChoice;

    void SetCurrentScreen(Screen screen)
    {
        Utility.SetCanvasGroupEnabled(mainScreen, screen == Screen.Main);
        Utility.SetCanvasGroupEnabled(settingsScreen, screen == Screen.Settings);
        Utility.SetCanvasGroupEnabled(levelChoice, screen == Screen.LevelChoice);
    }
    
    void Start()
    {
        Time.timeScale = 1f;
        SetCurrentScreen(Screen.Main);
    }

    public void StartLevel1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    
    public void StartLevel2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
    
    public void LevelChoice()
    {
        SetCurrentScreen(Screen.LevelChoice);
    }

    public void OpenSettings()
    {
        SetCurrentScreen(Screen.Settings);
    }

    public void CloseSettings()
    {
        SetCurrentScreen(Screen.Main);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
