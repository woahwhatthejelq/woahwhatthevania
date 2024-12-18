using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    [SerializeField]
    private GameObject slotsObject;
    [SerializeField]
    private GameObject go;
    [SerializeField]
    private GameObject exit;

    public void Play() {
        slotsObject.SetActive(true);
        ResolveGames();
        go.SetActive(false);
        exit.SetActive(false);
    }

    public void Exit() {
        Application.Quit();
    }

    public void Back() {
        slotsObject.SetActive(false);
        go.SetActive(true);
        exit.SetActive(true);
    }

    private void ResolveGames() {
        for (int i = 0; i < 5; i++) {

            Debug.Log(slotsObject.transform.GetChild(0).GetChild(i).name);

            if (PlayerPrefs.HasKey("gameData" + i.ToString())) {
                GameManager.instance.LoadData("gameData" + i.ToString());

                slotsObject.transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "partid " + i.ToString();
            } else {
                slotsObject.transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Empty Slot";
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
            GameManager.instance.gameData.Mana = 100;
            GameManager.instance.gameData.MaxMana = 100;
            GameManager.instance.gameData.PlayerPos = new Vector3(-0.762f, -1.62f, 0);
            GameManager.instance.gameData.CurrentScene = 1;
            GameManager.instance.gameData.Slot = slot;
        }

        Debug.Log("im tryna enter");

        SceneManager.LoadScene(GameManager.instance.gameData.CurrentScene);
    }
}
