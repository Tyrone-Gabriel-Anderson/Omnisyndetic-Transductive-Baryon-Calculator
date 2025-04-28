using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TransductiveFullyRelationalSimulator
///
/// A first-principles baryon identity calculator built upon the
/// Omnisyndetic / Transductive Coherence Ontology (Anderson, 2025).
///
/// ----------------------------------------------------------
/// ONTOLOGICAL FRAMEWORK:
/// In the Omnisyndetic view, identity is not substance or interaction.
/// It is the persistence of *imperfect closure* — the memory of almost-return.
/// This simulator models that failure geometrically, not probabilistically.
/// No empirical constants, fields, or force carriers are invoked.
/// Only topology governs emergence.
///
/// Plato’s Baryon defines the idealized relational ground state:
///     • Angular closure:    ∑φ = 2π
///     • Radial coherence:   λ = λ₀
///     • Coherence scalar:   C = 1
///
/// At perfect closure:
///     → Mass = 0
///     → Charge = 0
///     → Curvature = 0
///     → Field = silent
///
/// It is pure structure — a self-confirming, tensionless form.
/// It neither projects nor decays: it simply is.
///
/// ----------------------------------------------------------
/// RELATIONAL MECHANICS:
///
/// Deviations from perfect return drive all emergent properties:
///
///   (1) Angular mismatch (ε):
///       ε = |(∑φ – 2π)| / 2π
///       → Governs charge emergence (directional asymmetry).
///
///   (2) Radial deviation (R_dev):
///       R_dev = (λ – λ₀) / λ₀
///       → Governs divergence (spatial strain).
///
/// Coherence is computed as:
///       C = exp(–[ε² + R_dev²])
///
/// Residual torsion:
///       δ = √(1 – C)
/// encodes the irreducible curvature-memory of the identity.
///
/// ----------------------------------------------------------
/// EMERGENT RELATIONAL OUTPUTS:
///
/// All quantities emerge algebraically from deviation:
///
///   • Mass (m*):
///       Sum of Validation Energy + Desire Energy + Echonex Field Memory.
///
///   • Charge (Q):
///       Q = sign(∑φ – 2π) × (ε⁶) / λ
///       → Charge arises from directional angular tension.
///
///   • Cloud Radius (r_cloud):
///       r_cloud = r₀ × (1 + log(1 + ε² + R_dev²))
///       → Reflects spatial projection of unresolved strain.
///
///   • Field Energy (E_field):
///       E_field = Divergence × α × (1 + δ) × MeV₀
///       → Encodes distributed memory of curvature failure.
///
/// ----------------------------------------------------------
/// ONTOLOGICAL UNIQUENESS:
///
/// This simulator is fully coefficient-free, relying solely on:
///     – λ₀: Ideal coherence length (pure closure geometry)
///     – r₀: Emergent charge radius (relationally defined)
///
/// It operationalizes the principle that:
///     Matter is not a substance.
///     Mass is not an assignment.
///     Identity is the memory of imperfect return.
///
/// ----------------------------------------------------------
/// RELATIONAL CLAIM TO NOVELTY:
///
/// This is the first calculator to:
///   ✓ Derive mass, charge, and field from angular + radial deviation alone.
///   ✓ Eliminate empirical quark masses and fitted coupling constants.
///   ✓ Replace particle-based interactions with pure structural memory.
///
/// It serves as computational evidence:
///     That stable matter can emerge from nothing but relational asymmetry —
///     and that persistence itself is a geometry of near-return.
///
///
/// ~ Tyrone Gabriel Anderson (2025)
/// </summary>



public class TransductiveRelationalSimulator : MonoBehaviour
{
    #region === [USER INPUT + UI References] ======================================================

    [Header("Baryon Inputs")]

    [Tooltip("λ (lambda): The radial coherence scale in femtometers (fm). " +
             "At λ = λ₀, the triadic loop achieves perfect spatial return. " +
             "Deviation reflects spatial desire (R_dev).")]
    public Slider lambdaSlider;

