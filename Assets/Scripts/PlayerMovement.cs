using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private PlayerHitBoxController HitBox;
    private IntroScript Cutscenecontroller;
    public Text PlayerScore;
    public float speed;
    public float jumpforce;
    private float movment;
    private bool isGrounded;
    public Transform groundCheck;
    private int groundInt;
    public float checkRadius;
    public LayerMask WhatIsGround;
    private bool isfacingright = false;
    private bool isJumping;
    private bool JumpOnCooldown;
    public float jumpcooldown;
    private float JumpCooldownTimer;
    public bool isattackkingGround;
    private float ScoreTimer;
    private float TimeToWin =10;
    float jumpTimer;
    public float jumpTime;
    Animator anim;
    private float Score;
    public int GroundInt { get  { return groundInt; } }
    public float Timer { get { return ScoreTimer; } }
    public float score { get { return Score; } }
    
     Rigidbody2D rd2d;
    private Vector2 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        ScoreTimer = TimeToWin;
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isJumping = false;
        PlayerScore.text = "";
        GameObject AnotherObject = GameObject.FindWithTag("Intro");
        Cutscenecontroller = AnotherObject.GetComponent<IntroScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
            if (isGrounded)
            {
                groundInt = 1;
            }
            if (!isGrounded)
            {
                groundInt = 0;
            }
            //This is horizontal movement
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, WhatIsGround);
            if (!isGrounded)
                return;
        if (Cutscenecontroller.cutsceneisplaying == true)
        {
            speed = 0;
        }
        


            if (!isattackkingGround)
            {
                movment = Input.GetAxis("Horizontal");
            }

        
            

            rd2d.velocity = new Vector2(movment * speed, rd2d.velocity.y);
        GameObject PlayerObject = GameObject.FindWithTag("LeftHitBox");
        HitBox = PlayerObject.GetComponent<PlayerHitBoxController>();

        PlayerScore.text = HitBox.score.ToString();
            Score = HitBox.score;
       
        

    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Cutscenecontroller.cutsceneisplaying == false)
        {
            ScoreTimer -= Time.deltaTime;
            UIScript.instance.SetValue(ScoreTimer / (float)10);
            if(ScoreTimer<0)
            {
                movment = 0;
                
            }
            if (isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                anim.SetTrigger("Attacking");
                isattackkingGround = true;
                speed =0;
                movment = 0;


            }
            //This is vertical movement
            isJumping = false;
            if (!JumpOnCooldown)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rd2d.velocity = Vector2.up * jumpforce;
                    jumpTimer = jumpTime;
                    JumpCooldownTimer = jumpcooldown;
                    JumpOnCooldown = true;
                    anim.SetInteger("State", 2);
                    
                }
            }
            //This program limits the jump
            if(JumpOnCooldown)
            {
                JumpCooldownTimer -= Time.deltaTime;
                if(JumpCooldownTimer<0)
                { JumpOnCooldown = false; }
            }

            


            //This flips the character
            if (isfacingright == false && movment > 0)
            {
                Flip();
            }
            else if(isfacingright == true && movment < 0)
            {
                Flip();
            }
           //This is for grounded animations
            if (movment > 0 || movment < 0)
            {
                anim.SetInteger("State", 1);
            }
            if (movment == 0)
            {
                anim.SetInteger("State", 0);
            }
            
           
        }
        if (!isGrounded)
        {
            if (!isJumping)
            {
                anim.SetInteger("State", 3);
            }
            
        }
        if(isJumping ==true)
        { anim.SetInteger("State", 2); }
        
        
            
            jumpTimer -=Time.deltaTime;
            if (jumpTimer < 0)
            { isJumping = false; }
            if(jumpTimer > 0)
        { isJumping = true; }

        
            

    }
    void Flip() {
        //This Script flips the character
        isfacingright = !isfacingright;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        
    }
    }
}
