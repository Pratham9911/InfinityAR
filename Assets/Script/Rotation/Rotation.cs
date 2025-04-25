using UnityEngine;
using UnityEngine.UI; // Import UI namespace

public class Rotate : MonoBehaviour
{
    public GameObject PlanetObject;  // Planet or Moon
    public Vector3 RotationVector;   // Axis of rotation
    public float baseSpeed = 1f;  // Default speed for this specific object
    public Slider speedSlider;    // Reference to UI slider

    private float currentSpeed;  // Speed that updates dynamically

    void Start()
    {
        currentSpeed = baseSpeed; // Set initial speed

        if (speedSlider != null)
        {
            speedSlider.value = 1f;  // Default multiplier
            speedSlider.onValueChanged.AddListener(UpdateSpeed);
        }
    }

    void Update()
    {
        // Rotate the planet or moon with its relative speed
        PlanetObject.transform.Rotate(RotationVector * currentSpeed * Time.deltaTime);
    }

    // Function to update speed dynamically based on slider
    public void UpdateSpeed(float speedMultiplier)
    {
        currentSpeed = baseSpeed * speedMultiplier; // Maintain relative speeds
    }
}
