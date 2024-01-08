using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    public RandomSpawner randomSpawner;
    public TextMeshProUGUI levelText;

    void Start()
    {
        // Start the coroutine to continuously update difficulty level
        StartCoroutine(UpdateDifficultyLevel());
    }

    // Coroutine to continuously update the difficulty level
    IEnumerator UpdateDifficultyLevel()
    {
        while (true)
        {
            // Check if randomSpawner is not null
            if (randomSpawner != null)
            {
                // Access difficulty level through the property
                int difficultyLevel = randomSpawner.DifficultyLevel;

                // Update the TextMeshProUGUI component with the difficulty level
                levelText.text = "Level: " + difficultyLevel.ToString();
            }
            else
            {
                Debug.LogError("RandomSpawner is not assigned to Level script.");
            }

            // Wait for a short time before updating again
            yield return new WaitForSeconds(1f);
        }
    }
}
