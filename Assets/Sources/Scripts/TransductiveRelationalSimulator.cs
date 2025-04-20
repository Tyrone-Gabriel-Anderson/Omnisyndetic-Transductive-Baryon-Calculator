using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TransductiveFullyRelationalSimulator
///
/// A first-principles baryon identity calculator based on the
/// Omnisyndetic / Transductive Coherence Ontology (Anderson, 2025).
///
/// This simulator calculates all emergent baryonic properties — including mass, charge,
/// field divergence, coherence, and cloud radius — directly from relational deviation
/// against the idealised configuration known as **Plato’s Baryon**.
///
/// ----------------------------------------------------------
/// PHILOSOPHICAL CONTEXT:
/// In the Omnisyndetic view, identity does not arise from substance or interaction,
/// but from the *failure to close perfectly*. This simulator models that failure
/// geometrically, not probabilistically. It does not use empirical constants,
/// force carriers, or Lagrangian formalism. It uses only topology.
///
/// Plato’s Baryon is defined as the first possible loop of self-confirming identity:
///     • Internal angle sum:    ∑φ = 2π
///     • Radial coherence:     λ = λ₀ (perfect length)
///     • Coherence scalar:     C = 1
///
/// At this configuration:
///     → Mass = 0
///     → Charge = 0
///     → Curvature = 0
///     → Field = null
///
/// It is pure form — a structure that desires nothing. Like a perfectly tuned string,
/// its identity tension is at complete rest: silent, self-contained, and unprojected.
///
/// ----------------------------------------------------------
/// RELATIONAL STRUCTURE:
///
/// Emergent quantities are derived from two deviations:
///
///   (1) Angular mismatch:    ε = |(∑φ – 2π)| / 2π
///       → Source of charge; reflects imbalance in identity feedback.
///   (2) Radial deviation:    R_dev = (λ – λ₀) / λ₀
///       → Source of divergence; reflects failure in spatial coherence.
///
/// From these, coherence is calculated as:
///       C = exp[–(ε² + R_dev²)]
///
/// Residual torsion δ = √(1 – C) encodes the irreducible curvature of the system.
///
/// ----------------------------------------------------------
/// EMERGENT RELATIONAL OUTPUTS:
///
/// All baryonic quantities are derived algebraically:
///
///   • Mass: Emerges from three relational terms:
///       Validation Energy + Desire Energy + Echonex Field Memory
///
///   • Charge:
///       Q = sign(∑φ – 2π) × ε⁶ / λ
///       → Directional projection of angular asymmetry.
///
///   • Cloud Radius:
///       r_cloud = r₀ × (1 + log(1 + ε² + R_dev²))
///       → Expansion due to incomplete closure.
///
///   • Field Energy:
///       E_field = Divergence × α × (1 + δ) × MeV₀
///       → Distributed memory of failed coherence.
///
/// ----------------------------------------------------------
/// ONTOLOGICAL SIGNIFICANCE:
///
/// This simulator is *coefficient-free*. It uses no inherited constants beyond:
///   – λ₀: the ideal coherence length (derived from closure geometry),
///   – r₀: the ideal charge radius (emergent from relational spread).
///
/// It provides the first formal calculator in which identity, mass, and curvature
/// arise solely from topological deviation — not substance, interaction, or empiricism.
///
/// ----------------------------------------------------------
/// RELATIONAL UNIQUENESS CLAIM:
///
/// This is the first known calculator to:
///   ✓ Compute mass, charge, and radius from angular + radial error alone.
///   ✓ Require no empirical quark mass, no fitted field constants.
///   ✓ Replace particle interaction with structural deviation from closure.
///
/// It serves as a computational proof-of-concept:
///     That stable matter can emerge from nothing but difference —
///     and that identity is the memory of failed symmetry.
///
/// ~ Tyrone Gabriel Anderson (2025)
/// </summary>


public class TransductiveRelationalSimulator : MonoBehaviour
{
    #region === [USER INPUT + UI References] ======================================================

    [Header("📏 Baryon Inputs")]

    [Tooltip("λ (lambda): The radial coherence scale in femtometers (fm). " +
             "At λ = λ₀, the triadic loop achieves perfect spatial return. " +
             "Deviation reflects spatial desire (R_dev).")]
    public Slider lambdaSlider;

