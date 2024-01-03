using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryShowcase : MonoBehaviour
{
    public ThirdPersonShooterController playerController; // Reference to the player controller
    public GameObject itemDisplayPrefab; // Prefab for displaying an item
    public Transform itemDisplayContainer; // The container where item displays are instantiated

    // private void OnEnable()
    // {
    //     playerController.OnInventoryChanged += UpdateInventoryDisplay;
    //     UpdateInventoryDisplay(); // Initial update
    // }

    // private void OnDisable()
    // {
    //     if (playerController != null)
    //     {
    //         playerController.OnInventoryChanged -= UpdateInventoryDisplay;
    //     }
    // }

    private void Update() {
        // Clear existing displays
        foreach (Transform child in itemDisplayContainer)
        {
            Destroy(child.gameObject);
        }

        // Create a new display for each item
        foreach (ItemList item in playerController.items)
        {
            GameObject display = Instantiate(itemDisplayPrefab, itemDisplayContainer);

            // Update the image (if your item has a sprite property)
            Image imageComponent = display.GetComponentInChildren<Image>();
            // imageComponent.sprite = item.sprite; // Uncomment and modify this line according to how you store sprites in ItemList

            // Update the count text
            TextMeshProUGUI countText = display.GetComponentInChildren<TextMeshProUGUI>();
            countText.text = item.count.ToString();
        }
    }

}
