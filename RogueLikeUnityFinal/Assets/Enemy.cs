using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] FloatingHealthbar floatingHealthbar;

    private AudioSource HitMarker;

    // The duration for which the health bar should be shown (in seconds)
    public float healthBarDuration = 10f;

    // A boolean variable to indicate if the enemy is being attacked
    private bool isAttacked = false;

    public float health = 1f;
    public float maxHealth = 1f;
    public float damage = 1f;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public GameObject projectile;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private RandomSpawner SpawnerAndDifficulty;

    public WeightedRandomList<GameObject> lootTable;

    void Start(){
        SpawnerAndDifficulty = GameObject.Find("Spawner").GetComponent<RandomSpawner>();
    }

    private void Awake(){
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        floatingHealthbar = GetComponentInChildren<FloatingHealthbar>();
        HitMarker = GetComponent<AudioSource>();
        floatingHealthbar.gameObject.SetActive(false);
    }

    private void Update() {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange){
            Patroling();
        }
        if(playerInSightRange && !playerInAttackRange){
            ChasePlayer();
        }
        if(playerInSightRange && playerInAttackRange){
            AttackPlayer();
        }
    }

    private void Patroling(){
        if(!walkPointSet){
            SearchWalkPoint();
        }

        if (walkPointSet){
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //The WalkPoint destination is set
        if(distanceToWalkPoint.magnitude < 1f){
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)){
            walkPointSet = true;
        }
    }

    private void ChasePlayer(){
        agent.SetDestination(player.position);
    }

    private void AttackPlayer(){
        // this makes sure the enemy does not move whilst attacking
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked){
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            Destroy(rb.gameObject, 1);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void takeDamage(float amount){
        HitMarker.Play();
        health -= amount;
        floatingHealthbar.UpdateHealthBar(health, maxHealth);
        if (health <= 0f){
            Die();
        }else{
            // Activate the health bar
            floatingHealthbar.gameObject.SetActive(true);

            // Check if the enemy is already being attacked
            if (!isAttacked)
            {
                // Set the isAttacked flag to true
                isAttacked = true;

                // Start a coroutine that waits for the health bar duration and then deactivates the health bar
                StartCoroutine(HideHealthBar());
            }
        }
    }

    private IEnumerator HideHealthBar()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(healthBarDuration);

        // Deactivate the health bar
        floatingHealthbar.gameObject.SetActive(false);

        // Set the isAttacked flag to false
        isAttacked = false;
    }

    void Die(){
        Destroy(gameObject);
        Loot();
        SpawnerAndDifficulty.EnemyKilled();
    }

    public void Loot(){
        GameObject item = lootTable.GetRandom();
        Vector3 position = transform.position;
        Instantiate(item, position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
