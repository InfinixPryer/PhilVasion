using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlankState : CharacterBaseState
{
    public PlayerBlankState(PlayerStateMachine currentCtx, CharacterStateFactory playerStateFactory) : base (currentCtx, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    
    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void ExitState(){
        
    }

    public override void CheckSwitchStates(){
        
    }
    public override void InitializeSubState(){
        
    }
}
