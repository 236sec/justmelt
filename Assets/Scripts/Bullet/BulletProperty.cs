using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperty : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifetime = 10f;
    
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        StartCoroutine(Cleanup());
    }

    public void SetVelocity(Vector2 vel) {
        rb.velocity = vel;
    }
    
    public Vector2 GetVelocity() {
        return rb.velocity;
    }

    private IEnumerator Cleanup() {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<PlayerTag>() is null) return;
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health.godMode) return;

        health.TakeDamage(damage);
        // Debug.Log("Damaged player: current health is " + health.currentHP.ToString());
        Destroy(gameObject);
    }
}
