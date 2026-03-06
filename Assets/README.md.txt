OMNISYNDETIC BARYON CALCULATOR
Unity reference implementation
Open research tool for deterministic baryon band read-out


OVERVIEW

This calculator is a Unity implementation of the fixed baryonic register and ledger developed within the Omnisyndetic programme. It is intended as a direct inspection interface for one declared geometric construction. The register, certified seats, admitted lambda intervals, and rest-energy bands are read from a single derived object rather than assembled from independent phenomenological parts.

The calculator does not add fitted coefficients, per-state correction terms, hidden sector-specific adjustments, or runtime rule changes. Its role is to read off the consequences of the fixed construction and display them clearly.

In that sense, the tool demonstrates a geometric route to baryon organisation and mass-band assignment. It does not begin from Lie algebra, representation tables, quark-mass inputs, fitted mixing terms, or sector-specific correction rules. The classification and read-out are generated from the declared closure geometry and its ledger rules.

This point is important for interpretation. The calculator is not presented as a generic data-fitting device. It is a deterministic read-out from one fixed object. You specify the admissible state labels, and the tool returns the band assigned by the register and ledger.

The resulting family structure may be compared with familiar symmetry-group classifications in baryon physics, since both organise the same physical catalogue. The calculator should nonetheless be read on its own terms. It is a geometric derivation of the classification and band structure, not a presentation that starts from symmetry-group machinery.

The displayed midpoint is only a visual centre of the admitted band. It is not a point-mass prediction and it is not an accuracy claim. The claim of the tool is deterministic interval assignment and containment under the fixed ledger.

The lambda probe is a read-out control inside an already admitted interval. It is used to inspect live values at one point in the band, including phi, signed angular deviation, coherence, curvature spend, arc label, the current rest-energy output, and the mirror lambda that gives the same mass on the opposite side of lambda_0. This probing does not create the band. The band is fixed first by the register and seat rules. The probe only moves within what the fixed construction already admits.

This calculator forms part of an ongoing open research programme. The aim is transparency, reproducibility, and direct inspection of the declared construction. The same fixed object can be checked against the audited states discussed in Volume II, and it can also be used to test further experimentally established ground states under the same rule set. The point of the tool is deterministic containment and audit, not post hoc point fitting.


HOW TO READ THIS TOOL

This tool should be read as an inspection interface for one fixed geometric construction.

The register, certified seats, admitted lambda intervals, and rest-energy bands are read from a single derived object rather than assembled from separate physical inputs. The state classification and band structure are not inserted case by case. They are read off from the same coupled object.

This is why the tool should not be mistaken for a generic slider-based mass fitter. You enter an admissible flavour or step configuration, together with charge tier and spin tier, and the calculator returns the band assigned by the fixed register and ledger.

The calculator can also be read as a demonstration of a geometric route to symmetry-style organisation in the baryon catalogue. It does not start from Lie algebra or representation-theoretic machinery and then translate those results into a visual interface. It begins from the declared closure object and reads the classification directly from that construction.

This does not by itself prove equivalence to SU(3), SU(6), QCD, or any other established formalism. Any exact equivalence, embedding, reduction, or superiority claim would need to be shown separately. The narrower and more exact claim made here is that a single geometric construction yields the classification and band read-out deterministically.

The midpoint shown by the tool should be read only as a visual centre of the interval. It is not a fitted best estimate and it is not a point prediction with an accuracy claim attached.

The lambda slider should also be read correctly. It does not alter the underlying construction and it does not inject a hidden tuning freedom into the band generation. It moves the live probe within the already admitted interval assigned by the fixed object.


WHAT THE TOOL CURRENTLY DOES

1. Accepts either:
   - a flavour string using the letters u, d, s, c, b, with exactly three quarks, or
   - a three-digit step vector using digits 1 to 5.

2. Accepts:
   - net charge Q in units of e,
   - spin tier 1/2 or 3/2.

3. Parses the input and applies bookkeeping checks, including:
   - three-quark requirement when flavour letters are supplied,
   - charge consistency from flavour content,
   - the antisymmetry guard blocking fully symmetric triples at spin 1/2,
   - the current exclusion of mixed charm-bottom flavour content.

4. Determines the certified seat from the fixed seat rules:
   - light/strange tier,
   - charm tier,
   - bottom tier,
   with the appropriate L and U assignment, or U = ∅ where required.

5. Scans the declared outer lambda span and extracts the admitted lambda samples for that seat, then reports:
   - admitted lambda interval,
   - deterministic mass band in MeV,
   - certified seat,
   - sector note.

6. Publishes live probe diagnostics from the current lambda sample, including:
   - lambda_probe,
   - lambda_twin,
   - phi,
   - a_dev,
   - epsilon_res,
   - epsilon_abs,
   - r_dev,
   - coherence C,
   - kappa,
   - nearest arc label,
   - m* and twin m*.

7. Renders a text report suitable for a Unity UI output panel.


WHAT THIS TOOL IS FOR

This is a deterministic band calculator and inspection tool.

It is for:
- reading off the band assigned by the fixed register,
- checking whether a measured baryon mass lies within that admitted band,
- inspecting how the live probe moves within the certified interval,
- exploring how the same object behaves across flavour and charge combinations,
- checking further experimentally established states under the same fixed rules.

It is also intended as a practical demonstration of how a single geometric object can organise the baryon catalogue and produce a lawful band structure without starting from standard symmetry-group machinery.

It is not a point-prediction fitter.

