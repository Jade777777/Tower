using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool hitboxActive;
    public float atk = 1;
    private Rigidbody body;

    private void OnTriggerEnter(Collider collision)
    {
        if (hitboxActive && collision.gameObject.tag == "Enemy")
        {
            print("hit!");
            collision.gameObject.GetComponent<BasicEnemyAI>().getHurt(atk);
        }
    }

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHitboxActive(bool state)
    {
        hitboxActive = state;
    }
}
