using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlimeScript : BasicEnemyAI
{

    public int jumpHeight = 5;

    protected override void Attack()
    {
        print("slime attack");

        float xDir = Math.Sign(player.gameObject.transform.position.x - transform.position.x);
        hitboxActive = true;

        body.velocity = new Vector3((player.gameObject.transform.position.x - transform.position.x), jumpHeight, body.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitboxActive && collision.gameObject.tag == "Player")
        {
            print("player hit");
            collision.gameObject.GetComponent<Player>().takeDamage(1);
        }
        if (hitboxActive && collision.gameObject.tag == "Ground")
        {
            hitboxActive = false;
        }
    }

}
