using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : CharacterBaseState
{
    public PlayerIdleState(PlayerStateMachine currentCtx, CharacterStateFactory playerStateFactory) : base (currentCtx, playerStateFactory){
        
    }
    
    public override void EnterState()
    {
        Debug.Log("IDLE");

    }

    public override void UpdateState()
    {
        Debug.Log("IM IN IDLE");
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {

    }

    public override void ExitState(){

    }

    public override void CheckSwitchStates(){
        if(Ctx.IsMovePressed)
        {
            SwitchState(Factory.Move());
        }
    }
    public override void InitializeSubState(){
        
    }
}
