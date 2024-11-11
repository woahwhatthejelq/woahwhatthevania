using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    [SerializeField]
    private GameObject slotsObject;

    public void Play() {
        slotsObject.SetActive(true);
    }

    public void Exit() {
        Application.Quit();
    }

    public void Back() {
        slotsObject.SetActive(false);
    }

    private void ResolveGames() {
        for (int i = 1; i <= 3; i++) {
            if (PlayerPrefs.HasKey("gameData" + i.ToString())) {
                GameManager.instance.LoadData("gameData" + i.ToString());

                slotsObject.transform.GetChild(0).GetChild(i).GetComponent<TextMeshProUGUI>().text = "partid " + i.ToString();
            }
        }
    }

    public void StartGame(int slot) {
        if (PlayerPrefs.HasKey("gameData" + slot.ToString())) {
            GameManager.instance.LoadData("gameData" + slot.ToString());
        } else {
            GameManager.instance.gameData = new GameData();
            GameManager.instance.gameData.Life = 100;
            GameManager.instance.gameData.MaxLife = 100;
            GameManager.instance.gameData.PlayerPos = new Vector3(-0.762f, -1.62f, 0);
            GameManager.instance.gameData.Slot = slot;
        }
    }
}
