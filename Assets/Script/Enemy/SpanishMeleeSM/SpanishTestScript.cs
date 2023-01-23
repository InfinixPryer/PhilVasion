using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanishTestScript : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    [Header("Movement")]
    [SerializeField] float moveSpeed;
    bool isFacingRight = true;
    [SerializeField] bool isEdgePresent;
    [SerializeField] float edgeDetectLength;
    [SerializeField] float edgeDetectOffSet;
    Vector2 direction = Vector2.right;
    [SerializeField] float waitTime;
    [SerializeField] float waitTimer;
    
    [Header("Grounding")]
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] float groundRayLength;
    bool isGrounded;
    
    [Header("Player Detection")]
    [SerializeField] LayerMask playerLayerMask;


    bool GroundCheck( Vector2 position, float groundRayDistance, LayerMask whatIsGround )
    {
        bool isGrounded;
        RaycastHit2D groundHit = Physics2D.Raycast(position, Vector2.down, groundRayDistance, whatIsGround);
        Debug.DrawRay(position, Vector2.down * groundRayDistance, Color.green);
        isGrounded = groundHit;
        
        return isGrounded;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //CHECK FOR GROUND AND EDGE
        isGrounded = GroundCheck( transform.position, groundRayLength, groundLayerMask);
        isEdgePresent = GroundCheck( new Vector2 (transform.position.x + edgeDetectOffSet, transform.position.y), groundRayLength, groundLayerMask);
        Debug.DrawRay(new Vector2 (transform.position.x + edgeDetectOffSet, transform.position.y), Vector3.down * edgeDetectLength, Color.red);
        
        Patrol();
        Follow();
    }

    void Patrol() {
        if (isGrounded && rb.velocity.y == 0)
        {
            animator.SetBool("isPatrolling", true);
            rb.AddForce(direction * moveSpeed);
            rb.velocity = new Vector2 ( Mathf.Clamp(rb.velocity.x, -moveSpeed, moveSpeed), rb.velocity.y);
        }
        if (!isEdgePresent && (rb.velocity.y <= 0.1 && rb.velocity.y >= -0.1))
        {
            //Wait a few seconds then flip
            if(waitTimer <= waitTime) {
                waitTimer += Time.fixedDeltaTime;
                rb.velocity = Vector2.zero;
                animator.SetBool("isPatrolling", false);
            } else {
                waitTimer = 0;
                Flip();
            }         
        }
    }

    

    void Follow() {
        
    }

    public void Flip()
    {
        rb.velocity = Vector2.zero;
        switch(isFacingRight)
        {
            case false:
            transform.Rotate(0, 180, 0);
            edgeDetectOffSet = -edgeDetectOffSet;
            direction = Vector2.right;
            isFacingRight = true;
            break;

            case true:
            transform.Rotate(0, -180, 0);
            edgeDetectOffSet = -edgeDetectOffSet;
            direction = Vector2.left;
            isFacingRight = false;
            break;
            default: 
        }
    }
}
