using UnityEngine;
using TMPro;

public class ClickableEmailTMP : MonoBehaviour
{
    private TMP_Text tmpText;
    private RectTransform rectTransform;

    void Start()
    {
        tmpText = GetComponent<TMP_Text>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Check if the user clicks on the email text
        if (Input.touchCount > 0)  // Check for touch input (mobile)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)  // Touch started
            {
                Vector3 touchPos = touch.position;
                if (IsTouchOverEmail(touchPos))  // Check if touch is on the email text
                {
                    OnEmailClick();  // Open the email client
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))  // Check for mouse click (desktop)
        {
            Vector3 mousePos = Input.mousePosition;
            if (IsTouchOverEmail(mousePos))  // Check if mouse is over the email text
            {
                OnEmailClick();  // Open the email client
            }
        }
    }

    // Check if the touch/mouse is over the email text
    bool IsTouchOverEmail(Vector3 position)
    {
        // Convert screen position to local position in the RectTransform
        Vector2 localPosition = rectTransform.InverseTransformPoint(position);
        return rectTransform.rect.Contains(localPosition);  // Check if the position is within the bounds of the text
    }

    // Open the email client when clicked
    void OnEmailClick()
    {
        string email = "prathamtiwari0123@gmail.com";
        string subject = "Contribution to InfinityAR";
        string body = "I would like to contribute to InfinityAR.";
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }
}
