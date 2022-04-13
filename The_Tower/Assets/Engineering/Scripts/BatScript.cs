using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : BasicEnemyAI
{
    public GameObject projectile;
    public float pForceH = 5, pForceV = 3;

    protected override void Attack()
    {
        Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.right * pForceH * -1, ForceMode.Impulse);
        rb.AddForce(transform.up * pForceV, ForceMode.Impulse);
    }
}
