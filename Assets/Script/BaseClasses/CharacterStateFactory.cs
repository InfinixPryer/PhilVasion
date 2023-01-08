using System;
using System.Collections.Generic;

public enum CharacterStatesEnum {
    Walk,
    Idle,
    Move,
    Dash,
    Climb,
    Jump,
    Grounded,
    Fall,
}

public class CharacterStateFactory
{
    PlayerStateMachine context;

    Dictionary<CharacterStatesEnum, CharacterBaseState> playerStates = new Dictionary<CharacterStatesEnum, CharacterBaseState>();
    // Dictionary<string, PlayerBaseState>

    public CharacterStateFactory(PlayerStateMachine currentContext){
        context = currentContext;
    }

    public CharacterBaseState Grounded(){
        return GetInstance<PlayerGroundedState>(CharacterStatesEnum.Grounded);
    }

    public CharacterBaseState Idle(){
        return GetInstance<PlayerIdleState>(CharacterStatesEnum.Idle);
    }
    
    public CharacterBaseState Jump(){
        return GetInstance<PlayerJumpState>(CharacterStatesEnum.Jump);
    }

    public CharacterBaseState Move(){
        return GetInstance<PlayerMoveState>(CharacterStatesEnum.Move);
    }

    public CharacterBaseState Fall() {
        return GetInstance<PlayerFallState>(CharacterStatesEnum.Fall);
    }

    public CharacterBaseState Climb() {
        return GetInstance<PlayerClimbState>(CharacterStatesEnum.Climb);
    }
    private CharacterBaseState GetInstance<T>(CharacterStatesEnum key) where T: CharacterBaseState {
        if (!playerStates.ContainsKey(key)) {
            playerStates.Add(key, Activator.CreateInstance(typeof(T), new object[] { context, this }) as CharacterBaseState);
        }
        return playerStates[key];
    }
}
