using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpLabel;
    [SerializeField] TextMeshProUGUI parryLabel;
    [SerializeField] Bar hpBar;

    [SerializeField] PlayerHealth playerHealth; 
    [SerializeField] SwordParry swordParry; 

    void Update()
    {
        hpLabel.text = $"HP: {playerHealth.currentHP}/{playerHealth.maxHP}";
        hpBar.SetValue((float)playerHealth.currentHP / playerHealth.maxHP);

        if (swordParry.currentCooldownTime < 0) {
            parryLabel.text = $"0.00s";
        } else {
            parryLabel.text = $"{swordParry.currentCooldownTime:0.00}s";
        }
    }
}
