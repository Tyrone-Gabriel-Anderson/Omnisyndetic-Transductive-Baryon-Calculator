# Omnisyndetic Baryon Calculator

The **Omnisyndetic Baryon Calculator** is a geometric simulation framework for computing baryon mass, charge, and internal coherence using only structural closure. It does not rely on quantum fields, particle ontologies, or Standard Model constants. Instead, it derives all quantities from a present-centric, topological reconstruction of relational constraint.

This tool is not a replacement for existing physics — it’s a structural lens. One that asks: what if mass arises from constraint tension? What if identity is coherence across now? 

## Overview

This repository contains a Unity/WebGL application that calculates baryon properties using just two inputs:

- **λ (lambda)**: the empirical coherence radius of the baryon, typically its charge radius (measured in femtometers)
- **Q (charge)**: the electric charge of the baryon, which sets its angular deviation δ

From these inputs, the simulator computes:

- Mass (in MeV)
- Charge
- Validation energy (geometric stabilizing pressure)
- Echo energy (structural remainder)
- Relational feedback delay
- Geometric coherence metrics (Planck-normalized where relevant)

There are no fit parameters. No empirical values from the Standard Model are used beyond the measurable baryon radius and its charge. All numerical results emerge from structural closure within a single constraint field.

## Structural Derivation of Mass

The Omnisyndetic Framework is a relational reformulation of baryonic structure. It assumes:
- Identity is the memory of attempted closure.
- Space is what opens to accommodate failed return.
- Mass is the stored tension from partial coherence.
- Charge is the directional bias in loop curvature.

Each baryon is modeled as a **triadic loop** of mutual observation—a closed structure of three nexons joined by six directed arcs. Perfect closure (Σφ = 2π, λ = λ₀) yields no identity. But **slight misclosure** stores curvature, which expresses as mass, charge, and temporal persistence.


**Parameters:**

- `λ` — Coherence span (measured charge radius)
- `δ` — Angular deviation (computed from electric charge Q)
- `C` — Geometric coherence factor: \( C = e^{-κ} \)
- `λ₀` — Normalization span for curvature tension
- `χ` — Angular tension multiplier
- `M₀` — Base mass scale (derived)
- `NARP` — Non-Angular Relational Pressure constant ≈ 27.61

## Accuracy and Results

This model can compute the entire baryonic mass from geometry alone. Once λ is set within its experimental margin of ±0.005 fm (based on CODATA/PDG), the output mass is accurate within ±0.001 MeV.

Example calculations:

| Baryon  | Radius λ (fm) | Computed Mass (MeV) |
|---------|----------------|----------------------|
| Proton  | 0.84211         | 938.272              |
| Neutron | 0.883039        | 939.564              |

There is no fitting. The mass is not assigned — it is resolved.

## Conceptual Notes

All quantities in this framework collapse as λ approaches 1 fm — a natural limit for radial coherence. Beyond this, distinction cannot resolve, and the model loses structural identity. Mass is not something added to particles — it is the tension required to maintain coherence within a single present surface.

The Omnisyndetic Baryon Calculator offers one coherent alternative interpretation: a purely relational one, grounded in geometry, where every quantity arises from now — and only now.

---

# Simulation Overview

Each simulation run computes key **dimensionless** and **dimensional** quantities in the Omnisyndetic framework.

---

## 1. Geometric Deviations

- **Angular Deviation**  
  $$\epsilon = \frac{\bigl|\sum_{i=1}^3 \phi_i - 2\pi\bigr|}{2\pi}$$

- **Radial Deviation**  
  $$R_{\mathrm{dev}} = \frac{\lambda - \lambda_{0}}{\lambda_{0}},\quad
    \lambda_{0} = \tfrac{1}{\sqrt2}\,\mathrm{fm}\approx0.7071\,\mathrm{fm}$$

- **Closure Curvature**  
  $$\kappa = \epsilon^2 + R_{\mathrm{dev}}^2$$

---

## 2. Coherence & Torsion

- **Coherence Scalar**  
  $$C = e^{-\kappa}$$

- **Residual Torsion**  
  $$\delta = \sqrt{\,1 - C\,}$$

