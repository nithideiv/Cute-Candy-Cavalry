// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class EndGame : MonoBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player")) {
//             PlayerPrefs.SetString(SceneManager.GetActiveScene().name.ToString().Trim(), "Complete");
//             PlayerPrefs.Save();
//             Debug.Log("save");
//             SceneManager.LoadScene("CharacterSelection");
//         }
//     }
// }


using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI winText; 
    public AudioSource winSound; 
    private AudioSource audioSource;

    void Start()
    {
        winText.gameObject.SetActive(false); // Hide text at the start
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Player entered the trigger!");
            StartCoroutine(ShowWinTextAndLoadScene());
        }
    }


    IEnumerator ShowWinTextAndLoadScene()
    {
        winText.gameObject.SetActive(true); // Show the win message

        if (winSound != null)
        {
            winSound.Play(); // Play the win sound
        }
        else
        {
            Debug.LogWarning("Win sound not assigned in the Inspector!");
        }
        yield return new WaitForSeconds(1f); // Wait for 1 second
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name.ToString().Trim(), "Complete");
        PlayerPrefs.Save();
        Debug.Log("save");
        SceneManager.LoadScene("CharacterSelection"); // Load the next scene
    }
}
