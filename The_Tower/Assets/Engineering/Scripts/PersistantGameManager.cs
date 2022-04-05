using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class PersistantGameManager : MonoBehaviour
{
    public static PersistantGameManager Instance { get; private set; }

    public int currentLevel;

    //player variables
    public float jumpPower = 5;
    public double spd = 1, maxspd = 10;
    public int HP = 3, maxHP = 3, atk = 1;


    public void Awake() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }




    //--------------------------------------Save System-------------------------------------

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }
    public void SaveGame()
    {
        if (SceneManager.GetSceneByBuildIndex(0)!=SceneManager.GetActiveScene()) {
            if (!IsSaveFile())
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
                print(IsSaveFile());
            }
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Create(Application.persistentDataPath + "/game_save/data.txt");
            var json = JsonUtility.ToJson(this);
            bf.Serialize(file, json);
            file.Close();
            print("gameSaved");
        } 
    }
    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(Application.persistentDataPath + "/game_save/data.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/data.txt",FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), this);
            file.Close();
        }
    }

//subscribes to delegate  and automaticaly saves game 
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SaveGame();
        Debug.Log("Level Loaded, Autosave Complete");
    }
}