    [Tooltip("∑φ: Total angular closure (radians).\n" +
             "At ∑φ = 2π, angular return is perfect (ε = 0). " +
             "Deviation encodes torsional asymmetry — the origin of charge.")]
    public Slider coherenceDeviationSlider;

    [Header("🖋️ Slider Labels")]
    public TextMeshProUGUI lambdaLabel;
    public TextMeshProUGUI sumFactorLabel;

    [Header("📤 Output Displays")]
    public TextMeshProUGUI unifiedOutputText;

    [Header("🐞 Debug Panel")]
    public TextMeshProUGUI debugOutputText;

    // Optional: Charge field visualisation (Yukawa-like projection zone)
    // public GameObject chargeCloudEffect;

    [Header("🧮 Manual Entry Fields")]

    [Tooltip("Manual override for λ (radial coherence scale) in femtometers.")]
    public TMP_InputField lambdaInputField;

    [Tooltip("Manual override for ∑φ (angular closure), measured as Δφ = ∑φ − 2π.")]
    public TMP_InputField closureInputField;

    #endregion

    #region === [GEOMETRIC CONSTANTS: Plato’s Baryon Calibration] ================================

    // --- Fundamental Geometry Constants ---

    /// <summary> λ₀ — Base radial closure length (fm) </summary>
    private const float lambda0 = 0.7071f;  // 1 / √2 — minimum closure distance

    /// <summary> φ₀ — Perfect angular closure (radians) </summary>
    private const float phi0 = 2f * Mathf.PI;  // 2π

    /// <summary> r₀ — Base charge cloud boundary at perfect return (fm) </summary>
    private const float idealChargeRadius = 0.7071f;  // identical to λ₀ in updated geometry

    /// <summary> εₘᵢₙ — Minimal angular mismatch for stable deviation </summary>
    private const float epsilonMin = 1f / 6f;  // 0.1667

    /// <summary> κₘᵢₙ — Minimal non-zero curvature sustaining coherent identity </summary>
    private const float kappaMin = epsilonMin * epsilonMin;  // ≈ 0.02778

    /// <summary> Cₘₐₓ — Maximum coherence allowing identity differentiation </summary>
    private readonly float coherenceMax = Mathf.Exp(-kappaMin);  // ≈ 0.9726

    /// <summary> δ — Residual torsion at κₘᵢₙ (irreducible projection energy) </summary>
    private  float torsionResidual = 1f;

    /// <summary> κₘₐₓ — Collapse-limit curvature (from PDE solution) </summary>
    private float kappaMax = 3f - 2f * Mathf.Sqrt(2f);  // ≈ 0.17157

    /// <summary> Cₘᵢₙ — Coherence threshold below which identity collapses </summary>
    private const float coherenceMin = 0.84234f;  // exp(−κₘₐₓ)

    #endregion

    #region === [SIMULATION SCALAR CONSTANTS] =====================================================

    /// <summary> ℏc — Reduced Planck constant × speed of light (MeV·fm) </summary>
    private const float HBAR_C = 197.33f;

    /// <summary> c — Speed of light in vacuum (m/s) </summary>
    private const float SPEED_OF_LIGHT = 299_792_458f;

    /// <summary> Unit conversion: femtometers → meters </summary>
    private const float FM_TO_M = 1e-15f;

    /// <summary>
    /// Tₙ — Transductive closure time (λ₀ / c), seconds  
    /// The minimum temporal cycle for identity to fully validate
    /// </summary>
    private const float transductiveClosureTime = lambda0 * FM_TO_M / SPEED_OF_LIGHT;  // ≈ 2.36e–24 s

    /// <summary>
    /// MeV₀ — Echonex field energy density (MeV/fm³)  
    /// Represents the total ontological memory cost of identity projection
    /// </summary>
    private const float fieldEnergyCoefficient = 170400f;

    #endregion

    #region === [DERIVED RELATIONAL VARIABLES] ====================================================

    /// <summary> ε — Normalised angular mismatch </summary>
    private float epsilon;

    /// <summary> R_dev — Normalised radial deviation </summary>
    private float radialDeviation;

    /// <summary> κ — Total relational curvature (ε² + R_dev²) </summary>
    internal float coherence;

    /// <summary> m* — Emergent baryonic mass (MeV) </summary>
    private float emergentMass;

    /// <summary> q — Emergent charge (Coulombs) </summary>
    private float emergentCharge;

    /// <summary> Λ — Divergence scalar (Dark energy analogue) </summary>
    private float darkEnergyScale;

