using UnityEngine;
using UnityEngine.UI; // Import UI namespace

public class EarthRotation : MonoBehaviour
{
    public Transform earth;
    public Transform clouds;
    public Transform atmosphere;

    public float baseEarthRotationSpeed = 15f;      // Default Earth rotation speed
    public float baseCloudRotationSpeed = 20f;      // Clouds rotate faster than Earth
    public float baseAtmosphereRotationSpeed = 10f; // Atmosphere rotates slower than Earth

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
        // Rotate Earth
        earth.Rotate(Vector3.forward, baseEarthRotationSpeed * speedMultiplier * Time.deltaTime, Space.Self);

        // Rotate Clouds (Scaled proportionally)
        clouds.Rotate(Vector3.forward, baseCloudRotationSpeed * speedMultiplier * Time.deltaTime, Space.Self);

        // Rotate Atmosphere (Scaled proportionally)
        atmosphere.Rotate(Vector3.forward, baseAtmosphereRotationSpeed * speedMultiplier * Time.deltaTime, Space.Self);
    }

    // Function to update speed dynamically from slider
    public void UpdateSpeed(float value)
    {
        speedMultiplier = value; // Adjust the rotation speed for all objects
    }
}
