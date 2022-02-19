using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player, me;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position.Set(player.transform.position.x, transform.position.z, transform.position.z);
        
    }
}