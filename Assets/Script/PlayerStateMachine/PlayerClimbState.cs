using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : CharacterBaseState
{
    
    public PlayerClimbState(PlayerStateMachine currentCtx, CharacterStateFactory playerStateFactory) : base (currentCtx, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    
    public override void EnterState()
    {
        Ctx.IsOnLadder = true;
        Ctx.Rb.velocity = Vector2.zero;
        Debug.Log("CLIMB");
        Ctx.Anim.SetBool("isClimbing", true);
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
