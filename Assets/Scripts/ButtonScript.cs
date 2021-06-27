using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Character Character;
    private Button button;
    
    void Start()
    {
        button = GetComponent<Button>();
        
        if(!button.name.Contains("Policeman") && button.name.Contains("Attack"))
            button.onClick.AddListener(() => Character.SetState(Character.State.RunningToEnemy));
        else if(button.name.Contains("Policeman") && button.name.Contains("Attack")) //
            button.onClick.AddListener(() => Character.SetState(Character.State.BeginShoot));
        else
            button.onClick.AddListener(() => Character.SetState(Character.State.Dead));
    }
}
