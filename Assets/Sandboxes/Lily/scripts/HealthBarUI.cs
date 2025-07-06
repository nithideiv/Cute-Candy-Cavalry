

using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float lerpSpeed = 5f;
    
    public float targetFill = 1f;

    void Start()
    {
        Debug.Log("HealthBarUI initialized. fillImage is " + (fillImage != null ? "set" : "null"));
    }


    public void SetMaxValue(float maxValue)
    {
        targetFill = 1f;
        fillImage.fillAmount = 1f;
    }

    public void UpdateValue(float currentValue, float maxValue)
    {
        targetFill = Mathf.Clamp01(currentValue / maxValue);
    }

    void Update()
    {
        if (fillImage.fillAmount != targetFill)
        {
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFill, Time.deltaTime * lerpSpeed);
        }
    }
}



[System.Serializable]
public class Stat
{
    [SerializeField] private float maxVal;
    [SerializeField] public int currentVal;

    public Stat(int maxValue = 5)
    {
        MaxVal = maxValue;
        CurrentVal = maxValue;
    }

    public float MaxVal
    {
        get => maxVal;
        set => maxVal = value;
    }

    public float CurrentVal
    {
        get => currentVal;
        set => currentVal = Mathf.Clamp((int)value, 0, (int)MaxVal);
    }

    public void Initialize()
    {
        this.CurrentVal = currentVal;
    }
}

