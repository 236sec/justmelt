using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int initialHP;
    [SerializeField] private int currentHP;
    public MonsterDrop dropManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = initialHP;    
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
    }

    void TakeDamage(int damage)
    {
        currentHP -= damage;
    }

    void CheckDeath()
    {
        if(currentHP <= 0)
        {
            Destroy(gameObject);
            dropManager.DropLoot();
        }
    }
}
