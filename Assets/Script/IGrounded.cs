using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrounded
{

    bool GroundCheck( Transform transform, float groundRayDistance, LayerMask whatIsGround )
    {
        bool isGrounded;
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, groundRayDistance, whatIsGround);
        Debug.DrawRay(transform.position, Vector2.down * groundRayDistance, Color.green);
        isGrounded = groundHit;
        
        return isGrounded;
    }
}
