using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RiddleUI : MonoBehaviour
{
    public GameObject riddlePanel;
    public Text riddleText;
    public InputField answerInput;
    public Text errorMessage;
    public TMP_Text successMessage;


    void Start()
    {
        riddlePanel.SetActive(false);

        answerInput.onEndEdit.AddListener(delegate {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SubmitAnswer();
            }
        });
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            answerInput.Select();
            answerInput.ActivateInputField();
        }
    }

  

    public void ShowRiddle(string riddle)
    {
        riddlePanel.SetActive(true);
        riddleText.gameObject.SetActive(true);  
        answerInput.gameObject.SetActive(true); 
        errorMessage.gameObject.SetActive(true);

        riddleText.text = riddle;
        answerInput.text = "";  
        errorMessage.text = ""; 


        answerInput.interactable = true;
        Invoke("EnableInput", 0.1f);
        answerInput.Select();
        answerInput.ActivateInputField();
        answerInput.GetComponent<InputFieldOutlineEffect>().HighlightInputField();


    }
    
    private void EnableInput()
    {
        answerInput.Select();
        answerInput.ActivateInputField();
        answerInput.caretPosition = answerInput.text.Length; 
    }


    public void SubmitAnswer()
    {
        string answer = answerInput.text.Trim();

        if (string.IsNullOrEmpty(answer))
        {
            ShowErrorMessage("Please enter an answer!");
            return;
        }

        bool isCorrect = RiddleManager.instance.CheckAnswer(answer);

        if (isCorrect)
        {
            Debug.Log("Correct Answer! Closing riddle panel...");
            successMessage.gameObject.SetActive(true);
            answerInput.GetComponent<InputFieldOutlineEffect>().RemoveHighlight();
            Invoke("CloseRiddlePanel", 1f);
        }
        else
        {
            ShowErrorMessage("Incorrect answer! Try again.");
            answerInput.text = ""; 
            Invoke("EnableInput", 0.1f); 
        }
    }



    public void ShowErrorMessage(string message)
    {
        errorMessage.text = message;
    }

        private void CloseRiddlePanel()
    {
        riddlePanel.SetActive(false);
    }

}
