using UnityEngine;

/// ============================================================================
/// TransductiveCoherenceParticles
///
/// A small particle-system controller used as a visual diagnostic for the
/// *register coherence* value C ∈ [0,1].
///
/// Interpretation inside the programme
///   - C is a closure-grade scalar computed from ε and r_dev:
///       C(λ,φ) = exp( - ( ε(λ,φ)^2 + r_dev(λ)^2 ) )
///   - Visuals sharpen as C approaches 1 and diffuse as C decreases.
///
/// This component is deliberately agnostic about physical ontology.
/// It is a UI layer for one scalar.
/// ============================================================================
[RequireComponent(typeof(ParticleSystem))]
public class TransductiveCoherenceParticles : MonoBehaviour
{
    [Range(0f, 1f)]
    public float coherence = 1.0f;

    private ParticleSystem ps;
    private ParticleSystem.MainModule main;
    private ParticleSystem.EmissionModule emission;
    private ParticleSystem.ShapeModule shape;
    private ParticleSystem.ColorOverLifetimeModule colorOverLifetime;
    private ParticleSystem.NoiseModule noise;

    private Gradient lowCoherenceGradient;
    private Gradient highCoherenceGradient;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        main = ps.main;
        emission = ps.emission;
        shape = ps.shape;
        colorOverLifetime = ps.colorOverLifetime;
        noise = ps.noise;

        BuildGradients();
        UpdateVisualsFromCoherence();
    }

    private void Update() => UpdateVisualsFromCoherence();

    private void UpdateVisualsFromCoherence()
    {
        // Size: diffuse at low C, tight at high C.
        main.startSize = Mathf.Lerp(6.5f, 0.4f, coherence);

        // Shape: expand at low C, compress at high C.
        shape.radius = Mathf.Lerp(5f, 0.2f, coherence);
        shape.radiusThickness = Mathf.Lerp(1.0f, 0.1f, coherence);

        // Lifetime: longer trails at low C, cleaner at high C.
        main.startLifetime = Mathf.Lerp(8.0f, 2.0f, coherence);

        // Emission: quiet under coherence (high C), noisy under low C.
        emission.rateOverTime = Mathf.Lerp(10f, 120f, coherence);

        // Colour: blend between two fixed gradients.
        colorOverLifetime.enabled = true;
        Gradient g = GradientLerp(lowCoherenceGradient, highCoherenceGradient, coherence);
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(g);

        // Noise: enabled only under low coherence (visual turbulence).
        noise.enabled = coherence < 0.5f;
        noise.strength = Mathf.Lerp(0.2f, 2.5f, 1.0f - coherence);
        noise.frequency = 0.2f + (1.0f - coherence) * 0.8f;
        noise.scrollSpeed = 0.1f + (1.0f - coherence) * 0.3f;

        // Base tint (HDR-like): intentionally mild so it does not dominate UI.
        main.startColor = Color.Lerp(
            new Color(0.15f, 0.15f, 0.25f, 0.25f),
            new Color(1.2f, 1.0f, 0.7f, 0.75f),
            coherence
        );
    }

    /// External setter. Input is assumed to already be in [0,1].
    public void SetCoherence(float c) => coherence = Mathf.Clamp01(c);

    private void BuildGradients()
    {
        // Low coherence: cool haze
        lowCoherenceGradient = new Gradient();
        lowCoherenceGradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(0.1f, 0.3f, 0.6f), 0.0f),
                new GradientColorKey(new Color(0.3f, 0.2f, 0.5f), 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.0f, 0.0f),
                new GradientAlphaKey(0.5f, 0.4f),
                new GradientAlphaKey(0.0f, 1.0f)
            }
        );

        // High coherence: warm core
        highCoherenceGradient = new Gradient();
        highCoherenceGradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(1.0f, 0.77f, 0.13f), 0.0f),
                new GradientColorKey(new Color(1.0f, 0.06f, 0.73f), 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.4f, 0.0f),
                new GradientAlphaKey(0.8f, 0.5f),
                new GradientAlphaKey(0.2f, 1.0f)
            }
        );
    }

    private static Gradient GradientLerp(Gradient a, Gradient b, float t)
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
