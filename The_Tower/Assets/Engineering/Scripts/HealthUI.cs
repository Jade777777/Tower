using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    public GameObject player;
    public Text text;
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else {
       text.text = player.GetComponent<Player>().HP.ToString();
        }
    }
}
