using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dodgeSpeed = 25f;

    private PlayerHealth playerHealth;

    private float currentDodgeSpeed = 0f;

    Vector2 moveDirection;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;

    private enum MovementState {idle,running,jumping,falling}

    private void Awake() {
        playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        // Move left or right
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            Debug.Log(currentDodgeSpeed);
            Dodge(dodgeSpeed);
        }
    }

    private void FixedUpdate() {
        Vector2 dodgeVelocity = currentDodgeSpeed * moveDirection;

        rb.velocity = moveDirection * moveSpeed + dodgeVelocity;
        currentDodgeSpeed = Mathf.Lerp(currentDodgeSpeed, 0, 0.1f);

        if (currentDodgeSpeed >= 0.25f * dodgeSpeed) {
            playerHealth.godMode = true;
        }
        else {
            playerHealth.godMode = false;
        }
    }

    private void Dodge(float speed) {
        currentDodgeSpeed = speed;
        // rb.AddForce(direction * dodgeSpeed, ForceMode2D.Impulse);
    }
}