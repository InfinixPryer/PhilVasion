using System;
using System.Collections.Generic;

public enum PlayerStatesEnum {
    Walk,
    Idle,
    Move,
    Dash,
    Climb,
    Jump,
    Grounded,
    Fall,
}

public class PlayerStateFactory
{
    PlayerStateMachine context;

    Dictionary<PlayerStatesEnum, PlayerBaseState> playerStates = new Dictionary<PlayerStatesEnum, PlayerBaseState>();

    public PlayerStateFactory(PlayerStateMachine currentContext){
        context = currentContext;
    }

    public PlayerBaseState Grounded(){
        return GetInstance<PlayerGroundedState>(PlayerStatesEnum.Grounded);
    }

    public PlayerBaseState Idle(){
        return GetInstance<PlayerIdleState>(PlayerStatesEnum.Idle);
    }
    
    public PlayerBaseState Jump(){
        return GetInstance<PlayerJumpState>(PlayerStatesEnum.Jump);
    }

    public PlayerBaseState Move(){
        return GetInstance<PlayerMoveState>(PlayerStatesEnum.Move);
    }

    public PlayerBaseState Fall() {
        return GetInstance<PlayerFallState>(PlayerStatesEnum.Fall);
    }

    public PlayerBaseState Climb() {
        return GetInstance<PlayerClimbState>(PlayerStatesEnum.Climb);
    }
    private PlayerBaseState GetInstance<T>(PlayerStatesEnum key) where T: PlayerBaseState {
        if (!playerStates.ContainsKey(key)) {
            playerStates.Add(key, Activator.CreateInstance(typeof(T), new object[] { context, this }) as PlayerBaseState);
        }
        return playerStates[key];
    }
}
