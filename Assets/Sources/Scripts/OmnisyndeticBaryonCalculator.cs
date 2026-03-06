using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// ============================================================================
/// OmnisyndeticBaryonCalculator (Unity reference implementation)
///
/// Purpose
///   A deterministic Unity-side replay of the *fixed* register used by the
///   HTML calculator and scan tools. This script does not introduce any new
///   constants, tuning factors, or per-state adjustments.
///
/// Functional contract (requested)
///   - Input A: flavour string (letters u d s c b; exactly three quarks)
///   - Input B: contrast step vector (three digits 1–5, first three read)
///   - Output: the predicted mass band [m_min, m_max] in MeV, together with
///     the certified arc-seat ⟨L,U⟩ (or ⟨L,∅⟩ in the bottom tier) and the
///     λ-interval that realises the band.
///
/// Notes on terminology
///   - "coherence" is used as a scalar returned by the register formula C(λ,φ).
///     It is a closure-grade *structural* diagnostic; no identity predicate is
///     assumed in the code.
///   - This file intentionally avoids "residue" language; the programme uses
///     bounded unresolved relata / incompleteness instead.
///
/// Implementation guide
///   The core register is implemented in the nested static class Register below:
///     constants: λ_min, λ_max, λ0, coherence window, IES, M0E, arc midpoints
///     functions: solve φ(λ,Z), state(λ,φ), energies(λ), arc(κ), mirror λ′(λ),
///                arc-pair for a given λ, deterministic band scan.
///
/// Unity integration
///   This MonoBehaviour only performs:
///     - input parsing and basic bookkeeping checks,
///     - seat selection rules (L,U) from inputs,
///     - calls Register.ScanMassBand(...)
///     - publishes current diagnostics to other scripts (visualisers).
///
/// ============================================================================

public class OmnisyndeticBaryonCalculator : MonoBehaviour
{
    // ------------------------------------------------------------------------
    // UI: minimal set. You can wire any subset in the Unity inspector.
    // ------------------------------------------------------------------------

    [Header("Auto-wire (optional)")]
    [Tooltip("If Spin Dropdown is not assigned, the script will try to locate one at runtime.")]
    public bool autoFindSpinDropdown = true;

    [Tooltip("If set, preferred scene object name for the Spin Dropdown (case-insensitive).")]
    public string spinDropdownObjectNameHint = "";

    [Header("Inputs")]
    [Tooltip("Flavour string (e.g. uud, uds, sss, uuc, ubb) OR three digits 1–5 (e.g. 123).")]
    public TMP_InputField flavourOrStepsInput;

    [Tooltip("Net charge Q in units of e (integer).")]
    public TMP_InputField chargeInput;

    [Tooltip("Spin selector. Options should be '1/2' and '3/2'. If unset, defaults to 1/2.")]
    public TMP_Dropdown spinDropdown;

    [Header("Optional λ probe (for live diagnostics)")]
    [Tooltip("If present, this is ONLY for live visual diagnostics at one λ. Band scan still uses the fixed lawful scan window.")]
    public Slider lambdaProbeSlider;

    [Tooltip("Optional numeric λ input (fm). If present, it stays synchronised with the slider.")]
    public TMP_InputField lambdaProbeInput;

    [Tooltip("Optional readout for the twin λ′ that yields the same m*(λ) on the opposite side of λ0.")]
    public TMP_InputField lambdaTwinReadout;

    [Tooltip("Optional mass input/readout for the live probe (MeV). If edited, λ is solved inside the admitted band interval.")]
    public TMP_InputField massProbeInput;

    [Tooltip("Optional readout for the twin mass m*(λ′) (MeV).")]
    public TMP_InputField massTwinReadout;

    [Header("Output")]
    public TextMeshProUGUI outputText;

    // ------------------------------------------------------------------------
    // Published live diagnostics (for RealTimeTriadVisualizer, etc.)
    // ------------------------------------------------------------------------

    [NonSerialized] public double CurrentLambda;   // current probe λ (fm)
    [NonSerialized] public int CurrentQ;           // integer net charge Q
    [NonSerialized] public double CurrentPhi;      // solved φ(λ,Q) at probe λ
    [NonSerialized] public double CurrentEps;      // ε(λ,Q) at probe λ
    [NonSerialized] public double CurrentRDev;     // rdev(λ) at probe λ
    [NonSerialized] public double CurrentC;        // coherence C(λ,Q) at probe λ
    [NonSerialized] public double CurrentKappa;    // κ = -ln C at probe λ
    [NonSerialized] public double CurrentMStar;    // m*(λ,Q) at probe λ
    [NonSerialized] public int CurrentArc;         // arc index from κ at probe λ
    [NonSerialized] public double CurrentLambdaTwin; // twin λ′ for same mass
    [NonSerialized] public double CurrentMStarTwin;  // m*(λ′)
    [NonSerialized] public int CurrentL;           // seat L (if computed)
    [NonSerialized] public int? CurrentU;          // seat U or null if ∅

    [NonSerialized] public bool HasAdmissibleSeat; // bookkeeping + seat selection passed
    [NonSerialized] public bool HasAdmissibleBand; // scan found at least one admissible sample

    // Cache the last full computation so slider changes can update output without re-scanning.
    private InputParser.ParsedInput _lastParsed;
    private InputParser.CheckResult _lastBookkeeping;
    private SeatSelectionResult _lastSeat;
    private Register.BandResult _lastBand;
    private double _lastSpin;
    private bool _hasLast;

    // Cache the last admitted λ interval (from band scan). Slider is clamped to this.
    private bool _hasLambdaInterval;
    private double _lambdaIntervalMin;
    private double _lambdaIntervalMax;

    // Cached sample grid from the most recent admitted band scan (sorted by λ).
    // This matches the HTML calculator behaviour: λ tuning moves along admitted
    // samples and never through gaps in the coherent set.
    private double[] _bandSampleLams;
    private double[] _bandSampleMasses;

    // ------------------------------------------------------------------------
    // Unity lifecycle
    // ------------------------------------------------------------------------

    private void Awake()
    {
        // Optional auto-wiring (useful when the prefab/UI is re-created at runtime).
        if (spinDropdown == null && autoFindSpinDropdown)
            spinDropdown = TryFindSpinDropdown();

        // Provide safe defaults if UI is partially wired.
        if (lambdaProbeSlider != null)
        {
            lambdaProbeSlider.wholeNumbers = false;
            // Default to the lawful global window; once a band interval is admitted we clamp to it.
            lambdaProbeSlider.minValue = (float)Register.LambdaMin;
            lambdaProbeSlider.maxValue = (float)Register.LambdaMax;
            lambdaProbeSlider.value = (float)Register.L0; // centre at Euclidean crossover
        }

        // Ensure the spin dropdown has the expected options.
        EnsureSpinDropdownOptions();
    }

    private void Start()
    {
        // In some UI setups the dropdown is instantiated after Awake.
        if (spinDropdown == null && autoFindSpinDropdown)
            spinDropdown = TryFindSpinDropdown();
        EnsureSpinDropdownOptions();

        HookUi();
        EnsureDefaultInputs();
        RecomputeAll();
    }

    private void EnsureDefaultInputs()
    {
        // Boot default: proton ready.
        if (flavourOrStepsInput != null && string.IsNullOrWhiteSpace(flavourOrStepsInput.text))
            flavourOrStepsInput.SetTextWithoutNotify("uud");

        if (chargeInput != null && string.IsNullOrWhiteSpace(chargeInput.text))
            chargeInput.SetTextWithoutNotify("1");

        if (spinDropdown != null && spinDropdown.options != null && spinDropdown.options.Count > 0)
        {
            spinDropdown.SetValueWithoutNotify(0);
            spinDropdown.RefreshShownValue();
        }
    }

