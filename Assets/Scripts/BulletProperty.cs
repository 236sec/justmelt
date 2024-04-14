using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperty : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector2 vel) {
        rb.velocity = vel;
    }
    
    public Vector2 GetVelocity() {
        return rb.velocity;
    }
}
