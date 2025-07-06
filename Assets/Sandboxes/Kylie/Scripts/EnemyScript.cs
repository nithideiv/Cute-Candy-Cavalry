using System.Xml.Serialization;
using GLTFast;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class EnemyScript : MonoBehaviour
{

    public int health = 5;
    public float speed = 5.2f;
    public bool canMove;

    [SerializeField] private float timer = 0.7f;
    private int velocity = 375;
    private float bulletTime;
    public GameObject enemyBullet;
    public Transform spawnPoint;



    public NavMeshAgent donut;
    public Transform player;
    public GameObject player_obj;

    public GameObject enemy_dad;
    public LayerMask groundLayer, playerLayer;
    public Vector3 walkPoint;
    public float walkPointRange;
    private NavMeshPath path;

    public GameObject[] waypoints;
    int currentWP = 0;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool Attacked;
    public float timeBetweenAttacks;
    public GameObject mainScript;

    [SerializeField] private Stat healthStat;
    [SerializeField] private HealthBarUI healthBarUI;
    public string canvasName;

    public int attackDamage;

    public AudioSource attackAudioSource;
    public AudioSource hurtAudioSource;
    public AudioSource deathAudioSource;

    public AudioSource lowHealthAudioSource;
    private bool lowHealthSoundPlayed = false;

    public AudioSource chaseAudioSource;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        Main_PlayerSelect script = mainScript.GetComponent<Main_PlayerSelect>();
        player_obj = script.returnPlayer();
        donut = GetComponent<NavMeshAgent>(); 
        canMove = true;
        path = new NavMeshPath();
        donut.SetPath(path);

        if (healthStat == null)
            healthStat = new Stat(health);

        healthStat.MaxVal = health;
        healthStat.CurrentVal = health;
        healthStat.Initialize();

        StartCoroutine(InitializeHealthBarUI());

        AudioSource[] audioSources = GetComponents<AudioSource>();

        if (audioSources.Length >= 3)
        {
            attackAudioSource = audioSources[0];
            hurtAudioSource = audioSources[1];
            deathAudioSource = audioSources[2];
            lowHealthAudioSource = audioSources[3];
            chaseAudioSource = audioSources[4];
        }
        else
        {
            Debug.LogError("Not enough AudioSources attached! Make sure there are at least 3.");
        }


    }

    IEnumerator InitializeHealthBarUI()
    {
        yield return new WaitForEndOfFrame(); // Wait for UI to be created

        GameObject canvas = GameObject.Find(canvasName); // Ensure this matches your actual Canvas name

        if (canvas != null)
        {
            healthBarUI = canvas.GetComponentInChildren<HealthBarUI>(); // Only search within the Canvas
        }
        else
        {
            Debug.LogError("Canvas not found! Make sure the name matches.");
        }

        if (healthBarUI == null)
        {
            Debug.LogError("HealthBarUI is not assigned in the Inspector!");
        }
        else
        {
            healthBarUI.SetMaxValue(healthStat.MaxVal);
        }
    }


    private void Patroling()
    {

        if (chaseAudioSource != null)
        {
            chaseAudioSource.Play();
        }

        donut.SetDestination(waypoints[currentWP].transform.position);
        if (Vector3.Distance(this.transform.position, waypoints[currentWP].transform.position) < 4)
        {
            currentWP++;
        }
        if (currentWP >= waypoints.Length) {
            currentWP = 0;
        }


    }

    private void ChasePlayer()
    {
        Debug.Log("chase");
        donut.SetDestination(player_obj.transform.position);

        if (chaseAudioSource != null)
        {
            chaseAudioSource.Play();
        }

        if (this.CompareTag("BananaBat"))
        {
            bulletTime -= Time.deltaTime;

            if (bulletTime > 0) return;

            bulletTime = timer;

            GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
            Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
            bulletRig.AddForce(bulletRig.transform.forward * speed * velocity);
            Destroy(bulletObj, 4f);
        }
    }

    private void AttackPlayer() {
        // stand still
        
        donut.SetDestination(transform.position);
        bool looking = Physics.CheckSphere(transform.position, 3f, playerLayer);

        if (!Attacked) {
            Attacked = true;
            this.GetComponent<Animator>().SetTrigger("attack");

            if (attackAudioSource != null)
            {
                attackAudioSource.Play();
            }
            
            float dot = Vector3.Dot(transform.forward, (player_obj.transform.position - transform.position).normalized);

            if ((dot <= 0.9f) && Physics.CheckSphere(player_obj.transform.position, 2, playerLayer))
            {
                Debug.Log("hit");
                player_obj.GetComponent<Player_Stats>().TakeDamage(attackDamage);
            }
            //player_obj.GetComponent<Player_Stats>().TakeDamage(5);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack () {
        Attacked = false;
        this.GetComponent<Animator>().ResetTrigger("attack");
        //player_obj.GetComponent<CapsuleCollider>().enabled = true;
        //this.GetComponent<CapsuleCollider>().enabled = true;
        return;
    }

    public void TakeDamage (int dmg) {
        
        health -= dmg;
        healthStat.CurrentVal = health;

        healthBarUI.UpdateValue(healthStat.CurrentVal, healthStat.MaxVal);

        if (hurtAudioSource != null)
        {
            hurtAudioSource.Play();
        }

        if (healthStat.CurrentVal <= 2 && !lowHealthSoundPlayed)
        {
            if (lowHealthAudioSource != null)
            {
                lowHealthAudioSource.Play();
            }
            lowHealthSoundPlayed = true;
        }

        if (healthStat.CurrentVal <= 0 ) {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    public float ShowHealth () {
        return health;
    }

    private void DestroyEnemy() {

        if (deathAudioSource != null)
        {
            deathAudioSource.Play();
        }

        //Destroy(gameObject, deathAudioSource != null ? deathAudioSource.clip.length : 0f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
    
        if (canMove == true) {
        if (!playerInSightRange && !playerInAttackRange) {
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange) {
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange) {
            AttackPlayer();
        }
        }
    }
}
