using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    // GETTERS AND SETTERS
    public CharacterBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public bool IsMovePressed { get { return isMovePressed; } set { isMovePressed = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public Vector2 MovementInput { get { return movementInput; } }
    public Rigidbody2D Rb { get { return rb; } }
    public Animator Anim {get { return anim; } }
    public bool IsJumpPressed { get { return isJumpPressed; } set { isJumpPressed = value; } }
    public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }
    public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } }
    public bool FacingRight { get { return facingRight; } set { facingRight = value; } }
    public bool IsFalling { get { return isFalling;} set { isFalling = value;} }
    public bool IsJumping { get { return isJumping; } set { isJumping = value; } }
    public float JumpTime { get { return jumpTime; } }
    public float InitialJumpForce { get { return initialJumpForce; } }
    public float SecondsTilIdle { get { return secondsTilIdle;} set { secondsTilIdle = value; } }
    public bool IsMoving { get { return isMoving; } set { isMoving = value; } }
    public float FallGravity { get { return fallGravity; } set { fallGravity = value;} }
    public bool IsLadderPresent { get { return isLadderPresent; } set { isLadderPresent = value; } }
    public bool IsClimbPressed { get { return isClimbPressed; } set { isClimbPressed = value; } }
    public float ClimbForce { get { return climbForce; } set { climbForce = value; } }
    public bool IsOnLadder { get { return isOnLadder; } set { isOnLadder = value; } }


    CharacterBaseState currentState;
    CharacterStateFactory states;

    Rigidbody2D rb;
    Animator anim;
    InputActions inputActions;

    bool isFalling;
    bool isJumping;
    bool isMoving;

    //GRAVITY Variables
    float gravity;
    float groundedGravity;
    [SerializeField] float fallGravity;

    //MOVEMENT Variables
    bool isMovePressed;
    [SerializeField]float moveSpeed;
    Vector2 movementInput;
    bool facingRight = true;
    float secondsTilIdle = 0.15f;

    //JUMP Variables
    bool isJumpPressed = false;
    [SerializeField] float initialJumpForce;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTime;

    //GROUNDING Variables
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] bool isGrounded;
    [SerializeField] float groundRayDistance;
    
    //CLIMBING Variables
    [SerializeField] LayerMask ladderLayerMask;
    [SerializeField] float ladderDetectRadius;
    [SerializeField] string stateName;
    Vector2 ladderDetectPos;
    bool isLadderPresent = false;
    bool isClimbPressed = false;
    [SerializeField] float climbForce;
    bool isOnLadder = false;

    void Awake() {
        states = new CharacterStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        inputActions = new InputActions();

        inputActions.Character.Move.started += OnMove;
        inputActions.Character.Move.performed += OnMove;
        inputActions.Character.Move.canceled += OnMove;

        inputActions.Character.Jump.started += OnJump;
        inputActions.Character.Jump.canceled += OnJump;

        inputActions.Character.Climb.started += OnClimb;
        inputActions.Character.Climb.canceled += OnClimb;
    }

    void OnJump ( InputAction.CallbackContext ctx)
    {
        isJumpPressed = ctx.ReadValueAsButton();
    }

    void OnMove ( InputAction.CallbackContext ctx ){
        movementInput = ctx.ReadValue<Vector2>();
        isMovePressed = movementInput.x != 0 || movementInput.y != 0;
        // Debug.Log(isMovePressed);
    }

    void OnClimb (InputAction.CallbackContext ctx ) {
        IsClimbPressed = ctx.ReadValueAsButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stateName = currentState.ToString();
        currentState.UpdateStates();
        
    }

    void GroundCheck()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, groundRayDistance, whatIsGround);
        Debug.DrawRay(transform.position, Vector2.down * groundRayDistance, Color.green);
        isGrounded = groundHit;
        
    }

    void LadderCheck()
    {
        ladderDetectPos = new Vector2 (transform.position.x, transform.position.y + 0.5f);
        Collider2D ladderCollider = Physics2D.OverlapCircle( ladderDetectPos, ladderDetectRadius, ladderLayerMask);
        isLadderPresent = ladderCollider;
    }

    void FixedUpdate()
    {
        LadderCheck();
        GroundCheck();
        currentState.FixedUpdateStates();
    }

    private void OnEnable() {
        inputActions.Enable();    
    }

    void OnDisable() {
        inputActions.Disable();    
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(ladderDetectPos, ladderDetectRadius);
    }
}
