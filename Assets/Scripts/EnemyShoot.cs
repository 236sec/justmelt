using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BulletPatterns;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;

    bool shooting = false;
    [SerializeField] private float initialAttackCooldown;
    private float attackCooldown;

    private float angleOffset = 0f;
    private float angleTime = 0f;

    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        attackCooldown = initialAttackCooldown;    
    }

    public void Shoot() {
        // int[] angles = { -45, 0, 45 };
        float[] angles = { 0 };
        for (int i = 0; i < angles.Length; i++) {
            GameObject bulletObject = Instantiate(bullet, transform.position, transform.rotation);
            BulletProperty bulletProperty = bulletObject.GetComponent<BulletProperty>();

            Vector2 direction = Quaternion.Euler(0, 0, 0 * angleOffset + angles[i]) * Vector2.down;
            bulletProperty.SetVelocity(direction * bulletProperty.speed);
        } 
    }

    public void ShootFinished() {
        shooting = false;
        attackCooldown = initialAttackCooldown;
    }

    void Update()
    {
        angleTime += Time.deltaTime;
        angleOffset = 45f * Mathf.Sin(angleTime);

        if (attackCooldown <= 0) {
            if (!shooting) {
                animator.SetTrigger("Attack");
                shooting = true;
            }
        } else {
            attackCooldown -= Time.deltaTime;
        }
    }
}
