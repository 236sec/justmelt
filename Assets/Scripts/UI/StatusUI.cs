using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpLabel;
    [SerializeField] TextMeshProUGUI parryLabel;
    [SerializeField] TextMeshProUGUI dodgeLabel;
    [SerializeField] Bar hpBar;
    [SerializeField] Bar enemyHpBar;
    [SerializeField] Bar parryBar;
    [SerializeField] Bar dodgeBar;

    [SerializeField] PlayerHealth playerHealth; 
    [SerializeField] EnemyHealth enemyHealth; 
    [SerializeField] SwordParry swordParry; 
    [SerializeField] PlayerMovement playerMovement; 

    void Update()
    {
        hpLabel.text = $"{playerHealth.currentHP}/{playerHealth.maxHP}";
        hpBar.SetValue((float)playerHealth.currentHP / playerHealth.maxHP);
        enemyHpBar.SetValue((float)enemyHealth.currentHP / enemyHealth.initialHP);

        if (swordParry.currentCooldownTime < 0) {
            parryLabel.text = $"0.00s";
        } else if (swordParry.currentCooldownTime > 0) {
            parryLabel.text = $"{swordParry.currentCooldownTime:0.00}s";
            parryBar.SetValue(swordParry.currentCooldownTime / swordParry.cooldownTime);
        } else {
            parryLabel.text = $"Ready";
            parryBar.SetValue(1);
        }

        if (playerMovement.currentDodgeCooldown < 0) {
            dodgeLabel.text = $"0.00s";
        } else if (playerMovement.currentDodgeCooldown > 0) {
            dodgeLabel.text = $"{playerMovement.currentDodgeCooldown:0.00}s";
            dodgeBar.SetValue(playerMovement.currentDodgeCooldown / playerMovement.dodgeCooldown);
        } else {
            dodgeLabel.text = $"Ready";
            dodgeBar.SetValue(1);
        }
    }
}
