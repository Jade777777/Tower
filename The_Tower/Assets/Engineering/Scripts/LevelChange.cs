using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChange : MonoBehaviour
{
    public int targetSceneID;
    public GameObject loadingScreen;
    public Slider slider;

    void Start(){
        loadingScreen = GameObject.Find("UI").transform.GetChild(3).gameObject;
        slider = loadingScreen.transform.GetChild(0).GetComponent<Slider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //update singleton
            collision.gameObject.GetComponent<Player>().updateSingleton();

            loadLevel(targetSceneID);
        }
    }
    public void loadLevel (int sceneIndex) {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync (int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (operation.isDone == false) {
            float progress = Mathf.Clamp01(operation.progress / 9f);

            slider.value = operation.progress;

            yield return null;
        }
    }
    
}
