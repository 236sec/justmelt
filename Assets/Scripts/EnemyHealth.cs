using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int initialHP;
    [SerializeField] public int currentHP;
    public MonsterDrop dropManager;

    void Start()
    {
        currentHP = initialHP;    
    }

    void Update()
    {
        CheckDeath();
    }

    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, initialHP);
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
