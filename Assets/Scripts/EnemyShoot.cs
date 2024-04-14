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
            GameObject bulletObject = Instantiate(bullet, transform.position, transform.rotation);
            BulletProperty bulletProperty = bulletObject.GetComponent<BulletProperty>();

            bulletProperty.SetVelocity(bulletProperty.speed * Vector2.down + Vector2.left * 5f);

            attackCooldown = initialAttackCooldown;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }
}
