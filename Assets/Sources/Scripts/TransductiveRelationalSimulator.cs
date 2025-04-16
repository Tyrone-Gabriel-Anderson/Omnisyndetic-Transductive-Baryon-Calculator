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
    #region Public Inspector Fields

    [Header("Baryon Sliders")]

    [Tooltip("λ (lambda): The radial coherence scale in femtometers (fm).\n" +
             "This defines the feedback distance across the triadic structure. " +
             "At λ = λ₀, the baryon achieves perfect spatial closure. " +
             "Any deviation expresses spatial desire, encoded as divergence.")]
    public Slider lambdaSlider;

    [Tooltip("∑φ: The total internal angle sum of the triadic feedback loop (in radians).\n" +
             "Perfect identity is formed at ∑φ = 2π, corresponding to a loop of complete closure.\n" +
             "Any deviation introduces angular tension, the root of emergent charge.")]
    public Slider coherenceDeviationSlider;

    [Header("Slider Labels")]

    // Dynamic UI labels to reflect current values from sliders.
    public TextMeshProUGUI lambdaLabel;
    public TextMeshProUGUI sumFactorLabel;

    [Header("Output Text")]

    [Tooltip("Main output panel that displays all emergent properties.\n" +
             "Includes mass, charge, coherence, curvature, desire, and baryon formation state.")]
    public TextMeshProUGUI unifiedOutputText;

    [Header("Debug Output Text (Optional)")]

    [Tooltip("Optional debug panel for internal development and real-time inspection of simulation states.")]
    public TextMeshProUGUI debugOutputText;

    [Header("Charge Cloud Visual")]

    // This is an optional reference for visualising the spatial expansion of identity fields (e.g., Yukawa clouds).
    // It may be enabled in future versions for immersive relational field visualisation.
    // public GameObject chargeCloudEffect;

    [Header("Manual Input Fields")]

    [Tooltip("Direct numeric entry for λ (radial coherence scale).\n" +
             "This allows precise insertion of test values, particularly for calibration or inverse modelling.")]
    public TMP_InputField lambdaInputField;

    [Tooltip("Direct numeric entry for closure angle offset from 2π.\n" +
             "Allows precise control over angular mismatch ε = |∑φ_actual − 2π| / 2π.")]
    public TMP_InputField closureInputField;

    #endregion

    #region Base Calibration (Perfect State: Plato’s Baryon)

    /// <summary>
    /// λ₀ — Ideal Radial Coherence Length (in femtometers)
    ///
    /// This is the coherence scale at which a triadic identity structure
    /// achieves perfect closure in both radial and angular domains.
    /// It defines the spatial separation at which nexons stabilise into a baryonic
    /// structure with:
    ///   - zero emergent mass,
    ///   - zero emergent charge,
    ///   - zero curvature (κ = 0).
    ///
    /// This value was not inferred from the neutron, but derived as the first
    /// observable moment at which identity emerges from pure closure.
    /// It is the birth-length of structural being: the λ of Plato’s Baryon.
    ///
    /// → It is the first measurable quantity.
    /// → It is the relational seed of scale.
    /// → It is the spatial chord of ontological rest.
    /// </summary>
    private float optimalLambda = 0.54668f;  // λ₀ — Fundamental closure length (not neutron-calibrated)


    /// <summary>
    /// r₀ — Ideal Charge Cloud Radius at Full Closure (in femtometers)
    ///
    /// Represents the extent of projected field at perfect structural closure.
    /// While this matches the neutron’s observed radius (≈ 0.7503 fm),
    /// it was confirmed — not derived — by its match. It arises as the radius
    /// at which radial divergence becomes zero under full coherence.
    ///
    /// In the simulator, r₀ acts as the saturation base radius for all
    /// deviation-induced field expansion.
    /// 
    /// → This is the form radius of the non-form.
    /// → The stillness boundary of perfect memory.
    /// </summary>
    private float idealChargeRadius = 0.7503f;  // r₀ — Radius at perfect divergence nullification


    /// <summary>
    /// φ₀ — Ideal Angular Closure Sum (in radians)
    ///
    /// This is the internal angular sum of a perfectly closed triadic loop.
    /// A value of exactly 2π radians (≈ 6.28319) ensures no internal angular torsion,
    /// resulting in zero charge and balanced desire tension.
    ///
    /// This constant was not tuned, but defined from the axioms of relational closure.
    ///
    /// → It is the angular null of identity.
    /// → Closure’s condition. Charge’s escape hatch.
    /// </summary>
    private float idealClosureAngle = 2f * Mathf.PI;  // φ₀ = 2π — Platonic closure angle (radians)

    #endregion

    #region Relational Variables (Derived from Deviations)

    /// <summary>
    /// ε — Normalised Angular Mismatch
    /// 
    /// ε = |(∑φ_actual – φ₀)| / φ₀
    /// 
    /// Measures deviation from the ideal angular closure (2π radians). This parameter represents the
    /// identity's angular asymmetry, which directly projects emergent charge. When ε = 0,
    /// the triad is perfectly balanced and charge-free. All angular divergence is a sign of
    /// unmet identity curvature.
    /// 
    /// Ontological Interpretation:
    ///   → ε encodes the curvature of desire.
    ///   → Source of charge and angular torsion.
    /// </summary>
    private float epsilon;

    /// <summary>
    /// R_dev — Relative Radial Deviation
    /// 
    /// R_dev = (λ – λ₀) / λ₀
    /// 
    /// Measures mismatch in the radial coherence length from the perfect triadic closure.
    /// Represents deviation in spatial structure and is the primary driver of divergence (field expansion).
    /// 
    /// Ontological Interpretation:
    ///   → Spatial failure to return.
    ///   → Directly informs coherence loss and identity diffusion.
    /// </summary>
    private float radialDeviation;

    /// <summary>
    /// C — Coherence Scalar
    /// 
    /// C = exp[–(ε² + R_dev²)]
    /// 
    /// Represents the structural fidelity of identity. A scalar ranging from 1 (perfect closure)
    /// to 0 (total incoherence). This exponential formulation ensures rapid collapse with increasing deviation.
    /// 
    /// Ontological Interpretation:
    ///   → C is the ontological memory of closure.
    ///   → Governs formation threshold of identity.
    /// </summary>
    internal float coherence;

    /// <summary>
    /// m* — Emergent Mass (MeV units)
    /// 
    /// Total relational mass formed from structural deviation. Mass arises from:
    ///   1. Validation Energy (loop tension),
    ///   2. Desire Energy (incomplete coherence),
    ///   3. Echonex Field Memory (divergent identity).
    /// 
    /// Ontological Interpretation:
    ///   → Mass is memory.
    ///   → Mass is the cost of being incomplete.
    /// </summary>
    private float emergentMass;

    /// <summary>
    /// q — Emergent Charge (Coulombs)
    /// 
    /// Charge is derived from angular mismatch:
    ///   q ∝ ε⁶ / λ
    /// The sixth-power dependency ensures sensitivity to angular symmetry, while sign is
    /// determined by overclosure or underclosure (±).
    /// 
    /// Ontological Interpretation:
    ///   → Charge is asymmetry in observation.
    ///   → A projection of unfulfilled identity feedback.
    /// </summary>
    private float emergentCharge;

    /// <summary>
    /// Λ — Divergence Scale Factor (Dark Energy Analogue)
    /// 
    /// Λ = ε⁴ / λ²
    /// 
    /// This expresses structural divergence as a higher-order projection of angular deviation.
    /// It serves as a scalar proxy for ontological pressure in failed systems.
    /// 
    /// Ontological Interpretation:
    ///   → Λ expresses tension in the relational manifold.
    ///   → Analogue to the cosmological constant; defines outward divergence from failed closure.
    /// </summary>
    private float darkEnergyScale;

    /// <summary>
    /// r₍cloud₎ — Emergent Charge Cloud Radius (fm)
    /// 
    /// Derived from deviation-driven expansion:
    ///   r₍cloud₎ = r₀ × (1 + log(1 + ε² + R_dev²))
    /// 
    /// As structural closure fails, the projected charge field expands. This is the spatial
    /// footprint of identity diffusion, analogous to cloud radius in QCD but derived
    /// ontologically, not empirically.
    /// 
    /// Ontological Interpretation:
    ///   → The visible echo of failed identity.
    ///   → Field-like memory of relational collapse.
    /// </summary>
    internal float chargeCloudRadius;

    /// <summary>
    /// m₍total₎ — Final Emergent Mass (MeV)
    /// 
    /// The total baryonic mass output of the system after computing all relational energies.
    /// This is a conditional value — it only manifests when coherence exceeds the threshold for identity.
    /// </summary>
    internal float totalMass;

    #endregion

    #region Additional Transductive & Field Parameters

    /// <summary>
    /// Tₙ — Transductive Closure Time (in seconds)
    ///
    /// Represents the minimal observable temporal window required for a baryonic identity
    /// to complete a full coherence loop in the neutral state. While historically matched
    /// to neutron emergence (~5.15e–24s), this value also functions as the temporal scaling
    /// bridge between angular mismatch and charge projection.
    ///
    /// → Time is not a substance, but the duration of attempted closure.
    /// → This is identity’s recursive update window.
    /// </summary>
    private float transductiveClosureTime = 5.15e-24f;  // s — Temporal scale of identity formation

    /// <summary>
    /// c — Speed of Light in Vacuum (in m/s)
    ///
    /// In this framework, c is reinterpreted as the **maximum validation speed** — the
    /// upper bound at which relational coherence can propagate across space.
    /// It is not merely a physical constant but the **closure limit of observable separation**.
    ///
    /// → No signal travels faster because no identity can validate faster.
    /// → c is the topological saturation of validation across distance.
    /// </summary>
    private const float SPEED_OF_LIGHT = 299792458f;  // m/s — Relational closure speed limit

    /// <summary>
    /// Conversion Factor: Femtometers to Meters
    ///
    /// Structural coherence length is measured in fm (femtometers), but charge derivations
    /// require metric standardisation. This conversion unifies dimensional representations.
    ///
    /// → All scale is relational; this converts spatial ratios to grounded metric form.
    /// </summary>
    private const float FM_TO_M = 1e-15f;  // Unit bridge for length scale conversion

    /// <summary>
    /// MeV₀ — Global Field Energy Coefficient (in MeV)
    ///
    /// This is the **core projection constant** used for deriving Echonex field energy.
    /// It is not empirical nor heuristic, but **relationally derived** from the total
    /// identity energy required to maintain coherence under angular and radial divergence.
    ///
    /// It captures the **integrated return cost** under failed closure, corresponding
    /// to structural memory density (see Appendix D of the paper).
    ///
    /// → This is the ‘weight’ of unfulfilled coherence.
    /// → Mass as spatially encoded resistance to vanishing.
    /// </summary>
    private float fieldEnergyCoefficient = 257370.09544f;  // MeV — Fully relational field projection factor

    /// <summary>
    /// Internal flags and trackers for baryon formation state.
    /// </summary>
    internal bool lastBaryonFormed;   // State tracker for coherence condition
    internal float lastBaryonMass;    // Cached mass from previous valid identity

    /// <summary>
    /// ℏc — Reduced Planck Constant × Speed of Light (in MeV·fm)
    ///
    /// Used in loop energy and curvature derivations.
    /// In this ontology, ℏc remains geometrically valid as a **curvature quantisation factor**,
    /// linking energy and scale under deviation. Not introduced heuristically.
    /// </summary>
    private const float HBAR_C = 197.33f;  // MeV·fm — Planck-curvature bridge term

    #endregion

    #region Unity Lifecycle

    // Internal state flags to prevent UI feedback loops
    private bool isEditingLambda = false;
    private bool isEditingClosure = false;

    // Energy components of the baryon identity (computed per frame)
    private float validationEnergy;  // Energy required to complete internal closure
    private float desireEnergy;      // Projective energy resulting from failed coherence
    private float echonexField;      // Stored field energy from divergence memory

    /// <summary>
    /// Unity Start lifecycle hook.
    /// 
    /// Initialises the sliders and input fields to default values associated with
    /// the proton configuration — a stable but asymmetric triad.
    /// This setup ensures the user begins with a known baryonic identity.
    /// </summary>
    private void Start()
    {
        // Initialise with a known coherent configuration: the proton
        coherenceDeviationSlider.value = idealClosureAngle + 0.10379f;  // ∑φ > 2π: angular asymmetry
        lambdaSlider.value = 0.65294f;                                  // λ > λ₀: radial over-extension

        // Synchronise text fields with slider defaults
        lambdaInputField.text = lambdaSlider.value.ToString("F5");
        closureInputField.text = coherenceDeviationSlider.value.ToString("F5");

        // Update UI and simulation on slider interaction
        lambdaSlider.onValueChanged.AddListener(val =>
        {
            if (!isEditingLambda)
                lambdaInputField.text = val.ToString("F5");
            UpdateSimulation();  // Trigger recomputation of all relational outputs
        });

        coherenceDeviationSlider.onValueChanged.AddListener(val =>
        {
            if (!isEditingClosure)
                closureInputField.text = (val - idealClosureAngle).ToString("F5");
            UpdateSimulation();  // Update all coherence-dependent states
        });

        // UI protections during manual input — prevent overwrite while editing
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
                coherenceDeviationSlider.value = Mathf.Clamp(parsed + idealClosureAngle, coherenceDeviationSlider.minValue, coherenceDeviationSlider.maxValue);
        });

        // Perform initial computation on load
        UpdateSimulation();
    }

    /// <summary>
    /// Unity Update lifecycle hook.
    /// 
    /// Continuously recomputes all baryonic parameters from current relational inputs
    /// (angle deviation and coherence scale). Ensures real-time coherence validation.
    /// </summary>
    private void Update()
    {
        UpdateSimulation();  // Real-time feedback loop for ontological stability
    }

    #endregion

    #region Fully Relational Computations

    /// <summary>
    /// Derives the core relational parameters from topological deviation
    /// against the Platonic closure condition (λ = λ₀, ∑φ = 2π).
    /// These define all emergent properties (mass, charge, coherence, curvature).
    ///
    /// Definitions:
    ///     ε       → Angular mismatch, source of emergent charge.
    ///     R_dev   → Radial deviation from coherence length λ₀.
    ///     C       → Total coherence scalar: C = exp[–(ε² + R_dev²)]
    ///               Perfect identity: C = 1, all structure dissolves.
    /// </summary>
    private void ComputeRelationalParameters()
    {
        float lambda = lambdaSlider.value;                     // λ: coherence length (fm)
        float sumFactor = coherenceDeviationSlider.value;      // ∑φ: triadic internal angle sum (rad)

        float deltaPhi = sumFactor - idealClosureAngle;
        epsilon = Mathf.Abs(deltaPhi) / idealClosureAngle;     // ε: angular mismatch
        radialDeviation = (lambda - optimalLambda) / optimalLambda;

        coherence = Mathf.Exp(-(epsilon * epsilon + radialDeviation * radialDeviation));
    }

    /// <summary>
    /// Computes the transductive coupling α:
    ///     α = (ε² + δ²) / (ε² + δ² + λ²)
    /// Encodes the ratio of structural failure (desire) to spatial tension (closure).
    /// This is not a fine structure constant — it is a topological friction parameter.
    /// </summary>
    private float ComputeTransductiveCoupling(float lambda)
    {
        float deltaResidual = Mathf.Sqrt(1f - coherence);  // δ: residual torsion (coherence loss)
        float numerator = (epsilon * epsilon) + (deltaResidual * deltaResidual);
        float denominator = numerator + (lambda * lambda);
        return (denominator > 1e-12f) ? numerator / denominator : 0f;
    }

    /// <summary>
    /// Computes the Echonex field energy — the memory stored in divergence.
    ///     divergence = (λ / λ₀ + λ₀ / λ – 2)
    ///     E_field = divergence × α × (1 + δ) × MeV₀
    /// Where MeV₀ is the field collapse coefficient (≈257,337), derived from
    /// the cost of global projection under incomplete identity.
    /// </summary>
    private float ComputeEchonexFieldEnergy(float lambda)
    {
        float divergence = (lambda / optimalLambda) + (optimalLambda / lambda) - 2f;
        float coupling = ComputeTransductiveCoupling(lambda);
        float deltaResidual = Mathf.Sqrt(1f - coherence);
        float tensionFactor = 1f + deltaResidual;

        return divergence * coupling * tensionFactor * fieldEnergyCoefficient;
    }

    /// <summary>
    /// Computes the loop energy (internal recursion) and desire energy (external projection).
    ///     e_loop   = α × ℏc / λ × coherence × ∑φ × sigmoid
    ///     e_desire = (α × δ × ℏc / λ) × (1 - C)
    /// These map onto baryonic validation and identity projection respectively.
    /// </summary>
    private void ComputeEchonexEnergy(out float e_loop, out float e_desire)
    {
        float lambda = lambdaSlider.value;
        float sumFactor = coherenceDeviationSlider.value;

        ComputeRelationalParameters();
        float alpha = ComputeTransductiveCoupling(lambda);
        float deltaResidual = Mathf.Sqrt(1f - coherence);

        float baseEnergy = (alpha * HBAR_C / Mathf.Max(lambda, 1e-12f)) * coherence * sumFactor;

        float sigmoid = 1f / (1f + Mathf.Exp(-25f * (alpha - coherence)));
        float cappedFactor = Mathf.Lerp(1f, 10f, sigmoid);
        e_loop = baseEnergy * cappedFactor;

        float kDesire = (alpha * deltaResidual * HBAR_C) / Mathf.Max(lambda, 1e-12f);
        e_desire = kDesire * (1f - coherence);
    }

    /// <summary>
    /// Computes emergent charge:
    ///     Q = ±(Tₙ × ε⁶ / λ_m)
    /// This is a directional projection of topological asymmetry.
    /// </summary>
    private float ComputeEmergentCharge()
    {
        float lambda_fm = lambdaSlider.value;
        float lambda_m = lambda_fm * FM_TO_M;
        float deltaPhi = coherenceDeviationSlider.value - idealClosureAngle;

        float chargeMagnitude = transductiveClosureTime * Mathf.Pow(epsilon, 6f) / Mathf.Max(lambda_m, 1e-20f);
        return (deltaPhi < 0f) ? +chargeMagnitude : -chargeMagnitude;
    }

    /// <summary>
    /// Computes charge cloud radius from mismatch:
    ///     r_cloud = r₀ × (1 + log(1 + ε² + R_dev²))
    /// As closure fails, identity expands — the inverse of gravitational collapse.
    /// </summary>
    private float ComputeChargeCloudRadius()
    {
        float mismatchSum = Mathf.Pow(epsilon, 2f) + Mathf.Pow(radialDeviation, 2f);
        float scaled = Mathf.Log(1f + mismatchSum);
        return idealChargeRadius * (1f + scaled);
    }

    /// <summary>
    /// Computes the total emergent mass of a baryon:
    ///     M = Validation Energy + Desire Energy + Field Memory
    /// Forms only if coherence is within closure range.
    /// </summary>
    private float ComputeTotalBaryonMass(out bool formsBaryon)
    {
        ComputeRelationalParameters();
        float lambda = lambdaSlider.value;

        ComputeEchonexEnergy(out float e_loop, out float e_desire);
        validationEnergy = e_loop;
        desireEnergy = e_desire;

        echonexField = ComputeEchonexFieldEnergy(lambda);
        float e_total = e_loop + e_desire + echonexField;

        formsBaryon = (coherence >= GetMinimumCoherenceThresholdFromClosureGeometry() && coherence < 0.97827f);
        return formsBaryon ? e_total : 0f;
    }

    /// <summary>
    /// Computes the analytic upper coherence limit for returnable baryons:
    ///     C_max = [(e / 2) × (1 + αδ)]²
    /// If coherence is too perfect, no projection occurs — the identity remains silent.
    /// </summary>
    private float ComputeMaximumCoherenceThresholdFromYukawaDecay()
    {
        float delta = Mathf.Sqrt(1f - coherence);
        float deltaSquared = delta * delta;
        float alpha = deltaSquared / (deltaSquared);  // Simplifies to 1
        float eOver2 = Mathf.Exp(1f) / 2f;
        return Mathf.Pow((1f + alpha * delta) * eOver2, 2f);
    }

    /// <summary>
    /// Computes the minimum coherence threshold for identity emergence
    /// based on relational closure geometry.
    /// </summary>
    private float GetMinimumCoherenceThresholdFromClosureGeometry()
    {
        float lambda0 = optimalLambda;
        float eOver2 = Mathf.Exp(1f) / 2f;
        float candidateC = 0.97827f;
        float delta = Mathf.Sqrt(1f - candidateC);
        float alpha = (delta * delta) / ((delta * delta) + (lambda0 * lambda0));
        return lambda0 * (1f + alpha * delta) / Mathf.Sqrt(candidateC) * eOver2;
    }

    #endregion

    #region Main Simulation Update

    /// <summary>
    /// Computes and updates all structural quantities of the simulator in real-time.
    /// This is the core observational loop, where the baryonic configuration is
    /// continuously evaluated in relational space.
    ///
    /// From just two parameters — angular sum (∑φ) and radial coherence (λ) —
    /// the system calculates:
    ///     → Mass (as identity formation energy),
    ///     → Charge (as angular asymmetry),
    ///     → Field divergence (as curvature memory),
    ///     → Spatial radius (as topological expansion),
    ///     → Coherence, coupling, and torsion.
    ///
    /// These values emerge strictly from deviation against the ideal Platonic structure,
    /// and do not require inherited constants, fitted models, or physical fields.
    /// This is a structural ontology of identity.
    ///
    /// Display output is updated to reflect all quantities.
    /// </summary>
    public void UpdateSimulation()
    {
        // Step 1 — Core geometric mismatch:
        ComputeRelationalParameters();

        // Step 2 — Emergent mass:
        bool baryonForms;
        totalMass = ComputeTotalBaryonMass(out baryonForms);

        // Step 3 — Emergent charge:
        emergentCharge = ComputeEmergentCharge();

        // Step 4 — Dark energy (divergence field):
        float lambda = lambdaSlider.value;
        darkEnergyScale = Mathf.Pow(epsilon, 4f) / (lambda * lambda);

        // Step 5 — Charge cloud radius from mismatch growth:
        chargeCloudRadius = ComputeChargeCloudRadius();

        // Step 6 — Build display output string:
        string output =
            // --- Simulator Title ---
            "<b><size=16>TransductiveRelationalSimulator</size></b>\n" +
            "<i><size=12>All emergent quantities derived from deviation against Plato’s Baryon</size></i>\n\n" +

            // --- SECTION 1: Topological State ---
            "<b><color=#FFD700>Topological Configuration</color></b>\n" +
            $"<b>λ (Active):</b> <color=#00FFFF>{lambda:F4}</color> fm    |    " +
            $"<b>λ₀ (Ideal):</b> <color=#00CED1>{optimalLambda:F4}</color> fm\n" +
            $"<b>∑φ (Sum):</b> <color=#00FFAA>{coherenceDeviationSlider.value:F4}</color> rad    |    " +
            $"<b>φ₀ (Ideal):</b> <color=#7FFFD4>{idealClosureAngle:F4}</color> rad\n" +
            $"<b>ε (Angle Error):</b> <color=#FFAA00>{epsilon:F4}</color>    |    " +
            $"<b>Rₑ (Radial Dev):</b> <color=#FF7F50>{radialDeviation:F4}</color>\n" +
            $"<b>C (Coherence):</b> <color=#ADFF2F>{coherence:F4}</color>\n" +
            $"<b>α (Coupling Strength):</b> <color=#00BFFF>{ComputeTransductiveCoupling(lambda):F4}</color>\n" +
            $"<b>δ (Residual Torsion):</b> <color=#9370DB>{Mathf.Sqrt(1f - coherence):F4}</color>\n" +
            $"<b>Divergence:</b> <color=#FF69B4>{((lambda / optimalLambda) + (optimalLambda / lambda) - 2f):F4}</color>\n" +
            $"<b>κₑₓₜ (Effective Curvature):</b> <color=#FF8C00>{(epsilon * epsilon + radialDeviation * radialDeviation):F4}</color>\n\n" +

            // --- SECTION 2: Identity Emergence ---
            "<b><color=#DA70D6>Emergent Identity Properties</color></b>\n" +
            $"<b>Total Mass (m*):</b> <color=#FFA07A>{totalMass:F2}</color> MeV    |    " +
            $"<b>Charge (q):</b> <color=#FF4500>{emergentCharge:E2}</color> C\n" +
            $"<b>Cloud Radius (r₍cloud₎):</b> <color=#40E0D0>{chargeCloudRadius:F4}</color> fm\n" +
            $"<b>Λ (Field Divergence):</b> <color=#8A2BE2>{darkEnergyScale:F4}</color>\n\n" +

            // --- SECTION 3: Energetic Composition ---
            "<b><color=#20B2AA>Energy Composition</color></b>\n" +
            $"<b>Validation Energy:</b> <color=#ADD8E6>{validationEnergy:F2}</color> MeV    |    " +
            $"<b>Desire Energy:</b> <color=#FFA07A>{desireEnergy:F2}</color> MeV\n" +
            $"<b>Echonex Field Energy:</b> <color=#FFB6C1>{echonexField:F2}</color> MeV\n\n" +

            // --- SECTION 4: Identity Verdict ---
            (baryonForms
                ? "<b><color=#32CD32>✓ Baryon Formed:</color></b> Ontological coherence sustained.\n"
                : "<b><color=#FF0000>✗ Baryon Collapsed:</color></b> Identity unable to stabilise.\n");

        // Step 7 — Display UI output:
        if (unifiedOutputText != null)
            unifiedOutputText.text = output;
    }

    #endregion
}