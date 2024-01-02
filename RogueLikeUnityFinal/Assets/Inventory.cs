using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI fireRateCountText;
    public TextMeshProUGUI damageCountText;
    public TextMeshProUGUI healthCountText;
    public TextMeshProUGUI speedCountText;

    public GameObject fireRateBoostItem;
    public GameObject fireDamageItem;
    public GameObject healingItem;
    public GameObject speedBoostItem;

    // Function to handle picking up items
    public void PickUpItem(GameObject item)
    {
        if (item == fireRateBoostItem)
        {
            IncrementCount(fireRateCountText);
        }
        else if (item == fireDamageItem)
        {
            IncrementCount(damageCountText);
        }
        else if (item == healingItem)
        {
            IncrementCount(healthCountText);
        }
        else if (item == speedBoostItem)
        {
            IncrementCount(speedCountText);
        }
        else
        {
            Debug.LogWarning("Unknown item picked up.");
        }
    }

    void IncrementCount(TextMeshProUGUI countText)
    {
        int currentCount = int.Parse(countText.text);
        currentCount++;
        countText.text = currentCount.ToString();
    }
}