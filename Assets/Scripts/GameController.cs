using Assets.Scripts.CustomYieldInstructions;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Character;
using Assets.Scripts.Character.Health;
using TMPro;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance {get => instance; }
    public CharacterComponent[] playerCharacters;
    public CharacterComponent[] enemyCharacters;
    private CharacterComponent[] aliveEnemies;
    
    public TextMeshProUGUI resultText;
    
    private AttackButtonScript attackButtonScript;
    private bool attackButtonPressed;
    
    private SelectEnemyButtonScript selectEnemyButtonScript;
    private CharacterComponent selectedEnemy;
    private CharacterComponent currentCharacter;
    public CharacterComponent CurrentCharacter { get => currentCharacter; }
    private int enemyIndex;

    private Coroutine gameLoop;


    void Awake()
    {
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
        }
 
        instance = this;
    }

    private void Start()
    {
        attackButtonScript = GameObject.FindObjectOfType<AttackButtonScript>();
        attackButtonScript.AttackButtonPressed += AttackEnemy;

        selectEnemyButtonScript = GameObject.FindObjectOfType<SelectEnemyButtonScript>();
        selectEnemyButtonScript.SelectEnemyButtonPressed += SelectEnemy;
        
        resultText.text = " ";
        gameLoop = StartCoroutine(GameLoop());
    }

    private CharacterComponent SelectEnemy(CharacterComponent[] enemies)
    {
        DeselectEnemy();
        
        
        aliveEnemies = enemies.Where(e=> !e.HealthComponent.IsDead).ToArray();
        Debug.Log(aliveEnemies.Length);
        if (aliveEnemies.Length <= 0)
            return null;
        
        enemyIndex = (enemyIndex + 1) % aliveEnemies.Length;
        selectedEnemy = aliveEnemies[enemyIndex];
        selectedEnemy.GetComponent<Outline>().OutlineColor = Color.red;
        selectedEnemy.OutlineCharacterOn();
        Debug.Log(selectedEnemy);
        currentCharacter.SetTarget(selectedEnemy.HealthComponent);
        return selectedEnemy;
    }

    private void DeselectEnemy()
    {
        if(!selectedEnemy)
            return;
        
        selectedEnemy.OutlineCharacterOff();
        selectedEnemy = null;
    }

    void SelectCharacter()
    {
        currentCharacter.GetComponent<Outline>().OutlineColor = Color.green;
        currentCharacter.OutlineCharacterOn();
    }
    
    void DeselectCharacter()
    {
        currentCharacter.OutlineCharacterOff();
    }

    private void AttackEnemy()
    {
        attackButtonPressed = true;
    }

    private IEnumerator GameLoop()
    {
        Coroutine turn = StartCoroutine(Turn(playerCharacters, enemyCharacters));

        yield return new WaitUntil(() =>
        playerCharacters.FirstOrDefault(c => !c.HealthComponent.IsDead) == null ||
        enemyCharacters.FirstOrDefault(c => !c.HealthComponent.IsDead) == null);

        StopCoroutine(turn);
        GameOver();
    }

    private CharacterComponent GetTarget(CharacterComponent[] characterComponents)
    {
        return characterComponents.FirstOrDefault(c => !c.HealthComponent.IsDead);
    }

    private void GameOver()
    {
        bool isPlayerCharacherAlive = false;
        bool isEnemyCharacherAlive = false;

        bool isVictory;

        for (int i = 0; i < playerCharacters.Length; i++)
        {
            if (!playerCharacters[i].HealthComponent.IsDead)
            {
                isPlayerCharacherAlive = true;
            }
        }

        for (int i = 0; i < enemyCharacters.Length; i++)
        {
            if (!enemyCharacters[i].HealthComponent.IsDead)
            {
                isEnemyCharacherAlive = true;
            }
        }

        isVictory = isPlayerCharacherAlive && !isEnemyCharacherAlive;

        Debug.Log(isVictory ? "Victory" : "Defeat");
        resultText.text = isVictory ? "Victory" : "Defeat";
    }

    private IEnumerator Turn(CharacterComponent[] playerCharacters, CharacterComponent[] enemyCharacters)
    {
        int turnCounter = 0;
        while (true)
        {
            for (int i = 0; i < playerCharacters.Length; i++)
            {
                if(playerCharacters[i].HealthComponent.IsDead)
                {
                    Debug.Log("Character: " + playerCharacters[i].name + " is dead");
                    continue;
                }

                currentCharacter = playerCharacters[i];
                SelectCharacter();
                SelectEnemy(enemyCharacters);
                //playerCharacters[i].SetTarget(SelectEnemy(enemyCharacters).HealthComponent);

                //TODO: hotfix
                yield return null; // ugly fix need to investigate
                yield return new WaitUntil(() => attackButtonPressed);
                playerCharacters[i].StartTurn();
                attackButtonPressed = false;
                yield return new WaitUntilCharacterTurn(playerCharacters[i]);
                Debug.Log("Character: " + playerCharacters[i].name + " finished turn");
                DeselectEnemy();
                DeselectCharacter();
            }

            yield return new WaitForSeconds(.5f);

            for (int i = 0; i < enemyCharacters.Length; i++)
            {
                if (enemyCharacters[i].HealthComponent.IsDead)
                {
                    Debug.Log("Enemy character: " + enemyCharacters[i].name + " is dead");
                    continue;
                }
                
                currentCharacter = enemyCharacters[i];
                SelectCharacter();
                SelectEnemy(playerCharacters);
                //enemyCharacters[i].SetTarget(SelectEnemy(playerCharacters).HealthComponent);
                yield return new WaitForSeconds(.5f);
                enemyCharacters[i].StartTurn();
                yield return new WaitUntilCharacterTurn(enemyCharacters[i]);
                Debug.Log("Enemy character: " + enemyCharacters[i].name + " finished turn");
                DeselectEnemy();
                DeselectCharacter();
            }

            yield return new WaitForSeconds(.5f);

            turnCounter++;
            Debug.Log("Turn #" + turnCounter + " has been ended");
        }
    }

    void OnDestroy()
    {
        attackButtonScript.AttackButtonPressed -= AttackEnemy;
        selectEnemyButtonScript.SelectEnemyButtonPressed -= SelectEnemy;
    }
}