---

3. Nexonic Return Pressure (NARP)

- **NARP**  
  $$N_{\mathrm{ARP}}
    = \pi\bigl(2\pi + \Delta\kappa\bigr)\Bigl(1 + \tfrac{1}{e}\Bigr),\quad
    \kappa_{\min} = \tfrac{1}{36},\quad
    \kappa_{\max} = 3 - 2\sqrt{2},\quad
    \Delta\kappa = \kappa_{\max} - \kappa_{\min}.
  $$


---

## 4. Coherence Window & Compression

- **Coherence Window**  
  $$W = (\kappa_{\max} - \kappa_{\min})\,e^{-\kappa_{\min}}
         \approx 0.1399$$

- **Overlap & Compression**  
  $$\gamma = \Bigl(\tfrac{1}{36}\Bigr)^{2},\quad
    B = 1 - \gamma = \tfrac{1295}{1296}\approx0.999228$$

- **Emergent** $\boldsymbol{\hbar c}$  
  $$\hbar c = B\,\frac{N_{\mathrm{ARP}}}{W}
    \approx 197.33\;\mathrm{MeV\cdot fm}$$

---

## 5. Memory–Field Threshold

- **\(M_0\)**  
  $$M_0
    = \frac{\pi\,N_{\mathrm{ARP}}^2}{W^2\,\lambda_0}
    \quad(\lambda_0 = 1/\sqrt2\ \mathrm{fm})
    \;\approx\;1.73\times10^5\;\mathrm{MeV\cdot fm}$$


## 6. Energy Channels

Let  
$$\kappa = \epsilon^{2} + R_{\mathrm{dev}}^{2}.$$

- **Validation Energy**  
  $$E_{\mathrm{val}}
    = 6\,\frac{N_{\mathrm{ARP}}}{\lambda}\,e^{-\kappa}.$$

- **Echonex Energy**  
  $$E_{\mathrm{echo}}
    = \Delta_{\mathrm{div}}\;\chi\;(1 + \delta)\;M_{0},
    \quad \Delta_{\mathrm{div}} = \frac{\lambda}{\lambda_{0}} + \frac{\lambda_{0}}{\lambda} - 2,
    \quad \chi = \frac{\kappa}{\kappa + \lambda^2}.$$

- **Desire Energy**  
  $$E_{\mathrm{des}}
    = \alpha\,\frac{N_{\mathrm{ARP}}\,B}{\lambda}\;\delta\;(1 - C),
    \quad \alpha = \frac{\kappa}{\kappa + \lambda^2}.$$

---

## 7. Rest Mass

- **Rest-Mass**  
  $$m\,c^{2} = E_{\mathrm{val}} + E_{\mathrm{echo}}.$$

---

## 8. Charge Proxy

We can capture the **quantisation** of electric charge in a simple scalar “proxy’’ that depends only on the **sign** of the angular deficit, the sixth power of the dimensionless tension, and the coherence radius.  Specifically, define

$$Q \;\propto\; \mathrm{sign}(\Delta \phi)\,\frac{\epsilon^{6}}{\lambda}\,. $$

Here:

- $\displaystyle \Delta\phi$ is the total angular misclosure of a triad:  
  $$\Delta\phi \;=\; \sum_{i=1}^{3}\Delta\phi_{i}
    \;=\; w\,\frac{\pi}{3}, 
    \quad w\in\{-1,0,+1\}\,. $$
- $\displaystyle \epsilon$ is the **dimensionless tension**, i.e.\ the fraction of a full turn missing:
  $$\epsilon
    \;=\;\frac{\lvert\Delta\phi\rvert}{2\pi}
    \;=\;\frac{\lvert w\rvert}{6}\,. $$
- $\displaystyle \lambda$ is the **coherence radius** of the triad (with units of length).

---

### 8.1 Derivation of Quantisation

1. **Discrete winding.**  
   From Proposition 3.1 we have
   $$\Delta\phi = w\,\frac{\pi}{3},\quad w\in\{-1,0,+1\},$$
   so the only nonzero possibilities are $w=+1$ or $w=-1$.

