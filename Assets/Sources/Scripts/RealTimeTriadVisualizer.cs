using UnityEngine;

/// ============================================================================
/// RealTimeTriadVisualizer
///
/// This component renders a *triadic* visual diagnostic driven by the register
/// quantities produced by OmnisyndeticBaryonCalculator:
///   - λ (probe) controls the triad radius,
///   - ε and r_dev control a curvature cue,
///   - coherence C controls particle intensity,
///   - optional collapse gating uses the coherence window + seat admissibility.
///
/// This is a visualisation of register state. It is not a claim about literal
/// QCD trajectories.
/// ============================================================================
public class RealTimeTriadVisualizer : MonoBehaviour
{
    [Header("Calculator link (source of register diagnostics)")]
    public OmnisyndeticBaryonCalculator simulator;

    [Header("Prefabs and materials")]
    public GameObject pointPrefab;
    public GameObject tracerPrefab;               // a moving tracer along each curve
    public GameObject collapseHaloPrefab;
    public Material curveMaterial;

    [Header("Optional central cloud (enabled only when admitted)")]
    public GameObject centralCloudEffect;

    [Header("Visual settings")]
    public float maxLineWidth = 0.08f;
    public float tracerSpeed = 1.5f;
    public int curveResolution = 16;

    private GameObject[] points = new GameObject[3];
    private GameObject[] tracers = new GameObject[3];
    private LineRenderer[] curves = new LineRenderer[3];
    private Vector3[][] curvePaths = new Vector3[3][];
    private float[] tracerTimers = new float[3];
    private ParticleSystem[] pointParticles = new ParticleSystem[3];

    private GameObject collapseHalo;
    private bool isCollapsed = false;
    private float collapseTimer = 0f;
    private const float collapseDelay = 2f;

    private void Start() => InitialiseTriad();

    private void Update()
    {
        if (simulator == null) return;

        // Read probe diagnostics from the calculator.
        float lambda = (float)simulator.CurrentLambda;
        float eps = (float)simulator.CurrentEps;
        float C = (float)simulator.CurrentC;

        // Determine "active" vs "collapsed":
        // - Active requires: admitted seat AND coherence inside the register window.
        bool inWindow = (simulator.CurrentC >= OmnisyndeticBaryonCalculator.Register.CMIN) &&
                        (simulator.CurrentC <= OmnisyndeticBaryonCalculator.Register.CMAX);
        bool active = simulator.HasAdmissibleSeat && inWindow;

        // Update triad positions (returns world positions and the triad radius used).
        float triadRadius;
        Vector3[] positions = UpdateTriadPositions(lambda, out triadRadius);

        if (!active)
        {
            if (!isCollapsed) Collapse();
            else
            {
                collapseTimer -= Time.deltaTime;
                if (collapseTimer <= 0f) Recover(); // allow recovery if inputs become admissible again
            }
        }
        else
        {
            if (isCollapsed) Recover();
            AnimateCurves(positions, eps, C);
        }

        // The central cloud is a containment cue: it should scale with the same triad radius
        // that places the witness sites, not with raw λ.
        UpdateCentralCloud(triadRadius, active);
        UpdatePointParticles(C);
    }

    private Vector3[] UpdateTriadPositions(float lambda, out float triadRadius)
    {
        Vector3[] posArray = new Vector3[3];

        // Simple radius scaling for visibility; adjust as desired.
        float radius = lambda * 3f;
        triadRadius = radius;
        float[] angles = { 0f, 120f, 240f };

        for (int i = 0; i < 3; i++)
        {
            float rad = angles[i] * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * radius;
            points[i].transform.localPosition = pos;
            posArray[i] = points[i].transform.position;

            var coherenceParticles = points[i].GetComponent<TransductiveCoherenceParticles>();
            if (coherenceParticles != null)
                coherenceParticles.SetCoherence((float)simulator.CurrentC);
        }

        return posArray;
    }

