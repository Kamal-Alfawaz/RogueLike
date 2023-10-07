using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHitbox : MonoBehaviour
{

    public ThirdPersonShooterController player;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy"){
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.health -= player.damage;
            player.CallItemOnHit(enemy);
        }
        
    }

}