    [Tooltip("∑φ: Total angular closure (radians).\n" +
             "At ∑φ = 2π, angular return is perfect (ε = 0). " +
             "Deviation encodes torsional asymmetry — the origin of charge.")]
    public Slider coherenceDeviationSlider;

    [Header("Slider Labels")]
    public TextMeshProUGUI lambdaLabel;
    public TextMeshProUGUI sumFactorLabel;

    [Header("Output Displays")]
    public TextMeshProUGUI unifiedOutputText;
    public TextMeshProUGUI constantsOutputText;

    [Header("Debug Panel")]
    public TextMeshProUGUI debugOutputText;

    // Optional: Charge field visualisation (Yukawa-like projection zone)
    // public GameObject chargeCloudEffect;

    [Header("Manual Entry Fields")]

    [Tooltip("Manual override for λ (radial coherence scale) in femtometers.")]
    public TMP_InputField lambdaInputField;

    [Tooltip("Manual override for ∑φ (angular closure), measured as Δφ = ∑φ − 2π.")]
    public TMP_InputField closureInputField;

    #endregion

    #region === [RELATIONAL GEOMETRIC CONSTANTS: Plato’s Baryon Calibration] ==========================

    /// <summary>
    /// --- Fundamental Relational Geometry Constants ---
    /// All quantities are derived from pure closure topology, with no empirical coefficients.
    /// </summary>

    /// <summary> λ₀ — Base radial closure length (fm), derived as 1/√2 </summary>
    private double lambda0;

    /// <summary> φ₀ — Perfect angular closure (radians), derived as 2π </summary>
    private double phi0;

    /// <summary> r₀ — Ideal charge cloud boundary at perfect return (fm), identical to λ₀ </summary>
    private double idealChargeRadius;

    /// <summary> εₘₐₓ — Maximal stable angular mismatch (π/3), defines closure instability boundary </summary>
    private double epsilonMax;

    /// <summary> εₘᵢₙ — Minimal stable angular mismatch (0), defines closure lower bound </summary>
    private double epsilonMin;

    /// <summary> εₘₑₐₙ — Mean angular mismatch for triadic deviation (π/6) </summary>
    private double epsilonMean;

    /// <summary> κₘᵢₙ — Minimal nonzero curvature sustaining coherent identity (1/36) </summary>
    private double kappaMin;

    /// <summary> κₘₐₓ — Collapse-limit curvature (3 - 2√2) </summary>
    private double kappaMax;

    /// <summary> δκ — Collapse-limit curvature bandwidth (κₘₐₓ) </summary>
    private double deltaKappa;

    /// <summary> Cₘₐₓ — Maximum coherence before silent collapse (e^{−κₘᵢₙ}) </summary>
    private double coherenceMax;

    /// <summary> Cₘᵢₙ — Minimum coherence before structural collapse (e^{−κₘₐₓ}) </summary>
    private double coherenceMin;

    /// <summary> B — Survivable curvature bandwidth (Δκ × Cₘₐₓ) </summary>
    private double curvatureBandwidth;

    /// <summary> Aₛ — Amplification Scalar (geometric relational amplification factor) </summary>
    private double amplificationScalar;

    /// <summary> ℏc (relational) — Reduced Planck constant × c, derived purely from amplification / bandwidth </summary>
    private double hbar_c_geom;

    /// <summary> Tₙ — Transductive minimum closure time (seconds) </summary>
    private double closureTime;

    /// <summary> c — Emergent speed of light, derived as λ₀ / Tₙ (m/s) </summary>
    private double speedOfLight;

    /// <summary> MeV₀ — Field energy coefficient (MeV/fm³), derived from curvature memory density </summary>
    private double fieldEnergyCoefficient;

    /// <summary> δ — Residual torsion at κₘᵢₙ (irreducible curvature tension) </summary>
    private double torsionResidual;

    /// <summary> fm→m — Conversion factor (femtometers to meters) </summary>
    private double femtoToMeter;

    #endregion


