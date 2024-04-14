using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int initialHP;
    [SerializeField] private int currentHP;
    public MonsterDrop dropManager;

    void Start()
    {
        currentHP = initialHP;    
    }

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
