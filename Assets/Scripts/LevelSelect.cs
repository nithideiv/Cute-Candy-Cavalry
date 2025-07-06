using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public GameObject[] characters;
    int selectedCharacter;
    private string selectedCharacterName = "SelectedCharacter";



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterName, 0);

    }

    // Update is called once per frame

    public void Level_One()
    {
        PlayerPrefs.SetInt(selectedCharacterName, selectedCharacter);
        SceneManager.LoadScene("castle");
    }

    public void Level_Two()
    {
     
        if (PlayerPrefs.HasKey("castle"))
        {
            PlayerPrefs.SetInt(selectedCharacterName, selectedCharacter);
            SceneManager.LoadScene("castle 2");
        }
    }

    public void Level_Three()
    {

        if (PlayerPrefs.HasKey("castle 2"))
        {
            PlayerPrefs.SetInt(selectedCharacterName, selectedCharacter);
            SceneManager.LoadScene("castle 3");
        }
    }


    void Update()
    {
        
    }
}
