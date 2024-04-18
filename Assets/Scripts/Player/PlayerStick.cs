using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStick : MonoBehaviour
{
    public Rigidbody2D playerRigidbody; // Reference to the player's Rigidbody2D

    void Start()
    {
        AddFixedJoint();
    }

    void AddFixedJoint()
    {
        // Add FixedJoint2D component to the sword GameObject
        FixedJoint2D fixedJoint = gameObject.AddComponent<FixedJoint2D>();

        // Connect the FixedJoint2D to the player's Rigidbody2D
        fixedJoint.connectedBody = playerRigidbody;
    }
}