    private void HookUi()
    {
        if (flavourOrStepsInput != null)
            flavourOrStepsInput.onEndEdit.AddListener(_ => OnFlavourEdited());

        if (chargeInput != null)
            chargeInput.onEndEdit.AddListener(_ => OnChargeEdited());

        if (spinDropdown != null)
            spinDropdown.onValueChanged.AddListener(_ => RecomputeAll());

        if (lambdaProbeSlider != null)
            lambdaProbeSlider.onValueChanged.AddListener(_ => OnLambdaSliderChanged());

        if (lambdaProbeInput != null)
            lambdaProbeInput.onEndEdit.AddListener(_ => OnLambdaInputEdited());

        if (massProbeInput != null)
            massProbeInput.onEndEdit.AddListener(_ => OnMassInputEdited());
    }

    // ------------------------------------------------------------------------
    // Spin dropdown bootstrapping
    //   Unity/TMP prefabs often ship with placeholder options (A/B/C or Option A/B/C).
    //   The user request: at boot-up, populate the dropdown with the lawful options.
    // ------------------------------------------------------------------------

    private TMP_Dropdown TryFindSpinDropdown()
    {
        // Prefer a name hint if provided, otherwise look for something with "spin".
        TMP_Dropdown[] all;
        try
        {
            all = FindObjectsOfType<TMP_Dropdown>(true);
        }
        catch
        {
            return null;
        }

        if (all == null || all.Length == 0)
            return null;

        string hint = (spinDropdownObjectNameHint ?? "").Trim();
        if (!string.IsNullOrEmpty(hint))
        {
            var byName = all.FirstOrDefault(d => d != null && d.gameObject != null &&
                string.Equals(d.gameObject.name, hint, StringComparison.OrdinalIgnoreCase));
            if (byName != null)
                return byName;
        }

        var bySpin = all.FirstOrDefault(d => d != null && d.gameObject != null &&
            d.gameObject.name != null && d.gameObject.name.IndexOf("spin", StringComparison.OrdinalIgnoreCase) >= 0);
        return bySpin != null ? bySpin : all[0];
    }

