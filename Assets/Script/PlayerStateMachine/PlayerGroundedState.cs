using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentCtx, PlayerStateFactory playerStateFactory) : base (currentCtx, playerStateFactory){
        IsRootState = true;
        
    }
    
    public override void EnterState()
    {
        InitializeSubState();
        Debug.Log("GROUNDED");
    }

    public override void UpdateState()
    {
        // Debug.Log("IM IN GROUNDED");
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void ExitState(){
        
    }

    public override void CheckSwitchStates(){
        if(Ctx.IsJumpPressed && Ctx.IsGrounded )
        {
            // Debug.Log("STUCK TO JUMP");
            SwitchState(Factory.Jump());
        }
        else if ( !Ctx.IsGrounded && !Ctx.IsJumpPressed)
        {
            // Debug.Log("STUCK TO FALL");
            SwitchState(Factory.Fall());
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