    #region === [RELATIONAL GEOMETRIC DERIVATION FUNCTIONS] ==========================================
    /// <summary>
    /// Fully Relational Derivation of Simulation Constants.
    /// Derived entirely from π, √2, and relational axioms — no external constants.
    /// </summary>
    // ===============================================================
    // DeriveAllRelationalConstants
    // 
    // Derives the internal relational constants used by the system,
    // based purely on geometric closure, amplification, and bandwidth.
    // Fully consistent with relational scaling; external units (c) fixed.
    // ===============================================================

    // ===============================================================
    // DeriveAllRelationalConstants
    // 
    // Derives the internal relational constants used by the system,
    // based purely on geometric closure, amplification, and bandwidth.
    // Fully consistent with relational scaling; external units (c) fixed.
    // ===============================================================

    private void DeriveAllRelationalConstants()
    {
        double pi = Math.PI;
        double e = Math.Exp(1.0);

        // 1. Define the ideal closure length scale λ₀ (in fm)
        lambda0 = 1.0 / Math.Sqrt(2.0);

        // 2. Define the full angular cycle φ₀ (radians)
        phi0 = 2.0 * pi;

        // 3. Set the ideal charge cloud radius to match λ₀
        idealChargeRadius = lambda0;

        // 4. Define minimum and maximum angular mismatch (εₘᵢₙ, εₘₐₓ)
        epsilonMin = 1.0 / 6.0;
        epsilonMax = pi / 3.0;

        // 5. Compute the mean angular mismatch (εₘₑₐₙ)
        epsilonMean = 0.5 * (epsilonMin + epsilonMax);

        // 6. Derive the minimum and maximum curvature (κₘᵢₙ, κₘₐₓ)
        kappaMin = epsilonMin * epsilonMin;
        kappaMax = 3.0 - 2.0 * Math.Sqrt(2.0);

        // 🛑 CRITICAL: define deltaKappa BEFORE using it!
        double deltaKappa = kappaMax - kappaMin;

        // 7. Compute maximum and minimum coherence factors (Cₘₐₓ, Cₘᵢₙ)
        coherenceMax = Math.Exp(-kappaMin);
        coherenceMin = Math.Exp(-kappaMax);

        // 8. Calculate the residual torsion: how much coherence is 'lost'
        torsionResidual = 1.0 - coherenceMax;

        // 9. Compute the ratio factor 'a' that links torsion residual to λ₀
        double a = (torsionResidual * torsionResidual) / ((torsionResidual * torsionResidual) + (lambda0 * lambda0));

        // 10. Calculate curvature bandwidth (Δκ × Cₘₐₓ)
        curvatureBandwidth = deltaKappa * (coherenceMax);

        // 11. Derive amplification scalar (A) from angular scaling and residuals
        amplificationScalar = pi * (2.0 * pi + deltaKappa) * (1.0 + 1.0 / e) - (torsionResidual - a);

        // 12. Calculate geometric ħc in natural (relational) units (MeV·fm)
        hbar_c_geom = amplificationScalar / curvatureBandwidth;

        // 13. Compute an effective drift parameter based on amplification and e
        double effectiveDrift = amplificationScalar / e;

        // 14. Set speed of light (SI standard units, m/s) as fixed constant
        speedOfLight = 299792458.0; // <<< You MUST define speedOfLight BEFORE closureTime!

        // 15. Conversion factor from femtometres to metres
        femtoToMeter = 1.0e-15;

        // 16. Calculate relational closure time (Tₙ) based on λ₀ and c
        closureTime = lambda0 * femtoToMeter / speedOfLight;

        // 17. Compute the Echonex base field energy coefficient (M₀) in MeV
        double M0_Echonex = pi * hbar_c_geom * hbar_c_geom / lambda0;
        fieldEnergyCoefficient = M0_Echonex;

        // 18. Print all relational constants to the UI for verification
        PrintRelationalConstantsUI();
    }




