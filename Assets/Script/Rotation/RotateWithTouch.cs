using UnityEngine;
using UnityEngine.EventSystems;

public class SmoothRotate : MonoBehaviour
{
    public float rotationSpeed = 0.05f; // Adjust for smoothness
    private Transform targetModel;
    private Vector2 lastTouchPosition;
    private bool isDragging = false;

    void Start()
    {
        targetModel = transform; // Attach this script to the Earth Core model
    }

    void Update()
    {
        // 🛑 Prevent rotation when touching UI
        if (Input.touchCount == 1 && !IsTouchOverUI(Input.GetTouch(0)))
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 delta = touch.position - lastTouchPosition;
                RotateModel(delta);
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }

        // 🖱️ PC Mouse Rotation (For Testing)
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            lastTouchPosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastTouchPosition;
            RotateModel(delta);
            lastTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void RotateModel(Vector2 delta)
    {
        float rotationX = delta.y * rotationSpeed;
        float rotationY = -delta.x * rotationSpeed;
        targetModel.Rotate(rotationX, rotationY, 0, Space.World);
    }

    bool IsTouchOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current) { position = touch.position };
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
