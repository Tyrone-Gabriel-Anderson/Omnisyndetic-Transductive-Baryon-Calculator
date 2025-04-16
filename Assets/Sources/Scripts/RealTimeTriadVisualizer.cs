using UnityEngine;

/// <summary>
/// RealTimeTriadVisualizer animates and renders a triadic configuration of nexons in real time,
/// simulating the ontological dynamics of transductive identity formation based on the principles
/// of the Omnisyndetic Framework.
///
/// ⚠️ This visualiser is not a literal model of baryonic matter or QCD.
/// Instead, it is a **topological metaphor**, a graphical output for the ontology of the framework calculations:
/// - Nexons represent localised loci of feedback.
/// - Curved lines (Echonex) represent identity return paths (feedback coherence).
/// - Particle cloud radius reflects emergent spatial deviation from ideal closure.
/// - Energy values are derived from deviation-based structural tension (not empirical constants).
///
/// This system visually communicates how baryonic identity forms (or collapses) as a function
/// of radial and angular deviation from Plato’s Baryon — the idealised triadic closure.
///
/// When coherence is low, identity breaks down: emission halts, the Echonex vanishes,
/// and charge clouds destabilise. When coherence is high, the system converges and forms
/// a stable baryon.
/// </summary>
public class RealTimeTriadVisualizer : MonoBehaviour
{
    [Header("Simulator Link")]
    public TransductiveRelationalSimulator simulator;

    [Header("Prefabs and Materials")]
    public GameObject pointPrefab;               // Nexon visual
    public GameObject identityEchoPrefab;        // Animated feedback particle
    public GameObject decayHaloPrefab;           // Collapse effect
    public Material echonexMaterial;             // Curved line (Echonex)

    [Header("Charge Cloud Visual")]
    public GameObject chargeCloudEffect;         // Visualisation of emergent charge cloud

    [Header("Visual Settings")]
    public float maxLineWidth = 0.08f;
    public float identitySpeed = 1.5f;
    public int curveResolution = 16;

    private GameObject[] points = new GameObject[3];
    private GameObject[] echoes = new GameObject[3];
    private LineRenderer[] echonexLines = new LineRenderer[3];
    private Vector3[][] curvePaths = new Vector3[3][];
    private float[] echoTimers = new float[3];
    private ParticleSystem[] pointParticles = new ParticleSystem[3];

    private GameObject decayHalo;
    private bool isCollapsed = false;
    private bool isCloudVisible = false;
    private float collapseTimer = 0f;
    private const float collapseDelay = 2f;

    void Start()
    {
        InitializeTriad(); // Create nexons and initialise render structures
    }

    void Update()
    {
        if (simulator == null) return;

        // --- Read parameters from simulator ---
        float lambda = simulator.lambdaSlider.value;
        float sumPhi = simulator.coherenceDeviationSlider.value;

        // --- Compute deviation from ideal baryon ---
        float epsilon = Mathf.Abs(sumPhi - (2f * Mathf.PI)) / (2f * Mathf.PI); // angular
        float radialDev = (lambda - 0.54668f) / 0.54668f;                      // radial
        float coherence = Mathf.Exp(-(epsilon * epsilon + radialDev * radialDev));
        float curvatureSign = (sumPhi < 2f * Mathf.PI) ? 1f : -1f;

        // --- Update nexon positions in local space ---
        Vector3[] positions = UpdateTriadPositions(lambda);

        // --- Collapse or animate depending on stability ---
        if (simulator.totalMass <= 0f)
        {
            if (!isCollapsed) Collapse();
            else
            {
                collapseTimer -= Time.deltaTime;
                if (collapseTimer <= 0f) Recover();
            }
        }
        else
        {
            if (!isCollapsed)
                AnimateEchonex(positions, epsilon, curvatureSign, coherence);
            else
                Recover();
        }

        // --- Update dependent visuals ---
        UpdateChargeCloud(simulator.chargeCloudRadius, simulator.totalMass > 0f);
        UpdatePointParticles(coherence);
    }

    /// <summary>
    /// Updates nexon positions and assigns current coherence to their particle visualisers.
    /// </summary>
    Vector3[] UpdateTriadPositions(float lambda)
    {
        Vector3[] posArray = new Vector3[3];
        float radius = lambda * 3f;
        float[] angles = { 0f, 120f, 240f };

        for (int i = 0; i < 3; i++)
        {
            float rad = angles[i] * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * radius;
            points[i].transform.localPosition = pos;
            posArray[i] = points[i].transform.position;

            // Assign coherence (drives particle visualiser)
            points[i].GetComponent<TransductiveCoherenceParticles>().SetCoherence(simulator.coherence);
        }

        return posArray;
    }