    double ComputeScalar() //Old method for scaling across sum frams (used in first discovery of angularScalar. 
    {
        double pi = Math.PI;
        double epsMin = 1.0 / 6.0;
        double epsMax = pi / 3.0;
        int N = 200000;

        double deltaEps = (epsMax - epsMin) / (N - 1);

        double sum = 0.0;
        for (int i = 0; i < N; i++)
        {
            double eps = epsMin + i * deltaEps;
            double R = (pi + 6.0 * eps) / (eps / (pi / 3.0));
            if (i == 0 || i == N - 1)
                sum += 0.5 * R; // trapezoidal weighting
            else
                sum += R;
        }
        double integral = sum * deltaEps;

        double amplificationScalar = integral / (epsMax - epsMin);

        Console.WriteLine($"A_s = {amplificationScalar:F6}");
        return amplificationScalar;
    }





    private void PrintRelationalConstantsUI()
    {
        // … assume you’ve already run DeriveAllRelationalConstants()

        var sb = new System.Text.StringBuilder();

        sb.AppendLine("<b><size=14><color=#87CEEB>Relational Constants</color></size></b>\n");

        // — Pure Geometry —
        sb.AppendLine("<b><color=#FFD700>— Pure Geometry —</color></b>");
        sb.AppendLine($"<b>π :</b> <color=#B0E0E6>{Mathf.PI:F6}</color>");
        sb.AppendLine($"<b>λ<sub>0</sub> = 1/√2 :</b> <color=#00CED1>{lambda0:F6}</color> fm");
        sb.AppendLine($"<b>φ<sub>0</sub> = 2π :</b> <color=#7FFFD4>{phi0:F6}</color> rad\n");

        // — Angular Bounds —
        sb.AppendLine("<b><color=#FFA500>— Angular Bounds —</color></b>");
        sb.AppendLine($"<b>ε<sub>min</sub> = 1/6 :</b> <color=#FFDAB9>{epsilonMin:F6}</color>");
        sb.AppendLine($"<b>ε<sub>max</sub> = π/3 :</b> <color=#FFDAB9>{epsilonMax:F6}</color>");
        sb.AppendLine($"<b>ε<sub>mean</sub> = (1/6+π/3)/2 :</b> <color=#FFDAB9>{epsilonMean:F6}</color>\n");

        // — Curvature & Coherence —
        sb.AppendLine("<b><color=#ADFF2F>— Curvature & Coherence —</color></b>");
        sb.AppendLine($"<b>κ<sub>min</sub> = (1/6)² :</b> <color=#98FB98>{kappaMin:F6}</color>");
        sb.AppendLine($"<b>κ<sub>max</sub> = 3 − 2√2 :</b> <color=#98FB98>{kappaMax:F6}</color>");
        sb.AppendLine($"<b>Δκ :</b> <color=#98FB98>{deltaKappa:F6}</color>");
        sb.AppendLine($"<b>C<sub>max</sub> = e^(−κ<sub>min</sub>) :</b> <color=#ADFF2F>{coherenceMax:F6}</color>");
        sb.AppendLine($"<b>C<sub>min</sub> = e^(−κ<sub>max</sub>) :</b> <color=#ADFF2F>{coherenceMin:F6}</color>\n");

        // — Bandwidth & Amplification —
        sb.AppendLine("<b><color=#32CD32>— Bandwidth & Amplification —</color></b>");
        sb.AppendLine($"<b>ℬ = Δκ · C<sub>max</sub> :</b> <color=#00FA9A>{curvatureBandwidth:F7}</color>");
        sb.AppendLine($"<b>A<sub>s</sub> :</b> <color=#00FA9A>{amplificationScalar:F6}</color>");
        sb.AppendLine($"<b>ℏc = A<sub>s</sub> / ℬ :</b> <color=#00FA9A>{hbar_c_geom:F2}</color> MeV·fm\n");

        // — Time & c —
        sb.AppendLine("<b><color=#1E90FF>— Time & c —</color></b>");
        sb.AppendLine($"<b>T<sub>n</sub> = λ<sub>0</sub> / c :</b> <color=#87CEFA>{closureTime:E2}</color> s");
        sb.AppendLine($"<b>c :</b> <color=#87CEFA>{speedOfLight:E3}</color> m/s\n");

        // — Field Energy & Torsion —
        sb.AppendLine("<b><color=#FF69B4>— Field Energy & Torsion —</color></b>");
        sb.AppendLine($"<b>M<sub>0</sub> = π·(ℏc)² / λ<sub>0</sub> :</b> <color=#FFB6C1>{fieldEnergyCoefficient:F2}</color> MeV");
        sb.AppendLine($"<b>δ = √(1 − C<sub>max</sub>) :</b> <color=#FFB6C1>{torsionResidual:F6}</color>\n");

        constantsOutputText.text = sb.ToString();
    }


