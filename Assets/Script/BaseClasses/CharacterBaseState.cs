using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseState
{
    private bool isRootState = false;
    private PlayerStateMachine baseCtx;
    private CharacterStateFactory baseFactory;
    private CharacterBaseState currentSubState;
    private CharacterBaseState currentSuperState;

    protected bool IsRootState {set { isRootState = value;} }
    protected PlayerStateMachine Ctx {get { return baseCtx;} }
    protected CharacterStateFactory Factory {get { return baseFactory; } }

    public CharacterBaseState(PlayerStateMachine currentCtx, CharacterStateFactory characterStateFactory){
        baseCtx = currentCtx;
        baseFactory = characterStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();

    public void UpdateStates(){
        UpdateState();
        if(currentSubState != null){
            currentSubState.UpdateStates();
        }
    }

    public void FixedUpdateStates(){
        FixedUpdateState();
        if(currentSubState != null){
            currentSubState.FixedUpdateStates();
        }
    }

    protected void SwitchState(CharacterBaseState newState){
        //current state exits
        ExitState();

        
        //new state enters
        newState.EnterState();

        //switch current state
        // baseCtx.CurrentState = newState;
        if(isRootState){
            baseCtx.CurrentState = newState;    
        } else if ( currentSuperState != null){
            currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(CharacterBaseState newSuperState){
        currentSuperState = newSuperState;
    }
    
    protected void SetSubState(CharacterBaseState newSubState){
        currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
    
}
