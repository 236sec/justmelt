using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperty : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerTag pTag = other.gameObject.GetComponent<PlayerTag>();
        if (pTag != null)
        {
            PlayerHealth playerHP = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHP != null)
            {
                Debug.Log("Current HP is: " + playerHP.currentHP); // Concatenate the health value with the log message
                playerHP.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        SwordTag sTag = other.gameObject.GetComponent<SwordTag>();
        if (sTag != null)
        {
            Debug.Log("Sword Block");
            Vector2 reflect = new Vector2(-rb.velocity.x, -rb.velocity.y);
            rb.velocity = reflect * speed;
        }
    }
}
