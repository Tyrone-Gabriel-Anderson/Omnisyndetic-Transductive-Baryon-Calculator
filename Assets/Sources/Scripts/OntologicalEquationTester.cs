using UnityEngine;

public class OntologicalEquationTester : MonoBehaviour
{
    void Start()
    {
        // 🌌 Ontological Equation Test
        Debug.Log("---- Ontological Return Test ----");

        // --- SECTION 1: Define Base Constants ---

        // h = Planck constant [J·s]
        // Represents the action per full cycle of coherence validation
        double h = 6.62607015e-34;

        // hbar = reduced Planck constant [J·s]
        // Represents action per radian, fundamental to angular identity
        double hbar = h / (2 * Mathf.PI);

        // c = speed of light [m/s]
        // Maximum return rate — not velocity, but coherence propagation limit
        double c = 3.0e8;

        // λ = coherence length [m]
        // The maximum range over which an identity can maintain relational feedback
        double lambda = 6.5294e-16;

        // --- SECTION 2: Derive Core Quantities from Ontological Return ---

        // Δt = coherence time [s]
        // How long an identity can wait before it must return to itself
        double deltaT = lambda / c;

        // f = frequency of return [Hz]
        // The number of complete identity revalidations per second
        double f = 1 / deltaT;

        // ω = angular frequency [rad/s]
        // The rate of recurrence expressed in radians, full cycle = 2π
        double omega = 2 * Mathf.PI / (float)deltaT;

        // --- SECTION 3: Derive Energy from Frequency Domain ---

        // E1 = energy from angular frequency using ħ·ω
        // Interprets energy as the cost per angular recurrence
        double E1 = hbar * omega;

        // E2 = energy from linear frequency using h·f
        // Classical Planck relation: energy as cycles per second
        double E2 = h * f;

        // --- SECTION 4: Derive Mass ---

        // m = derived from energy E1 = mc²
        double m = E1 / (c * c);

        // mDirect = mass from time-based formulation: m = h / (c²·Δt)
        double mDirect = h / (c * c * deltaT);

        // E3 = energy reconstructed from mass
        double E3 = m * c * c;

        // --- SECTION 5: Verify Closure Ratios ---

        // Ratio of ω/f — should return 2π, showing coherence as full recurrence
        double ratio1 = omega / f;

        // Should equal 1 — checks E1 equals ħω
        double ratio2 = E1 / (hbar * omega);

        // Should equal 1 — checks energy consistency: h·f vs. mc²
        double ratio3 = E2 / E3;

        // Closure test: (E3/h) / f = 1, shows full collapse of relational identity
        double closure = (E3 / h) / f;

        // --- SECTION 6: Output Results ---

        Debug.Log($"deltaT = {deltaT:E2} s");                       // Time delay of identity
        Debug.Log($"Frequency f = {f:E2} Hz");                      // Return pulse
        Debug.Log($"Angular frequency ω = {omega:E2} rad/s");      // Angular return rate
        Debug.Log($"Energy from hbar·ω: {E1:E2} J");               // Energy via angular domain
        Debug.Log($"Energy from h·f   : {E2:E2} J");               // Energy via frequency domain
        Debug.Log($"Energy from mc^2  : {E3:E2} J");               // Mass-reconstructed energy
        Debug.Log($"Mass from energy  : {m:E2} kg");               // Derived mass
        Debug.Log($"Mass from Δt      : {mDirect:E2} kg");         // Time-based identity cost

        Debug.Log("--------------------------------");

        // Display all closure identities
        Debug.Log($"ω / f = {ratio1}  (should be ≈ 2π)");
        Debug.Log($"E / (ħω) = {ratio2}  (should be 1)");
        Debug.Log($"E_hf / E_mc2 = {ratio3}  (should be 1)");
        Debug.Log($"Final Closure Identity = {closure}  (should be 1)");
    }
}
