using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private Camera currentCamera;

    public float DestroyTime = 1f;
    public Vector3 Offset = new Vector3(0, 2, 0);
    public Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 0);

    void Start()
    {
        currentCamera = GameObject.Find ("MainCamera").GetComponent<Camera>();
        Destroy(gameObject, DestroyTime);
        transform.localPosition += Offset;
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x), Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y), Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));
    }

    private void Update() {
        transform.rotation = currentCamera.transform.rotation;
    }
}
