using UnityEngine;
using TMPro;

public class TextEffects : MonoBehaviour
{
    private Material textMaterial;
    private float colorChangeSpeed = 0.5f; // 1 color per 3 seconds
    private float offsetChangeSpeed = 0.5f; // 1 offset change per 2 seconds

    void Start()
    {
        textMaterial = GetComponent<TextMeshProUGUI>().fontMaterial;
        textMaterial.EnableKeyword("UNDERLAY_ON"); // Enable underlay
    }

    void Update()
    {
        // 🔥 Alternate X & Y Offset Every 2 Seconds
        float offsetValue = Mathf.PingPong(Time.time * offsetChangeSpeed, 1); // Goes from 0 → 1 → 0 every 2 sec
        textMaterial.SetFloat("_UnderlayOffsetX", offsetValue);
        textMaterial.SetFloat("_UnderlayOffsetY", 1 - offsetValue); // Opposite of X (0 when X is 1, and vice versa)

        // 🔥 Color Transition Logic (Red → Pink → Blue → Purple every 3 sec)
        Color[] colors = { Color.red, new Color(1f, 0.4f, 0.7f), Color.blue, new Color(0.6f, 0.2f, 0.8f) };
        float t = Mathf.PingPong(Time.time * colorChangeSpeed, colors.Length - 1);
        Color underlayColor = Color.Lerp(colors[Mathf.FloorToInt(t)], colors[Mathf.CeilToInt(t)], t % 1);

        textMaterial.SetColor("_UnderlayColor", underlayColor); // Apply color change
    }
}
