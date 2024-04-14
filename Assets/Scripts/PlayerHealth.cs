using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 50;
    public int currentHP;
    //private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        //sprite = GetComponent<SpriteRenderer>();
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BulletTag bTag = other.gameObject.GetComponent<BulletTag>();
        if (bTag != null)
        {
            BulletProperty bProp = other.gameObject.GetComponent<BulletProperty>();
            Debug.Log("Current HP is: " + currentHP);
            TakeDamage(bProp.damage);
            Destroy(other.gameObject);
        }
    }
}
