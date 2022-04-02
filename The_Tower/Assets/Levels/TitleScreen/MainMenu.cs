using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void NewGame()
    {
        print("Starting a new game, deleting all save data");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void LoadGame()
    {
        //SaveData.load();
        print("loading previous game.");
    }

    public void ExitGame()
    {
        print("Exiting Game");
        Application.Quit();
    }
}
