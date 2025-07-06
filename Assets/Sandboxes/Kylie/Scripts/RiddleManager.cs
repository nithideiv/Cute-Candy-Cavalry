using UnityEngine;

public class RiddleManager : MonoBehaviour
{

    public static RiddleManager instance;
    private int score;
    private RiddleLoader riddleLoader;
    private string currentRiddle;
    private string correctAnswer;
    private RiddleUI riddleUI;
    private RiddleScoreUI uiManager;
    public DoorController door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameObject doorObject = GameObject.FindWithTag("ExitDoor");
        if (doorObject != null)
        {
            door = doorObject.GetComponent<DoorController>();
        }
        uiManager = FindObjectOfType<RiddleScoreUI>(); 
        UpdateUI();

        if (instance == null) {
            instance = this;
        }

        riddleLoader = FindObjectOfType<RiddleLoader>();

        if (riddleLoader == null)
        {
            Debug.LogError("RiddleLoader not found! Make sure it's attached to a GameObject.");
        }

        riddleUI = FindObjectOfType<RiddleUI>();
        if (riddleUI == null)
        {
            Debug.LogError("RiddleUI not found! Make sure it's assigned correctly.");
        }

        score = 0;
    }

    void Start()
    {
        // TestRiddleSystem();
    }

    private void TestRiddleSystem()
    {
        Debug.Log("Testing RiddleManager...");

        if (riddleLoader == null)
        {
            Debug.LogError("RiddleLoader is not initialized!");
            return;
        }

        (string riddle, string answer) = riddleLoader.GetRiddle();
        
        if (string.IsNullOrEmpty(riddle))
        {
            Debug.LogError("No riddle found for level 1!");
            return;
        }

        // Store the correct answer for later input verification
        currentRiddle = riddle;
        correctAnswer = answer;

        Debug.Log($"Test Riddle: {riddle} | Player must enter answer.");
        
        riddleUI.ShowRiddle(riddle); // Display the riddle UI
    }



    // Update is called once per frame
    public void AddScore(int amt)
    {
        Debug.Log($"Score before adding amt: {score} with amt: {amt}");
        score += amt;
        Debug.Log($"Score in after add score: {score}");
        UpdateUI();
        if (score == 3) // When the score reaches 3, trigger the riddle
        {
            Debug.Log("Score is now 3! Triggering riddle...");
            TriggerRiddle();
        }
    }

    public int ReturnScore() 
    {
        Debug.Log($"Score is: {score}");
        return score;
    }
    private void UpdateUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateScoreUI();
        }
    }

    public void TriggerRiddle()
    {
        if (riddleLoader == null)
        {
            Debug.LogError("RiddleLoader is not initialized! Cannot fetch a riddle.");
            return;
        }

        // get the level

        (currentRiddle, correctAnswer) = riddleLoader.GetRiddle();

        if (string.IsNullOrEmpty(currentRiddle))
        {
            Debug.LogError("No riddle found!");
            return;
        }

        Debug.Log($"Triggered Riddle: {currentRiddle}");
        riddleUI.ShowRiddle(currentRiddle); // Display the riddle UI
    }


    public bool CheckAnswer(string input)
    {
        if (input.ToLower().Trim() == correctAnswer.ToLower().Trim())
        {
            Debug.Log("Correct Answer! Unlocking the door...");
            UnlockDoor();
            return true; // Indicate correct answer
        }
        else
        {
            Debug.Log("Wrong answer! Try again.");
            return false; // Allow retry
        }
    }

    public void UnlockDoor()
    {
        Debug.Log("Calling door unlock method...");
        FindObjectOfType<alpha_doors>().DisableDoorCollider(); // Call door script
        if (door != null)
        {
            door.OpenDoor();
        }
        else
        {
            Debug.Log("Door was null");
        }
    }

}
