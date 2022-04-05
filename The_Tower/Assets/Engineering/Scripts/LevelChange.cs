using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public int targetSceneID;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //update singleton
            collision.gameObject.GetComponent<Player>().updateSingleton();

            SceneManager.LoadScene(targetSceneID);
        }
    }
}
