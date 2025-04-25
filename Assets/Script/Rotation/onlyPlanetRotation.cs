using UnityEngine;
using UnityEngine.UI; // Import UI namespace

public class PlanetRotation : MonoBehaviour
{
    public Transform planet; // Only the planet rotates

    public float baseRotationSpeed = 15f; // Default rotation speed

    public Slider speedSlider; // UI Slider to control speed
    private float speedMultiplier = 1f; // Multiplier for adjusting speed

    void Start()
    {
        if (speedSlider != null)
        {
            speedSlider.value = 1f; // Default multiplier
            speedSlider.onValueChanged.AddListener(UpdateSpeed);
        }
    }

    void Update()
    {
        // Rotate only the planet
        planet.Rotate(Vector3.forward, baseRotationSpeed * speedMultiplier * Time.deltaTime, Space.Self);
    }

    // Function to update speed dynamically from slider
    public void UpdateSpeed(float value)
    {
        speedMultiplier = value; // Adjust the rotation speed
    }
}
