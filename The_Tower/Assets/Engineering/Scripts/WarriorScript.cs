using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WarriorScript : BasicEnemyAI
{

    private BoxCollider hitbox;

    protected override void Attack() {
        print("attacking");
        animator.SetBool("Attacking", true);
        Invoke(nameof(endAttack), .1f);
    }

    protected override void Update(){
        base.Update();
        
        if (hitbox == null) {
            hitbox = GetComponent<BoxCollider>();
        }
        if (facing == 1) {
            hitbox.center = new Vector3(Math.Abs(hitbox.center.x) * 1, hitbox.center.y, hitbox.center.z);
        }
        else if (facing == -1) hitbox.center = new Vector3(Math.Abs(hitbox.center.x) * -1, hitbox.center.y, hitbox.center.z);
        
    }

    private void endAttack() {
        animator.SetBool("Attacking", false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("player hit");
            collision.gameObject.GetComponent<Player>().takeDamage(1);

        }
    }
}
