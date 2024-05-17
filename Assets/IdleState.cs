using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class IdleState : State
{
    public AnimationClip animationClip;
    public override void Enter()
    {
        anim.Play("PlayerIdle");
    }
    public override void Do()
    {
        if(!input.grounded || input.xInput != 0){
            isComplete = true;
        }
    }

    public override void Exit()
    {
        
    }
}
