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
    private bool left, right, up = false, down, roll, onGround, swing, isSwinging, leftGround, rolling = false, canMove = true, swingCD, jump;
    float facing = 0, distanceToGround = .5f, dir = 0;
    double xvel = 0, finalXvel = 0;
    Vector3 preppedVelocity;
    Vector2 movementValue;

    //public variables
    public bool interact, hitstun = false, hittable = true;
    public float swingSecs = 1, swingCDSecs = 2, jumpPower = 5;
    public LayerMask whatIsGround;
    public double spd = 1, maxspd = 10;
    public int HP = 3, maxHP = 3;

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
        if (onGround && jump)
        {
            print("might as well jump");
            onGround = false;
            body.velocity = new Vector3(body.velocity.x, (float)jumpPower, body.velocity.z);
        }
        else print(onGround + " " + jump);
        
        //calculate movement
        if (!rolling)
        {
            if (onGround) xvel += dir * spd;
            else xvel += dir * (0.8 * spd);
            if (Math.Abs(xvel) > maxspd) xvel = maxspd * dir;
        }
        animator.SetFloat("spd", Mathf.Abs((float)xvel));
        //prep xvel for fixed update
        finalXvel = xvel;
    }

    //update velocity at a fixed rate
    private void FixedUpdate()
    {
        //account for time difference
        body.velocity = new Vector3((float)(finalXvel * Time.deltaTime * SPDMULTI), body.velocity.y, body.velocity.z);
    }
    
    private void resetCoyo() {
        if (!onGround) {
            leftGround = false;
            onGround = false;
        }
    }
}