    /// <summary>
    /// Animates curved Echonex lines between nexons.
    /// The curvature is based on angular mismatch (ε) and folds inward or outward based on topology.
    /// </summary>
    void AnimateEchonex(Vector3[] positions, float epsilon, float curvatureSign, float coherence)
    {
        for (int i = 0; i < 3; i++)
        {
            int j = (i + 1) % 3;
            Vector3 a = positions[i];
            Vector3 b = positions[j];
            float distance = Vector3.Distance(a, b);
            Vector3 normal = Vector3.Cross((b - a).normalized, Vector3.forward);
            float arcHeight = 0.5f * epsilon * distance;

            var lr = echonexLines[i];
            lr.positionCount = curveResolution;

            for (int k = 0; k < curveResolution; k++)
            {
                float t = k / (float)(curveResolution - 1);
                Vector3 mid = Vector3.Lerp(a, b, t);
                mid += normal * Mathf.Sin(t * Mathf.PI) * arcHeight * curvatureSign;
                lr.SetPosition(k, mid);
                curvePaths[i][k] = mid;
            }

            lr.startWidth = lr.endWidth = Mathf.Lerp(0.01f, maxLineWidth, epsilon);
            lr.startColor = lr.endColor = (curvatureSign > 0f) ? Color.cyan : Color.red;

            // Animate the echo particle (identity tracer)
            echoTimers[i] += Time.deltaTime * identitySpeed;
            float tEcho = Mathf.Repeat(echoTimers[i], 1f);
            echoes[i].transform.position = EvaluateCurve(curvePaths[i], tEcho);
        }
    }

    /// <summary>
    /// Initialises nexons, their line renderers, and coherence particle systems.
    /// </summary>
    void InitializeTriad()
    {
        for (int i = 0; i < 3; i++)
        {
            points[i] = Instantiate(pointPrefab, transform);
            echoes[i] = Instantiate(identityEchoPrefab, transform);
            curvePaths[i] = new Vector3[curveResolution];
            echoTimers[i] = i * 0.33f;
            pointParticles[i] = points[i].GetComponentInChildren<ParticleSystem>();

            var lineObj = new GameObject($"Echonex_{i}");
            lineObj.transform.SetParent(transform);
            var lr = lineObj.AddComponent<LineRenderer>();
            lr.material = echonexMaterial;
            lr.widthMultiplier = 0.05f;
            lr.useWorldSpace = true;
            echonexLines[i] = lr;
        }

        if (chargeCloudEffect != null)
        {
            chargeCloudEffect.SetActive(false);
            isCloudVisible = false;
        }
    }

    /// <summary>
    /// Updates the radius and pulse of the charge cloud based on coherence breakdown.
    /// </summary>
    void UpdateChargeCloud(float radius, bool active)
    {
        if (chargeCloudEffect == null) return;

        if (active && !isCloudVisible)
        {
            chargeCloudEffect.SetActive(true);
            isCloudVisible = true;
        }
        else if (!active && isCloudVisible)
        {
            chargeCloudEffect.SetActive(false);
            isCloudVisible = false;
        }

        if (isCloudVisible)
        {
            float scale = radius * 7f;
            float pulse = 1f + 0.03f * Mathf.Sin(Time.time * 4f);
            chargeCloudEffect.transform.localScale = Vector3.one * scale * pulse;
            chargeCloudEffect.transform.position = transform.position;
        }
    }

    /// <summary>
    /// Dynamically controls particle emission around each nexon point.
    /// Visualises coherence by shrinking and calming under high coherence.
    /// </summary>
    void UpdatePointParticles(float coherence)
    {
        for (int i = 0; i < 3; i++)
        {
            if (pointParticles[i] != null)
            {
                var emission = pointParticles[i].emission;
                emission.rateOverTime = Mathf.Lerp(80f, 0f, coherence); // full emission at low C
                var main = pointParticles[i].main;
                main.startSize = Mathf.Lerp(0.4f, 0.05f, coherence);
            }
        }
    }

    /// <summary>
    /// Lerp evaluation for curve movement of echo particles.
    /// </summary>
    Vector3 EvaluateCurve(Vector3[] path, float t)
    {
        float scaled = t * (path.Length - 1);
        int index = Mathf.FloorToInt(scaled);
        int next = Mathf.Min(index + 1, path.Length - 1);
        return Vector3.Lerp(path[index], path[next], scaled - index);
    }

    /// <summary>
    /// Triggers a baryon collapse: disables feedback structures, halts emission, and spawns decay halo.
    /// </summary>

    void Collapse()
    {
        isCollapsed = true;
        collapseTimer = collapseDelay;

        if (decayHaloPrefab != null && decayHalo == null)
            decayHalo = Instantiate(decayHaloPrefab, transform.position, Quaternion.identity, transform);

        // Stop emission for all point particles
        foreach (var pt in points)
        {
            var ps = pt.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var emission = ps.emission;
                emission.enabled = false;
            }
        }

        // Disable visual echoes by turning off their emission
        foreach (var e in echoes)
        {
            var ps = e.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var emission = ps.emission;
                emission.enabled = false;
            }
        }

        // Hide Echonex line renderers only (no particles)
        foreach (var l in echonexLines)
        {
            l.enabled = false;
        }

        // Stop charge cloud emission only
        if (chargeCloudEffect != null)
        {
            var ps = chargeCloudEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var emission = ps.emission;
                emission.enabled = false;
            }
        }
    }

    void Recover()
    {
        isCollapsed = false;

        if (decayHalo != null)
        {
            Destroy(decayHalo);
            decayHalo = null;
        }

        // Reactivate particle emission for each nexon
        foreach (var pt in points)
        {
            var ps = pt.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var emission = ps.emission;
                emission.enabled = true;
            }
        }

        // Reactivate identity echoes
        foreach (var e in echoes)
        {
            var ps = e.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var emission = ps.emission;
                emission.enabled = true;
            }
        }

        // Restore Echonex visual lines
        foreach (var l in echonexLines)
        {
            l.enabled = true;
        }

        // Resume charge cloud emission
        if (chargeCloudEffect != null)
        {
            var ps = chargeCloudEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var emission = ps.emission;
                emission.enabled = true;
            }
        }
    }

}
