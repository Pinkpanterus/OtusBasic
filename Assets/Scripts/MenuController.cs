using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    enum Screen
    {
        None,
        Main,
        Settings
    }

    public CanvasGroup mainScreen;
    public CanvasGroup settingsScreen;

    void SetCurrentScreen(Screen screen)
    {
        Utility.SetCanvasGroupEnabled(mainScreen, screen == Screen.Main);
        Utility.SetCanvasGroupEnabled(settingsScreen, screen == Screen.Settings);
    }
    
    void Start()
    {
        SetCurrentScreen(Screen.Main);
        
        PlaySound.instance.SetLoop(true, 0);
        PlaySound.instance.SetLoop(false, 1);
        PlaySound.instance.Play("MainMenuBackgroundMusic", 0);
    }

    public void StartNewGame()
    {
        PlaySound.instance.Play("Click", 1);
        SetCurrentScreen(Screen.None);
        LoadingScreen.instance.LoadScene("Level-1");
    }

    public void OpenSettings()
    {
        PlaySound.instance.Play("Click", 1);
        SetCurrentScreen(Screen.Settings);
    }

    public void CloseSettings()
    {
        PlaySound.instance.Play("Click", 1);
        SetCurrentScreen(Screen.Main);
    }

    public void ExitGame()
    {
        PlaySound.instance.Play("Click", 1);
        Application.Quit();
    }
}
