using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] private float initialAttackCooldown;
    private float attackCooldown;

    void Start()
    {
        attackCooldown = initialAttackCooldown;    
    }

    void Update()
    {
        if(attackCooldown <= 0)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            attackCooldown = initialAttackCooldown;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }
}
