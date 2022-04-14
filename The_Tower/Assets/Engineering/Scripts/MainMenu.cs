using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    PersistantGameManager pData;
    private void Start()
    {
        pData = FindObjectOfType<PersistantGameManager>();
        PersistantGameManager.Instance.playSong(0);
    }
    public void NewGame()
    {
        pData.NewGame();
        SceneManager.LoadScene(1);
    }
    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//this loads the next level in the build index, place anywhere you need to load another level.
        SceneManager.LoadScene(1);
    }


    public void ExitGame()
    {
        Application.Quit();

    }
}
