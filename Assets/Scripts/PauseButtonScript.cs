using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseButtonScript : MonoBehaviour
{
    private Button pauseButton;
    public GameObject secondaryMenuPanel;
    
    void Start()
    {
        pauseButton = GetComponent<Button>();
        pauseButton.onClick.AddListener(() => {secondaryMenuPanel.SetActive(true);});
    }
}