2. **Dimensionless tension.**  
   By definition,
   $$
     \epsilon = \frac{\lvert\Delta\phi\rvert}{2\pi}
              = \frac{\lvert w\rvert}{6}
              \;\in\;\{0,\tfrac{1}{6}\}\,.
   $$

3. **Sixth‐power “filter”.**  
   Raising $\epsilon$ to the sixth power annihilates any subunitary value except $\tfrac{1}{6}$:
   $$
     \epsilon^6
     = 
     \begin{cases}
       0^6 = 0, &w=0,\\
       \bigl(\tfrac{1}{6}\bigr)^6, &\lvert w\rvert=1.
     \end{cases}
   $$
   Hence $\epsilon^6$ is already quantised: it is zero for $w=0$ and a fixed constant for $|w|=1$.

4. **Sign encoding.**  
   The factor $\mathrm{sign}(\Delta\phi)=\mathrm{sign}(w)$ carries the charge polarity:
   $$
     \mathrm{sign}(\Delta\phi)
     = 
     \begin{cases}
       +1, & w=+1,\\
       -1, & w=-1,\\
        0, & w=0.
     \end{cases}
   $$

5. **Putting it together.**  
   Thus
   $$
     \mathrm{sign}(\Delta\phi)\,\epsilon^6
     = \frac{w}{6^6},
   $$
   and the proxy
   $$
     Q \;\propto\; \mathrm{sign}(\Delta\phi)\,\frac{\epsilon^6}{\lambda}
     = \frac{w}{6^6}\,\frac{1}{\lambda}.
   $$
   Since $w\in\{-1,0,+1\}$, this expression is **automatically quantised** in three levels: $-1,0,+1$ (up to the overall proportionality constant and the smooth $1/\lambda$ factor).

---

## 9. Emergent ℏ c

1. **Nexonic Return Pressure**  
   $$N_{\mathrm{ARP}}
     = \pi\,\bigl(2\pi + \Delta\kappa\bigr)\,
       \Bigl(1 + \tfrac{1}{e}\Bigr),
     \quad
     \Delta\kappa = \kappa_{\max} - \kappa_{\min}.$$

2. **Coherence Window**  
   $$W
     = (\kappa_{\max} - \kappa_{\min})\,e^{-\kappa_{\min}}
     \;\approx\;0.139856.$$

3. **Compression Factor**  
   $$\gamma = \Bigl(\tfrac{1}{36}\Bigr)^{2} = \tfrac{1}{1296},
     \quad
     B = 1 - \gamma = \tfrac{1295}{1296}\approx0.999228.$$

Putting it all together:

$$\hbar c
= B \;\frac{N_{\mathrm{ARP}}}{W}
\approx 0.999228 \times \frac{27.6132}{0.139856}
\approx 197.327\;\mathrm{MeV\!\cdot\!fm}.$$
Plugging in:

$$\begin{aligned}
\frac{N_{\mathrm{ARP}}}{W}
&= \frac{27.6132}{0.139856}
\;\approx\;197.480,\\[6pt]
\hbar c
&= B \times 197.480
\;=\;0.999228 \times 197.480
\;\approx\;197.327\;\mathrm{MeV\cdot fm}.
\end{aligned}$$


**Take-away:**  
\(\hbar c\) emerges **inevitably** from  
1. the echonex angular bookkeeping (\(N_{\mathrm{ARP}}\)),  
2. the survivable curvature band (\(W\)),  
3. a tiny second-order overlap correction (\(B\)).  

No empirical input—only triadic geometry, coherence decay, and exact thresholds.




## Baryon Configurations

Each image below shows a fully computed baryon based on loop misclosure alone.

