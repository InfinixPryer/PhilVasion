using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : PlayerBaseState
{
    
    public PlayerClimbState(PlayerStateMachine currentCtx, PlayerStateFactory playerStateFactory) : base (currentCtx, playerStateFactory){
        IsRootState = true;
        
    }
    
    public override void EnterState()
    {
        InitializeSubState();
        Ctx.IsOnLadder = true;
        Ctx.Rb.velocity = Vector2.zero;
        Debug.Log("CLIMB");
        Ctx.Anim.SetBool("isClimbing", true);
        Ctx.Anim.SetBool("isMoving", false);
        Ctx.Rb.gravityScale = Ctx.GravityScale;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        if(Ctx.IsClimbPressed)
        {
            Ctx.Rb.AddForce(Vector2.up * Ctx.ClimbForce);
        }
    }

    public override void ExitState(){
        Ctx.Anim.SetBool("isClimbing", false);
    }

    public override void CheckSwitchStates(){
        if (!Ctx.IsLadderPresent)
        {
            SwitchState(Factory.Fall());
        }if(!Ctx.IsClimbPressed)
        {
            SwitchState(Factory.Fall());
        }
        else if (!Ctx.IsClimbPressed && Ctx.IsGrounded)
        {
            SwitchState(Factory.Grounded());
        }
    }

    public override void InitializeSubState(){
        
    }
}
