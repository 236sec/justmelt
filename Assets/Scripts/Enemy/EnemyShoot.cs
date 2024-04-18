using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BulletPatterns;

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
            int[] angles = { -45, 0, 45 };

            for (int i = 0; i < 3; i++) {
                GameObject bulletObject = Instantiate(bullet, transform.position, transform.rotation);
                BulletProperty bulletProperty = bulletObject.GetComponent<BulletProperty>();

                Vector2 direction = Quaternion.Euler(0, 0, angles[i]) * Vector2.down;
                bulletProperty.SetVelocity(direction * bulletProperty.speed);
            }

            /*
            NormalBulletPattern normalBulletPattern = new (bulletProperty);
            bulletObject.AddComponent<NormalBulletPattern>();
            */

            attackCooldown = initialAttackCooldown;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }
}
