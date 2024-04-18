using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParry : MonoBehaviour
{
    public GameObject reflectedBullet;
    public float cooldownTime = 5f;

    [SerializeField] private Vector2 hitboxSize = new Vector2(1, 1);

    [SerializeField] private float currentCooldownTime = 0f;

    private void FixedUpdate() {
        if (currentCooldownTime > 0) {
            currentCooldownTime -= Time.fixedDeltaTime;
        }
        else {
            currentCooldownTime = 0f;
        }
    }

    private void OnDrawGizmosSelected() {
        UnityEditor.Handles.DrawWireCube(transform.position, hitboxSize);
    }

    private void CheckBullets() {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, hitboxSize, 0);

        foreach (Collider2D collider in colliders) {
            ParryBullet(collider);
            Debug.Log(collider.gameObject.name);
        }
    }

    public void Parry() {
        if (currentCooldownTime > 0) return;

        currentCooldownTime = cooldownTime;

        CheckBullets();
    }

    void ParryBullet(Collider2D other) {
        BulletTag bTag = other.gameObject.GetComponent<BulletTag>();
        BulletProperty bProp = other.gameObject.GetComponent<BulletProperty>();

        if (bTag != null) {
            Debug.Log("Sword Block");
            GameObject reflectedB = Instantiate(reflectedBullet, other.transform.position, other.transform.rotation);
            BulletProperty reflectedBp = reflectedB.GetComponent<BulletProperty>();

            reflectedBp.SetVelocity(-bProp.GetVelocity());

            Destroy(other.gameObject);
        }
    }
}
