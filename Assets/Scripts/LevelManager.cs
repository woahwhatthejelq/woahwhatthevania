using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Image lifeBar;
    [SerializeField]
    private Image manaBar;
    [SerializeField]
    private Transform[] spawnPoints;

    private void Start()
    {

        GameObject.FindGameObjectWithTag("Player").transform.position =
            spawnPoints[GameManager.instance.nextSpawnPoint].position;
        GameObject.FindGameObjectWithTag("Player").transform.rotation =
            spawnPoints[GameManager.instance.nextSpawnPoint].rotation;
        UpdateLife();
        
    }

    private void Update() {
        UpdateLife();
        UpdateMana();
    }
    public void UpdateLife()
    {
        lifeBar.fillAmount = GameManager.instance.gameData.Life/GameManager.instance.gameData.MaxLife;
    }

    public void UpdateMana() {
        manaBar.fillAmount = GameManager.instance.gameData.Mana / GameManager.instance.gameData.MaxMana;
    }
}
