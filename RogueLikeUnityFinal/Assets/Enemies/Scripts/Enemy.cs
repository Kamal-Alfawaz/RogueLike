using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] FloatingHealthbar floatingHealthbar;

    public GameObject FloatingTextPrefab;
    public static event Action OnBossKilled;

    // The duration for which the health bar should be shown (in seconds)
    public float healthBarDuration = 10f;

    // A boolean variable to indicate if the enemy is being attacked
    private bool isAttacked = false;

    public float health = 1f;
    public float maxHealth = 1f;
    public float damage = 1f;

    public bool isBoss = false;

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
    private bool isDead = false;

    void Start(){
        SpawnerAndDifficulty = GameObject.Find("Spawner").GetComponent<RandomSpawner>();
    }

    private void Awake(){
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        floatingHealthbar = GetComponentInChildren<FloatingHealthbar>();
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
        // get the direction vector from the enemy to the player
        Vector3 direction = player.position - transform.position;
        // set the enemy's rotation to face the player, but keep the same y-axis rotation
        transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(direction, Vector3.up).eulerAngles.y, 0);
        if(!alreadyAttacked){
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            // add force to the bullet in the direction of the player, with some random variation
            rb.AddForce((direction + Vector3.up * 1f).normalized * 32f + Random.insideUnitSphere * 1f, ForceMode.Impulse);
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
        if (isDead) {
            return;
        }
        health -= amount;
        floatingHealthbar.UpdateHealthBar(health, maxHealth);
        if(FloatingTextPrefab){
            ShowFloatingText(amount);
        }
        if (health <= 0f){
            Die();
        }else{
            floatingHealthbar.gameObject.SetActive(true);
            if (!isAttacked)
            {
                isAttacked = true;
                StartCoroutine(HideHealthBar());
            }
        }
    }

    void ShowFloatingText(float amount){
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMeshPro>().text = amount.ToString();
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

    public void KillBoss()
    {
        OnBossKilled?.Invoke();
    }

    void Die(){
        if (isBoss)
        {
            KillBoss();
        }
        isDead = true;
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


// public class ProjectileHandler : MonoBehaviour{

//     private int damage;
//     private string targetTag;

//     public void Initialize(int damage, string targetTag)
//     {
//         this.damage = damage;
//         this.targetTag = targetTag;
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag(targetTag))
//         {
//             ThirdPersonShooterController playerHealth = other.GetComponent<ThirdPersonShooterController>();
//             if (playerHealth != null)
//             {
//                 playerHealth.TakeDamage(damage);
//             }
//             Destroy(gameObject); // Destroy the projectile
//         }
//     }
// }
