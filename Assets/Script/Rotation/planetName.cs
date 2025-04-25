using UnityEngine;

public class PlanetLabelFollower : MonoBehaviour
{
    [Header("Assign Planet RectTransform")]
    public RectTransform planetRect;  // UI planet button/image

    [Header("Offset from Planet (UI units)")]
    public float xOffset = 0f;        // Left/Right shift
    public float yOffset = -40f;      // Up/Down shift (negative = below planet)

    private RectTransform labelRect;

    void Awake()
    {
        labelRect = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        if (planetRect == null) return;

        // Step 1: Get planet position in screen space
        Vector3 planetScreenPos = planetRect.position;

        // Step 2: Apply X and Y offsets
        Vector3 labelScreenPos = new Vector3(
            planetScreenPos.x + xOffset,
            planetScreenPos.y + yOffset,
            planetScreenPos.z
        );

        // Step 3: Set label position and reset rotation
        labelRect.position = labelScreenPos;
        labelRect.rotation = Quaternion.identity; // Stay upright
    }
}