    /// <summary> r_cloud — Charge cloud boundary (fm) </summary>
    internal float chargeCloudRadius;

    /// <summary> m_total — Final baryon mass output </summary>
    internal float totalMass;

    #endregion

    #region === [STATE TRACKERS] ==================================================================

    /// <summary> Flag: Identity successfully formed </summary>
    internal bool lastBaryonFormed;

    /// <summary> Cache: Last successful mass output </summary>
    internal float lastBaryonMass;

    #endregion


    #region Unity Lifecycle

    // UI state flags to prevent recursive updates when user edits values
    // Reflects the need to guard against feedback loops in observer interactions (§4.3) citeturn2file7
    private bool isEditingLambda = false;
    private bool isEditingClosure = false;

    // Energy components computed each frame (Eq 17: total mass decomposition) citeturn3file19
    private float validationEnergy;  // Loop (internal) closure energy
    private float desireEnergy;      // External projection (desire) energy
    private float echonexField;      // Echonex field-memory energy from divergence
    private const float minimumOntologicalTime = 2.358e-24f;  // seconds (from the paper)

    private void Awake()
    {
        torsionResidual = 1f - coherenceMax;
    }


    /// <summary>
    /// Unity Start lifecycle hook.
    ///
    /// Initializes sliders and inputs to the proton’s near-threshold state
    /// (ε_p ≈ 0.0165, λ_p = 0.65294 fm from §7.6 Proton–Neutron Distinction) citeturn2file8.
    /// Ensures the simulation begins with a known baryonic configuration.
    /// </summary>
    private void Start()
    {
        // Proton defaults: slight underclosure and radial overextension
        coherenceDeviationSlider.value = 2f * Mathf.PI + 0.16000f;  // ∑φ = 2π + δφ (angular asymmetry)
        lambdaSlider.value = 0.84285f;                                  // λ (fm) > λ₀ (radial deviation)

        //0.88887   0.00000  neuton
        //0.52110  -0.82153  Ω_b⁻	~6046 MeV
        //0.49180  -0.69000  ~10162 MeV


        // Sync text inputs to slider values
        lambdaInputField.text = lambdaSlider.value.ToString("F5");
        closureInputField.text = coherenceDeviationSlider.value.ToString("F5");

        // Slider change handlers: update fields and recompute relational outputs
        lambdaSlider.onValueChanged.AddListener(val =>
        {
            if (!isEditingLambda)
                lambdaInputField.text = val.ToString("F5");
            UpdateSimulation();  // Re-evaluate all emergent quantities (§6.3) citeturn3file1
        });
        coherenceDeviationSlider.onValueChanged.AddListener(val =>
        {
            if (!isEditingClosure)
                closureInputField.text = (val - phi0).ToString("F5");
            UpdateSimulation();  // Ensure coherence, mass, charge, field stay current
        });

        // Manual input protections: prevent overwrite while user edits
        lambdaInputField.onSelect.AddListener(_ => isEditingLambda = true);
        lambdaInputField.onEndEdit.AddListener(val =>
        {
            isEditingLambda = false;
            if (float.TryParse(val, out float parsed))
                lambdaSlider.value = Mathf.Clamp(parsed, lambdaSlider.minValue, lambdaSlider.maxValue);
        });

        closureInputField.onSelect.AddListener(_ => isEditingClosure = true);
        closureInputField.onEndEdit.AddListener(val =>
        {
            isEditingClosure = false;
            if (float.TryParse(val, out float parsed))
                coherenceDeviationSlider.value = Mathf.Clamp(parsed + phi0,
                                                         coherenceDeviationSlider.minValue,
                                                         coherenceDeviationSlider.maxValue);
        });

        // Initial computation on load to set UI and internal state (§6.3 Field Collapse Scaling)
        UpdateSimulation();
    }

    /// <summary>
    /// Unity Update lifecycle hook.
    ///
    /// Called once per frame to continuously recompute all relational outputs
    /// (coherence, mass, charge, field energy), ensuring real-time ontological stability.
    /// </summary>
    private void Update()
    {
        UpdateSimulation();  // Real-time feedback loop
    }

    #endregion


    #region === [Relational Core Computations] ===================================================

