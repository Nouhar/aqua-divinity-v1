using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
// https://www.youtube.com/watch?v=0-c3ErDzrh8
public class PlayerMovement : MonoBehaviour{

    State state;

    public AirState airState;
    public IdleState idleState;
    public RunState runState;

    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public Animator anim;
    public float acceleration;
    [Range(0f, 1f)]
    public float groundDecay;
    public float maxXSpeed;
    public bool grounded {get;private set;}
    public float xInput {get;private set;}
    public float yInput {get;private set;}

    bool stateComplete;



    void Start(){
        idleState.Setup(body, anim, this);
        runState.Setup(body, anim, this);
        airState.Setup(body, anim, this);
        state = idleState;
    }
    void Update(){
        CheckInput();
        HandleJump();

        if(state.isComplete){
            SelectState();
        }
        state.Do();
    }
    void FixedUpdate(){
        CheckGround();
        HandleXMovement();
        ApplyFriction();
    }
    
    void SelectState(){
        if(grounded){
            if(xInput == 0){
                state = idleState;
            }
            else{
                state = runState;
            }
        }
        else{
            state = airState;
        }
        state.Enter();
    }

    void CheckInput(){
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    void HandleXMovement(){
        if(Mathf.Abs(xInput) > 0){
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(body.velocity.x + increment, -maxXSpeed, maxXSpeed);
            body.velocity = new Vector2(newSpeed, body.velocity.y);
            FaceInput();
        }
    }

    void FaceInput(){
            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction, 1, 1);
    }

    void HandleJump(){
        if(yInput > 0 && grounded){
            body.velocity = new Vector2(body.velocity.x, airState.jumpSpeed);
        }

    }
    void CheckGround(){
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    void ApplyFriction(){
        if(grounded && xInput == 0 && body.velocity.y <= 0){
           body.velocity *= groundDecay;
        }
    }
}