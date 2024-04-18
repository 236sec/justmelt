using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int initialHP;
    [SerializeField] public int currentHP;

    void Start()
    {
        currentHP = initialHP;    
    }

    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, initialHP);
    }
}
