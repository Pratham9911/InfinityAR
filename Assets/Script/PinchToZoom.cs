using UnityEngine;

public class PinchToZoom : MonoBehaviour
{
    public float zoomSpeed = 0.1f;  // Speed of zooming
    public float minScale = 0.005f; // Minimum size limit
    public float maxScale = 0.02f;  // Maximum size limit

    private Vector2 touchStart; // Stores touch position
    private float initialDistance; // Initial distance between fingers
    private Vector3 initialScale; // Initial scale of object

    void Start()
    {
        // Check zoom setting from PlayerPrefs
        bool zoomEnabled = PlayerPrefs.GetInt("ZoomEnabled", 1) == 1;
        if (!zoomEnabled)
        {
            this.enabled = false;
        }
    }

    void Update()
    {
        if (Input.touchCount == 2) // Detect two-finger touch
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            float currentDistance = Vector2.Distance(touch1.position, touch2.position);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                initialDistance = currentDistance;
                initialScale = transform.localScale;
            }

            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                if (initialDistance > 0)
                {
                    float scaleFactor = currentDistance / initialDistance;
                    Vector3 newScale = initialScale * scaleFactor;

                    // Clamp the scale
                    newScale = Vector3.Max(Vector3.one * minScale, Vector3.Min(newScale, Vector3.one * maxScale));

                    transform.localScale = newScale;
                }
            }
        }
    }
}
