using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP = 50;

    public bool godMode = false;
    
    void Start()
    {
        currentHP = maxHP;
    }

    private void CheckDeath() { 
        if (currentHP == 0) {
            
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
        CheckDeath();
    }
}
