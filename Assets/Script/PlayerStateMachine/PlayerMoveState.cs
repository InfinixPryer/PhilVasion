using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    float moveTimer = 0;
    public PlayerMoveState(PlayerStateMachine currentCtx, PlayerStateFactory playerStateFactory) : base (currentCtx, playerStateFactory){

    }
    
    public override void EnterState()
    {
        Debug.Log("MOVE");
        Ctx.Anim.SetBool( "isMoving", true);
        Ctx.IsMoving = true;
    }

    public override void UpdateState()
    {
        // Debug.Log("CHECKING MOVE UPDATE");
        if(!Ctx.IsMovePressed)
        {
            moveTimer += Time.deltaTime;
        }
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        // Debug.Log("MOOOOOOOOOOOVIIIIIIIIIIIIINGGGGGGGGGG");

        if(Ctx.MovementInput.x < 0 && Ctx.FacingRight){
            Flip();
        }else if (Ctx.MovementInput.x > 0 && !Ctx.FacingRight){
            Flip();
        }
        Ctx.Rb.AddForce( new Vector2 ( Ctx.MovementInput.x, 0) * Ctx.MoveSpeed , ForceMode2D.Force);
        Ctx.Rb.velocity = new Vector2 ( Mathf.Clamp(Ctx.Rb.velocity.x, -Ctx.MoveSpeed, Ctx.MoveSpeed),Ctx.Rb.velocity.y );
    }

    public override void ExitState(){
        Ctx.Anim.SetBool("isMoving", false);
        moveTimer = 0;
    }

    public override void CheckSwitchStates(){
        if(!Ctx.IsMovePressed & moveTimer >= Ctx.SecondsTilIdle)
        {
            SwitchState(Factory.Idle());
            // Ctx.Rb.velocity = Vector2.zero;
        }
    }

    public override void InitializeSubState(){
        
    }

    public void Flip()
    {
        Ctx.FacingRight = !Ctx.FacingRight;
        Vector3 localScale = Ctx.transform.localScale;
        localScale.x *= -1f;
        Ctx.transform.localScale = localScale;

        // Ctx.transform.Rotate(0, 180, 0);
    }
}
