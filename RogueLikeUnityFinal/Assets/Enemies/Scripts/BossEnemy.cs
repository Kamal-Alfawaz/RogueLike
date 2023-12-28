using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public GameObject nextLevelTeleportObjectPrefab;
    public GameObject currentInteractableObject;

    // This is your custom method for the BossEnemy
    public void SpawnToNextLevel(){            
        Instantiate(nextLevelTeleportObjectPrefab, currentInteractableObject.transform.position, Quaternion.identity);
    }
}

