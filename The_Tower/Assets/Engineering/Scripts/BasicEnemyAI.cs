using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BasicEnemyAI : MonoBehaviour
{

    protected Transform player;
    public GameObject sounds;
    public Rigidbody body;
    public LayerMask whatIsGround, whatIsPlayer;
    public float hp;
    protected Animator animator;
    protected int dir = 0, facing = -1;
    protected SpriteRenderer sprite;

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
        player = GameObject.FindGameObjectWithTag("Player").transform;
        body = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
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


    protected void Patroling()
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

    protected void ChasePlayer()
    {
        moveTo(player.position);
    }

    protected void AttackPlayer()
    {
        moveTo(transform.position);

        float playerDir = Math.Sign(player.position.x - transform.position.x);
        int SpriteDir = Math.Sign(playerDir);
        if (SpriteDir != 0 || SpriteDir != facing) {
            if (SpriteDir == 1) {
                facing = 1;
                sprite.flipX = true;
            }
            else if (SpriteDir == -1) {
                sprite.flipX = false;
                facing = -1;
            }
        }

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

    protected void ResetAttack()
    {
        alreadyAttacked = false;
    }

    protected void moveTo(Vector3 loc)
    {
        float xDir = Math.Sign(loc.x - transform.position.x);
        float zDir = Math.Sign(loc.z - transform.position.z);

        int SpriteDir = Math.Sign(xDir);
        if (SpriteDir != 0 || SpriteDir != facing) {
            if (SpriteDir == 1) {
                facing = 1;
                sprite.flipX = true;
            }
            else if (SpriteDir == -1) {
                sprite.flipX = false;
                facing = -1;
            }
        }

        Vector3 destination = new Vector3(spd * xDir, body.velocity.y, spd * zDir);

        body.velocity = destination;
    }

    public void getHurt(float dmg)
    {
        hp -= dmg;

        if (hp < 0)
        {
            getSound(1).Play();
            Destroy(gameObject);
        }
        else {
            getSound(0).Play();
        }
    }

    protected AudioSource getSound(int repositoryIndex) {
        GameObject soundFolder = sounds.transform.GetChild(repositoryIndex).gameObject;
        return soundFolder.transform.GetChild(UnityEngine.Random.Range(0,soundFolder.transform.childCount)).gameObject.GetComponent<AudioSource>();
    }
}
