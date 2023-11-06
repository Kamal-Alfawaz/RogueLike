using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ThirdPersonShooterController playerHealth = other.GetComponent<ThirdPersonShooterController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on the player object.");
            }

            // Always destroy the projectile, regardless of whether it hit the player or not
            Destroy(gameObject);
        }
    }
}