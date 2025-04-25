using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform centerObject;  // Planet to orbit around
    public float orbitSpeed = 5f;  // Speed of orbit
    public Vector3 orbitTilt = new Vector3(0, 1, 0); // Default tilt

    private void Start()
    {
        if (centerObject == null)
        {
            Debug.LogError("Center Object not assigned! Assign a planet to orbit around.");
        }
    }

    void Update()
    {
        if (centerObject != null)
        {
            // Orbit around the center object in a circular path
            transform.RotateAround(centerObject.position, orbitTilt.normalized, orbitSpeed * Time.deltaTime);
        }
    }
}
