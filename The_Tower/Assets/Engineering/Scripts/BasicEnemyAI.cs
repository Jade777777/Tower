using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicEnemyAI : MonoBehaviour
{

    public Transform player;
    public Rigidbody body;
    public LayerMask whatIsGround, whatIsPlayer;
    public float hp;

    //patroling
    public Vector3 walkPoint;
    bool walkPointset;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks, spd, maxSpd, distanceToGround = .6f;
    protected bool alreadyAttacked, hitboxActive;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, onGround;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        body = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (Physics.Raycast(transform.position, Vector3.down, distanceToGround, whatIsGround))
        {
            onGround = true;
        }
        else onGround = false;

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }


    private void Patroling()
    {
        if (!walkPointset) SearchWalkPoint();

        if (walkPointset)
        {
            moveTo(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkPoint Reached
        if (distanceToWalkPoint.magnitude < 1f) walkPointset = false;
    }
    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);

        if (Physics.Raycast(walkPoint, -transform.up, distanceToGround, whatIsGround))
        {
            walkPointset = true;
        }
    }

    private void ChasePlayer()
    {
        moveTo(player.position);
    }

    private void AttackPlayer()
    {
        moveTo(transform.position);

        if (!alreadyAttacked)
        {
            //attack code here
            Attack();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    protected virtual void Attack()
    {
        print("enemy attack");
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void moveTo(Vector3 loc)
    {
        float xDir = Math.Sign(loc.x - transform.position.x);
        float zDir = Math.Sign(loc.z - transform.position.z);

        Vector3 destination = new Vector3(spd * xDir, body.velocity.y, spd * zDir);

        body.velocity = destination;
    }

    public void getHurt(float dmg)
    {
        hp -= dmg;

        if (hp < 0)
        {
            Destroy(gameObject);
        }
    }
}