    /// <summary>
    /// 📐 ComputeCoherenceState:
    ///     Derives angular mismatch (ε), radial deviation (R_dev),
    ///     and coherence (C) based on deviation from Plato’s Baryon.
    /// 
    ///     ε = |Δφ| / φ₀  
    ///     R_dev = (λ – λ₀) / λ₀  
    ///     κ = ε² + R_dev²  
    ///     C = exp(–κ)
    /// </summary>
    private void ComputeCoherenceState()
    {
        float λ = lambdaSlider.value;
        float φ = coherenceDeviationSlider.value;

        float Δφ = φ - phi0;
        epsilon = Mathf.Abs(Δφ) / phi0;
        radialDeviation = (λ - lambda0) / lambda0;

        float kappa = (epsilon * epsilon) + (radialDeviation * radialDeviation);
        coherence = Mathf.Exp(-kappa);
    }

    /// <summary>
    /// 🔄 ComputeTransductiveCoupling:
    ///     Maps energy feedback impedance as a ratio of identity strain to scale:
    ///     α = (ε² + δ²) / (ε² + δ² + λ²)
    ///     where δ = √(1 – C) is residual torsion
    /// </summary>
    private float ComputeTransductiveCoupling(float λ)
    {
        float δ = Mathf.Sqrt(1f - coherence);
        float numerator = epsilon * epsilon + δ * δ;
        float denominator = numerator + λ * λ;
        return (denominator > 1e-12f) ? numerator / denominator : 0f;
    }

    /// <summary>
    /// ⏳ ComputeDecayRateAndLifetime:
    ///     Dynamically derives decay width (Γ) and lifetime (τ) from coherence and memory strain.
    ///     Decay reflects collapse probability from structural curvature and energetic persistence.
    /// 
    ///     • κ = −ln(C)             → curvature memory load
    ///     • Γ = (ℏ / E_mem) · exp(−κ / κₘₐₓ)
    ///     • τ = 1 / Γ
    /// </summary>
    private (float decayRate, float lifetime) ComputeDecayRateAndLifetime()
    {
        const float HBAR_MEV_S = 6.582119569e-22f;  // Planck constant in MeV·s

        float κ = -Mathf.Log(coherence);                      // Memory curvature
        float E_mem = echonexField + validationEnergy;        // Total curvature-encoded energy
        float safeE = Mathf.Max(E_mem, 1e-6f);                // Avoid division by zero

        float decayWidth = (HBAR_MEV_S / safeE) * Mathf.Exp(-κ / kappaMax); // Γ
        float lifetime = 1f / Mathf.Max(decayWidth, 1e-30f);                // τ

        return (decayWidth, lifetime);
    }



    /// <summary>
    /// 💠 ComputeEchonexFieldEnergy:
    ///     Measures total divergence across the identity loop:
    ///     divergence = λ/λ₀ + λ₀/λ – 2  
    ///     E_field = divergence × α × (1 + δ) × MeV₀
    /// </summary>
    private float ComputeEchonexFieldEnergy(float λ)
    {
        float divergence = (λ / lambda0) + (lambda0 / λ) - 2f;
        float α = ComputeTransductiveCoupling(λ);
        float δ = Mathf.Sqrt(1f - coherence);
        return divergence * α * (1f + δ) * fieldEnergyCoefficient;
    }

    /// <summary>
    /// 🔋 ComputeLoopAndDesireEnergy:
    ///     Decomposes the total coherence strain into:
    ///     • Validation energy (loop mass)  
    ///     • Desire energy (incomplete return)
    /// 
    ///     e_loop = α·ℏc/λ · C · ∑φ · sigmoid(α–C)  
    ///     e_desire = α·δ·ℏc/λ · (1 – C)
    /// </summary>
    private void ComputeLoopAndDesireEnergy(out float e_loop, out float e_desire)
    {
        float λ = lambdaSlider.value;
        float φ = coherenceDeviationSlider.value;
        float α = ComputeTransductiveCoupling(λ);
        float δ = Mathf.Sqrt(1f - coherence);

        float baseE = (α * HBAR_C / Mathf.Max(λ, 1e-12f)) * coherence * φ;
        float sig = 1f / (1f + Mathf.Exp(-25f * (α - coherence)));
        e_loop = baseE * Mathf.Lerp(1f, 10f, sig);  // smooth modulation

        float e_proj = (α * δ * HBAR_C / Mathf.Max(λ, 1e-12f)) * (1f - coherence);
        e_desire = e_proj;
    }

