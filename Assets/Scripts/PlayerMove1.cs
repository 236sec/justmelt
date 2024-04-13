using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private float moveDirection = 0f;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    private enum MovementState {idle,running,jumping,falling}

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        // Freeze rotation along the Z-axis
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        // Move left or right
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput).normalized;
        rb.velocity = movementDirection * moveSpeed;
        Invoke("IsGrounded",0);
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        // Flip sprite if moving in opposite direction
        // Return whether character is running or not
        if (moveDirection > 0)
        {
            state = MovementState.running;
            sprite.flipX = false;
            transform.GetChild(0).position = transform.position + new Vector3(1f,-.55f, 0); // move child back to default orientation
        }
        else if (moveDirection < 0)
        {
            state = MovementState.running;
            sprite.flipX = true;
            transform.GetChild(0).position = transform.position + new Vector3(-1f,-.55f, 0); // move child to match parent's orientation
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        // Update animator
        anim.SetInteger("state",(int)state);

    }

    private bool IsGrounded()   
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down,.1f);
        if (hit.collider != null) 
        {   
            return true;
        }
        return false;
    }

}