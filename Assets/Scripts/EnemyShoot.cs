using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BulletPatterns;
using Unity.VisualScripting;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;

    public float initialAttackCooldown;
    public float bulletSpeed = 10f;
    private float attackCooldown;

    Animator animator;

    public enum ShootingModes {
        None, OneTap, DoubleTap, Radial, Radial3
    }
    public ShootingModes currentMode = ShootingModes.OneTap;
    public List<ShootingModes> availableModes = new() { 
        ShootingModes.OneTap
    };

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        attackCooldown = initialAttackCooldown;
    }

    public void Shoot(float[] angles) {
        for (int i = 0; i < angles.Length; i++) {
            GameObject bulletObject = Instantiate(bullet, transform.position, transform.rotation);
            BulletProperty bulletProperty = bulletObject.GetComponent<BulletProperty>();

            Vector2 direction = Quaternion.Euler(0, 0, angles[i]) 
                * (GameRound.instance.player.transform.position - transform.position).normalized;
            bulletProperty.speed = bulletSpeed;
            bulletProperty.SetVelocity(direction * bulletProperty.speed);
        } 
    }

    void Update()
    {
        if (GameRound.instance.gameOver) return;

        if (attackCooldown <= 0) {
            animator.SetTrigger("Attack");
            attackCooldown = initialAttackCooldown;

            currentMode = GetRandomMode();
            StartCoroutine(currentMode.ToString());
        } else {
            attackCooldown -= Time.deltaTime;
        }
    }

    private ShootingModes GetRandomMode() {
        return availableModes[Random.Range(0, availableModes.Count)];
    }

    // Shooting Modes

    public IEnumerator OneTap() {
        Shoot(new float[] { 0 });
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator DoubleTap() {
        Shoot(new float[] { 0 });
        yield return new WaitForSeconds(0.1f);
        Shoot(new float[] { 0 });
    }

    public IEnumerator Radial() {
        Shoot(new float[] { -90, -45, 0, 45, 90 });
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator Radial3() {
        Shoot(new float[] { -90, -45, -15, 0, 15, 45, 90 });
        yield return new WaitForEndOfFrame();
    }
}
