using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletTag bTag = other.gameObject.GetComponent<BulletTag>();
        if (bTag != null)
        {
            Debug.Log("Sword Block");
            // Instantiate the bulletFriend
            GameObject bullet = Instantiate(bulletFriend, transform.position, transform.rotation);
            // Get the Rigidbody2D component of the bullet
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            // Calculate the reflected velocity
            Vector2 reflect = -Vector2.up * speed;
            // Set the velocity of the bullet
            bulletRb.velocity = reflect;

            // Destroy the original bullet GameObject
            Destroy(other.gameObject);
        }
    }
}
