using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    // Tag of the bullet GameObject
    public string bulletTag = "Bullet";
    private Rigidbody2D rb;
    // Key to press for parrying
    public KeyCode parryKey = KeyCode.Space;
    void Start () {
        rb = GetComponent<Rigidbody2D>();

        // Freeze rotation along the Z-axis
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the bullet tag and the parry key is pressed
        if (other.CompareTag(bulletTag) && Input.GetKey(parryKey))
        {
            // Perform parry action
            ParryBullet(other.gameObject);
        }
    }

    void ParryBullet(GameObject bullet)
    {
        // Example: Destroy the bullet
        Destroy(bullet);

        // Example: Add force to the sword for visual effect
        Rigidbody2D swordRb = GetComponent<Rigidbody2D>();
        swordRb.AddForce(Vector2.up * 200f); // Adjust force as needed
    }
}
