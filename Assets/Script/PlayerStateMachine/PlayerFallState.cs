using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine currentCtx, PlayerStateFactory playerStateFactory) : base (currentCtx, playerStateFactory){
        IsRootState = true;
    }
    
    public override void EnterState()
    {
        InitializeSubState();
        Debug.Log("FALL");
        Ctx.Anim.SetBool("isFalling", true);
        Ctx.Rb.gravityScale = Ctx.Rb.gravityScale * Ctx.FallGravity;
    }

    public override void UpdateState()
    {
        // Debug.Log("IM IN FALL");
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        // Ctx.Rb.AddForce(Vector2.down * Ctx.FallGravity);
    }

    public override void ExitState(){
        Ctx.Anim.SetBool("isFalling", false);
        
    }

    public override void CheckSwitchStates(){
        if(Ctx.IsGrounded)
        {
            SwitchState(Factory.Grounded());
            Ctx.Rb.velocity = new Vector2 ( Ctx.Rb.velocity.x, 0f);
            Ctx.Rb.gravityScale = Ctx.GravityScale;
        }
        else if ( Ctx.IsLadderPresent && Ctx.IsClimbPressed)
        {
            SwitchState(Factory.Climb());
        }
    }

    public override void InitializeSubState(){
        if(Ctx.IsMovePressed)
        {
            SetSubState(Factory.Move());
        }
        else if(!Ctx.IsMovePressed)
        {
            SetSubState(Factory.Idle());
        }
    }
}
