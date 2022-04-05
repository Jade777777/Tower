using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}