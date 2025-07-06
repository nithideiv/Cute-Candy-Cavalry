using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 20; 
    public float rotationSpeed = 50f;

    private AudioSource audioSource;
    private bool hasPlayedSound = false; // To avoid multiple triggers

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on HealthPickup object.");
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayedSound)
        {
            Player_Stats playerStats = other.GetComponent<Player_Stats>();
            if (playerStats != null)
            {
                playerStats.Heal(healthAmount);
            }

            hasPlayedSound = true;

            if (audioSource != null)
            {
                audioSource.Play();
                // Destroy object after audio finishes playing
                Destroy(gameObject, audioSource.clip.length);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
