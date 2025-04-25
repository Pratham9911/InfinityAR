using UnityEngine;

public class AREffectManager : MonoBehaviour
{
    public ParticleSystem[] arParticles;
    public float fullEffectRate = 50f;
    public float lowEffectRate = 5f; // Or 0f to completely hide

    void Start()
    {
        bool effectsEnabled = PlayerPrefs.GetInt("EffectsEnabled", 1) == 1;

        foreach (var ps in arParticles)
        {
            var emission = ps.emission;
            emission.rateOverTime = effectsEnabled ? fullEffectRate : lowEffectRate;
        }
    }
}