    /// Curves are drawn between the three points with a signed normal curvature.
    /// We use the sign of (φ - 2π) as a stable cue.
    private void AnimateCurves(Vector3[] positions, float eps, float coherence)
    {
        float curvatureSign = (simulator.CurrentPhi < 2f * Mathf.PI) ? 1f : -1f;

        for (int i = 0; i < 3; i++)
        {
            int j = (i + 1) % 3;
            Vector3 a = positions[i];
            Vector3 b = positions[j];

            float distance = Vector3.Distance(a, b);
            Vector3 normal = Vector3.Cross((b - a).normalized, Vector3.forward);

            float arcHeight = 0.5f * eps * distance;

            var lr = curves[i];
            lr.positionCount = curveResolution;

            for (int k = 0; k < curveResolution; k++)
            {
                float t = k / (float)(curveResolution - 1);
                Vector3 mid = Vector3.Lerp(a, b, t);
                mid += normal * Mathf.Sin(t * Mathf.PI) * arcHeight * curvatureSign;
                lr.SetPosition(k, mid);
                curvePaths[i][k] = mid;
            }

            // Width: stronger deviation => thicker line
            lr.startWidth = lr.endWidth = Mathf.Lerp(0.01f, maxLineWidth, eps);

            // Tracer motion
            tracerTimers[i] += Time.deltaTime * tracerSpeed;
            float tTracer = Mathf.Repeat(tracerTimers[i], 1f);
            if (tracers[i] != null)
                tracers[i].transform.position = EvaluateCurve(curvePaths[i], tTracer);
        }
    }

    private void InitialiseTriad()
    {
        for (int i = 0; i < 3; i++)
        {
            points[i] = Instantiate(pointPrefab, transform);
            tracers[i] = (tracerPrefab != null) ? Instantiate(tracerPrefab, transform) : null;

            curvePaths[i] = new Vector3[curveResolution];
            tracerTimers[i] = i * 0.33f;

            pointParticles[i] = points[i].GetComponentInChildren<ParticleSystem>();

            var lineObj = new GameObject($"TriadCurve_{i}");
            lineObj.transform.SetParent(transform);
            var lr = lineObj.AddComponent<LineRenderer>();
            lr.material = curveMaterial;
            lr.widthMultiplier = 0.05f;
            lr.useWorldSpace = true;
            curves[i] = lr;
        }

        if (centralCloudEffect != null)
            centralCloudEffect.SetActive(false);
    }

    private void UpdateCentralCloud(float radius, bool active)
    {
        if (centralCloudEffect == null) return;

        if (!active)
        {
            centralCloudEffect.SetActive(false);
            return;
        }

        if (!centralCloudEffect.activeSelf)
            centralCloudEffect.SetActive(true);

        // Keep the relator triangle inside the cloud: cloud radius > triad radius.
        // We treat 'radius' as the triad radius already.
        float scale = radius * 2.25f;
        float pulse = 1f + 0.03f * Mathf.Sin(Time.time * 4f);
        centralCloudEffect.transform.localScale = Vector3.one * scale * pulse;
        centralCloudEffect.transform.position = transform.position;
    }

    /// Coherence-driven particle adjustment on each point.
    private void UpdatePointParticles(float coherence)
    {
        for (int i = 0; i < 3; i++)
        {
            if (pointParticles[i] == null) continue;

            var emission = pointParticles[i].emission;
            emission.rateOverTime = Mathf.Lerp(80f, 0f, coherence);

            var main = pointParticles[i].main;
            main.startSize = Mathf.Lerp(0.4f, 0.05f, coherence);
        }
    }

    private static Vector3 EvaluateCurve(Vector3[] path, float t)
    {
        float scaled = t * (path.Length - 1);
        int index = Mathf.FloorToInt(scaled);
        int next = Mathf.Min(index + 1, path.Length - 1);
        return Vector3.Lerp(path[index], path[next], scaled - index);
    }

    private void Collapse()
    {
        isCollapsed = true;
        collapseTimer = collapseDelay;

        if (collapseHaloPrefab != null && collapseHalo == null)
            collapseHalo = Instantiate(collapseHaloPrefab, transform.position, Quaternion.identity, transform);

        foreach (var l in curves)
            if (l != null) l.enabled = false;

        foreach (var t in tracers)
            if (t != null) t.SetActive(false);

        if (centralCloudEffect != null)
            centralCloudEffect.SetActive(false);
    }

    private void Recover()
    {
        isCollapsed = false;

        if (collapseHalo != null)
        {
            Destroy(collapseHalo);
            collapseHalo = null;
        }

        foreach (var l in curves)
            if (l != null) l.enabled = true;

        foreach (var t in tracers)
            if (t != null) t.SetActive(true);
    }
}
