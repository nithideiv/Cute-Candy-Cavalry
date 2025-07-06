using UnityEngine;

public class Riddles : MonoBehaviour
{
    public int val = 1;
    private AudioSource audioSource;
    private bool triggered = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on Riddle GameObject!");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            RiddleManager.instance.AddScore(val);

            if (audioSource != null)
            {
                audioSource.Play();
                // Destroy the object after the clip finishes playing
                Destroy(gameObject, audioSource.clip.length);
            }
            else
            {
                Destroy(gameObject); // fallback if no AudioSource
            }

            // if (RiddleManager.instance.ReturnScore() == 3)
            // {
            //     RiddleManager.instance.TriggerRiddle();
            // }
        }
    }
}
