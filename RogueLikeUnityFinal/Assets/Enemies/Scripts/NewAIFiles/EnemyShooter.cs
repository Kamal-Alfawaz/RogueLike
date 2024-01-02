using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShooter : MonoBehaviour
{

    [Header("General")]
    public Transform shootPoint;

    public Transform gunPoint;

    public LayerMask layerMask;

    [Header("Gun")]

    public Vector3 spread = new Vector3(0.06f, 0.06f, 0.06f); // The spread of the bullets

    public TrailRenderer bulletTrail;
    
    private EnemyReferences enemyReferences;

    private void Awake() {
        enemyReferences = GetComponent<EnemyReferences>();
    }

    private void Start() {
        
    }

    private void Update() {
        
    }

    public void Shoot(){
        Vector3 direction = GetDirection();
        if(Physics.Raycast(shootPoint.position, direction, out RaycastHit hit, float.MaxValue, layerMask)){
            TrailRenderer trail = Instantiate(bulletTrail, gunPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;
        direction += new Vector3(
            Random.Range(-spread.x, spread.x),
            Random.Range(-spread.y, spread.y),
            Random.Range(-spread.z, spread.z)
        );
        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0f;
        while (time < 1f){
            trail.transform.position = Vector3.Lerp(gunPoint.position, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }
        trail.transform.position = hit.point;

        Destroy(trail.gameObject, trail.time);
    }
}
