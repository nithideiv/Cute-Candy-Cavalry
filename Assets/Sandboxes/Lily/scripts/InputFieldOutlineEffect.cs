using UnityEngine;
using UnityEngine.UI;

public class InputFieldOutlineEffect : MonoBehaviour
{
    private Outline outline;
    private InputField inputField;

    void Start()
    {
        outline = GetComponent<Outline>();
        inputField = GetComponent<InputField>();

        if (outline == null)
        {
            Debug.LogError("Outline component missing!");
        }

        if (inputField == null)
        {
            Debug.LogError("InputField component missing!");
        }

        // Initially disable the outline effect
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    public void HighlightInputField()
    {
        if (outline != null)
        {
            outline.enabled = true; 
        }
    }

    public void RemoveHighlight()
    {
        if (outline != null)
        {
            outline.enabled = false; 
        }
    }
}
