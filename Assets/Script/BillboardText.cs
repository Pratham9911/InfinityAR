using UnityEngine;

public class BillboardText : MonoBehaviour
{
    public Camera arCamera; // Drag AR Camera here if Camera.main doesn't work
    public GameObject[] textObjects; // Assign multiple text objects here

    private bool[] textVisible; // Array to track visibility of each text object

    void Start()
    {
        // If no AR Camera is manually assigned, use Camera.main
        if (arCamera == null)
        {
            arCamera = Camera.main;
        }

        // Initialize visibility state for all text objects
        textVisible = new bool[textObjects.Length];
        for (int i = 0; i < textObjects.Length; i++)
        {
            if (textObjects[i] != null)
            {
                textObjects[i].SetActive(false); // Hide all text initially
                textVisible[i] = false;
            }
        }
    }

    void Update()
    {
        if (arCamera != null)
        {
            foreach (GameObject textObj in textObjects)
            {
                if (textObj != null)
                {
                    // Make text always face the AR Camera
                    textObj.transform.LookAt(arCamera.transform);
                    textObj.transform.Rotate(0, 180, 0); // Ensure correct facing
                }
            }
        }

        // Handle touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            CheckTouch(Input.GetTouch(0).position);
        }
    }

    void CheckTouch(Vector2 touchPosition)
    {
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            for (int i = 0; i < textObjects.Length; i++)
            {
                if (hit.transform.gameObject == this.gameObject) // If the planet is tapped
                {
                    textVisible[i] = !textVisible[i]; // Toggle visibility
                    textObjects[i].SetActive(textVisible[i]);
                }
            }
        }
    }
}
