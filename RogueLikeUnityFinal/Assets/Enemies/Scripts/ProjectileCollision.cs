using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public string playerTag = "Player"; // Assign this in the Inspector
    public LayerMask obstacleLayer; // Assign this in the Inspector

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles") || collision.gameObject.tag != playerTag)
        {
            Destroy(gameObject);
        }
    }
}
