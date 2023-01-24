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
        if(Ctx.MovementInput.x < 0 && Ctx.FacingRight){
            Flip();
        }else if (Ctx.MovementInput.x > 0 && !Ctx.FacingRight){
            Flip();
        }
        
        float targetSpeed = Ctx.MovementInput.x * Ctx.MoveSpeed; // calculate movement direction and speed
        float speedDif = targetSpeed - Ctx.Rb.velocity.x; //Difference of target speed and current speed
        float accelerationRate = ( Mathf.Abs(targetSpeed) > 0.01f ? Ctx.Acceleration : Ctx.Deceleration ); //change between acceleration and deceleration based on target speed
        float movement = Mathf.Pow( Mathf.Abs(speedDif) * accelerationRate, Ctx.VelPower) * Mathf.Sign(speedDif);

        Ctx.Rb.AddForce(movement * Vector2.right);
    }

    public override void ExitState(){
        Ctx.Anim.SetBool("isMoving", false);
        moveTimer = 0;
    }

    public override void CheckSwitchStates(){
        if(!Ctx.IsMovePressed & moveTimer >= Ctx.SecondsTilIdle)
        {
            SwitchState(Factory.Idle());
            Ctx.Rb.velocity = new Vector2 ( 0, Ctx.Rb.velocity.y);
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
