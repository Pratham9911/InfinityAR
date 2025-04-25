using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class SliderValueDisplay : MonoBehaviour
{
    public Slider slider; // Reference to the UI Slider
    public TMP_Text valueText; // Reference to TextMeshPro UI

    void Start()
    {
        if (slider != null)
        {
            // Ensure the slider moves only in whole number steps
            slider.wholeNumbers = true;

            slider.onValueChanged.AddListener(UpdateValueText);
            UpdateValueText(slider.value); // Initialize the text at start
        }
    }

    void UpdateValueText(float value)
    {
        if (valueText != null)
        {
            // Convert float to whole number and update UI
            valueText.text = $"Speed: {(int)value}x";
        }
    }
}
