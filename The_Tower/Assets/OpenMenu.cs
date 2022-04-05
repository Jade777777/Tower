using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menu;
    void OnMenu()
    {
        menu.SetActive(!menu.activeInHierarchy);
        Time.timeScale = Mathf.Abs(Time.timeScale-1);
    }
}