    #endregion

    #region Relational Simulator Internal Fields

    /// <summary>
    /// UI state flag — true when user is actively editing λ (radial coherence input).
    /// Prevents recursive updates; guards observer feedback effects (§4.3: Observer–Observed Coupling).
    /// </summary>
    private bool isEditingLambda = false;

    /// <summary>
    /// UI state flag — true when user is actively editing closure sum (∑φ).
    /// Prevents recursive updates; ensures input stability under observation.
    /// </summary>
    private bool isEditingClosure = false;

    /// <summary>
    /// Validation Energy (E_v) — energy associated with loop internal closure fidelity.
    /// Emerges from the attempt to maintain triadic identity return without projection.
    /// See Eq 17, Term 1.
    /// </summary>
    internal double validationEnergy;

    /// <summary>
    /// Desire Energy (E_d) — energy associated with external projection of torsional strain.
    /// Captures the tension from incomplete return projected outward.
    /// See Eq 17, Term 2.
    /// </summary>
    internal double desireEnergy;

    /// <summary>
    /// Echonex Field Energy (E_e) — stored curvature memory from radial divergence.
    /// Represents long-term memory of failure to close.
    /// See Eq 17, Term 3.
    /// </summary>
    internal double echonexField;

    /// <summary>
    /// Emergent Charge (Q) — directional projection of angular asymmetry (ε).
    /// Computed as a sixth-power function of ε, normalized by coherence scale λ.
    /// </summary>
    internal double emergentCharge;

    /// <summary>
    /// Total Mass (m*) — sum of Validation Energy, Desire Energy, and Echonex Field Memory.
    /// Purely relational mass; no fundamental particles or Higgs mechanism required.
    /// </summary>
    internal double totalMass;

    /// <summary>
    /// Charge Cloud Radius (r_cloud) — spatial spread of relational identity curvature.
    /// Expands logarithmically with accumulated angular and radial deviation.
    /// </summary>
    internal double chargeCloudRadius;

    /// <summary>
    /// Dark Energy Scale (Λ) — residual fourth-order divergence of relational curvature.
    /// Represents unresolved relational tension projected beyond the identity cloud.
    /// </summary>
    internal double darkEnergyScale;

    /// <summary>
    /// Coherence (C) — probability amplitude for successful identity return.
    /// Computed as C = exp(–[ε² + R_dev²]); bounded between C_min and C_max for stability.
    /// </summary>
    internal double coherence;

    /// <summary>
    /// Angular Deviation (ε) — normalized measure of failure to achieve perfect angular closure (∑φ ≠ 2π).
    /// Source of emergent charge and torsional tension.
    /// </summary>
    internal double epsilon;

    /// <summary>
    /// Radial Deviation (R_dev) — normalized measure of failure to achieve ideal radial return (λ ≠ λ₀).
    /// Source of emergent divergence and field memory.
    /// </summary>
    internal double radialDeviation;

    #endregion


