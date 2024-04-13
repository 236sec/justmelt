using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStick : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject

    void Start()
    {
        // Check if player reference is assigned
        if (player == null)
        {
            Debug.LogWarning("Player reference is not assigned in StickWithPlayer script!");
            return;
        }

        // Make the sword GameObject a child of the player GameObject
        transform.parent = player.transform;
    }
}