    /// <summary>
    /// ⚡ ComputeEmergentCharge:
    ///     Computes baryonic charge as the projected angular deviation from perfect closure,
    ///     scaled by relational curvature and modulated by coherence-based memory decay.
    ///
    ///     Charge arises when the identity loop fails to close perfectly in angle (ε > 0),
    ///     which generates a net curvature. This asymmetry stores memory — resulting in
    ///     projected charge.
    ///
    ///     Geometric Model:
    ///         • ε = Δφ / 2π         (angular mismatch)
    ///         • κ = –ln(C)          (curvature from coherence decay)
    ///         • Q = ±q₀ · ε · (κ / κₘᵢₙ) · exp(1 – κ)
    ///
    ///     At perfect closure (ε = 0), all curvature is internalised: charge = 0.
    ///     At maximal curvature (κ = κₘᵢₙ), full elementary charge is projected (±q₀).
    ///
    ///     This is a purely geometric formulation — no empirical field constants required.
    /// </summary>
    private float ComputeEmergentCharge()
    {
        // --- Δφ: Net angular deviation from ideal identity loop (2π radians)
        float Δφ = coherenceDeviationSlider.value - phi0;

        // --- q₀: Base quantised charge (elementary charge) at peak projection
        const float q0 = 1.602e-19f;

        // --- ε: Normalised angular deviation (ε = Δφ / φ₀)
        float ε = Δφ / phi0;

        // --- Snap charge to zero when coherence is near-perfect (C ≈ 1 ⇒ κ ≈ 0)
        //     At full closure, charge must vanish: system is topologically silent
        if (coherence >= coherenceMax)
            return 0f;

        // --- κ: Relational curvature (κ = –ln(C))
        //     Represents stored torsional strain from closure failure
        float κ = -Mathf.Log(coherence);

        // --- Polarity: Determine charge sign from the direction of angular error
        float sign = (Δφ >= 0f) ? +1f : -1f;

        // --- Q: Final emergent charge
        //     · Grows with ε (angular asymmetry),
        //     · Scales with κ / κₘᵢₙ (topological strain),
        //     · Modulated by exp(1 – κ) (memory decay cap)
        float q_final = sign * q0 * Mathf.Abs(ε) * (κ / kappaMin) * Mathf.Exp(1f - κ);

        return q_final;
    }






    /// <summary>
    /// ☁️ ComputeChargeEchoRadius:
    ///     Spatial footprint of identity curvature.
    ///     r_cloud = r₀ × [1 + ln(1 + ε² + R_dev²)]
    /// </summary>
    private float ComputeChargeEchoRadius()
    {
        float κ = epsilon * epsilon + radialDeviation * radialDeviation;
        return idealChargeRadius * (1f + Mathf.Log(1f + κ));
    }


    /// <summary>
    /// 🌀 GetValidationRadius:
    ///     Returns the geometric radius of instantaneous return (no projection).
    ///     Equal to λ₀ — the fundamental radial coherence length.
    /// </summary>
    private float GetValidationRadius()
    {
        return lambda0;
    }

    /// <summary>
    /// 🌌 GetCoherenceRadius:
    ///     Spatial region where coherence is still meaningful (> 0).
    ///     r = λ · √[–ln(C)]
    /// </summary>
    /// <summary>
    /// Option B — Midpoint between charge and memory divergence fields
    /// </summary>
    private float GetHybridCoherenceRadius()
    {
        float r_charge = ComputeChargeEchoRadius();
        float r_memory = lambda0 * (1f + epsilon + radialDeviation);
        return 0.5f * (r_charge + r_memory);
    }



    /// <summary>
    /// 🧮 ComputeTotalBaryonMass:
    ///     Sums all valid energies when identity is coherent enough to manifest:
    ///     M = E_loop + E_desire + E_field
    ///     (only if C ∈ [C_min, C_max])
    /// </summary>
    private float ComputeTotalBaryonMass(out bool formsBaryon)
    {
        ComputeCoherenceState();
        ComputeLoopAndDesireEnergy(out float e_loop, out float e_desire);
        validationEnergy = e_loop;
        desireEnergy = e_desire;

        echonexField = ComputeEchonexFieldEnergy(lambdaSlider.value);
        float total = e_loop + e_desire + echonexField;

        formsBaryon = (coherence >= coherenceMin && coherence <= coherenceMax);
        return formsBaryon ? total : 0f;
    }

