using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject me, weapon;
    private Rigidbody body;
    bool left, right, up, down, roll, onGround, swing, isSwinging, leftGround, rolling,  canMove, swingCD;
    float dir = 0, facing = 0, distanceToGround = .5f;
    double xvel = 0, yvel = 0, finalXvel = 0;
    Vector3 preppedVelocity;

    //public variables
    public bool interact, hitstun = false, hittable = true;
    public float swingSecs = 1, swingCDSecs = 2, jumpPower = 10;
    public LayerMask whatIsGround;
    public double spd = 1, maxspd = 10;
    public int HP = 3, maxHP = 3;

    //counters
    int coyoCounter = 0, rollCounter = 0;

    //constants
    const int SPDMULTI = 10;


    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        me = this.gameObject;
        body = GetComponent<Rigidbody>();
        body.isKinematic = false;
        weapon = me.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
            left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
            up = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space);
            swing = Input.GetKeyDown(KeyCode.F) || Input.GetMouseButton(0);
            //roll = Input.GetKey(KeyCode.LeftShift);
            interact = Input.GetKey(KeyCode.E);
        }

        dir = 0;
        yvel = 0;

        //basic h movement
        if (left)
        {
            dir -= 1;
        }
        if (right)
        {
            dir += 1;
        }

        //find direction faced last
        if (dir != 0 && facing != dir)
        {
            facing = dir;
        }

        //decel
        if (dir == 0 && !rolling) {
            xvel /= 2;
        }
        else if (rolling) xvel /= 1.02;

        if (Physics.Raycast(transform.position, Vector3.down, distanceToGround, whatIsGround))
        {
            leftGround = false;
            onGround = true;
        }

        //coyote time
        if (leftGround)
        {
            if (coyoCounter >= 90 && onGround)
            {
                leftGround = false;
                onGround = false;
                coyoCounter = 0;
                print("no more coyo");
            }
            else if (onGround)
            {
                coyoCounter++;
            } 
            else
            {
                leftGround = false;
                coyoCounter = 0;
            }
        }

        //roll
        if (roll && !rolling && onGround)
        {
            print("rolling");
            rolling = true;
            canMove = false;
            xvel = maxspd * 4 * facing;
        }

        if (rollCounter >= 300 && rolling)
        {
            print("stopped rolling");
            rolling = false;
            canMove = true;
            rollCounter = 0;
        }
        else if (rolling)
        {
            rollCounter++;
        }

        //jump
        if (onGround && up)
        {
            onGround = false;
            yvel += jumpPower;
            print("yump");
            body.velocity = new Vector3(body.velocity.x, (float)yvel, body.velocity.z);
        }

        //swing
        if (!isSwinging && swing && !swingCD)
        {
            isSwinging = true;
            startSwing();
        }
        //movement calculation
        if (!rolling)
        {
            xvel += dir * spd;
            if (!onGround)
            {
                xvel /= 1.2;
            }

            if (Math.Abs(xvel) > maxspd) xvel = maxspd * dir;

            /*if (xvel != 0 && Physics.Raycast(transform.position, Vector3.right * facing, (float)xvel/5, whatIsGround))
            {
                print("ground detect in front, stopping");
                xvel = 0;
            }*/
        }

        //movement application
        finalXvel = xvel;
    }

        public void startSwing()
    {
        print("starting swing");
        weapon.GetComponent<Sword>().setHitboxActive(true);
        weapon.transform.Translate(Vector3.down / 4);
        weapon.transform.Translate(Vector3.right / 4);
        weapon.transform.Rotate(Vector3.forward, 90);
        Invoke(nameof(endSwing), swingSecs);
    }

        public void endSwing()
    {
        print("ending swing");
        isSwinging = false;
        swingCD = true;
        weapon.GetComponent<Sword>().setHitboxActive(false);
        weapon.transform.Rotate(Vector3.back, 90);
        weapon.transform.Translate(Vector3.up / 4);
        weapon.transform.Translate(Vector3.left / 4);
        Invoke(nameof(endSwingCD), 2 * swingSecs);
    }

    private void FixedUpdate()
    {

        //account for time difference
        body.velocity = new Vector3((float)(finalXvel * Time.deltaTime * SPDMULTI), body.velocity.y, body.velocity.z);
    }

    public void endSwingCD()
    {
        print("ending swing cooldown");
        swingCD = false;
    }

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

    private void endHitstun()
    {
        hitstun = false;
    }
}
