using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RunState : State
{
    public AnimationClip animationClip;
    public override void Enter()
    {
        anim.Play("PlayerMove");
    }
    public override void Do()
    {
        float velX = body.velocity.x;
        anim.speed = Helpers.Map(input.maxXSpeed, 0, 1, 0, 1.6f, true);

        if(!input.grounded || Mathf.Abs(velX) < 0.1f){
            isComplete = true;
        }
    }

    public override void Exit()
    {
        
    }
}
