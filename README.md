# Omnisyndetic Framework Unity Project
## Pre-Dynamical Geometric Baryon Calculator

![Omnisyndetic Framework preview](https://github.com/user-attachments/assets/ae98bd65-3947-42d4-9010-2dba73f4e098)

This repository hosts the Unity project for the **pre-dynamical geometric baryon calculator** within the wider **Omnisyndetic Framework**.

It is public so that anyone can inspect the code, audit the implementation, modify the tool, and reason with the calculator directly. Some browser-based demonstrations also appear on the main website, but this repository provides the downloadable Unity project for direct inspection and development.

The calculator sits within a wider open programme organised around the question:

> **What follows if identity is not treated as primitive?**

Under that inversion, the baryonic construction is explored without taking intrinsic constituent masses or arbitrary fitted constants as primitive inputs. The numerics used here are intended to be relationally and geometrically derived within the construction itself.

The full derivation chain and surrounding reasoning are set out in **Volume II — Pre-Dynamical Baryonic Structure**:  
[https://omnisyndetics.org/volume-ii-pre-dynamical-baryonic-structure](https://omnisyndetics.org/volume-ii-pre-dynamical-baryonic-structure)

Step-by-step baryon derivations that can be followed directly in the mathematics are available here:  
[https://omnisyndetics.org/predynamics-baryon-demonstrations](https://omnisyndetics.org/predynamics-baryon-demonstrations)

This repository is here as a practical space for learning, testing, auditing, modification, and further development around the calculator itself.

---

## Omnisyndetic Framework

**Pronounced:** omni-syn-detic  
**Name:** *omni* (all) + *syndetic* (binding) → **all-binding**

**Definition:** an open programme in which identity is treated as a certified settlement outcome fixed by binding relations, rather than assumed in advance.

### Organising question

**What follows if identity is not treated as primitive?**

This repository belongs to the baryonic side of that wider programme. In this part of the work, the question is whether some aspects of baryon organisation, baryon classification, and associated mass-band structure can be approached through a fixed relational and geometric construction, prior to a full dynamical treatment.

---

## Main links

| Resource | Purpose | Link |
|---|---|---|
| Main website | Public home page and project overview | [omnisyndetics.org](https://omnisyndetics.org/) |
| OSF archive | Open research archive, hosted documents, and project materials | [OSF Project Overview](https://osf.io/2qdmu/overview) |
| Why Pre-Dynamics | Introductory explanation of the pre-dynamical starting point | [why-pre-dynamics](https://omnisyndetics.org/why-pre-dynamics) |
| Volume II | Full derivation chain for the geometric baryon construction | [Volume II](https://omnisyndetics.org/volume-ii-pre-dynamical-baryonic-structure) |
| Demonstrations | Step-by-step baryon derivations and worked case files | [Baryon demonstrations](https://omnisyndetics.org/predynamics-baryon-demonstrations) |

---

## What this repository is for

This repository is the public codebase for the Unity implementation of the **pre-dynamical geometric baryon calculator**.

It is here for:

- **learning**, by giving a direct route into the project through a runnable geometric baryon tool
- **testing**, by allowing users to enter baryon states and inspect the resulting outputs first-hand
- **auditing**, by making the implementation open to inspection alongside the written derivations
- **development**, as the calculator and wider project continue to grow
- **extension**, for anyone who wants to adapt the tool or build from it in related work

This is a public working project. It is here to be opened, read, checked, modified, and explored.

---

## Repository structure and core files

The main Unity source files for the calculator and its related visual components can be found under:

`Assets/Sources/Scripts/`

Within that folder, the central implementation is:

- `OmnisyndeticBaryonCalculator.cs`

That file is the core of the project. Its own header states that it is the **Unity reference implementation**, a deterministic replay of the fixed register used by the wider calculator system, and that it does not introduce new constants, tuning factors, or per-state adjustments. It also lays out the functional contract of the tool, the core register functions, and how the Unity layer connects inputs, seat selection, band scanning, and diagnostic publication.

The other named scripts in this repository are better read as supporting visual layers around that core:

- `RealTimeTriadVisualizer.cs`
- `TransductiveCoherenceParticles.cs`

`RealTimeTriadVisualizer.cs` is explicitly described as a **triadic visual diagnostic** driven by the quantities published by the baryon calculator. It is a visualisation layer, not the underlying derivation engine.

`TransductiveCoherenceParticles.cs` is a particle-system controller used to visualise the scalar coherence value. It is also a UI and visual diagnostic layer rather than the core register logic.

So reading the codebase plainly: the actual centre of the work in this repository is `OmnisyndeticBaryonCalculator.cs`, while the triad and coherence scripts are there to help render parts of the state visually.

The calculator file is commented and structured so that a reader can follow the implementation directly, inspect how the inputs are parsed, how the seat rules are applied, how the lawful scan is performed, and how the reported band is produced.

---

## What the calculator delivers

The calculator provides a **pre-dynamical geometric calculation of baryonic structure**.

A user can enter either:

- the **quark flavour content** of a baryon
- or the equivalent **structural values** used by the framework

From there, the calculator resolves the same state under the same fixed construction. The intended reading is that valid routes of entry converge on the same certified result, without a separate rescue layer behind the interface.

The calculator returns linked outputs showing:

- the geometric organisation associated with the chosen baryon state
- the allowed or excluded charge-tier behaviour visible from that organisation
- the corresponding state-space displacement read-out
- the triadic geometric view associated with the construction
- the resulting **rest-energy band** associated with that certified placement

The result is shown as a **band**, not as a fitted single value. The midpoint is reported only as the centre of the admitted interval.

So putting this simply, the calculator lets a user enter flavour content, charge, and spin, then inspect how the fixed geometric construction resolves the placement and the corresponding baryon mass interval.

---

## Pre-dynamical geometric baryon derivation

The derivation implemented here is **pre-dynamical** in a strict sense.

It does not begin by assuming space and time as primitive substrates of the construction, and it does not begin from a prior spacetime background into which the baryon is then placed. Instead, it begins from a fixed relational and geometric register, from which admissible organisation and mass-band assignment are read.

This keeps the work at the level of prior structure. It also leaves open the possibility that later work may examine how dynamical, relativistic, and symmetry-group descriptions stand in relation to that same foundation.

This repository should therefore be read as a practical implementation of a **pre-dynamical geometric baryon derivation**, not as a finished treatment of every further layer that may later be studied.

---

## Geometric and relational numerics

A central commitment of the project is that the numerics used by the baryon calculator are meant to come from the framework’s own fixed geometric and relational construction.

Under the inversion where identity is not primitive, this part of the programme explores whether some aspects of baryon structure can be approached without beginning from:

- intrinsic constituent masses
- arbitrary fitted constants
- state-by-state adjustment layers

The intended contrast here is methodological. The project is exploring a non-essentialist relational route to particle structure, baryon classification, and baryon mass bands through a geometric construction internal to the framework itself.

This should be read carefully. The calculator is not presented as a replacement slogan for established physics. It is a concrete object that can be inspected and checked by anyone interested in geometric baryon derivation, relational numerics, pre-dynamical structure, or alternative approaches to symmetry group classification.

---

## What Volume II sets out

**Volume II — Pre-Dynamical Baryonic Structure** sets out the full derivation chain for the geometric baryon construction used by this calculator.

It presents:

- a rigid six-site geometric register
- a discrete admissible index
- a finite catalogue of admissible placements indexed by ordered pairs ⟨L, U⟩
- a uniform rule assigning each admissible placement a rest-energy bracket **[m_min, m_max]**
- the corresponding midpoint as the centre of that admitted interval

The construction is presented as a structural classification scaffold and mass-band read-out prior to any interaction model. Familiar labels such as flavour family, isospin, hypercharge, and multiplet organisation are then read as names for structure already fixed by the register once an admissible placement has been identified.

The evaluation uses geometric and trigonometric relations internal to the construction, together with one declared reporting interface for conventional units. Within the declared boundary, the construction is presented without per-state fitting or particle-specific adjustment.

---

## Audit form and boundary

The audit form used by the wider release is **containment-only**.

Each demonstration file records:

- the measured PDG rest-energy value **m\***
- the derived bracket **[m_min, m_max]**
- the derived midpoint
- the deterministic value chain producing those outputs

### Boundary used in the current release

The declared boundary covers PDG-established three-quark ground states across the light and strange sectors and through charm and bottom, together with immediate low-lying spin-3/2 partners where the PDG lists them in the same family.

The working set for the released audit is:

- **56 step-by-step baryon demonstrations**
- under the selection rule stated in **Volume II**

Higher resonances and orbital or radial excitations are excluded by design. Those states generally involve additional dynamical structure such as orbital angular momentum, radial excitation, mixing, and channel coupling. The present archive restricts the audit to a pre-dynamical boundary where the comparison can be made as a clean containment check.

---

## Status at release

As of this **2026 release**, the calculator reproduces the declared in-scope audit boundary with full containment under the stated rule. Within that boundary, the released construction places the audited ground-state baryons, including the immediate spin-3/2 partners included in the same selection, inside their assigned geometric rest-energy brackets.

This statement is tied to the release boundary and release date. It refers to the confirmed PDG-established cases used for that release. Within that scope, the model is working as intended at the time of upload.

The repository is also here for continued testing beyond that date. One purpose of making the calculator public is that future PDG-in-scope ground states can be checked against the same fixed construction and the same containment rule without changing the underlying setup.

Users are also welcome to explore configurations beyond the original audited set. You can enter states that were not part of the released audit, inspect how the calculator resolves them, and see whether they align with a certified band under the same geometric rules.

---

## What a user can do with it

### Reproduce audited baryon states

Users can enter established baryon flavour combinations and inspect the resulting geometric placement and associated rest-energy band under the fixed rules used by the calculator.

### Explore beyond the audited set

The same tool can also be used to test combinations beyond the presently audited census. This makes the repository useful not only for reproduction, but also for exploratory extension.

### Inspect the geometry directly

The Unity project is intended to show more than a numerical output. It allows users to inspect how geometric placement, charge-tier restriction, state-space displacement, and band assignment sit together as one connected construction.

### Compare implementation and written material

Because the project is public, users can compare the executable calculator against the wider written project and assess whether the implementation behaves in line with the stated structure.

### Modify and build from it

Anyone can inspect the codebase, adapt the interface, refine the presentation, extend the visualisation, experiment with related interaction models, or use the work as a starting point for nearby ideas of their own.

---

## Ongoing programme and forward testing

This repository belongs to an ongoing programme.

The tools and calculators are included so that the same decoding and bracket evaluation can be executed for future catalogue updates, including new ground states accepted under the same declared boundary after the release date.

A clean failure condition is built into the wider audit form:

> if a future PDG-in-scope ground state has a stable rest energy outside the bracket assigned to its admissible placement under the stated rule, the construction fails on that case

That condition is part of keeping the work open to independent checking at the level of specific cases.

---

## How to use this repository

### Recommended route

1. Clone or download this repository.
2. Open the Unity project locally.
3. Go to `Assets/Sources/Scripts/` if you want to inspect the code directly.
4. Start with `OmnisyndeticBaryonCalculator.cs`, which contains the core implementation and comments.
5. Enter a baryon flavour configuration or the equivalent structural input.
6. Adjust charge and spin where appropriate.
7. Inspect the resulting geometric panels and certified band.
8. Cross-check the outcome against the wider project materials.

### Wider audit route

A fuller audit route is:

1. Run the calculator and obtain **⟨L, U⟩**, **[m_min, m_max]**, the midpoint, and the reported classification labels.
2. Cross-check the bracket against the audit tables.
3. Open the matching demonstration file and verify the stepwise value chain.
4. Use **Volume II** to trace each step back to its definition and lemma-level derivation.
5. Use the wider project materials for the admissibility discipline governing what counts as defined.

Agreement or disagreement should then localise to a specific stage:

- admissibility or definition
- identification
- bracket evaluation
- table transcription
- implementation behaviour

---

## Relationship to the wider project

This repository is one part of the broader Omnisyndetic research programme.

The wider project includes:

| Area | Description |
|---|---|
| Pre-dynamical baryon work | geometric treatment of baryonic placement and rest-energy bands |
| Demonstration files | step-by-step case derivations for the released audit set |
| Audit materials | brackets and containment checks |
| Browser tools | HTML and JavaScript calculators and demonstrations |
| Interactive tooling | runnable projects such as this Unity implementation |
| Public archive | hosted documents, research records, and released materials |

This Unity repository belongs to the interactive and developer side of that wider effort.

---

## Current role of the repository

At present, this repository should be understood as both:

- a **usable public entry point** into the project
- a **growing codebase** around the geometric baryon calculator

It is already here to be used, tested, inspected, modified, and developed.

---

## Who this may be useful for

This repository may be useful to:

- readers who want a practical route into the project
- researchers interested in geometric or structural approaches to baryonic organisation
- developers who want to inspect or extend the interactive tooling
- independent auditors who want to compare executable behaviour with the written claims
- curious visitors interested in non-essentialist relational approaches to particle structure

---

## Science, philosophy, and open inspection

The project is intended to remain connected to science, to philosophy, and to the space where the two still speak to one another with care.

The scientific side asks whether the construction can be checked, followed, reproduced, or found wanting at the level of specific definitions, value chains, and cases.

The philosophical side asks what changes when identity is treated as a certified outcome of binding relations rather than as an assumed primitive.

This repository is one small part of holding those two concerns together in public, in a form that can be opened and examined rather than only described.

---

## Contact

If you have questions about the project, the calculator, the implementation, or the surrounding derivations, you are welcome to get in touch:

**Email:** tyronegabrielanderson@gmail.com

---

## A closing note

If you have found this repository, thank you for taking the time to look through it.

This is a small project within a larger ongoing piece of work. It is shared openly in the hope that it may be useful to readers, testers, critics, developers, and anyone with an interest in geometric approaches to baryons, relational approaches to physics, or careful alternatives to more essentialist starting points.

Whether you are here to inspect the code, follow the derivations, test the calculator, question the construction, or simply look around with curiosity, your attention is genuinely appreciated.

---

## Licence

- **Content:** CC BY-SA 4.0
- **Code:** AGPL-3.0

---

## Quick access

- **Main website:** [https://omnisyndetics.org/](https://omnisyndetics.org/)
- **OSF archive:** [https://osf.io/2qdmu/overview](https://osf.io/2qdmu/overview)
- **Why Pre-Dynamics:** [https://omnisyndetics.org/why-pre-dynamics](https://omnisyndetics.org/why-pre-dynamics)
- **Volume II:** [https://omnisyndetics.org/volume-ii-pre-dynamical-baryonic-structure](https://omnisyndetics.org/volume-ii-pre-dynamical-baryonic-structure)
- **Demonstrations:** [https://omnisyndetics.org/predynamics-baryon-demonstrations](https://omnisyndetics.org/predynamics-baryon-demonstrations)