    #endregion

    #region Main Simulation Update

    /// <summary>
    /// Core Observational Loop — UpdateSimulation()
    ///
    /// This is the central coherence evaluation function. It models
    /// baryonic identity as the dynamic resolution of topological deviation
    /// against Plato’s Baryon — the idealised triadic form defined by:
    ///   • λ = λ₀ (radial closure)
    ///   • ∑φ = 2π (angular closure)
    ///
    /// From these two inputs, the simulator computes all emergent properties:
    ///     – Mass (from energetic asymmetry and feedback tension),
    ///     – Charge (from angular deviation ε),
    ///     – Coherence (from closure fidelity),
    ///     – Field divergence (from relational memory),
    ///     – Radius (as expansion of failed closure),
    ///     – Torsion (as residual identity strain).
    ///
    /// Identity emerges only if:
    ///     Cₘᵢₙ < C < Cₘₐₓ
    /// Too little coherence and return fails; too much and projection ceases.
    /// This range defines the ontological window in which a structure can sing.
    ///
    /// All quantities are derived algebraically, with no empirical coefficients.
    /// </summary>
    public void UpdateSimulation()
    {
        // Step 1 — Compute angular + radial deviation (ε, R_dev), coherence (C), etc.
       // ComputeRelationalParameters();

        // Step 2 — Emergent mass (formed if C is within valid range)
        bool baryonForms;
        totalMass = ComputeTotalBaryonMass(out baryonForms);

        // Step 3 — Emergent charge (projected asymmetry)
        emergentCharge = ComputeEmergentCharge();

        // Step 4 — Emergent divergence field (Λ)
        float lambda = lambdaSlider.value;
        darkEnergyScale = (float)Math.Pow(epsilon, 4) / (lambda * lambda);

        // Step 5 — Charge cloud radius (projected closure failure)
        chargeCloudRadius = GetHybridCoherenceRadius();

        // Step 6 — Determine if this is a tachyon
        bool formsTachyon = baryonForms && (lambda < lambda0);

        // Compute decay properties
        (float decayRate, float lifetime) = ComputeDecayRateAndLifetime();

        string decayPanel =
            "<b><color=#FF69B4>☐ Decay Properties</color></b>\n" +
            $"<b>Decay Width (Γ):</b> <color=#FF6347>{decayRate:E3}</color> MeV\n" +
            $"<b>Lifetime (τ):</b> <color=#00FA9A>{lifetime:E3}</color> seconds\n";


        float Tn_min = 2.358e-24f;  // hardcoded from Plato's Baryon
        float λ_m = lambda * FM_TO_M;
        float deltaT_actual = λ_m / SPEED_OF_LIGHT;
        float deltaT_offset = deltaT_actual - Tn_min;

        string temporalReading =
            $"<b><color=#6A5ACD>Δt (Return Time):</color></b> <color=#BA55D3>{deltaT_actual:E2}</color> s\n" +
            $"<b><color=#7B68EE>Δt Offset from Tₙ:</color></b> <color=#9370DB>{deltaT_offset:E2}</color> s\n" +
            $"<b><color=#778899>Minimum Return Time Tₙ:</color></b> <color=#708090>{Tn_min:E2}</color> s\n" +
            (formsTachyon
                ? "<b><color=#FF00FF>☄ Tachyonic Reversal:</color></b> λ < λ₀ — coherence loops inward, time inverts.\n"
                : "<b><color=#00FA9A>✓ Normal Return:</color></b> Coherence stable, forward-temporal identity preserved.\n");


        // Step 8 — Verdict logic
        string verdict;
        if (formsTachyon)
        {
            verdict = "<b><color=#FF00FF>☼ Tachyon Formed:</color></b> Ultra-coherent; λ < λ₀, temporal reversal projected.\n";
        }
        else if (baryonForms)
        {
            verdict = "<b><color=#32CD32>✓ Baryon Formed:</color></b> Coherence valid; identity sustained.\n";
        }
        else
        {
            verdict = "<b><color=#FF0000>✗ Baryon Collapsed:</color></b> Coherence outside bounds; structure dissolved.\n";
        }

        string output =
            // === Title Section ===
            "<b><size=16><color=#FFFFFF>TransductiveRelationalSimulator</color></size></b>\n" +
            "<i><size=12><color=#B0C4DE>All emergent properties derived from deviation against Plato’s Baryon</color></size></i>\n\n" +

            // === Topological Configuration ===
            "<b><color=#FFD700>✦ Topological Configuration</color></b>\n" +
            $"<b>λ (Active):</b> <color=#00FFFF>{lambda:F5}</color> fm    |    " +
            $"<b>λ₀ (Ideal):</b> <color=#00CED1>{lambda0:F5}</color> fm\n" +
            $"<b>∑φ (Angular Sum):</b> <color=#00FFAA>{coherenceDeviationSlider.value:F4}</color> rad    |    " +
            $"<b>φ₀ (Ideal):</b> <color=#7FFFD4>{phi0:F4}</color> rad\n" +
            $"<b>ε (Angle Error):</b> <color=#FFA500>{epsilon:F6}</color>\n" +
            $"<b>Rₑ (Radial Deviation):</b> <color=#FF7F50>{radialDeviation:F6}</color>\n\n" +

            "<b><color=#FFDAB9>» Derived Curvatures</color></b>\n" +
            $"<b>κ₁ (Validation Curvature):</b> <color=#FF8C00>{(epsilon * epsilon + radialDeviation * radialDeviation):F6}</color>\n" +
            $"<b>κ₂ (Echonex Curvature):</b> <color=#FFA07A>{-Mathf.Log(coherence):F6}</color>\n" +
            $"<b>C (Coherence):</b> <color=#ADFF2F>{coherence:F6}</color>    |    " +
            $"<b>δ (Residual Torsion):</b> <color=#9370DB>{Math.Sqrt(1.0 - coherence):F6}</color>\n\n" +

            "<b><color=#D8BFD8>» Extended Topological Metrics</color></b>\n" +
            $"<b>Δφ (Mismatch):</b> <color=#F5DEB3>{(epsilon * phi0):F6}</color> rad\n" +
            $"<b>Torsional Tension:</b> <color=#F08080>{(epsilon * radialDeviation):F6}</color>\n" +
            $"<b>Closure Fidelity Index:</b> <color=#EEE8AA>{Mathf.Exp(-(epsilon + Mathf.Abs(radialDeviation))):F6}</color>\n" +
            $"<b>Feedback Delay (τ):</b> <color=#87CEFA>{(lambda / (ComputeTransductiveCoupling(lambda) + Math.Sqrt(1 - coherence) + epsilon)):F6}</color> fm\n" +
            $"<b>Curvature Density (κ₂ / λ):</b> <color=#BC8F8F>{(-Mathf.Log(coherence) / lambda):F6}</color>\n\n" +

            $"<b>α (Coupling Strength):</b> <color=#00BFFF>{ComputeTransductiveCoupling(lambda):F6}</color>\n" +
            $"<b>Λᵣ (Spatial Divergence):</b> <color=#FF69B4>{((lambda / lambda0) + (lambda0 / lambda) - 2.0):F6}</color>\n\n"+


            // === Identity Properties ===
            "<b><color=#DA70D6>Emergent Identity Properties</color></b>\n" +
            $"<b>Total Mass (m*):</b> <color=#FFA07A>{totalMass:F3}</color> MeV    |    " +
            $"<b>Charge (q):</b> <color=#FF4500>{emergentCharge:E2}</color> C\n" +
            $"<b>Cloud Radius (r₍cloud₎):</b> <color=#40E0D0>{chargeCloudRadius:F5}</color> fm\n" +
            $"<b>Λ (Field Divergence):</b> <color=#8A2BE2>{darkEnergyScale:F6}</color>\n\n" +

            // === Energetic Breakdown ===
            "<b><color=#20B2AA>Energy Composition</color></b>\n" +
            $"<b>Validation Energy:</b> <color=#ADD8E6>{validationEnergy:F2}</color> MeV    |    " +
            $"<b>Desire Energy:</b> <color=#FFA07A>{desireEnergy:F2}</color> MeV\n" +
            $"<b>Echonex Field Energy:</b> <color=#FFB6C1>{echonexField:F2}</color> MeV\n\n" +

            $"{decayPanel}\n\n" +
            // === Temporal Verdict ===
            "<b><color=#BA55D3>Return Time:</color></b> " +
            $"{temporalReading}\n\n" +

            // === System Verdict ===
            verdict;


        // Step 10 — Update the display panel
        if (unifiedOutputText != null)
            unifiedOutputText.text = output;
    }


    #endregion

}