The midpoint shown by the tool is only a display convenience. It helps the user see the centre of the interval, but it should not be read as a claimed exact baryon mass. The actual claim of the tool is interval containment under a fixed deterministic ledger.


WHAT THIS TOOL DOES NOT DO

This tool does not:
- insert baryon masses as intrinsic inputs,
- fit one coefficient per state,
- tune one sector separately from another,
- introduce hidden corrective parameters,
- derive new rules at runtime,
- use the visual layer to alter the underlying register output.

The construction begins from the fixed register and ledger, and the Unity calculator replays that fixed structure.

This tool should not be read as a proof that the construction is already equivalent to SU(3), SU(6), QCD, or any other established formalism.

It should also not be read as a free-form curve fitter.

The claim is narrower and more exact. A single fixed geometric object is declared, and the baryon classification and mass-band read-out are taken from that object deterministically. Any equivalence to existing symmetry-group descriptions, or any physical superiority over them, would need to be argued separately.


REQUIRED FILES

The core functional file is:

- OmnisyndeticBaryonCalculator.cs

This is the calculator itself. It handles input parsing, bookkeeping, seat selection, deterministic lambda scanning, live probe evaluation, twin-lambda calculation, and output rendering. If you want the calculator to function, this is the file that matters.

Minimum UI wiring for practical use:
- flavourOrStepsInput
- chargeInput
- spinDropdown
- outputText

These are the useful minimum fields for entering a state and seeing a result. If the spin dropdown is not assigned, the script can try to find one automatically at runtime.


OPTIONAL BUT FUNCTIONAL DIAGNOSTIC FIELDS

These are not required for the basic band calculation, but they are useful if you want live lambda inspection:

- lambdaProbeSlider
- lambdaProbeInput
- lambdaTwinReadout
- massProbeInput
- massTwinReadout

These fields drive or display the live probe state only. The band scan still uses the fixed lawful scan window and the admitted samples found by the deterministic scan. They are optional diagnostics, not extra degrees of freedom.


VISUAL-ONLY FILES

The following files are presentation layers only. They are not needed to compute the register output.

- RealTimeTriadVisualizer.cs
- TransductiveCoherenceParticles.cs

RealTimeTriadVisualizer reads probe diagnostics already produced by the calculator and draws a triadic visual cue from them. It is a visualisation layer for the live register state.

TransductiveCoherenceParticles is a particle controller for the coherence value C in the interval [0,1]. It changes particle size, emission, colour, noise, and related display properties according to coherence. It is a UI layer for visual feedback only.

So putting this simply:

- OmnisyndeticBaryonCalculator.cs is the actual tool.
- RealTimeTriadVisualizer.cs is optional visual presentation.
- TransductiveCoherenceParticles.cs is optional visual polish for coherence display.


SETUP NOTES

1. Add OmnisyndeticBaryonCalculator.cs to a GameObject in your Unity scene.
2. Wire the input and output fields in the Inspector.
3. Provide a TMP_Dropdown for spin with the options 1/2 and 3/2, or let the script auto-find and initialise it.
4. If you want live probe control, wire the optional lambda and mass fields.
5. If you want the triad animation, add RealTimeTriadVisualizer.cs and point its simulator field to the calculator.
6. If you want particle-based coherence feedback, place TransductiveCoherenceParticles on the relevant particle objects used by the visualiser.


INTERPRETATION NOTES

The calculator reports a deterministic band, not a single tuned mass.

The lambda slider should be read as a probe inside the admitted interval for the certified state. Moving it does not alter the underlying object. It only changes where the live read-out is taken from inside the already lawful set.

The same applies when solving lambda from a mass input. The code chooses the closest admitted band sample rather than inventing a new state-specific fit.

The broader claim carried by the tool is that the register, seats, and ledger are fixed by one coupled construction. The Unity layer should be understood in that same spirit. It is an inspection layer over a fixed object.

The classification it returns may be compared with familiar baryon family organisation in standard physics, but the route taken here is geometric rather than representation-theoretic. That comparison is natural. Conflation is not. The tool should therefore be read carefully and on its own stated terms.


OPEN RESEARCH CONTEXT

This calculator forms part of the wider Omnisyndetic Open Research Programme.

The public archive is intended to keep the volumes, calculators, derivations, and supporting materials available in one inspectable record. The aim is direct evaluation, reproducibility, and open scrutiny rather than black-box presentation.

Volume I presents the formal foundation.
Volume II presents the six-site geometric baryon construction and ledger.
The calculators provide direct entry points for interactive inspection and independent checking.


PROJECT LINKS

Main site
https://omnisyndetics.org/

Archive overview
https://www.omnisyndetics.org/home

Volume I
https://omnisyndetics.org/volume-i-a-treatise-on-the-law-of-contrast

Volume II
https://www.omnisyndetics.org/volume-ii-pre-dynamical-baryonic-structure

Geometric Baryon Calculator
https://omnisyndetics.org/baryon-calculator

Ledger derivation inventory
https://omnisyndetics.org/triads

Glossary
https://www.omnisyndetics.org/glossary

Glossary download page
https://omnisyndetics.org/glossary-download

Open Science Framework record
https://osf.io/2qdmu/overview


OPEN RESEARCH

This calculator is released as part of an open research effort. The purpose is to let the declared structure be inspected, tested, challenged, and reproduced directly.

You are welcome to use it to:
- inspect the audited states,
- test further ground-state candidates as experimental catalogues grow,
- compare admitted bands against published masses,
- examine the current read-out discipline in a transparent way.

Thank you for taking the time to look at the project and explore the tool.