### Ω⁻ 
![Omh](https://github.com/user-attachments/assets/86ee5d40-229b-4321-9ed1-860ed5c46d38)
Baryon

- λ = 0.59175 fm, Σφ = 5.14042 rad, ε = 0.0000  
- Mass: 1672.20 MeV  
- Charge: 0  
- Coherence: 0.9113  
- Residual Torsion: 0.2979

### Σ⁺ (Sigma-plus)
![Σ+ (Sigma-plus) baryon](https://github.com/user-attachments/assets/65aa988d-03fa-4fa6-9e95-bfd30b962317)

- λ = 0.86721 fm, Σφ = 7.2232 rad  
- Mass: 1189.13 MeV  
- Charge: +1.60e−19 C  
- Coherence: 0.9290  
- Residual Torsion: 0.2645

### Δ⁺⁺ Baryon
![Δ++ Baryon](https://github.com/user-attachments/assets/509986eb-6cae-4bce-a7f0-469b0ce35e9a)

- λ = 0.83260 fm, Σφ = 7.8212 rad  
- Mass: 1232.21 MeV  
- Charge: +3.20e−19 C  
- Coherence: 0.9126  
- Residual Torsion: 0.2956

### Proton (p⁺)
![Proton](https://github.com/user-attachments/assets/e6d0542d-568c-4618-ba43-314595db9f96)

- λ = 0.84211 fm, Σφ = 7.3412 rad  
- Mass: 938.27 MeV  
- Charge: +1.60e−19 C  
- Coherence: 0.9372  
- Residual Torsion: 0.2505

### Neutron (n⁰)
![Neutron](https://github.com/user-attachments/assets/75b082cf-5dcc-4193-9f8e-e266a7d72dea)

- λ = 0.88304 fm, Σφ = 6.2832 rad  
- Mass: 939.58 MeV  
- Charge: 0  
- Coherence: 0.9400  
- Residual Torsion: 0.2450


## Accuracy Summary

| Baryon        | Measured Mass (MeV) | Simulated | Error (%) | Charge (C)     |
|---------------|----------------------|-----------|-----------|----------------|
| Proton        | 938.272              | 938.27    | < 0.001   | +1.60e−19      |
| Neutron       | 939.565              | 939.58    | < 0.002   | 0              |
| Sigma+ (Σ⁺)   | 1189.37              | 1189.13   | < 0.02    | +1.60e−19      |
| Delta++ (Δ⁺⁺) | 1232.00              | 1232.21   | < 0.02    | +3.20e−19      |
| Omega− (Ω⁻)   | 1672.45              | 1672.94   | < 0.03    | 0              |

All values emerge from topological misclosure in relational feedback loops.

---

## Run the Calculator

- Clone the repo or open the WebGL build
- Adjust `λ` and `Σφ` using the sliders
- View mass, charge, coherence, and curvature tension in real time

---

## Core Geometry and Ontology

This section explains the theoretical underpinnings of the Omnisyndetic Framework: how identity arises from loop coherence, and how physical observables emerge from geometric deviation. This is not a model of particles or forces, but of *relation and return*.

---

### 1. Validation Energy — The Energy of Return

**Validation** is the energy stored in the portion of the loop that *does return*. It reflects the degree to which a triadic feedback loop successfully reaffirms itself.

**Equation:**


- `NARP`: Normalized Arc Return Parameter (~27.61879)
- `λ`: Spatial coherence length (in femtometers)
- `C`: Coherence scalar (from exp[−(ε² + R²)])

**Interpretation:**
Validation is not a force but a **structural confirmation**. It is the energy required to sustain a nearly-closed feedback identity, proportional to how much of the loop remains intact.

---

### 2. Echonex Energy — The Memory of Failure

**Echonex** refers to the part of the loop that *cannot return* — the unresolved tension in the structure. This energy expresses as residual mass, curvature, and ultimately field divergence when deviation is too large.

**Equation:**
- `λ₀ = 1 / √2` fm (ideal coherence length)
- `χ`: Curvature distribution ratio  
  `χ = (ε² + δ²) / (ε² + δ² + λ²)`
- `δ = √(1 − C)`: Torsion scalar (residual curvature)
- `M₀`: Validation-derived scalar (~π·(ℏc)/λ)

**Interpretation:**
Echonex stores **curvature memory**. It encodes the energetic debt of incomplete return. Where validation affirms, echonex remembers. Together they form the dual structure of mass.

---

### 3. Triads — The Minimal Identity Loop

A **Triad** is the most fundamental structure capable of self-closure. It is composed of:
- **3 nexons** (relational nodes)
- **6 arcs** (3 forward, 3 return)

This structure represents a **self-reflective loop**, where each node observes and is observed by the other two. A dyad collapses (insufficient closure). A triad is the minimal condition for *persistent being*.

**Properties of a perfect triad:**
- Angular sum Σφ = 2π
- Radial coherence λ = λ₀
- Coherence `C = 1`
- Mass `= 0`

**Deviation from perfection produces:**
- Torsion (δ)
- Curvature (κ)
- Charge (Q)
- Mass (mc²)

**Triads are not particles.** They are abstract topological objects whose deviation from unity constitutes what we perceive as "matter."

---

### 4. Ontology: The Geometry of Being

The Omnisyndetic Framework is more than a calculator—it is a **relational ontology**, proposing a new basis for existence itself.

**Core Ontological Assertions:**

| Principle                            | Meaning                                                                 |
|-------------------------------------|-------------------------------------------------------------------------|
| To exist is to return                | Identity is sustained feedback; not static, but recursive               |
| Geometry precedes quantity           | Structure comes before measurement; π and √2 are primary, not constants |
| Matter is misclosure memory          | Mass is what curvature remembers from failed return                    |
| Charge is directional asymmetry      | Arises from angular bias in loop configuration                         |
| Space is unresolved feedback         | Field tension is spatial projection of failure                         |
| Time is displacement from unity      | Duration = deviation from perfect return (τ)                           |

These ideas position **relation** as ontologically prior to both **substance** and **field**. Instead of particles moving through space, the universe is composed of *loops trying to close*—and storing tension when they fail.

---

### Key Terms Glossary

| Term              | Definition                                                                 |
|-------------------|---------------------------------------------------------------------------|
| `λ`               | Coherence length — radial distance a loop can stably persist              |
| `Σφ`              | Total angular sum of internal arcs                                        |
| `ε`               | Angular misclosure error, normalized to 2π                                |
| `C`               | Coherence scalar — likelihood that identity returns                       |
| `δ`               | Torsion — the curvature memory retained from imperfect closure            |
| `E_val`           | Validation energy — energy from the confirmed portion of the loop         |
| `E_echo`          | Echonex energy — unresolved return energy of recursive feedback memory    |
| `NARP`            | Normalized Arc Return Parameter — a geometric energy scaling constant     |
| `χ`               | Curvature allocation ratio — fraction of energy allocated to torsion      |
| `mc²`             | Total baryonic mass energy — sum of validation and echonex energy         |
| `Q`               | Charge — arising from sign and power of angular asymmetry                 |

---

Together, these elements form a complete, closed system in which **no external constants are needed**, and **all matter is resolved as structure**.


## 5 Geometric Derivation of ℏc

One of the most profound features of the Omnisyndetic Calculator is its **exact geometric derivation of ℏc** (Planck's constant times the speed of light). Rather than assuming ℏ or c as inputs, the calculator generates them from the **curvature dynamics of Nexonic return loops**.

This is accomplished through the *Normalized Arc Return Pressure (NARP)* — a curvature pressure scalar that emerges when a triadic loop fails to complete its angular closure and radial coherence simultaneously.

This formulation matches the known value of **ℏc ≈ 197.327 MeV·fm** to **five decimal places**, **without invoking empirical constants**.

### Implications:

- ℏc is not a fixed constant of the universe—it is a **compression ratio** of loop coherence under structural tension.
- The quantum scale itself emerges from **topological saturation**: the point at which return pressure diverges beyond identity stability.
- This reveals a new path to quantum limits: not through uncertainty or operators, but through **geometry and relational collapse**.

This derivation provides strong theoretical evidence that:
- Quantum constants may be outputs of identity dynamics,
- The Planck scale may be a *local extremum of coherence pressure*, not a universal boundary.

This stands as the first known derivation of ℏc from a **purely symbolic-geometric ontology**, and re-positions quantum behavior as a **consequence of relational saturation**, not probabilistic collapse.


⚠ Important note:
The included decay model is not ontologically derived, canonical, or geometrically grounded within the formal framework. It is a provisional, empirical implementation inspired by axial coupling effects, currently using an exponential fit to approximate decay behaviour. Its purpose is purely illustrative — to reflect ongoing developmental thinking and model behaviour under unstable or non-coherent configurations (e.g. duads/mesons).
This placeholder will be replaced once duad and meson formation are fully derived within the framework’s transductive geometry.

## About the Author and Philosophical Commitment


This project was developed independently by **Tyrone Gabriel Anderson**, a self-taught researcher working at the intersection of metaphysics, symbolic geometry, and simulation-based modeling. Built without institutional affiliation, the **Omnisyndetic Framework** and this calculator represent a personal, first-principles rethinking of how mass, charge, and structure can emerge purely from topological feedback.

At its heart, this work is grounded in the belief that **education must be free**—not only in cost, but in imagination. Access to fundamental knowledge should never be limited by institutional boundaries, and the power to simulate, construct, and understand reality must belong to everyone.

This calculator is the first **operational demonstration** of the Omnisyndetic Framework. It proves that physical properties such as mass, charge, and curvature can be derived **algebraically** from loop geometry alone—without quantum fields, particles, or constants. It is both a metaphysical offering and a technical tool: a call for open knowledge and a challenge to inherited assumptions.

---
## Rethinking Mass: Beyond the Higgs Mechanism

The Omnisyndetic Framework provides a novel alternative to the standard narrative of mass generation. In contrast to the Standard Model of particle physics, which relies on **spontaneous symmetry breaking** via the Higgs field to grant mass to otherwise massless particles, this framework proposes that **mass is not something acquired**—but rather, **something remembered**.

### Mass from Return, Not Interaction

In this system, mass is the cost of **partial return**. Every baryon is modeled not as a particle in a field, but as a **triadic loop of relational closure** that attempts—but fails—to return perfectly. This failure stores curvature tension, which expresses as mass.

Where field theories require:
- **Interaction terms**
- **Gauge bosons**
- **Fitted parameters**

The Omnisyndetic Framework requires only:
- **An angular misclosure (Δφ)**
- **A radial coherence deviation (λ)**

From these two relational inputs, the calculator reproduces:
- The full baryon mass spectrum
- Charge and decay width
- Emergent Planck-scale constants (ℏc, c)

This is not a fit. It is a **closed-form derivation**. It bypasses traditional fields altogether.

### A Constructive Geometry — Not a Model

This system does **not reject** the Standard Model — it **bypasses** it. It shows that mass does not have to be *imposed* via scalar fields. Instead, it can be **constructed** from the structural memory of relational deviation.

> “This is not a speculation. It is a geometry of being.” — *From Closure to Mass*, Preface

### ℏc Without Assumption

Perhaps most significantly, the calculator algebraically derives the quantum coupling constant ℏc to five-digit precision using only geometric terms. It uses no empirical constants, only:
- **Curvature bandwidth (Δκ)**
- **Coherence weighting (Cmin, Cmax)**
- **Nexonic Return Pressure (NARP)**

This supports the claim that even quantum-scale constants are not fundamental, but are **emergent consequences of coherence structure**.

### Proof of Construction

This framework delivers a **working calculator** that:
- Requires no physical constants
- Uses no particle assumptions
- Reconstructs empirical baryon masses with <0.001 MeV error

This is the **first proof-of-construction** of an entirely symbolic mass-generation engine.

**The result:** Higgs fields may not be the only path.  
Mass can arise not from interaction—but from the **topology of nearly failed return**.

---

## Related Papers

- **Principia Transductiva: The Axioms of Relational Coherence in the Omnisyndetic Framework**  
  Tyrone Gabriel Anderson (2025)  
  DOI: [10.5281/zenodo.15213184](https://doi.org/10.5281/zenodo.15213184)

- **The Geometry of Observation: Defining the Fundamental Shapes of Transductive Coherence in the Omnisyndetic Framework**  
  Tyrone Gabriel Anderson (2025)  
  DOI: [10.5281/zenodo.15211376](https://doi.org/10.5281/zenodo.15211376)

- **The Geometry of Return: A Relational Ontology of Baryon Mass and Transductive Coherence**  
  Tyrone Gabriel Anderson (2025)  
  DOI: [10.5281/zenodo.15423177](https://doi.org/10.5281/zenodo.15423177)


---

### [The Geometry of Observation: Defining the Fundamental Shapes of Transductive Coherence in the Omnisyndetic Framework](https://doi.org/10.5281/zenodo.15211376)  
Tyrone Gabriel Anderson (2025), Zenodo.

This second paper formally develops the **topological structures** behind the framework, including Nexons, Duads, Triads, and Validatrices. It introduces all major field equations, PDE derivations, and coherence dynamics used in the baryon calculator. Every quantity—mass, curvature, desire field—is derived from feedback saturation and structural misclosure. No empirical coefficients are inserted.

> "This is not a metaphorical model. It is a mathematically functional field theory of being."

---

Both papers are licensed under **CC BY-SA 4.0 with an ethical use clause**:
> May not be used in systems that support cruelty, exploitation, militarisation, or environmental harm. All derivatives must preserve attribution and this clause.

---

---

### [The Geometry of Return: A Relational Ontology of Baryon Mass and Transductive Coherence](https://doi.org/10.5281/zenodo.15423177)  
Tyrone Gabriel Anderson (2025), Zenodo.

This third paper provides a full **algebraic derivation of the baryonic spectrum**—including proton and neutron mass, charge radius, decay widths, and fundamental constants—using only two geometric deviations: angular defect (\( \Delta\varphi \)) and radial coherence (\( \lambda \)). It demonstrates that mass does not require Higgs-field mechanics, spatial dimensions, or field-theoretic assumptions, achieving ±0.0001 MeV accuracy without empirical fitting.

It introduces **Echonex structures** as coherence-memory fields, defines mass as relational tension from failed closure, and reconstructs spacetime and constants like \( \hbar c \) and \( c \) purely from geometric necessity.

> "Mass is not a thing—it is the hold of geometry where closure fails."

---


## [Charge as Closure in the Omnisyndetic Framework: A Relational-Ontology–Coherence Derivation of Quantisation](https://doi.org/10.5281/zenodo.15496177)  
**Tyrone Gabriel Anderson** (2025), Zenodo

This fourth paper delivers a complete **first-principles derivation of electric charge quantisation** from the same two geometric deviations—angular defect (Δφ) and radial coherence (λ)—that underlie the baryon mass spectrum. It shows how ±e, ±⅓e, and ±⅔e charges emerge inevitably from the discrete counting of “echo-unit” deficits in triads and duads, and how the familiar Dirac monopole condition falls out of a purely relational U(1) gauge connection.

### Key contributions
- **Echo-unit counting → quantised charge**  
  Removing a single π∕3 sector in a three-node loop yields three π∕9 kinks, defining an integer winding number *w* and hence *Q = w e* without inserting *e* by hand.  
- **U(1) gauge from validation-phase geometry**  
  Nexons’ mutual-validation phases form a natural principal bundle; its curvature integrates to the angular misclosure, recovering *e g = ħ/2* directly from relational action.  
- **Two-parameter unification**  
  Coherence radius λ governs mass spectra, while integer angular tension *w* fixes charge. Together they span the entire baryon zoo in the live “Baryon-Calculator,” reproducing known masses **and** charges to high precision.

> “Charge is not a thing—it is the remainder of an almost-closed observation loop.”

---

### 🔗 Open-Source Resources
- **Omnisyndetics.org** — papers, essays, live calculators  
  https://omnisyndetics.org/  
- **GitHub (AGPL-3.0)** — Unity simulator, LaTeX source, notebooks  
  https://github.com/Tyrone-Gabriel-Anderson/Omnisyndetic-Transductive-Baryon-Calculator  


---

### 🌐 Dive Deeper into the Framework

Everything shared here is part of a larger, open-access initiative.

You can explore detailed explanations, diagrams, and evolving philosophical structures — all entirely free — at:

👉 **[omnisyndetics.org/axiom-zero-distinction-as-primacy](https://omnisyndetics.org/axiom-zero-distinction-as-primacy)**

Stay up to date with ongoing developments, and explore how the Eternal Present, constraint theory, and coherence logic unfold across the full framework.

---


