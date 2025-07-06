using UnityEngine;
using System.Collections.Generic;

public class HealthPickupSpawner : MonoBehaviour
{
    public List<GameObject> healthPickupPrefabs; 
    public List<Transform> spawnPoints;          
    public GameObject player;                    

    public int maxHealth = 5;
    public float spawnCooldown = 10f; 
    private float spawnTimer = 0f;

    void Awake()
    {
        // Automatically find all spawn points with the tag "SpawnPoint"
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnPoints = new List<Transform>();

        foreach (GameObject obj in spawnPointObjects)
        {
            spawnPoints.Add(obj.transform);
        }

        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points found! Did you forget to tag them?");
        }
    }


    void Update(){}

    public void SpawnBasedOnHealth()
    {

        Player_Stats playerStats = player.GetComponent<Player_Stats>();
        if (playerStats == null) return;

        int maxHealth = (int)playerStats.healthStat.MaxVal;
        int currentHealth = (int)(playerStats.healthBarUI.targetFill * maxHealth);

        int pickupsToSpawn = maxHealth - currentHealth;

        Debug.Log("Current Health: " + currentHealth);
        Debug.Log("Max Health:" + maxHealth);
        Debug.Log("Pickups to spawn: " + pickupsToSpawn);


        // Shuffle spawn points
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            int rand = Random.Range(i, spawnPoints.Count);
            Transform temp = spawnPoints[i];
            spawnPoints[i] = spawnPoints[rand];
            spawnPoints[rand] = temp;
        }        


        int spawned = 0;
        float checkRadius = 0.5f; 

        for (int i = 0; i < spawnPoints.Count && spawned < pickupsToSpawn; i++)
        {
            Transform point = spawnPoints[i];

            // Check for existing pickups using overlap sphere
            Collider[] colliders = Physics.OverlapSphere(point.position, checkRadius);
            bool occupied = false;

            foreach (var col in colliders)
            {
                if (col.CompareTag("Candy"))
                {
                    occupied = true;
                    break;
                }
            }

            if (!occupied)
            {
                GameObject prefab = healthPickupPrefabs[Random.Range(0, healthPickupPrefabs.Count)];
                GameObject newObj = Instantiate(prefab, point.position, Quaternion.identity);
                newObj.tag = "Candy"; 
                spawned++;
            }
        }
    }
}
