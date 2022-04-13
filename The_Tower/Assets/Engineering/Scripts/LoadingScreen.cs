using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public void loadLevel (int sceneIndex) {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync (int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (operation.isDone == false) {
            Debug.Log (operation.progress);

            yield return null;
        }
    }
}
