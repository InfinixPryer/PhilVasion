using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    

    private float timer = 0;

    public PlayerJumpState(PlayerStateMachine currentCtx, PlayerStateFactory playerStateFactory) : base (currentCtx, playerStateFactory){
        IsRootState = true;
    }
    
    public override void EnterState()
    {
        InitializeSubState();
        Ctx.IsJumping = true;
        timer = 0;

        Ctx.Rb.velocity = new Vector2 ( Ctx.Rb.velocity.x, 1 );
        Ctx.Rb.AddForce(Vector2.up * Ctx.InitialJumpForce, ForceMode2D.Impulse);
        Ctx.Anim.SetBool("isJumping", true);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        if (Ctx.IsJumpPressed && timer < Ctx.JumpTime)
        {
            JumpRoutine();
        } else if (!Ctx.IsJumpPressed)
        {
            Ctx.Rb.velocity = new Vector2 ( Ctx.Rb.velocity.x, 0);
            Ctx.IsJumping = false;
        }
    }

    void JumpRoutine()
    {
        float portionCompleted = timer / Ctx.JumpTime;
        Vector2 jumpVector = Vector2.Lerp (Vector2.up * (Ctx.JumpForce * 5), Vector2.zero, portionCompleted);
        Ctx.Rb.AddForce(jumpVector);
        timer += Time.fixedDeltaTime;

        if(timer >= Ctx.JumpTime)
        {
            Ctx.IsJumping = false;
        }
        
    }

    public override void ExitState(){
        Ctx.Anim.SetBool( "isJumping", false);
        timer = 0;
    }

    public override void CheckSwitchStates(){
        if ( Ctx.IsGrounded && !Ctx.IsJumping )
        {
            SwitchState(Factory.Grounded());
        }
        else if ( Ctx.IsLadderPresent && Ctx.IsClimbPressed)
        {
            SwitchState(Factory.Climb());
        }
        else if((Ctx.Rb.velocity.y <= 0 && !Ctx.IsGrounded) )
        {
            SwitchState(Factory.Fall());
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
