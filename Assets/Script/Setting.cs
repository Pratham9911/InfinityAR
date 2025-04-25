using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Toggle zoomToggle;
    public Toggle effectsToggle;

    void Start()
    {
        // Load saved settings (default is ON)
        zoomToggle.isOn = PlayerPrefs.GetInt("ZoomEnabled", 1) == 1;
        effectsToggle.isOn = PlayerPrefs.GetInt("EffectsEnabled", 1) == 1;

        // Listen for changes
        zoomToggle.onValueChanged.AddListener(OnZoomToggleChanged);
        effectsToggle.onValueChanged.AddListener(OnEffectsToggleChanged);
    }

    void OnZoomToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("ZoomEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    void OnEffectsToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("EffectsEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}
