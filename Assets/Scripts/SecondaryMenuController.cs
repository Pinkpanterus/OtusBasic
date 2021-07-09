using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondaryMenuController : MonoBehaviour
{
    public void Continue()
    {
        gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
