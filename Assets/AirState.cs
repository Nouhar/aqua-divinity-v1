using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class AirState : State
{
    public AnimationClip animationClip;

    public float jumpSpeed;


    public override void Enter()
    {
        anim.Play("PlayerJumpUp");
    }
    public override void Do()
    {
        float time = Helpers.Map(body.velocity.y, jumpSpeed, -jumpSpeed, 0, 1, true);
        anim.Play("PlayerJump", 0, time);
        anim.speed = 0;

        if(input.grounded){
            isComplete = true;
        }
    }

    public override void Exit()
    {
        
    }

}
