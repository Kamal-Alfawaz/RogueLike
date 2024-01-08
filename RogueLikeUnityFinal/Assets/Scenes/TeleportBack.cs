using UnityEngine;

public class TeleportBack : MonoBehaviour
{
    public Transform teleportLocation; // Assign this in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpawnLocation"))
        {
            other.transform.position = teleportLocation.position;
        }
    }
}