using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperty : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

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
    }
}
