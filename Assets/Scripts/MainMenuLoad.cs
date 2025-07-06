using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    public string mainMenuSceneName = "CharacterCustomization"; // Replace with your main menu scene name

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Or change to another key
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}
