using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menu;
    void OnMenu()
    {
        menu.SetActive(!menu.activeInHierarchy);
        Time.timeScale = Mathf.Abs(Time.timeScale-1);
    }
    void OnEnable()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    void OnDisable()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

}
