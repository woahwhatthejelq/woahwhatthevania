using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameData gameData;

    public int nextSpawnPoint;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    // Start is called before the first frame update
    void Update()
    {
        //Temporal for testing
        if(Input.GetKeyDown(KeyCode.G))
        {
            gameData.PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            SaveData(instance.gameData.Slot);
            Debug.Log("Se ha guardado el progreso");
        }

        if(Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    public void SaveData(int slot)
    {
        string data = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("gameData"+slot.ToString(), data);
    }

    public void LoadData(string slot)
    {
        if(PlayerPrefs.HasKey(slot) == true)
        {
            string data = PlayerPrefs.GetString(slot);
            gameData = JsonUtility.FromJson<GameData>(data);
        }
        else
        {
            gameData = new GameData();
            gameData.Life = 100;
            gameData.MaxLife = 100;
            gameData.PlayerPos = new Vector3(-0.762f, -1.62f, 0);
            gameData.Slot = 1;
        }
        
    }
    // Update is called once per frame
   
}
