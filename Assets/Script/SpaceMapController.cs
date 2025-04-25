using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpaceMapController : MonoBehaviour
{
    public RectTransform mapRect;
    public float zoomSpeed = 0.1f;
    public float minZoom = 0.3f;
    public float maxZoom = 5f;

    private Vector2 lastTouchPosition;
    private bool isDragging = false;

    void Update()
    {
        HandleMouseZoom();
        HandleTouchZoom();
        HandleDrag();
        ClampPosition();
    }

    void HandleMouseZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            Zoom(scroll, Input.mousePosition);
        }
    }

    void HandleTouchZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0Prev = touch0.position - touch0.deltaPosition;
            Vector2 touch1Prev = touch1.position - touch1.deltaPosition;

            float prevMagnitude = (touch0Prev - touch1Prev).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;
            Vector2 midPoint = (touch0.position + touch1.position) * 0.5f;

            Zoom(difference * 0.005f, midPoint);
        }
    }

    void Zoom(float increment, Vector2 screenZoomCenter)
    {
        float scaleFactor = 1 + increment * zoomSpeed;
        float newScale = Mathf.Clamp(mapRect.localScale.x * scaleFactor, minZoom, maxZoom);
        scaleFactor = newScale / mapRect.localScale.x;

        // Convert screen point to local point in the RectTransform
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mapRect, screenZoomCenter, null, out Vector2 localZoomCenter);
        Vector3 worldZoomCenter = mapRect.TransformPoint(localZoomCenter);

        // Apply scaling
        mapRect.localScale = Vector3.one * newScale;

        // Adjust position to zoom toward the focus point
        Vector3 postWorldZoomCenter = mapRect.TransformPoint(localZoomCenter);
        Vector3 positionDelta = postWorldZoomCenter - worldZoomCenter;
        mapRect.position -= positionDelta;
    }

    void HandleDrag()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 difference = touch.position - lastTouchPosition;
                mapRect.position += (Vector3)difference;
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 difference = (Vector2)Input.mousePosition - lastTouchPosition;
            mapRect.position += (Vector3)difference;
            lastTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void ClampPosition()
    {
        RectTransform parentRect = mapRect.parent as RectTransform;

        // Get size of map and canvas
        float scaledWidth = mapRect.rect.width * mapRect.localScale.x;
        float scaledHeight = mapRect.rect.height * mapRect.localScale.y;

        float parentWidth = parentRect.rect.width;
        float parentHeight = parentRect.rect.height;

        Vector3 pos = mapRect.localPosition;

        float clampX = Mathf.Max((scaledWidth - parentWidth) / 2f, 0);
        float clampY = Mathf.Max((scaledHeight - parentHeight) / 2f, 0);

        pos.x = Mathf.Clamp(pos.x, -clampX, clampX);
        pos.y = Mathf.Clamp(pos.y, -clampY, clampY);

        mapRect.localPosition = pos;
    }
}
