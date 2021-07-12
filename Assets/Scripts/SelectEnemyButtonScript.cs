using System;
using System.Linq;
using Assets.Scripts.Character;
using UnityEngine;
using UnityEngine.UI;

public class SelectEnemyButtonScript : MonoBehaviour
{
    private Button SelectEnemyButton;
    public Func<CharacterComponent[],CharacterComponent> SelectEnemyButtonPressed;

    private CharacterComponent[] enemies;
    
    
    void Start()
    {
        SelectEnemyButton = GetComponent<Button>();
        SelectEnemyButton.onClick.AddListener(() => { SelectEnemyButtonPressed?.Invoke(FindEnemies()); });
    }

    CharacterComponent[] FindEnemies()
    {
        return GameController.Instance.enemyCharacters.Contains(GameController.Instance.CurrentCharacter) ? GameController.Instance.playerCharacters : GameController.Instance.enemyCharacters;
    }
}
