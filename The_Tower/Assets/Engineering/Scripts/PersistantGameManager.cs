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
    public GameObject songs;
    public int songPlaying = -1;

    //player variables
    public float jumpPower = 5;
    public double spd = 1, maxspd = 10;
    public int HP = 3, maxHP = 3, atk = 1;

    [SerializeField]
    private float dJumpPower;
    private double dSpd, dMaxspd;
    private int dHP, dMaxHP, dAtk;

    public void Awake() {
        if (Instance == null)
        {
            SetDefault();
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetDefault()
    {
        dJumpPower = jumpPower;
        dSpd = spd;
        dMaxspd = maxspd;
        dHP = HP;
        dMaxHP = maxHP;
        dAtk = atk;
    }
    public void RestoreDefault()
    {
        jumpPower = dJumpPower;
        print("Hello");
        spd= dSpd;
        maxspd = dMaxspd;
        HP=dHP;
        maxHP= dMaxHP;
        atk=dAtk;
    }

    //-- music player --
    public void playSong(int songIndex) {
        if (songIndex == songPlaying) {
            return;
        }
        else if (songIndex >= 0 && songPlaying == -1) {
            songs.transform.GetChild(songIndex).GetComponent<AudioSource>().Play();
            songPlaying = songIndex;
        }
        else if (songIndex >= 0 && songPlaying >= 0) {
            songs.transform.GetChild(songPlaying).GetComponent<AudioSource>().Stop();
            songs.transform.GetChild(songIndex).GetComponent<AudioSource>().Play();
            songPlaying = songIndex;
        }
        else if (songIndex <= 0) {
            songs.transform.GetChild(songPlaying).GetComponent<AudioSource>().Stop();
            songPlaying = -1;
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
        } 
    }
    public void LoadGame()
    {

        if (File.Exists(Application.persistentDataPath + "/game_save/data.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/data.txt", FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), this);
            file.Close();
        }
    }
    public void NewGame()
    {
        RestoreDefault();
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
    }

    //subscribes to delegate, automaticaly saves game 
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
        //used to autosave.
        //SaveGame();
        //Debug.Log("Level Loaded, Autosave Complete");
    }
}
