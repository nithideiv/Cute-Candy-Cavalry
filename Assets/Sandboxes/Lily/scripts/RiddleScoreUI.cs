using UnityEngine;
using UnityEngine.UI;

public class RiddleScoreUI : MonoBehaviour
{
    [SerializeField] private Text scoreText; 
    [SerializeField] private Image[] riddleImages; 
    

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("Score Text is not assigned! Drag the UI text object into the field.");
        }

        if (riddleImages.Length != 3)
        {
            Debug.LogError("Assign exactly 3 images for the riddle UI!");
        }

        UpdateScoreUI(); 
    }

    public void UpdateScoreUI()
    {
        if (RiddleManager.instance != null)
        {
            int score = RiddleManager.instance.ReturnScore();
            scoreText.text = "Riddle Pieces: " + score + "/3";

            UpdateRiddleImages(score);
        }
    }

    private void UpdateRiddleImages(int score)
    {
        for (int i = 0; i < riddleImages.Length; i++)
        {
            Color imgColor = riddleImages[i].color;

            if (i < score)
            {
                imgColor.a = 1f; 
            }
            else
            {
                imgColor.a = 0.3f; 
            }

            riddleImages[i].color = imgColor;
        }
    }
}
