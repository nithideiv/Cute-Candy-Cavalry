using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class Character_Customization : MonoBehaviour
{

    public GameObject[] characters;
    public Texture2D lil_cursor;
    public Transform camera_ref;
    public Transform playerStart;
    int selectedCharacter;
    private string selectedCharacterName = "SelectedCharacter";
    public GameObject player;
    
    void Start()
    {
        // get chosen character value
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterName, 0);
        Debug.Log(selectedCharacter);
        Debug.Log(characters[selectedCharacter]);
        player = Instantiate(characters[selectedCharacter], playerStart);

        player.SetActive(true);

        // Hard coding fixes for character selection

        if (selectedCharacter == 1) {
            player.transform.position = new Vector3(-0.3f, 1.23f, -3.8f);
        } 
        // in case they look away

        player.transform.LookAt(camera_ref);

    }

    public void HideAllCharacters() {
        foreach (GameObject g in characters) {
            g.SetActive(false);
        }
    }


    public void StartGame() {
        // set preference to store it across files
        PlayerPrefs.SetInt(selectedCharacterName, selectedCharacter);
        HideAllCharacters();
        SceneManager.LoadScene("LevelSelect");
    }

    public void Tutorial()
    {
        PlayerPrefs.SetInt(selectedCharacterName, selectedCharacter);
        HideAllCharacters();
        SceneManager.LoadScene("Tutorial");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
