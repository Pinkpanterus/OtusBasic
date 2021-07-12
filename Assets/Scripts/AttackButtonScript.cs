using System;
using UnityEngine;
using UnityEngine.UI;


public class AttackButtonScript : MonoBehaviour
{
    private Button AttackButton;
    public Action AttackButtonPressed;
    
    
    void Start()
    {
        AttackButton = GetComponent<Button>();
        AttackButton.onClick.AddListener(() => {AttackButtonPressed?.Invoke();});
    }
}
