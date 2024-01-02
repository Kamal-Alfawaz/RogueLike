using UnityEngine;
using TMPro;

public class AbilityCounter : MonoBehaviour
{
    public int fireRateCount = 0;
    public int damageCount = 0;
    public int healthCount = 0;
    public int speedCount = 0;

    public GameObject fireRateBoostItem;
    public GameObject fireDamageItem;
    public GameObject healingItem;
    public GameObject speedBoostItem;

    // Function to handle picking up items
    public void PickUpItem(GameObject item)
    {
        if (item == fireRateBoostItem)
        {
            fireRateCount++;
            Debug.Log("Picked up FireRateBoost. Current Fire Rate Count: " + fireRateCount);
        }
        else if (item == fireDamageItem)
        {
            damageCount++;
            Debug.Log("Picked up FireDamageItem. Current Damage Count: " + damageCount);
        }
        else if (item == healingItem)
        {
            healthCount++;
            Debug.Log("Picked up HealingItem. Current Health Count: " + healthCount);
        }
        else if (item == speedBoostItem)
        {
            speedCount++;
            Debug.Log("Picked up SpeedBoost. Current Speed Count: " + speedCount);
        }
        else
        {
            Debug.LogWarning("Unknown item picked up.");
        }
    }
}