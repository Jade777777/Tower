using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
public class Player : MonoBehaviour
{
    //private variables
    private GameObject me, weapon;
    private Rigidbody body;
    PlayerInput playerInput;
    Animator animator;
    private TheTowerInput playerInputActions;
    private bool left, right, up = false, down, roll, onGround, leftGround, rolling = false, canMove = true, swing, jump;
    float facing = 0, dir = 0;
    double xvel = 0;
    Vector3 preppedVelocity;
    Vector2 movementValue;

    //public variables
    public bool interact, hitstun = false, hittable = true, attacking = false;
    public float swingSecs = 1, swingCDSecs = 2, jumpPower = 5, distanceToGround = .5f;
    public LayerMask whatIsGround;
    public double spd = 1, maxspd = 10;
    public int HP = 3, maxHP = 3, atk = 1;

    //constants
    const int SPDMULTI = 10;


    //obtain the tentative variables
    void Start()
    {
        me = this.gameObject;
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new TheTowerInput();
        playerInputActions.Player.Enable();
    }

    void Update()
    {
        //obtain inputs only if the player can move
        if (canMove)
        {
            dir = playerInputActions.Player.Move.ReadValue<Vector2>().x;
            jump = playerInputActions.Player.Jump.IsPressed();
            swing = playerInputActions.Player.Attack.IsPressed();
            interact = playerInputActions.Player.Interact.IsPressed();
        }
        

        //find direction faced last
        if (dir != 0 && facing != dir)
        {
            facing = dir;
            if (facing < 0) GetComponent<SpriteRenderer>().flipX = true;
            else GetComponent<SpriteRenderer>().flipX = false;
        }
        

        //deceleration
        if (dir == 0 && !rolling) xvel /= 2;
        else if (rolling) xvel /= 1.02;

        //determine if on ground or not
        if (Physics.Raycast(transform.position, Vector3.down, distanceToGround, whatIsGround))
        {
            leftGround = false;
            onGround = true;
        }

        //coyote time
        if (leftGround & !onGround)
        {
            Invoke(nameof(resetCoyo), 1);
        }


        //jump
        if (onGround && jump && !attacking)
        {
            onGround = false;
            body.velocity = new Vector3(body.velocity.x, (float)jumpPower, body.velocity.z);
        }
        
        //if swing, set the animator to swinging
        if (swing && onGround) {
            animator.SetBool("swinging", true);
        }
        else if (!swing) {
            animator.SetBool("swinging", false);
        }

        //calculate movement
        /*
        if (!rolling)
        {
            if (onGround) xvel += dir * spd;
            else xvel += dir * (0.8 * spd);
            if (Math.Abs(xvel) > maxspd) xvel = maxspd * dir;
        }

        if (animator.GetBool("swinging")) {
            xvel /= 2;
        }
        */

        //set animator variables
        animator.SetBool("onground", onGround);
        animator.SetFloat("spd", Mathf.Abs((float)xvel));
        animator.SetInteger("rise", (int)body.velocity.y);
    }

    //weapon collision
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            print("hit!");
            collision.gameObject.GetComponent<BasicEnemyAI>().getHurt(atk);
        }
    }


    //update velocity at a fixed rate
    private void FixedUpdate()
    {
        if (!rolling)
        {
            if (onGround) xvel += dir * spd;
            else xvel += dir * (0.8 * spd);
            if (Math.Abs(xvel) > maxspd) xvel = maxspd * dir;
        }

        if (animator.GetBool("swinging")) {
            xvel /= 2;
        }
        //account for time difference
        body.velocity = new Vector3((float)(xvel * Time.deltaTime * SPDMULTI), body.velocity.y, body.velocity.z);
    }
    
    //function for taking damage
    public void takeDamage(int DMGAmount)
    {
        if (hittable && !hitstun)
        {
            HP -= DMGAmount;
            hitstun = true;
            Invoke(nameof(endHitstun), 2);

            if (HP <= 0) { Application.Quit(); }
        }
        else print("player cannot take damgage");       
    }

    //end the hitstun
    private void endHitstun()
    {
        hitstun = false;
    }

    //reset the coyote time variables
    private void resetCoyo() {
        if (!onGround) {
            leftGround = false;
            onGround = false;
        }
    }
}
