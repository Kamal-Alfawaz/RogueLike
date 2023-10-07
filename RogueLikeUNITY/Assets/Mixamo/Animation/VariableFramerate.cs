using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableFramerate : MonoBehaviour
{
    public Animator animator; // The animator component of the character
    public float updateInterval = 0.1f; // The time interval between pose updates

    private float timer; // A timer to keep track of the time elapsed

    void Start()
    {
        // Initialize the timer
        timer = 0f;
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;
        // Check if the timer has reached the update interval
        if (timer >= updateInterval)
        {
            // Update the character's pose by sampling the animator at the current time
            animator.Update(timer);
            // Reset the timer
            timer = 0f;
        }
    }
}
