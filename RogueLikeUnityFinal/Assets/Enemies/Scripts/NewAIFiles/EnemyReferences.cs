using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[DisallowMultipleComponent]
public class EnemyReferences : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Animator animator;

    [Header("Enemy Stats")]
    public float pathUpdateDelay = 0.2f;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
}
