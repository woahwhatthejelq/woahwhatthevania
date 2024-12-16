using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour {
    [SerializeField]
    private Image lifeBar;
    [SerializeField]
    private Image manaBar;

    private void Start() {
           
    }

    private void Update() {
        UpdateLife();
        UpdateMana();
    }
    public void UpdateLife() {
        lifeBar.fillAmount = GameManager.instance.gameData.Life/GameManager.instance.gameData.MaxLife;
    }

    public void UpdateMana() {
        manaBar.fillAmount = GameManager.instance.gameData.Mana / GameManager.instance.gameData.MaxMana;
    }
}
