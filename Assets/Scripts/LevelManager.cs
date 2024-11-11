using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Image lifeBar;
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
    public void UpdateLife()
    {
        lifeBar.fillAmount = GameManager.instance.gameData.Life/GameManager.instance.gameData.MaxLife;
    }
}
