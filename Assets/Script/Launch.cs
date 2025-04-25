using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RocketController : MonoBehaviour
{
    public Animator rocketAnimator;
    public Button launchButton;
    public TMP_Text liftOffTimeText; // "Lift-off + X sec"
    public TMP_Text launchTimeText;  // "Lift-off Time: HH:MM:SS"
    public GameObject[] fireEffects;

    private bool hasLaunched = false;
    private float launchTime;
    private int elapsedSeconds = -7;

    void Start()
    {
        rocketAnimator.enabled = false;
        launchButton.interactable = true;
        liftOffTimeText.text = ""; // Keep text hidden
        launchTimeText.text = "";

        foreach (GameObject fire in fireEffects)
        {
            fire.SetActive(false);
        }

        launchButton.onClick.AddListener(LaunchRocket);
    }

    void LaunchRocket()
    {
        if (!hasLaunched)
        {
            hasLaunched = true;
            launchTime = Time.time;
            launchButton.interactable = false;

            // Store actual lift-off time
            string formattedTime = System.DateTime.Now.ToString("HH:mm:ss");
            launchTimeText.text = "Lift-off Time: " + formattedTime;

            // Start countdown timer
            InvokeRepeating("UpdateElapsedTime", 1f, 1f);
        }
    }

    void UpdateElapsedTime()
    {
        if (hasLaunched)
        {
            elapsedSeconds++;  // Increment time (-4, -3, -2... 0, 1, 2...)

            if (elapsedSeconds == 0)
            {
                liftOffTimeText.text = "Lift-off!"; // Start displaying text
                rocketAnimator.enabled = true;
                rocketAnimator.SetTrigger("LaunchRocket");

                foreach (GameObject fire in fireEffects)
                {
                    fire.SetActive(true);
                }
            }
            else if (elapsedSeconds > 0)
            {
                liftOffTimeText.text = "Lift-off + " + elapsedSeconds + " sec"; // Show elapsed time
            }
        }
    }
}
