using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] FloatingHealthbar floatingHealthbar;

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

    void Start(){
        SpawnerAndDifficulty = GameObject.Find("DifficultyManager").GetComponent<RandomSpawner>();
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

        transform.LookAt(player);

        if(!alreadyAttacked){
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void takeDamage(float amount){
        floatingHealthbar.gameObject.SetActive(true);
        health -= amount;
        floatingHealthbar.UpdateHealthBar(health, maxHealth);
        if (health <= 0f){
            Die();
        }
    }

    void Die(){
        Destroy(gameObject);
        Loot();
        SpawnerAndDifficulty.EnemyKilled();
    }

    public void Loot(){
        
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