    private void EnsureSpinDropdownOptions()
    {
        if (spinDropdown == null)
            return;

        bool needsInit = spinDropdown.options == null || spinDropdown.options.Count == 0;
        if (!needsInit)
            needsInit = LooksLikeTmpDefaultOptions(spinDropdown.options);

        if (!needsInit)
            return;

        spinDropdown.options = new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("1/2"),
            new TMP_Dropdown.OptionData("3/2")
        };
        spinDropdown.value = 0;
        spinDropdown.RefreshShownValue();
    }

    private static bool LooksLikeTmpDefaultOptions(List<TMP_Dropdown.OptionData> opts)
    {
        if (opts == null || opts.Count != 3)
            return false;

        string a = (opts[0]?.text ?? "").Trim();
        string b = (opts[1]?.text ?? "").Trim();
        string c = (opts[2]?.text ?? "").Trim();

        // TMP default template usually has "Option A/B/C". Some users rename to "A/B/C".
        bool optionABC =
            string.Equals(a, "Option A", StringComparison.OrdinalIgnoreCase) &&
            string.Equals(b, "Option B", StringComparison.OrdinalIgnoreCase) &&
            string.Equals(c, "Option C", StringComparison.OrdinalIgnoreCase);

        bool plainABC =
            string.Equals(a, "A", StringComparison.OrdinalIgnoreCase) &&
            string.Equals(b, "B", StringComparison.OrdinalIgnoreCase) &&
            string.Equals(c, "C", StringComparison.OrdinalIgnoreCase);

        return optionABC || plainABC;
    }

    private void OnFlavourEdited()
    {
        // Auto-update charge from flavour content when flavour letters are present.
        if (flavourOrStepsInput != null && chargeInput != null)
        {
            var p = InputParser.ParseFlavourOrSteps(flavourOrStepsInput.text);
            if (p.HasFlavourLetters)
            {
                if (TryComputeIntegerChargeFromContent(p, out int qContent))
                    chargeInput.SetTextWithoutNotify(qContent.ToString(CultureInfo.InvariantCulture));
            }
        }
        RecomputeAll();
    }

    private void OnChargeEdited()
    {
        // If Q is edited while flavour letters are present, keep heavy content fixed and
        // rebuild u/d to match the requested Q (or leave as-is if infeasible).
        if (flavourOrStepsInput != null && chargeInput != null)
        {
            var p = InputParser.ParseFlavourOrSteps(flavourOrStepsInput.text);
            if (p.HasFlavourLetters)
            {
                int Q = ParseIntOrDefault(chargeInput.text, 0);
                if (TryRebuildFlavourWithFixedHeavy(p, Q, out var rebuilt))
                    flavourOrStepsInput.SetTextWithoutNotify(rebuilt);
            }
        }
        RecomputeAll();
    }

    private static bool TryComputeIntegerChargeFromContent(InputParser.ParsedInput p, out int Q)
    {
        int num = 2 * (p.NU + p.NC) - (p.ND + p.NS + p.NB); // = 3Q
        if (num % 3 == 0)
        {
            Q = num / 3;
            return true;
        }
        Q = 0;
        return false;
    }

    private static bool TryRebuildFlavourWithFixedHeavy(InputParser.ParsedInput p, int Q, out string flavour)
    {
        int nS = p.NS, nC = p.NC, nB = p.NB;
        int rem = 3 - (nS + nC + nB);
        if (rem < 0)
        {
            flavour = "";
            return false;
        }

        // Let nD = rem - nU and solve the charge constraint.
        int rhs = 3 * Q + rem + nS + nB - 2 * nC; // = 3 nU
        if (rhs % 3 != 0)
        {
            flavour = "";
            return false;
        }
        int nU = rhs / 3;
        int nD = rem - nU;
        if (nU < 0 || nD < 0)
        {
            flavour = "";
            return false;
        }

        flavour = new string('u', nU) + new string('d', nD) + new string('s', nS) + new string('c', nC) + new string('b', nB);
        return flavour.Length == 3;
    }

    // ------------------------------------------------------------------------
    // Main recomputation
    // ------------------------------------------------------------------------

    public void RecomputeAll()
    {
        // 1) Parse inputs
        var raw = (flavourOrStepsInput != null) ? flavourOrStepsInput.text : "uud";
        var qStr = (chargeInput != null) ? chargeInput.text : "1";

        int Q = ParseIntOrDefault(qStr, 0);
        CurrentQ = Q;

        double spin = ReadSpinFromDropdownOrDefault();

        var parsed = InputParser.ParseFlavourOrSteps(raw);

        // 2) Bookkeeping checks
        //    If a flavour string is provided, enforce triad size and charge consistency.
        //    If digits are provided, we keep the step vector but we cannot do charge
        //    bookkeeping unless flavour letters were also present.
        var bookkeeping = InputParser.BookkeepingOK(parsed, Q, spin);

        // 3) Seat selection (arc-seat ⟨L,U⟩)
        SeatSelectionResult seat = SeatRules.DeriveSeat(parsed, Q, spin);

        HasAdmissibleSeat = bookkeeping.Ok && seat.Ok;
        CurrentL = seat.L;
        CurrentU = seat.U;

        // 4) Scan the lawful λ window to get the predicted band
        Register.BandResult band = default;
        HasAdmissibleBand = false;

        if (HasAdmissibleSeat)
        {
            band = Register.ScanMassBand(seat.L, seat.U, Q, seat.Sector);
            HasAdmissibleBand = band.Ok;
        }

        // Cache full state so slider changes can update live output without re-scanning.
        _lastParsed = parsed;
        _lastBookkeeping = bookkeeping;
        _lastSeat = seat;
        _lastBand = band;
        _lastSpin = spin;
        _hasLast = true;

        // If a band interval is admitted, clamp the slider to it and snap to midpoint.
        if (HasAdmissibleBand)
        {
            _hasLambdaInterval = true;
            _lambdaIntervalMin = band.LambdaMin;
            _lambdaIntervalMax = band.LambdaMax;
            _bandSampleLams = band.SampleLams;
            _bandSampleMasses = band.SampleMasses;
            ClampSliderToIntervalAndSnapMidpoint();
        }
        else
        {
            _hasLambdaInterval = false;
            _bandSampleLams = null;
            _bandSampleMasses = null;
        }

        // 5) Probe diagnostics (for visuals and for "one λ" display)
        UpdateProbeDiagnosticsOnly();
        SyncProbeUiFields();

        // 6) Render result
        if (outputText != null)
            outputText.text = RenderWithLiveProbe(parsed, Q, spin, bookkeeping, seat, band);
    }

    public void UpdateProbeDiagnosticsOnly()
    {
        // Probe λ is for live diagnostics only, not the band scan.
        double lam = lambdaProbeSlider != null ? lambdaProbeSlider.value : Register.L0;
        CurrentLambda = lam;

        // φ is defined by the charge tier Z = Q (dimensionless, in units of e).
        CurrentPhi = Register.SolvePhiAt(lam, CurrentQ);

        var s = Register.State(lam, CurrentPhi);
        CurrentEps = s.Eps;
        CurrentRDev = s.RDev;
        CurrentC = s.C;
        CurrentKappa = s.Kappa;
        CurrentArc = Register.ArcFromKappa(CurrentKappa);

        var e = Register.Energies(lam, s.Eps, s.C);
        CurrentMStar = e.MStar;

        // Twin λ′ (opposite side of λ0) that yields the same m*(λ).
        // This is the "expressive/compressive" pairing shown in the HTML tool.
        CurrentLambdaTwin = double.NaN;
        CurrentMStarTwin = double.NaN;
        if (HasAdmissibleSeat)
        {
            double lamTwin = Register.TwinLambdaForSameMass(lam, CurrentQ);
            if (double.IsFinite(lamTwin) && lamTwin > Register.LambdaMin && lamTwin < Register.LambdaMax)
            {
                CurrentLambdaTwin = lamTwin;
                var s2 = Register.State(lamTwin, Register.SolvePhiAt(lamTwin, CurrentQ));
                CurrentMStarTwin = Register.Energies(lamTwin, s2.Eps, s2.C).MStar;
            }
        }
    }

    private void OnLambdaSliderChanged()
    {
        // Enforce the admitted interval and snap to the nearest admitted sample.
        if (_hasLambdaInterval && lambdaProbeSlider != null)
        {
            double v = lambdaProbeSlider.value;
            v = Math.Max(_lambdaIntervalMin, Math.Min(_lambdaIntervalMax, v));

            if (_bandSampleLams != null && _bandSampleLams.Length > 0)
                v = NearestBandSampleLambda(v, _bandSampleLams);

            lambdaProbeSlider.SetValueWithoutNotify((float)v);
        }

        UpdateProbeDiagnosticsOnly();
        SyncProbeUiFields();
        UpdateOutputFromCache();
    }

    private void OnLambdaInputEdited()
    {
        if (lambdaProbeInput == null) return;
        if (!double.TryParse(lambdaProbeInput.text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var lam))
            return;

        if (_hasLambdaInterval)
        {
            lam = Math.Max(_lambdaIntervalMin, Math.Min(_lambdaIntervalMax, lam));
            if (_bandSampleLams != null && _bandSampleLams.Length > 0)
                lam = NearestBandSampleLambda(lam, _bandSampleLams);
        }

        if (lambdaProbeSlider != null)
            lambdaProbeSlider.SetValueWithoutNotify((float)lam);

        UpdateProbeDiagnosticsOnly();
        SyncProbeUiFields();
        UpdateOutputFromCache();
    }

    private void OnMassInputEdited()
    {
        if (massProbeInput == null) return;
        if (!_hasLambdaInterval) return; // cannot solve without an admitted window

        if (!double.TryParse(massProbeInput.text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var targetMeV))
            return;

        // Match the HTML tool: choose the closest admitted λ sample (no continuous root finding).
        double lamSolved = SolveLambdaForMassOnBandSamples(targetMeV, CurrentQ, _bandSampleLams);
        if (!double.IsFinite(lamSolved))
            return;

        if (lambdaProbeSlider != null)
            lambdaProbeSlider.SetValueWithoutNotify((float)lamSolved);
        if (lambdaProbeInput != null)
            lambdaProbeInput.SetTextWithoutNotify(lamSolved.ToString("F6", CultureInfo.InvariantCulture));

        UpdateProbeDiagnosticsOnly();
        SyncProbeUiFields();
        UpdateOutputFromCache();
    }

    private void ClampSliderToIntervalAndSnapMidpoint()
    {
        if (lambdaProbeSlider == null) return;
        if (!_hasLambdaInterval) return;

        lambdaProbeSlider.minValue = (float)_lambdaIntervalMin;
        lambdaProbeSlider.maxValue = (float)_lambdaIntervalMax;

        // HTML behaviour:
        //   choose the admitted sample whose mass lies closest to the band centre.
        // This is NOT necessarily the middle index in λ, and it is NOT the midpoint
        // of the enclosing λ interval.
        double lam = 0.5 * (_lambdaIntervalMin + _lambdaIntervalMax);
        if (_bandSampleLams != null && _bandSampleMasses != null &&
            _bandSampleLams.Length > 0 && _bandSampleMasses.Length == _bandSampleLams.Length)
        {
            double mCentre = 0.5 * (_lastBand.MMin + _lastBand.MMax);
            int bestIdx = 0;
            double best = double.PositiveInfinity;
            for (int i = 0; i < _bandSampleMasses.Length; i++)
            {
                double d = Math.Abs(_bandSampleMasses[i] - mCentre);
                if (d < best) { best = d; bestIdx = i; }
            }
            lam = _bandSampleLams[Mathf.Clamp(bestIdx, 0, _bandSampleLams.Length - 1)];
        }
        else if (_bandSampleLams != null && _bandSampleLams.Length > 0)
        {
            // Fallback: centre index.
            lam = _bandSampleLams[Mathf.Clamp(_bandSampleLams.Length / 2, 0, _bandSampleLams.Length - 1)];
        }

        lambdaProbeSlider.SetValueWithoutNotify((float)lam);
        CurrentLambda = lam;
    }

    private void SyncProbeUiFields()
    {
        if (lambdaProbeInput != null)
            lambdaProbeInput.SetTextWithoutNotify(CurrentLambda.ToString("F6", CultureInfo.InvariantCulture));

        if (lambdaTwinReadout != null)
        {
            string t = double.IsFinite(CurrentLambdaTwin)
                ? CurrentLambdaTwin.ToString("F6", CultureInfo.InvariantCulture)
                : "";
            lambdaTwinReadout.SetTextWithoutNotify(t);
        }

        if (massProbeInput != null)
            massProbeInput.SetTextWithoutNotify(CurrentMStar.ToString("F6", CultureInfo.InvariantCulture));

        if (massTwinReadout != null)
        {
            string t = double.IsFinite(CurrentMStarTwin)
                ? CurrentMStarTwin.ToString("F6", CultureInfo.InvariantCulture)
                : "";
            massTwinReadout.SetTextWithoutNotify(t);
        }
    }

    private void UpdateOutputFromCache()
    {
        if (!_hasLast) return;
        if (outputText == null) return;
        outputText.text = RenderWithLiveProbe(_lastParsed, CurrentQ, _lastSpin, _lastBookkeeping, _lastSeat, _lastBand);
    }

    private static double SolveLambdaForMassInInterval(double targetMeV, int Q, double a, double b)
    {
        // Find lam in [a,b] such that m*(lam) = targetMeV.
        // We avoid assuming monotonicity: sample for a sign change then bisect.
        double F(double lam)
        {
            var s = Register.State(lam, Register.SolvePhiAt(lam, Q));
            return Register.Energies(lam, s.Eps, s.C).MStar - targetMeV;
        }

        double fa = F(a);
        double fb = F(b);
        if (!double.IsFinite(fa) || !double.IsFinite(fb)) return double.NaN;

        // If no bracket, locate one by sampling.
        if (fa * fb > 0.0)
        {
            double lastX = a;
            double lastF = fa;
            const int N = 200;
            for (int i = 1; i <= N; i++)
            {
                double x = a + (b - a) * i / (double)N;
                double fx = F(x);
                if (!double.IsFinite(fx)) continue;
                if (lastF * fx <= 0.0)
                {
                    a = lastX; fa = lastF;
                    b = x; fb = fx;
                    break;
                }
                lastX = x; lastF = fx;
            }
            if (fa * fb > 0.0) return double.NaN;
        }

        // Bisection.
        for (int it = 0; it < 80; it++)
        {
            double m = 0.5 * (a + b);
            double fm = F(m);
            if (!double.IsFinite(fm)) return double.NaN;
            if (Math.Abs(fm) < 1e-8) return m;
            if (fa * fm <= 0.0) { b = m; fb = fm; }
            else { a = m; fa = fm; }
            if (Math.Abs(b - a) < 1e-10) break;
        }
        return 0.5 * (a + b);
    }

    private static double SolveLambdaForMassOnBandSamples(double targetMeV, int Q, double[] bandLams)
    {
        if (bandLams == null || bandLams.Length == 0) return double.NaN;

        double bestLam = double.NaN;
        double bestDiff = double.PositiveInfinity;

        for (int i = 0; i < bandLams.Length; i++)
        {
            double lam = bandLams[i];
            var s = Register.State(lam, Register.SolvePhiAt(lam, Q));
            if (s.C < Register.CMIN || s.C > Register.CMAX) continue;
            double m = Register.Energies(lam, s.Eps, s.C).MStar;
            if (!double.IsFinite(m)) continue;
            double d = Math.Abs(m - targetMeV);
            if (d < bestDiff)
            {
                bestDiff = d;
                bestLam = lam;
            }
        }

        return bestLam;
    }

    private static double NearestBandSampleLambda(double lam, double[] sortedBandLams)
    {
        if (sortedBandLams == null || sortedBandLams.Length == 0) return lam;

        int lo = 0;
        int hi = sortedBandLams.Length - 1;

        if (lam <= sortedBandLams[lo]) return sortedBandLams[lo];
        if (lam >= sortedBandLams[hi]) return sortedBandLams[hi];

        while (hi - lo > 1)
        {
            int mid = (lo + hi) >> 1;
            if (sortedBandLams[mid] < lam) lo = mid; else hi = mid;
        }

        double a = sortedBandLams[lo];
        double b = sortedBandLams[hi];
        return (Math.Abs(lam - a) <= Math.Abs(lam - b)) ? a : b;
    }

    // ------------------------------------------------------------------------
    // Rendering helpers (human-readable output)
    // ------------------------------------------------------------------------

    private string RenderWithLiveProbe(
        InputParser.ParsedInput parsed,
        int Q,
        double spin,
        InputParser.CheckResult bookkeeping,
        SeatSelectionResult seat,
        Register.BandResult band)
    {
        const bool useRichText = true;

        string C(string hex, string s) => useRichText ? $"<color={hex}>{s}</color>" : s;
        string B(string s) => useRichText ? $"<b>{s}</b>" : s;
        string I(string s) => useRichText ? $"<i>{s}</i>" : s;

        string Title(string s) => C("#C9A7FF", B(s));
        string Sub(string s) => C("#BFA0FF", s);
        string Dim(string s) => C("#9AA3B2", s);
        string Body(string s) => C("#E7EAF0", s);
        string Bad(string s) => C("#FF6B6B", s);

        string Label(string s) => C("#5EDDD1", s.PadRight(12));

        string F9(double x) => double.IsFinite(x) ? x.ToString("F9") : "∅";
        string F6(double x) => double.IsFinite(x) ? x.ToString("F6") : "∅";
        string F4(double x) => double.IsFinite(x) ? x.ToString("F4") : "∅";

        var sb = new System.Text.StringBuilder(1700);

        // --------------------------------------------------------------------
        // Header
        // --------------------------------------------------------------------
        sb.AppendLine(Title("OMNISYNDETIC REGISTER"));
        sb.AppendLine(Sub("Deterministic band report"));
        sb.AppendLine();

        // --------------------------------------------------------------------
        // Inputs
        // --------------------------------------------------------------------
        sb.AppendLine(Title("Inputs"));
        sb.AppendLine($"{Label("Raw")}{Body(parsed.Raw)}");

        string derivedFromSteps = StepsToFlavour(parsed.StepA, parsed.StepB, parsed.StepC);
        if (parsed.HasFlavourLetters)
        {
            sb.AppendLine($"{Label("Flavour")}{Body(parsed.FlavourSanitised)}   " +
                          Dim($"(u:{parsed.NU} d:{parsed.ND} s:{parsed.NS} c:{parsed.NC} b:{parsed.NB})"));
        }
        else
        {
            sb.AppendLine($"{Label("Flavour")}{Body(derivedFromSteps)}   " + Dim("derived from steps"));
        }

        sb.AppendLine($"{Label("Steps")}{Body($"({parsed.StepA},{parsed.StepB},{parsed.StepC})")}   " + Dim("digits 1–5"));
        sb.AppendLine($"{Label("Charge")}{Body($"Q = {Q}")}   " + Dim("net charge tier (units of e)"));
        sb.AppendLine($"{Label("Spin")}{Body(System.Math.Abs(spin - 1.5) < 1e-9 ? "3/2" : "1/2")}   " + Dim("closure tier selector"));
        sb.AppendLine();

        // --------------------------------------------------------------------
        // Gates
        // --------------------------------------------------------------------
        if (!bookkeeping.Ok)
        {
            sb.AppendLine(Title("Status"));
            sb.AppendLine(Bad("Input inadmissible under bookkeeping."));
            sb.AppendLine(Body(bookkeeping.Reason));
            return sb.ToString();
        }

        if (!seat.Ok)
        {
            sb.AppendLine(Title("Status"));
            sb.AppendLine(Bad("No certified seat is admitted."));
            sb.AppendLine(Body(seat.Reason));
            return sb.ToString();
        }

        // --------------------------------------------------------------------
        // Seat
        // --------------------------------------------------------------------
        sb.AppendLine(Title("Seat"));
        sb.AppendLine($"{Label("Sector")}{Body(seat.Sector)}");

        if (seat.U.HasValue)
            sb.AppendLine($"{Label("Certified")}{Body($"⟨L,U⟩ = ⟨{seat.L},{seat.U.Value}⟩")}");
        else
            sb.AppendLine($"{Label("Certified")}{Body($"⟨L,U⟩ = ⟨{seat.L},∅⟩")}   " + Dim("bottom tier: U undefined"));

        if (!string.IsNullOrWhiteSpace(seat.Comment))
            sb.AppendLine($"{Label("Rule note")}{Body(seat.Comment)}");

        sb.AppendLine();

        // --------------------------------------------------------------------
        // Band scan
        // --------------------------------------------------------------------
        if (!band.Ok)
        {
            sb.AppendLine(Title("Status"));
            sb.AppendLine(Bad("No admissible band in the lawful λ window."));
            sb.AppendLine(Body("No sample satisfies coherence and certified seat constraints simultaneously."));
            return sb.ToString();
        }

        sb.AppendLine(Title("Band"));
        sb.AppendLine($"{Label("λ interval")}{Body($"[{F9(band.LambdaMin)}, {F9(band.LambdaMax)}] fm")}   " +
                      Dim("admitted λ samples for this seat"));
        sb.AppendLine($"{Label("Mass band")}{Body($"[{F6(band.MMin)}, {F6(band.MMax)}] MeV")}   " +
                      Dim("ledger output over the admitted λ set"));
        sb.AppendLine();

        // --------------------------------------------------------------------
        // Live probe
        //   IMPORTANT SEMANTICS FIX:
        //   - a_dev is signed angular deviation share (φ−2π)/2π
        //   - ε_res is |a_dev|  (your CurrentEps is this magnitude)
        //   - ε_abs = 1/6 is the global enforced aperture (Register.EPS_MIN)
        // --------------------------------------------------------------------
        sb.AppendLine(Title("Live probe"));
        sb.AppendLine(Dim("Point evaluation at the current (λ, φ): signed deviation, magnitude, curvature spend, coherence, and m*."));
        sb.AppendLine();

        sb.AppendLine($"{Label("λ_probe")}{Body($"{F9(CurrentLambda)} fm")}");

        if (double.IsFinite(CurrentLambdaTwin))
            sb.AppendLine($"{Label("λ_twin")}{Body($"{F9(CurrentLambdaTwin)} fm")}   " + Dim("same m* on opposite side of λ₀"));

        sb.AppendLine($"{Label("φ")}{Body($"{F9(CurrentPhi)} rad")}   " + Dim("closure reference is 2π"));

        // Signed deviation share about 2π
        double aDev = (CurrentPhi - Register.TAU) / Register.TAU;

        // Magnitude of signed deviation share (paper ε_res). Your CurrentEps already equals |φ-2π|/2π.
        double epsRes = System.Math.Abs(aDev);

        // Global enforced aperture (paper ε_abs). You already declare EPS_MIN = 1/6 in Register.
        double epsAbs = Register.EPS_MIN;

        sb.AppendLine($"{Label("a_dev")}{Body(F9(aDev))}   " + Dim("signed angular deviation share (φ−2π)/2π"));
        sb.AppendLine($"{Label("ε_res")}{Body(F9(epsRes))}   " + Dim("local magnitude |a_dev|"));

        // Optional consistency check line (quiet, non-alarming)
        if (double.IsFinite(CurrentEps) && System.Math.Abs(CurrentEps - epsRes) > 1e-8)
            sb.AppendLine($"{Label("ε_res chk")}{Dim("probe mismatch: stored ε differs from |a_dev| (check State(...) definition).")}");

        sb.AppendLine($"{Label("ε_abs")}{Body(F9(epsAbs))}   " + Dim("global enforced aperture 1/6"));

        sb.AppendLine($"{Label("r_dev")}{Body(F9(CurrentRDev))}   " + Dim("centred radial deviation share (λ−λ₀)/λ₀"));

        sb.AppendLine($"{Label("C")}{Body(F9(CurrentC))}   " + Dim("coherence retention C = exp(−κ)"));

        // Avoid tofu: keep label as "kappa" even if κ glyph missing.
        sb.AppendLine($"{Label("kappa")}{Body(F9(CurrentKappa))}   " + Dim("curvature spend κ = a_dev² + r_dev²"));

        sb.AppendLine($"{Label("Arc")}{Body(CurrentArc.ToString())}   " + Dim("nearest arc-midpoint label from κ"));

        sb.AppendLine($"{Label("m*")}{Body($"{F4(CurrentMStar)} MeV")}   " + Dim("rest-energy output at the probe point"));

        if (double.IsFinite(CurrentMStarTwin))
            sb.AppendLine($"{Label("m* twin")}{Body($"{F4(CurrentMStarTwin)} MeV")}   " + Dim("mirror-side companion output"));

        return sb.ToString();
    }
 
    private static int ParseIntOrDefault(string s, int dflt)
    {
        if (int.TryParse((s ?? "").Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var v))
            return v;
        return dflt;
    }

    private static string StepsToFlavour(int a, int b, int c)
    {
        char Map(int v)
        {
            switch (Mathf.Clamp(v, 1, 5))
            {
                case 1: return 'u'  ;
                case 2: return 'd'  ;
                case 3: return 's'  ;
                case 4: return 'c'  ;
                case 5: return 'b'  ;
                default: return 'u' ;
            }
        }
        return new string(new[] { Map(a), Map(b), Map(c) });
    }

    private double ReadSpinFromDropdownOrDefault()
    {
        if (spinDropdown == null || spinDropdown.options == null || spinDropdown.options.Count == 0)
            return 0.5;

        string t = (spinDropdown.options[spinDropdown.value].text ?? "").Trim();

        // Accept: "1/2", "0.5", "1"  -> 1/2
        //         "3/2", "1.5", "3"  -> 3/2
        if (t.Contains("/"))
        {
            var parts = t.Split('/');
            if (parts.Length == 2 &&
                double.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out var a) &&
                double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var b) &&
                Math.Abs(b) > 1e-12)
            {
                t = (a / b).ToString(CultureInfo.InvariantCulture);
            }
        }

        if (double.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out var n))
        {
            if (Math.Abs(n - 0.5) < 1e-9 || Math.Abs(n - 1.0) < 1e-9) return 0.5;
            if (Math.Abs(n - 1.5) < 1e-9 || Math.Abs(n - 3.0) < 1e-9) return 1.5;
        }
        return 0.5;
    }

    // =========================================================================
    // INPUT PARSING + BOOKKEEPING
    // =========================================================================

    public static class InputParser
    {
        public struct ParsedInput
        {
            public string Raw;

            // Flavour counts (if flavour letters present)
            public bool HasFlavourLetters;
            public string FlavourSanitised;
            public int NU, ND, NS, NC, NB;

            // Step vector (always present)
            public int StepA, StepB, StepC;

            public int TotalQuarks => NU + ND + NS + NC + NB;
        }

        public struct CheckResult
        {
            public bool Ok;
            public string Reason;
        }

        /// Parse either:
        ///  - three digits 1–5 (contrast step vector), or
        ///  - flavour letters u d s c b (first three letters also map to steps),
        ///  or a mixture (digits take priority for the step vector).
        public static ParsedInput ParseFlavourOrSteps(string raw)
        {
            raw = raw ?? "";
            var p = new ParsedInput
            {
                Raw = raw,
                HasFlavourLetters = false,
                FlavourSanitised = "",
                NU = 0, ND = 0, NS = 0, NC = 0, NB = 0,
                StepA = 1, StepB = 1, StepC = 1
            };

            string s = raw.Trim().ToLowerInvariant();

            // 1) Extract digits 1–5
            var digits = s.Where(ch => ch >= '1' && ch <= '5').Select(ch => (int)(ch - '0')).ToList();
            if (digits.Count >= 3)
            {
                p.StepA = ClampStep(digits[0]);
                p.StepB = ClampStep(digits[1]);
                p.StepC = ClampStep(digits[2]);
            }
            else
            {
                // 2) Fallback: map first three flavour letters to steps
                //    Map is the same as the HTML tool: u→1, d→2, s→3, c→4, b→5.
                var map = new Dictionary<char, int> { { 'u', 1 }, { 'd', 2 }, { 's', 3 }, { 'c', 4 }, { 'b', 5 } };
                var letters = s.Where(ch => map.ContainsKey(ch)).Take(3).ToList();
                if (letters.Count > 0)
                {
                    var steps = letters.Select(ch => map[ch]).ToList();
                    while (steps.Count < 3) steps.Add(1);
                    p.StepA = ClampStep(steps[0]);
                    p.StepB = ClampStep(steps[1]);
                    p.StepC = ClampStep(steps[2]);
                }
            }

            // 3) Count flavour letters (for bookkeeping and sector rules)
            var flv = new string(s.Where(ch => "udsbc".Contains(ch)).ToArray());
            p.FlavourSanitised = flv;
            if (flv.Length > 0)
            {
                p.HasFlavourLetters = true;
                foreach (char ch in flv)
                {
                    if (ch == 'u') p.NU++;
                    else if (ch == 'd') p.ND++;
                    else if (ch == 's') p.NS++;
                    else if (ch == 'c') p.NC++;
                    else if (ch == 'b') p.NB++;
                }
            }

            return p;
        }

        private static int ClampStep(int v) => Mathf.Clamp(v, 1, 5);

        /// Bookkeeping checks:
        ///  - If flavour letters are present: require exactly 3 quarks and charge consistency with QCD bookkeeping
        ///  - Enforce Pauli guard: three identical quarks cannot be spin 1/2 in this map
        ///  - Guard mixed charm-bottom content (outside current seat map)
        public static CheckResult BookkeepingOK(ParsedInput p, int Q, double spin)
        {
            if (!p.HasFlavourLetters)
            {
                // Step-vector-only input: we cannot verify charge, but we can still apply
                // the antisymmetry (Pauli) guard when the implied triad is fully symmetric.
                bool threeSameSteps = (p.StepA == p.StepB) && (p.StepB == p.StepC);
                if (threeSameSteps && Math.Abs(spin - 0.5) < 1e-9)
                {
                    return new CheckResult
                    {
                        Ok = false,
                        Reason = "Pauli guard: a fully symmetric triple (e.g. 111, 222, …) does not resolve as spin 1/2 in this map. Use spin 3/2 or a mixed step vector (e.g. 112)."
                    };
                }

                return new CheckResult
                {
                    Ok = true,
                    Reason = "No flavour letters provided. Charge-content bookkeeping was skipped."
                };
            }

            int total = p.TotalQuarks;
            if (total != 3)
            {
                return new CheckResult
                {
                    Ok = false,
                    Reason = $"This tool expects a three-quark triad (B = 1). The flavour string contains {total} quarks."
                };
            }

            // QCD bookkeeping: Q = (2/3)(u+c) + (-1/3)(d+s+b)
            int num = 2 * (p.NU + p.NC) - (p.ND + p.NS + p.NB); // = 3Q_content
            if (num != 3 * Q)
            {
                double qCont = num / 3.0;
                return new CheckResult
                {
                    Ok = false,
                    Reason = $"Charge mismatch from flavour content. Content charge Q = {qCont:F2}, selected Q = {Q}."
                };
            }

            bool threeSame = (p.NU == 3) || (p.ND == 3) || (p.NS == 3) || (p.NC == 3) || (p.NB == 3);
            if (threeSame && Math.Abs(spin - 0.5) < 1e-9)
            {
                return new CheckResult
                {
                    Ok = false,
                    Reason = "Three identical quarks do not resolve as a spin-1/2 ground state under the antisymmetry guard in this scaffold. Use spin 3/2 for fully symmetric flavour triples."
                };
            }

            if (p.NC > 0 && p.NB > 0)
            {
                return new CheckResult
                {
                    Ok = false,
                    Reason = "Mixed charm–bottom flavour (c and b both present) is outside the current arc-seat map."
                };
            }

            return new CheckResult { Ok = true, Reason = "" };
        }
    }

    // =========================================================================
    // SEAT SELECTION RULES (⟨L,U⟩ from flavour depth, charge tier, and spin)
    // =========================================================================

    public struct SeatSelectionResult
    {
        public bool Ok;
        public string Reason;

        public string Sector;   // "light/strange", "charm", "bottom"
        public int L;
        public int? U;

        public int StrangeDepth;
        public int CharmDepth;
        public int BottomDepth;

        public string Comment;
    }

    public static class SeatRules
    {
        /// Implements the same seat rules as the HTML tool:
        ///   - bottom tier: U undefined (∅), inward L carries identity tier
        ///   - charm tier: U = 7 fixed
        ///   - light/strange: L by charge tier; U by strange depth with resonance lift for spin 3/2
        public static SeatSelectionResult DeriveSeat(InputParser.ParsedInput p, int Q, double spin)
        {
            int nS = p.NS;
            int nC = p.NC;
            int nB = p.NB;

            int absQ = Math.Abs(Q);
            int Z = absQ; // charge tier used by seat rule

            if (nC > 0 && nB > 0)
            {
                return new SeatSelectionResult
                {
                    Ok = false,
                    Reason = "Mixed charm–bottom content is outside the current lattice seats.",
                    Sector = "mixed",
                    L = 0,
                    U = null
                };
            }

            // ----------------------------
            // Bottom tier (U undefined)
            // ----------------------------
            if (nB > 0)
            {
                int d = Mathf.Clamp(nB, 1, 3);
                if (absQ > 1)
                {
                    return new SeatSelectionResult
                    {
                        Ok = false,
                        Reason = "|Q| = 2 at bottom collapses to the broken apex under this seat set.",
                        Sector = "bottom",
                        L = 0,
                        U = null
                    };
                }

                int L;
                if (d == 1) L = (Q == 0) ? 5 : 6;
                else if (d == 2) L = (Q == 0) ? 6 : 7;
                else
                {
                    if (Q != 0)
                    {
                        return new SeatSelectionResult
                        {
                            Ok = false,
                            Reason = "bbb carries Q = 0 only in this lattice seat set.",
                            Sector = "bottom",
                            L = 0,
                            U = null
                        };
                    }
                    L = 7;
                }

                return new SeatSelectionResult
                {
                    Ok = true,
                    Sector = "bottom",
                    L = L,
                    U = null,
                    StrangeDepth = nS,
                    CharmDepth = nC,
                    BottomDepth = nB,
                    Comment = "Bottom tier: U is undefined (right-fold partner of arc 1); inward L carries identity tier."
                };
            }

            // ----------------------------
            // Charm tier (U = 7 fixed)
            // ----------------------------
            if (nC > 0)
            {
                int d = Mathf.Clamp(nC, 1, 3);
                int L;

                if (d == 1)
                {
                    if (Q == 0) L = 2;
                    else if (absQ == 1) L = 3;
                    else if (Q == 2) L = 6;
                    else return Fail("c tier allows Q ∈ {0, ±1, +2}", "charm", nS, nC, nB);
                }
                else if (d == 2)
                {
                    if (Q == 0) L = 3;
                    else if (absQ == 1) L = 4;
                    else if (Q == 2) L = 6;
                    else return Fail("cc tier allows Q ∈ {0, ±1, +2}", "charm", nS, nC, nB);
                }
                else
                {
                    if (Q == 0) L = 4;
                    else if (absQ == 1) L = 5;
                    else if (Q == 2) L = 7;
                    else return Fail("ccc tier allows Q ∈ {0, ±1, +2}", "charm", nS, nC, nB);
                }

                return new SeatSelectionResult
                {
                    Ok = true,
                    Sector = "charm",
                    L = L,
                    U = 7,
                    StrangeDepth = nS,
                    CharmDepth = nC,
                    BottomDepth = nB,
                    Comment = "Charm tier: U = 7 fixed; L from charge tier and charm multiplicity."
                };
            }

            // ----------------------------
            // Light / strange tier
            // ----------------------------
            int Llight;
            if (Z == 0) Llight = 1;
            else if (Z == 1) Llight = 2;
            else if (Q == 2) Llight = 5;
            else return Fail("Light tier expects Q ∈ {0, ±1, +2}", "light/strange", nS, nC, nB);

            bool isSpin32 = Math.Abs(spin - 1.5) < 1e-9;

            int U;
            if (isSpin32)
            {
                // resonance lift, cap at 6 (arc 7 reserved for charm)
                if (nS == 0 && Z == 2)
                    U = 6; // Δ++ broken-apex guard
                else
                    U = Math.Min(6, 4 + nS);
            }
            else
            {
                U = 3 + nS;
            }

            return new SeatSelectionResult
            {
                Ok = true,
                Sector = "light/strange",
                L = Llight,
                U = U,
                StrangeDepth = nS,
                CharmDepth = nC,
                BottomDepth = nB,
                Comment = isSpin32
                    ? "Light/strange decuplet: U = min(6, 4 + S depth) (resonance lift)."
                    : "Light/strange octet: U = 3 + S depth."
            };
        }

        private static SeatSelectionResult Fail(string reason, string sector, int nS, int nC, int nB)
        {
            return new SeatSelectionResult
            {
                Ok = false,
                Reason = reason,
                Sector = sector,
                L = 0,
                U = null,
                StrangeDepth = nS,
                CharmDepth = nC,
                BottomDepth = nB
            };
        }
    }

    // =========================================================================
    // REGISTER: constants + deterministic numerical procedures (band scan)
    // =========================================================================

    public static class Register
    {
        // ----------------------------
        // Public constants (shared)
        // ----------------------------
        public const double PI = Math.PI;
        public const double TAU = 2.0 * Math.PI;

        // Lawful scan window endpoints
        public static readonly double LambdaMin = Math.Sqrt(2.0) - 1.0; // √2 − 1
        public static readonly double LambdaMax = 1.0;

        // Euclidean crossover λ0
        public static readonly double L0 = 1.0 / Math.Sqrt(2.0);

        // Coherence window
        public static readonly double EPS_MIN = 1.0 / 6.0;
        public static readonly double KMIN = EPS_MIN * EPS_MIN;
        public static readonly double KMAX = 3.0 - 2.0 * Math.Sqrt(2.0);
        public static readonly double CMAX = Math.Exp(-KMIN);
        public static readonly double CMIN = Math.Exp(-KMAX);

        // Unit seal span U0 (MeV·fm)
        public static readonly double U0 = 1.0;

        // Incompleteness energy scale (IES): (5π/3)^2 * U0
        public static readonly double IES_TILDE = Math.Pow(5.0 * Math.PI / 3.0, 2.0);
        public static readonly double IES = IES_TILDE * U0;
        public static readonly double IES_D = IES / U0;

        // Inherited MeV anchor (carrier constant)
        public static readonly double UL = 1e15; // MeV·fm (1 m = 10^15 fm)

        // Derived correspondence constant and echo closure scale
        public static readonly double HBARC_GEOM = (UL / TAU) * Math.Exp(-IES_TILDE);
        public static readonly double M0E = PI * (HBARC_GEOM * HBARC_GEOM) / (L0 * U0);

        // Deterministic scan resolution
        public const int BandSamplesDefault = 1000;

        // Arc midpoint κ values (dimensionless) expressed in register-normalised form.
        // This list is copied from the HTML reference implementation and must remain
        // in this exact order.
        private static readonly double[] ARC_MID =
        {
            1.0 / IES_D,
            Math.Sqrt(2*PI - 2*PI/(PI/2)) / IES_D,
            Math.Sqrt(2*PI - PI) / IES_D,
            Math.Sqrt(2*PI - 2*PI/3) / IES_D,
            Math.Sqrt(2*PI - PI/2) / IES_D,
            Math.Sqrt(2*PI) / IES_D,
            Math.Sqrt(2*PI + 2*PI/Math.E) / IES_D
        };

        // ----------------------------
        // State containers
        // ----------------------------

        public readonly struct StateResult
        {
            public readonly double Eps;
            public readonly double RDev;
            public readonly double C;
            public readonly double Kappa;
            public readonly double Phi;

            public StateResult(double eps, double rdev, double c, double kappa, double phi)
            {
                Eps = eps;
                RDev = rdev;
                C = c;
                Kappa = kappa;
                Phi = phi;
            }
        }

        public readonly struct EnergyResult
        {
            public readonly double Eval;
            public readonly double Eecho;
            public readonly double MStar;

            public EnergyResult(double eval, double eecho, double mstar)
            {
                Eval = eval;
                Eecho = eecho;
                MStar = mstar;
            }
        }

        public readonly struct ArcPoint
        {
            public readonly int Arc;
            public readonly double Kappa;
            public readonly double Lambda;

            public ArcPoint(int arc, double kappa, double lambda)
            {
                Arc = arc;
                Kappa = kappa;
                Lambda = lambda;
            }
        }

        public readonly struct DualArcResult
        {
            public readonly ArcPoint? Upper;
            public readonly ArcPoint? Lower;
            public readonly bool MirrorResolved;
            public readonly double MirrorLambda;
            public readonly int CurArc;
            public readonly double CurKappa;

            public DualArcResult(ArcPoint? upper, ArcPoint? lower, bool resolved, double mirrorLam, int curArc, double curKappa)
            {
                Upper = upper;
                Lower = lower;
                MirrorResolved = resolved;
                MirrorLambda = mirrorLam;
                CurArc = curArc;
                CurKappa = curKappa;
            }
        }

        public readonly struct BandResult
        {
            public readonly bool Ok;
            public readonly double MMin;
            public readonly double MMax;
            public readonly double LambdaMin;
            public readonly double LambdaMax;
            public readonly int SampleCount;

            // Sorted list of admitted λ samples used by the scan (matches HTML `band.samples[*].lam`).
            // This allows the Unity slider to snap to admitted samples and avoid interior gaps.
            public readonly double[] SampleLams;

            // Mass values aligned with SampleLams (matches HTML `band.samples[*].mass`).
            // Used to select the sample closest to the band centre on boot.
            public readonly double[] SampleMasses;

            // Probe diagnostics (reported for convenience)
            public readonly double ProbeLambda;
            public readonly double ProbePhi;
            public readonly double ProbeEps;
            public readonly double ProbeRDev;
            public readonly double ProbeC;
            public readonly double ProbeKappa;
            public readonly int ProbeArc;
            public readonly double ProbeMStar;

            public BandResult(
                bool ok, double mMin, double mMax, double lamMin, double lamMax, int sampleCount, double[] sampleLams, double[] sampleMasses,
                double probeLambda, double probePhi, double probeEps, double probeRDev,
                double probeC, double probeKappa, int probeArc, double probeMStar)
            {
                Ok = ok;
                MMin = mMin;
                MMax = mMax;
                LambdaMin = lamMin;
                LambdaMax = lamMax;
                SampleCount = sampleCount;
                SampleLams = sampleLams;
                SampleMasses = sampleMasses;
                ProbeLambda = probeLambda;
                ProbePhi = probePhi;
                ProbeEps = probeEps;
                ProbeRDev = probeRDev;
                ProbeC = probeC;
                ProbeKappa = probeKappa;
                ProbeArc = probeArc;
                ProbeMStar = probeMStar;
            }
        }

        // ----------------------------
        // Core functions: charge, solve φ, state, energy
        // ----------------------------

        /// Dimensionless charge function q/e, as in the HTML reference implementation.
        public static double ChargeAt(double lam, double phi)
        {
            double dphi = phi - TAU;
            double epsSigned = dphi / TAU;
            double rD = (lam - L0) / L0;

            double C = Math.Exp(-(epsSigned * epsSigned + rD * rD));
            if (C >= CMAX) return 0.0;

            double k = -Math.Log(C);
            return epsSigned * (k / KMIN) * Math.Exp(1.0 - k);
        }

        /// Solve φ(λ,Z) by bisection. Z is the integer charge tier (Q in units of e).
        public static double SolvePhiAt(double lam, int Z)
        {
            if (Z == 0) return 2.0 * PI;

            double tgt = Z;
            int sgn = Z > 0 ? 1 : -1;

            double lo = 2.0 * PI;
            double hi = lo;
            double step = 1e-3;

            // Find a bracket where (q(lo)-tgt) and (q(hi)-tgt) have opposite signs.
            for (int k = 0; k < 24; k++)
            {
                hi = lo + sgn * step * Math.Pow(2.0, k);
                if ((ChargeAt(lam, lo) - tgt) * (ChargeAt(lam, hi) - tgt) < 0.0) break;
            }

            // Bisection refinement
            for (int it = 0; it < 90; it++)
            {
                double mid = 0.5 * (lo + hi);
                double q = ChargeAt(lam, mid) - tgt;

                if (Math.Abs(q) < 1e-10) return mid;

                if ((ChargeAt(lam, lo) - tgt) * q < 0.0) hi = mid; else lo = mid;
                if (Math.Abs(hi - lo) < 1e-12) break;
            }

            return 0.5 * (lo + hi);
        }

        public static StateResult State(double lam, double phi)
        {
            double eps = Math.Abs(phi - TAU) / TAU;
            double rdev = (lam - L0) / L0;
            double C = Math.Exp(-(eps * eps + rdev * rdev));
            double k = -Math.Log(C);
            return new StateResult(eps, rdev, C, k, phi);
        }

        public static EnergyResult Energies(double lam, double eps, double C)
        {
            double kappa = -Math.Log(C);
            double delta = 1.0 - C;
            double D = Math.Sqrt(delta);
            double deltaDiv = lam / L0 + L0 / lam - 2.0;

            // Dimensionless λ-tilde: λ / λ_max
            double lamN = lam / LambdaMax;

            double alpha0 = (eps * eps + delta) / (eps * eps + delta + lamN * lamN);

            double Eval = 6.0 * IES / lam * C;
            double Eecho = deltaDiv * alpha0 * (1.0 + D) * M0E;

            return new EnergyResult(Eval, Eecho, Eval + Eecho);
        }

        // ----------------------------
        // Arc map
        // ----------------------------

        public static int ArcFromKappa(double kappa)
        {
            int best = 1;
            double bestD = double.PositiveInfinity;

            for (int i = 0; i < ARC_MID.Length; i++)
            {
                double d = Math.Abs(kappa - ARC_MID[i]);
                if (d < bestD)
                {
                    bestD = d;
                    best = i + 1;
                }
            }
            return best;
        }

        // ----------------------------
        // Mirror λ′(λ): solve for equal m*(λ′) = m*(λ)
        // ----------------------------

        public static double TwinLambdaForSameMass(double currentLam, int Z)
        {
            var S = State(currentLam, SolvePhiAt(currentLam, Z));
            double target = Energies(currentLam, S.Eps, S.C).MStar;

            double F(double lam)
            {
                var s = State(lam, SolvePhiAt(lam, Z));
                return Energies(lam, s.Eps, s.C).MStar - target;
            }

            double Root(double a, double b)
            {
                double fa = F(a), fb = F(b);
                if (!double.IsFinite(fa) || !double.IsFinite(fb)) return double.NaN;

                // If no sign change, try to locate one by sampling.
                if (fa * fb > 0.0)
                {
                    double lastX = a;
                    double lastF = fa;
                    for (int i = 1; i <= 120; i++)
                    {
						// Use distinct variable names here to avoid shadowing the
						// Newton/bisection iterate 'x' declared later in this method.
						double xSample = a + (b - a) * i / 120.0;
						double fSample = F(xSample);
						if (!double.IsFinite(fSample)) continue;
						if (lastF * fSample <= 0.0)
                        {
                            a = lastX; fa = lastF;
							b = xSample; fb = fSample;
                            break;
                        }
						lastX = xSample; lastF = fSample;
                    }
                }

                // Hybrid Newton-bisection (matches the reference behaviour closely).
                double x0 = 0.5 * (a + b);
                double x = x0;

                for (int it = 0; it < 64; it++)
                {
                    double fx = F(x);
                    if (!double.IsFinite(fx)) break;
                    if (Math.Abs(fx) < 1e-10) return x;

                    double h = 1e-9;
                    double d = (F(x + h) - F(x - h)) / (2.0 * h);

                    if (double.IsFinite(d) && Math.Abs(d) > 1e-14)
                        x = x - fx / d;
                    else
                        x = 0.5 * (a + b);

                    // Keep bracket consistent.
                    if (F(a) * fx <= 0.0) b = x; else a = x;
                    if (Math.Abs(b - a) < 1e-12) break;
                }

                return x;
            }

            // In the reference: if currentLam >= λ0, solve on (λ_min, λ0); else on (λ0, λ_max).
            return currentLam >= L0
                ? Root(LambdaMin, L0)
                : Root(L0, LambdaMax);
        }

        public static DualArcResult ComputeDualArcs(double lam, int Z)
        {
            var sCur = State(lam, SolvePhiAt(lam, Z));
            int curArc = ArcFromKappa(sCur.Kappa);

            double lamOpp = TwinLambdaForSameMass(lam, Z);
            bool valid = double.IsFinite(lamOpp) && lamOpp > LambdaMin && lamOpp < LambdaMax;

            ArcPoint? upper = null, lower = null;

            if (lam >= L0)
            {
                upper = new ArcPoint(curArc, sCur.Kappa, lam);

                if (valid)
                {
                    var s = State(lamOpp, SolvePhiAt(lamOpp, Z));
                    lower = new ArcPoint(ArcFromKappa(s.Kappa), s.Kappa, lamOpp);
                }
            }
            else
            {
                lower = new ArcPoint(curArc, sCur.Kappa, lam);

                if (valid)
                {
                    var s = State(lamOpp, SolvePhiAt(lamOpp, Z));
                    upper = new ArcPoint(ArcFromKappa(s.Kappa), s.Kappa, lamOpp);
                }
            }

            return new DualArcResult(upper, lower, valid, valid ? lamOpp : double.NaN, curArc, sCur.Kappa);
        }

        // ----------------------------
        // Band scan: the deterministic engine
        // ----------------------------

        /// Scan the lawful λ range and return the band [m_min, m_max] for a given seat.
        ///
        /// Seat constraints:
        ///   - light/strange and charm tiers: require BOTH lower and upper arcs; lower.arc == L and upper.arc == U
        ///   - bottom tier: require ONLY lower arc; lower.arc == L and mirror is NOT resolved (U is ∅)
        public static BandResult ScanMassBand(int L, int? U, int Q, string sector)
        {
            int samples = BandSamplesDefault;
            double step = (LambdaMax - LambdaMin) / (samples - 1);

            double mMin = double.PositiveInfinity;
            double mMax = double.NegativeInfinity;
            double bandLamMin = double.NaN;
            double bandLamMax = double.NaN;
            int count = 0;

            // Store admitted samples (sorted by construction, since we scan increasing λ).
            // We keep λ and mass aligned to reproduce the HTML selection rule:
            // pick the sample whose mass is closest to the band centre.
            var admittedLams = new List<double>(samples);
            var admittedMasses = new List<double>(samples);

            // We also report one probe point for convenience: λ0.
            double probeLam = L0;
            double probePhi = SolvePhiAt(probeLam, Q);
            var probeState = State(probeLam, probePhi);
            var probeEnergy = Energies(probeLam, probeState.Eps, probeState.C);
            int probeArc = ArcFromKappa(probeState.Kappa);

            for (int i = 0; i < samples; i++)
            {
                double lam = LambdaMin + i * step;

                double phi = SolvePhiAt(lam, Q);
                var S = State(lam, phi);

                // Coherence window gate
                if (S.C < CMIN || S.C > CMAX) continue;

                var pair = ComputeDualArcs(lam, Q);

                if (sector == "bottom")
                {
                    // bottom: lower must exist, match L; mirror must be unresolved.
                    if (!pair.Lower.HasValue) continue;
                    if (pair.Lower.Value.Arc != L) continue;
                    if (pair.MirrorResolved) continue;
                }
                else
                {
                    // light/charm: require both.
                    if (!pair.Lower.HasValue || !pair.Upper.HasValue) continue;
                    if (pair.Lower.Value.Arc != L) continue;
                    if (U.HasValue && pair.Upper.Value.Arc != U.Value) continue;
                }

                // Energy at THIS λ sample
                var E = Energies(lam, S.Eps, S.C);
                double m = E.MStar;

                if (m < mMin) { mMin = m; bandLamMin = lam; }
                if (m > mMax) { mMax = m; bandLamMax = lam; }
                count++;

                admittedLams.Add(lam);
                admittedMasses.Add(m);
            }

            bool ok = count > 0 && double.IsFinite(mMin) && double.IsFinite(mMax);

            double lamMinOut = ok ? admittedLams.First() : double.NaN;
            double lamMaxOut = ok ? admittedLams.Last() : double.NaN;
            double[] lamsOut = ok ? admittedLams.ToArray() : null;
            double[] massesOut = ok ? admittedMasses.ToArray() : null;

            return new BandResult(
                ok,
                ok ? mMin : double.NaN,
                ok ? mMax : double.NaN,
                lamMinOut,
                lamMaxOut,
                count,
                lamsOut,
                massesOut,
                probeLam, probePhi, probeState.Eps, probeState.RDev,
                probeState.C, probeState.Kappa, probeArc, probeEnergy.MStar
            );
        }
    }
}