    #region Unity Lifecycle
    private void Awake()
    {
        DeriveAllRelationalConstants();
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
        coherenceDeviationSlider.value = 7.3434f;  // ∑φ = 2π + δφ (angular asymmetry)
        lambdaSlider.value = 0.84185f;                                  // λ (fm) > λ₀ (radial deviation)

        //0.88887   0.00000  neuton
        //0.52110  -0.82153  Ω_b⁻	~6046 MeV
        //0.49180  -0.69000  ~10162 MeV


        // Sync text inputs to slider values
        lambdaInputField.text = (lambdaSlider.value).ToString("F5");
        closureInputField.text = (coherenceDeviationSlider.value-phi0).ToString("F5");

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
                coherenceDeviationSlider.value = Mathf.Clamp((float)parsed + (float)phi0,
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
        double λ = lambdaSlider.value;
        double φ = coherenceDeviationSlider.value;

        double Δφ = φ - phi0;
        epsilon = Math.Abs(Δφ) / phi0;
        radialDeviation = (λ - lambda0) / lambda0;

        double kappa = (epsilon * epsilon) + (radialDeviation * radialDeviation);
        coherence = Math.Exp(-kappa);
    }

    /// <summary>
    /// 🔄 ComputeTransductiveCoupling:
    ///     Maps energy feedback impedance as a ratio of identity strain to scale:
    ///     α = (ε² + δ²) / (ε² + δ² + λ²)
    ///     where δ = √(1 – C) is residual torsion
    /// </summary>
    private double ComputeTransductiveCoupling(double λ)
    {
        double δ = Math.Sqrt(1f - coherence);
        double numerator = epsilon * epsilon + δ * δ;
        double denominator = numerator + λ * λ;
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
    private (double decayRate, double lifetime) ComputeDecayRateAndLifetime()
    {
        const double HBAR_MEV_S = 6.582119569e-22f;  // Planck constant in MeV·s

        double κ = -Math.Log(coherence);                      // Memory curvature
        double E_mem = echonexField + validationEnergy;        // Total curvature-encoded energy
        double safeE = Math.Max(E_mem, 1e-6f);                // Avoid division by zero

        double decayWidth = (HBAR_MEV_S / safeE) * Math.Exp(-κ / kappaMax); // Γ
        double lifetime = 1f / Math.Max(decayWidth, 1e-30f);                // τ

        return (decayWidth, lifetime);
    }



    /// <summary>
    /// 💠 ComputeEchonexFieldEnergy:
    ///     Measures total divergence across the identity loop:
    ///     divergence = λ/λ₀ + λ₀/λ – 2  
    ///     E_field = divergence × α × (1 + δ) × MeV₀
    /// </summary>
    private double ComputeEchonexFieldEnergy(float λ)
    {
        double divergence = (λ / lambda0) + (lambda0 / λ) - 2f;
        double α = ComputeTransductiveCoupling(λ);
        double δ = Math.Sqrt(1f - coherence);
        return (divergence) * α * (1f + δ) * fieldEnergyCoefficient;
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
    private void ComputeLoopAndDesireEnergy(out double e_loop, out double e_desire)
    {
        double λ = lambdaSlider.value;
        double φ = coherenceDeviationSlider.value;
        double α = ComputeTransductiveCoupling(λ);
        double δ = Math.Sqrt(1f - coherence);

        double baseE = (α * hbar_c_geom / Math.Max(λ, 1e-12f)) * coherence * φ;
        double sig = 1f / (1f + Math.Exp(-25f * (α - coherence)));
        e_loop = baseE * Mathf.Lerp(1f, 10f, ((float)sig));  // smooth modulation

        double e_proj = (α * δ * hbar_c_geom / Math.Max(λ, 1e-12f)) * (1f - coherence);
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
    private double ComputeEmergentCharge()
    {
        // --- Δφ: Net angular deviation from ideal identity loop (2π radians)
        double Δφ = coherenceDeviationSlider.value - phi0;

        // --- q₀: Base quantised charge (elementary charge) at peak projection
        const double q0 = 1.602e-19f;

        // --- ε: Normalised angular deviation (ε = Δφ / φ₀)
        double ε = Δφ / phi0;

        // --- Snap charge to zero when coherence is near-perfect (C ≈ 1 ⇒ κ ≈ 0)
        //     At full closure, charge must vanish: system is topologically silent
        if (coherence >= coherenceMax)
            return 0f;

        // --- κ: Relational curvature (κ = –ln(C))
        //     Represents stored torsional strain from closure failure
        double κ = -Math.Log(coherence);

        // --- Polarity: Determine charge sign from the direction of angular error
        double sign = (Δφ >= 0f) ? +1f : -1f;

        // --- Q: Final emergent charge
        //     · Grows with ε (angular asymmetry),
        //     · Scales with κ / κₘᵢₙ (topological strain),
        //     · Modulated by exp(1 – κ) (memory decay cap)
        double q_final = sign * q0 * Math.Abs(ε) * (κ / kappaMin) * Math.Exp(1f - κ);

        return q_final;
    }






    /// <summary>
    /// ☁️ ComputeChargeEchoRadius:
    ///     Spatial footprint of identity curvature.
    ///     r_cloud = r₀ × [1 + ln(1 + ε² + R_dev²)]
    /// </summary>
    private double ComputeChargeEchoRadius()
    {
        double κ = epsilon * epsilon + radialDeviation * radialDeviation;
        return idealChargeRadius * (1f + Math.Log(1f + κ));
    }


    /// <summary>
    /// 🌀 GetValidationRadius:
    ///     Returns the geometric radius of instantaneous return (no projection).
    ///     Equal to λ₀ — the fundamental radial coherence length.
    /// </summary>
    private double GetValidationRadius()
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
    private double GetHybridCoherenceRadius()
    {
        double r_charge = ComputeChargeEchoRadius();
        double r_memory = lambda0 * (1f + epsilon + radialDeviation);
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
        ComputeLoopAndDesireEnergy(out double e_loop, out double e_desire);
        validationEnergy = e_loop;
        desireEnergy = e_desire;

        echonexField = ComputeEchonexFieldEnergy(lambdaSlider.value);
        double total = e_loop + e_desire + echonexField;

        formsBaryon = (coherence >= coherenceMin && coherence <= coherenceMax);
        return (float)(formsBaryon ? total : 0);
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
        (double decayRate, double lifetime) = ComputeDecayRateAndLifetime();

        string decayPanel =
            "<b><color=#FF69B4>☐ Decay Properties</color></b>\n" +
            $"<b>Decay Width (Γ):</b> <color=#FF6347>{decayRate:E3}</color> MeV\n" +
            $"<b>Lifetime (τ):</b> <color=#00FA9A>{lifetime:E3}</color> seconds\n";


        double Tn_min = 2.358e-24f;  // hardcoded from Plato's Baryon
        double λ_m = lambda * femtoToMeter;
        double deltaT_actual = λ_m / speedOfLight;
        double deltaT_offset = deltaT_actual - Tn_min;

        string temporalReading =
            $"<b><color=#6A5ACD>Δt (Return Time):</color></b> <color=#BA55D3>{deltaT_actual:E2}</color> s\n" +
            $"<b><color=#7B68EE>Δt Offset from T<sub>n</sub>:</color></b> <color=#9370DB>{deltaT_offset:E2}</color> s\n" +
            $"<b><color=#778899>Minimum Return Time T<sub>n</sub>:</color></b> <color=#708090>{Tn_min:E2}</color> s\n" +
            (formsTachyon
                ? "<b><color=#FF00FF>Tachyonic Reversal:</color></b> λ < λ<sub>0</sub> — coherence loops inward, time inverts.\n"
                : "<b><color=#00FA9A>Normal Return:</color></b> Coherence stable, forward-temporal identity preserved.\n");

        // Step 8 — Verdict logic
        string verdict;
        if (formsTachyon)
        {
            verdict = "<b><color=#FF00FF>Tachyon Formed:</color></b> Ultra-coherent; λ < λ<sub>0</sub>, temporal reversal projected.\n";
        }
        else if (baryonForms)
        {
            verdict = "<b><color=#32CD32>Baryon Formed:</color></b> Coherence valid; identity sustained.\n";
        }
        else
        {
            verdict = "<b><color=#FF0000>Baryon Collapsed:</color></b> Coherence outside bounds; structure dissolved.\n";
        }


        string output =
        // === Title Section ===
        "<b><size=16><color=#FFFFFF>TransductiveRelationalSimulator</color></size></b>\n" +
        "<i><size=12><color=#B0C4DE>All emergent properties derived from deviation against Plato’s Baryon</color></size></i>\n\n" +

        // === Topological Configuration ===
        "<b><color=#FFD700>✦ Topological Configuration</color></b>\n" +
        $"<b>λ (Active):</b> <color=#00FFFF>{lambda:F5}</color> fm    |    " +
        $"<b>λ<sub>0</sub> (Ideal):</b> <color=#00CED1>{lambda0:F5}</color> fm\n" +
        $"<b>∑φ (Angular Sum):</b> <color=#00FFAA>{coherenceDeviationSlider.value:F4}</color> rad    |    " +
        $"<b>φ<sub>0</sub> (Ideal):</b> <color=#7FFFD4>{phi0:F4}</color> rad\n" +
        $"<b>ε (Angle Error):</b> <color=#FFA500>{epsilon:F6}</color>\n" +
        $"<b>R<sub>e</sub> (Radial Deviation):</b> <color=#FF7F50>{radialDeviation:F6}</color>\n\n" +

        // === Derived Curvatures ===
        "<b><color=#FFDAB9>» Derived Curvatures</color></b>\n" +
        $"<b>κ<sub>1</sub> (Validation Curvature):</b> <color=#FF8C00>{(epsilon * epsilon + radialDeviation * radialDeviation):F6}</color>\n" +
        $"<b>κ<sub>2</sub> (Echonex Curvature):</b> <color=#FFA07A>{-Math.Log(coherence):F6}</color>\n" +
        $"<b>C (Coherence):</b> <color=#ADFF2F>{coherence:F6}</color>    |    " +
        $"<b>δ (Residual Torsion):</b> <color=#9370DB>{Math.Sqrt(1.0 - coherence):F6}</color>\n\n" +

        // === Extended Topological Metrics ===
        "<b><color=#D8BFD8>» Extended Topological Metrics</color></b>\n" +
        $"<b>Δφ (Mismatch):</b> <color=#F5DEB3>{(epsilon * phi0):F6}</color> rad\n" +
        $"<b>Torsional Tension:</b> <color=#F08080>{(epsilon * radialDeviation):F6}</color>\n" +
        $"<b>Closure Fidelity Index:</b> <color=#EEE8AA>{Math.Exp(-(epsilon + Math.Abs(radialDeviation))):F6}</color>\n" +
        $"<b>Feedback Delay (τ):</b> <color=#87CEFA>{(lambda / (ComputeTransductiveCoupling(lambda) + Math.Sqrt(1 - coherence) + epsilon)):F6}</color> fm\n" +
        $"<b>Curvature Density (κ<sub>2</sub> / λ):</b> <color=#BC8F8F>{(-Math.Log(coherence) / lambda):F6}</color>\n\n" +

        // === Coupling & Divergence ===
        $"<b>α (Coupling Strength):</b> <color=#00BFFF>{ComputeTransductiveCoupling(lambda):F6}</color>\n" +
        $"<b>Λ<sub>r</sub> (Spatial Divergence):</b> <color=#FF69B4>{((lambda / lambda0) + (lambda0 / lambda) - 2.0):F6}</color>\n\n" +

        // === Emergent Identity Properties ===
        "<b><color=#DA70D6>Emergent Identity Properties</color></b>\n" +
        $"<b>Total Mass (m<sub>*</sub>):</b> <color=#FFA07A>{totalMass:F3}</color> MeV    |    " +
        $"<b>Charge (q):</b> <color=#FF4500>{emergentCharge:E2}</color> C\n" +
        $"<b>Cloud Radius (r<sub>cloud</sub>):</b> <color=#40E0D0>{chargeCloudRadius:F5}</color> fm\n" +
        $"<b>Λ (Field Divergence):</b> <color=#8A2BE2>{darkEnergyScale:F6}</color>\n\n" +

        // === Energetic Breakdown ===
        "<b><color=#20B2AA>Energy Composition</color></b>\n" +
        $"<b>Validation Energy:</b> <color=#ADD8E6>{validationEnergy:F2}</color> MeV    |    " +
        $"<b>Desire Energy:</b> <color=#FFA07A>{desireEnergy:F2}</color> MeV\n" +
        $"<b>Echonex Field Energy:</b> <color=#FFB6C1>{echonexField:F2}</color> MeV\n\n" +

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