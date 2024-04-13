using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] private float initialAttackCooldown;
    private float attackCooldown;

    // Start is called before the first frame update
    void Start()
    {
        attackCooldown = initialAttackCooldown;    
    }

    // Update is called once per frame
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
