using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dodgeSpeed = 25f;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;

    private enum MovementState {idle,running,jumping,falling}

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
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

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            Debug.Log(movementDirection);
            Dodge(movementDirection);
        }
    }

    private void Dodge(Vector2 direction) {
        rb.AddForce(direction * dodgeSpeed, ForceMode2D.Impulse);
    }
}