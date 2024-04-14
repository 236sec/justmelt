using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParry : MonoBehaviour
{
    public GameObject reflectedBullet; 

    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BulletTag bTag = other.gameObject.GetComponent<BulletTag>();
        BulletProperty bProp = other.gameObject.GetComponent<BulletProperty>();
        if (bTag != null)   
        {
            Debug.Log("Sword Block");
            GameObject reflectedB = Instantiate(reflectedBullet, transform.position, transform.rotation);
            // Rigidbody2D reflectedRb = reflectedB.GetComponent<Rigidbody2D>();
            // reflectedRb.velocity = Vector2.left * bProp.speed;
            Destroy(other.gameObject);
        }
    }
}
