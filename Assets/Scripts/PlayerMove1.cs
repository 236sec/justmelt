using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dodgeSpeed = 25f;
    [SerializeField] public float dodgeCooldown = 5f;

    [SerializeField] private SwordParry swordParry;

    private PlayerHealth playerHealth;

    private float currentDodgeSpeed = 0f;
    public float currentDodgeCooldown = 0f;

    Vector2 moveDirection;

    public Rigidbody2D rb;
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
        if (GameRound.instance.gameOver) return;

        // Move left or right
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            Dodge(dodgeSpeed);
        }

        if (Input.GetMouseButtonDown(0)) {
            swordParry.Parry();
        }
    }

    private void FixedUpdate() {
        if (GameRound.instance.gameOver) return;
        Vector2 dodgeVelocity = currentDodgeSpeed * moveDirection;

        rb.velocity = moveDirection * moveSpeed + dodgeVelocity;
        currentDodgeSpeed = Mathf.Lerp(currentDodgeSpeed, 0, 0.1f);

        if (currentDodgeCooldown > 0) {
            currentDodgeCooldown -= Time.fixedDeltaTime;
        }
        else {
            currentDodgeCooldown = 0;
        }

        if (currentDodgeSpeed >= 0.25f * dodgeSpeed) {
            playerHealth.godMode = true;
        }
        else {
            playerHealth.godMode = false;
        }
    }

    private void Dodge(float speed) {
        if (currentDodgeCooldown > 0) return;
        if (GameRound.instance.gameOver) return;

        currentDodgeSpeed = speed;
        currentDodgeCooldown = dodgeCooldown;
    }
}