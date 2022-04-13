using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpit : MonoBehaviour
{
    private Rigidbody body;
    public int spitDamage = 1;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("player hit");
            collision.gameObject.GetComponent<Player>().takeDamage(spitDamage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.layer == 6) {
            Destroy(this.gameObject);
        }
    }

}
