using UnityEngine;
using UnityEngine.SceneManagement;



public class CharacterSelector : MonoBehaviour
{

        public GameObject[] playerChoices;
        public Texture2D lil_cursor;
        public int selectedCharacter;
        public string mainScene = "CharacterCustomization"; //TO BE CHANGED
        private string selectedCharacterName = "SelectedCharacter";

        public AudioSource clickSound; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        HideAllCharacters();
        selectedCharacter = 0;
        playerChoices[selectedCharacter].SetActive(true);

         // Find the GameObject tagged "on-click" and get its AudioSource
        GameObject soundObject = GameObject.FindWithTag("on-click");
        if (soundObject != null)
        {
            clickSound = soundObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogWarning("No GameObject with tag 'on-click' found!");
        }

    }
    public void HideAllCharacters() {
        foreach (GameObject g in playerChoices) {
            g.SetActive(false);
        }
    }
    public void NextCharacter() {
        if (clickSound != null) {
            clickSound.Play(); // Play the click sound
        }


        Debug.Log("clicked");
        playerChoices[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if (selectedCharacter >= playerChoices.Length) {
            selectedCharacter = 0;
        }
        playerChoices[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter() {

        if (clickSound != null) {
            clickSound.Play(); // Play the click sound
        }

        Debug.Log("back clicked");
        playerChoices[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0) {
            selectedCharacter = playerChoices.Length - 1;
        }
        playerChoices[selectedCharacter].SetActive(true);
    }

    public void StartGame() {

        clickSound.Play(); 
        // set preference to store it across files
        PlayerPrefs.SetInt(selectedCharacterName, selectedCharacter);
        HideAllCharacters();
        SceneManager.LoadScene(mainScene);
    }
}
