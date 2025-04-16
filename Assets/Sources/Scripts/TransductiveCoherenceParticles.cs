using UnityEngine;

/// <summary>
/// TransductiveCoherenceParticles controls the visual particle effect for a single "Nexon"
/// in the TransductiveRelationalSimulator. It is not a literal representation of a physical particle,
/// but a symbolic visualisation of coherence, identity stability, and topological integrity.
///
/// As coherence approaches 1 (perfect closure with zero angular/radial deviation), 
/// the particle becomes small, radiant, and stable — symbolising resolved identity.
/// As coherence drops (due to deviation from closure), the particle expands into a diffuse,
/// noisy cloud — representing ontological decoherence, unresolved feedback, and structural tension.
///
/// The effect uses Unity's ParticleSystem to modulate size, shape, noise, emission rate, 
/// and colour based on coherence. It interpolates between two gradients and adds procedural flicker
/// under low coherence to convey instability.
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class TransductiveCoherenceParticles : MonoBehaviour
{
    [Range(0f, 1f)] public float coherence = 1.0f; // Normalised coherence scalar (0 = decoherent, 1 = perfect)

    private ParticleSystem ps;
    private ParticleSystem.MainModule main;
    private ParticleSystem.EmissionModule emission;
    private ParticleSystem.ShapeModule shape;
    private ParticleSystem.ColorOverLifetimeModule colorOverLifetime;
    private ParticleSystem.NoiseModule noise;

    private Gradient lowCoherenceGradient;
    private Gradient highCoherenceGradient;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        main = ps.main;
        emission = ps.emission;
        shape = ps.shape;
        colorOverLifetime = ps.colorOverLifetime;
        noise = ps.noise;

        BuildGradients(); // Pre-define colour curves for coherence extremes
        UpdateVisualsFromCoherence(); // Initialise effect
    }

    void Update()
    {
        UpdateVisualsFromCoherence(); // Continuously respond to coherence changes
    }

    /// <summary>
    /// Updates the visual state of the Nexon particle effect based on the current coherence level.
    /// All attributes — size, radius, emission, noise, and colour — are dynamically adjusted.
    /// </summary>
    private void UpdateVisualsFromCoherence()
    {
        // Size: collapse from fog to focused point
        main.startSize = Mathf.Lerp(6.5f, 0.4f, coherence);

        // Shape: inward curve with coherence (radial compression)
        shape.radius = Mathf.Lerp(5f, 0.2f, coherence);
        shape.radiusThickness = Mathf.Lerp(1.0f, 0.1f, coherence);

        // Lifetime: more persistent under coherence
        main.startLifetime = Mathf.Lerp(8.0f, 2.0f, coherence);

        // Emission rate: higher coherence = higher rhythmic output
        emission.rateOverTime = Mathf.Lerp(10f, 120f, coherence);

        // Colour blend: use gradient to interpolate based on coherence level
        colorOverLifetime.enabled = true;
        Gradient g = GradientLerp(lowCoherenceGradient, highCoherenceGradient, coherence);
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(g);

        // Decoherence noise: enable only when unstable
        noise.enabled = coherence < 0.5f;
        noise.strength = Mathf.Lerp(0.2f, 2.5f, 1.0f - coherence);
        noise.frequency = 0.2f + (1.0f - coherence) * 0.8f;
        noise.scrollSpeed = 0.1f + (1.0f - coherence) * 0.3f;

        // HDR glow enhancement: intensify radiance with coherence
        main.startColor = Color.Lerp(
            new Color(0.1f, 0.1f, 0.2f, 0.3f),       // muted fog
            new Color(2f, 1.8f, 1.2f, 0.8f),         // glowing gold-violet
            coherence
        );
    }

    /// <summary>
    /// Optionally call this externally to set coherence based on simulation state.
    /// Uses a smooth exponential curve for visual response tuning.
    /// </summary>
    public void SetCoherence(float c)
    {
        float k = 2f; // curve steepness
        coherence = 1f - Mathf.Exp(-c * k); // sigmoid curve for perceptual balance
    }

    /// <summary>
    /// Builds two predefined colour gradients for low and high coherence states.
    /// These gradients are used to visually interpolate the particle cloud appearance.
    /// </summary>
    private void BuildGradients()
    {
        // 🔹 Low coherence → chaotic violet plasma fog
        lowCoherenceGradient = new Gradient();
        lowCoherenceGradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(0.1f, 0.3f, 0.6f), 0.0f),  // deep blue
                new GradientColorKey(new Color(0.3f, 0.2f, 0.5f), 1.0f)   // violet haze
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.0f, 0.0f),
                new GradientAlphaKey(0.5f, 0.4f),
                new GradientAlphaKey(0.0f, 1.0f)
            }
        );

        // 🔹 High coherence → stable radiant nexus core
        highCoherenceGradient = new Gradient();
        highCoherenceGradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(1.0f, 0.77f, 0.13f), 0.0f),  // golden orange
                new GradientColorKey(new Color(1.0f, 0.06f, 0.73f), 1.0f)   // violet resonance
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.4f, 0.0f),
                new GradientAlphaKey(0.8f, 0.5f),
                new GradientAlphaKey(0.2f, 1.0f)
            }
        );
    }

    /// <summary>
    /// Linearly interpolates between two Unity gradients.
    /// This is used to smoothly transition the Nexon cloud between coherent and decoherent states.
    /// </summary>
    private Gradient GradientLerp(Gradient a, Gradient b, float t)
    {
        Gradient result = new Gradient();

        int count = Mathf.Min(a.colorKeys.Length, b.colorKeys.Length);
        GradientColorKey[] colorKeys = new GradientColorKey[count];
        for (int i = 0; i < count; i++)
        {
            colorKeys[i].time = Mathf.Lerp(a.colorKeys[i].time, b.colorKeys[i].time, t);
            colorKeys[i].color = Color.Lerp(a.colorKeys[i].color, b.colorKeys[i].color, t);
        }

        count = Mathf.Min(a.alphaKeys.Length, b.alphaKeys.Length);
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[count];
        for (int i = 0; i < count; i++)
        {
            alphaKeys[i].time = Mathf.Lerp(a.alphaKeys[i].time, b.alphaKeys[i].time, t);
            alphaKeys[i].alpha = Mathf.Lerp(a.alphaKeys[i].alpha, b.alphaKeys[i].alpha, t);
        }

        result.SetKeys(colorKeys, alphaKeys);
        return result;
    }
}
