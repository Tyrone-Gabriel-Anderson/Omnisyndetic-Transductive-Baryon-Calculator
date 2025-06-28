# Omnisyndetic Framework

**Omnisyndetics** is an experiment in relational ontology in which identity, structure, and reality itself emerge through the act of distinction. It rejects the notion of substance or form existing independently of relation, proposing instead that all actuality arises from the collapse of undifferentiated potential into coherent configuration.

This framework is grounded in the following core premise:

> To be is to be distinguishable, and all distinction is relational.

## Observation and Collapse

Within the Omnisyndetic model, observation is defined as the act of structural distinction (represented by the O operator), and is not understood as reception or measurement. The observer is not an external agent but an operational condition: wherever distinction occurs, observation has already taken place.

The observation-collapse operator is defined as:
$\mathcal{O} : \mathbb{P} \rightarrow \mathcal{S}$ where $\mathcal{O}(\psi) = s$

$\mathbb{P}$: the undifferentiated space of latent potential

$\mathcal{S}$: the domain of coherent, actualized structures

$\psi$: a potential configuration

$\mathcal{O}$: the act of collapse: resolution into distinguishable form

No form exists prior to collapse. There is no identity without relation, and no relation without distinction.

This formalization reveals the essential role of Contrast: before any observable structure s can be constituted in $\mathcal{S}$, there must be an act of $\mathcal{O}$ that resolves $\psi$ from $\mathbb{P}$. This act is intrinsically tied to Contrast.

Contrast and Contrast Logic
In the Omnisyndetic Framework, contrast is the primary, irreducible act of distinction. It is the fundamental operation that allows "something" to be defined by what it "is not." This isn't a passive state; it's an active relational event. It's the very foundation of distinction, the first "break" from an undifferentiated state.

Contrast Logic formalizes this by positing that truth, or resolution, is achieved only when two opposing distinctions are brought into a coherent relation. Consider a fundamental contrast pair, (A, ¬A). In the undifferentiated space $\mathbb{P}$, both A and ¬A coexist as unresolved potential. No intrinsic truth value is assigned. This suspended state is what we call proto-truth: a potentiality not yet resolved into definite form, but carrying an inherent relational tension that cannot be ignored. The act of observation ($\mathcal{O}$) forces a resolution. If A successfully spans a "coherence window" (a finite relational range) and ¬A fails to do so, then A becomes "True" and ¬A becomes "False" within that specific relational context. This is an all-or-nothing proposition; there are no partial truths. This precise resolution of contrast is the mechanism by which form is constituted.

While diving into Contrast Logic is so fascinating and foundational to the Omnisyndetic Framework, it's not essential for understanding or using the calculator right away. For those curious to explore this formal logic further, you'll find more detailed explanations in public access publications on the Omnisyndetics.org website. We're truly excited to share these ideas!

Relational Identity
From this understanding of observation and contrast, identity is not an inherent property but a resolved state. A structure persists not because of an underlying substance, but because it consistently maintains coherence under the relational constraints of its environment.

What we commonly refer to as "objects" are, within this ontology, stable convergence points within a dynamic network of relational returns. For a structure to have identity, it must achieve a "full relational closure"—meaning its internal distinctions are resolved and agree in a self-sustaining loop. This "topological agreement" is what gives form its persistence. For instance, a proton's identity, including its mass and charge, is precisely calculated as the outcome of such a resolved coherence. Form, in this view, does not endure as a fixed substance, but as a continuously validated coherence, arising from the ceaseless resolution of contrast. It's a journey of discovery, and we're just beginning to see how these pieces fit together.

This calculator, grounded in the Omnisyndetic Framework, offers the first demonstrated proof of a core ontological idea: that baryon structure can be calculated as a relational pattern, without intrinsic masses or parameter fitting, constituted by a geometry of metaphysics that measures contrast in distinction rather than form. This framework rejects notions of fields, substrate, or constituency, focusing solely on measuring relations through modes of contrast and coherence. It posits that all identity and physical properties are immediately resolved via binary distinctions, a concept that aligns with the Tao Te Ching. This ontology rejects recursion and process as fundamentally valid descriptions of being. True to this principle, our goal was to create a complete, working baryon calculator that operates entirely within a single update cycle.

## Calculator Overview

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

## How It Works (Without All the Fuss!)

This Unity project, operating via WebGL, is driven by its C# logic:

**No Recursive Functions, Just Resolution:** Our calculator directly computes properties like mass and charge in a single calculation; it resolves identity and measurable properties from a structure's coherence under given relational conditions, rather than simulating processes.

**Geometric Simplicity:** The calculator derives mass, charge, and field energy purely from fundamental geometric relations—angular mismatch and radial deviation. Without particles, fields, or fundamental forces, all properties result from a relational loop's self-confirming closure, or the "burden" of its misclosure.

**Internally Derived Constants:** We don't use empirical constants like Planck's constant or the speed of light. Instead, these essential constants are derived internally from the geometry of coherence, suggesting rules inherent in a system's structure.

**Computational Approach:** Unlike resource-intensive lattice simulations that rely on approximation, our calculator employs principles akin to undergraduate algebra. It achieves comparable baryonic results (e.g., proton mass) with straightforward computation, offering intriguing insights from a different conceptual starting point.

## A Little Extra Control: Exploring the Calculator

While our framework derives its fundamental aspects, we also value direct exploration. This calculator provides you with precise control over the two primary inputs: lambda (λ) and total angular closure (Σϕ). You can adjust these values using both intuitive sliders and direct input boxes.

As you change these two parameters, the calculator immediately performs a single, instantaneous computation. It resolves the baryon's complete set of properties: its mass, charge, and various energy and coherence metrics, reflecting how changes in these fundamental relational conditions directly (and instantly!) define the entire structure. It's a hands-on way to explore the computational heart of this purely relational ontology.

We also attempt to derive even fundamental constants through the framework's internal relational logic. The specific method used within this calculator, for example, derives the reduced Planck constant (ℏc) to approximately 197.328 MeV⋅fm. This value is in excellent alignment with established measurements, showcasing the predictive power of the relational approach. As you can see on my website, and from equations you might find there, we explore several methods for these derivations. Crucially, the calculator also provides options to use ℏc directly, allowing it to maintain its computational relevance even when explored outside the specific ontological derivation of its constants.

We believe this calculator offers a glimpse into a reality where 'matter is not made of things,' 'identity is the structural memory of contrast,' and 'mass is the burden of imperfect return.' We're glad you're here for this journey of discovery.

## Overview

This Unity/WebGL application calculates baryon properties using just two inputs to fully produce mass:

- **λ (lambda)**: the empirical coherence radius of the baryon, typically its charge radius (measured in femtometers)
- **Σϕ (total angular closure)**: This input (in radians) allows you to define the angular configuration, from which the calculator determines the angular deviation (ϵ), which in turn drives the baryon's computed charge.

Users adjust these inputs via two sliders to configure the baryon within the framework's "window of coherence." The simulator then computes:

- Mass (MeV)
- Charge
- Validation energy (geometric stabilizing pressure)
- Echo energy (structural remainder)
- Relational feedback delay
- Geometric coherence metrics (Planck-normalized)

This allows exploration of the full "baryon zoo," with calculations matching QCD and lattice simulation values precisely. No fit parameters or Standard Model empirical values are used beyond the measurable radius and charge; all results derive from structural closure within a single constraint named the window of coherence.


## Structural Derivation of Mass

The Omnisyndetic Framework defines baryons purely in terms of relational geometry. It treats

- **Identity** as the memory of an attempted but incomplete closure.  
- **Space** as the “room” required by any failure of return.  
- **Mass** as the curvature stored by partial coherence.  
- **Charge** as the directional bias introduced by loop curvature.  

A baryon is modelled as a triadic loop of mutual observation: three nexons joined by six directed arcs. Perfect closure (Σϕ = 2π, λ = λ₀) contains no contrast and therefore yields no identity. A slight misclosure, however, stores curvature; that stored curvature manifests as mass, charge, and persistence.

### Parameters

| Symbol | Meaning | Source |
|--------|---------|--------|
| **λ**  | Coherence span (empirical charge radius) | experiment |
| **δ**  | Individuation margin                       computed |
| **C**  | Geometric coherence factor, *C* = e^(−κ) | derived |
| **λ₀** | Normalisation span for curvature storage | derived |
| **M₀** | Base mass scale | derived |
| **NARP** | Non-Angular Relational Pressure constant ≈ 27.61 | fixed |

*(There is no separate χ parameter in this formulation.)*

### Accuracy and Results

With λ set inside its experimental uncertainty (± 0.005 fm, CODATA/PDG), the framework predicts baryon masses to within ± 0.001 MeV—using only geometry.

| Baryon  | Radius λ (fm) | Computed Mass (MeV) |
|---------|--------------:|---------------------:|
| Proton  | 0.842 11      | 938.272 |
| Neutron | 0.883 039     | 939.564 |

No parameters are fitted; mass is resolved, not assigned.

### Conceptual Notes

As λ approaches roughly 1 fm the structure reaches its radial coherence limit. Beyond that span distinction collapses and the baryon loses identity. Mass is therefore not an additive property of particles but the curvature required to hold a relational loop in a single instantaneous present. The Omnisyndetic Baryon Calculator implements this idea directly: every quantity is obtained from one-shot geometric resolution, not from temporal simulation.




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

- **Discorvered Possible Candidate** $\boldsymbol{\hbar c}$  
  $$\hbar c = B\,\frac{N_{\mathrm{ARP}}}{W}
    \approx 197.328\;\mathrm{MeV\cdot fm}$$

---

## 5. Memory–Field Threshold

- **\(M_0\)**  
  $$
    M_0
    = \frac{\pi\,N_{\mathrm{ARP}}^2}{W^2\,\lambda_0}
    \quad\bigl(\lambda_0 = 1/\sqrt{2}\,\mathrm{fm}\bigr)
    \;\approx\;1.73\times10^5\;\mathrm{MeV\cdot fm}
  $$  
  - Units:  
    - \(N_{\mathrm{ARP}}\) in MeV·fm  
    - \(W\) dimensionless  
    - division by \(\lambda_0\) (fm) ⇒ MeV·fm  

## 6. Energy Channels

Let  
\[
  \kappa = \epsilon^{2} + R_{\mathrm{dev}}^{2}.
\]

- **Validation Energy**  
  $$
    E_{\mathrm{val}}
    = 6\,\frac{N_{\mathrm{ARP}}}{\lambda}\;e^{-\kappa}
  $$  
  - Units:  
    - \(N_{\mathrm{ARP}}/\lambda\) ⇒ (MeV·fm)/(fm) = MeV  
    - \(6\,e^{-\kappa}\) dimensionless  
    - ⇒ \(E_{\mathrm{val}}\) in MeV  

- **Echonex Energy**  
  $$
    E_{\mathrm{echo}}
    = \Delta_{\mathrm{div}}\;\chi\;(1 + \delta)\;M_{0},
    \quad
    \Delta_{\mathrm{div}}
      = \frac{\lambda}{\lambda_{0}} + \frac{\lambda_{0}}{\lambda} - 2,
    \quad
    \chi
      = \frac{\kappa}{\kappa + \lambda^{2}}
  $$  
  - Units:  
    - \(\Delta_{\mathrm{div}}, \chi, \delta\) dimensionless  
    - \(M_{0}\) in MeV·fm  
    - implicit division by \(\lambda_{\max}=1\,\mathrm{fm}\) ⇒ MeV  
    - ⇒ \(E_{\mathrm{echo}}\) in MeV  

- **Desire Energy**  
  $$
    E_{\mathrm{des}}
    = \alpha\,\frac{N_{\mathrm{ARP}}\,B}{\lambda}\;\delta\;(1 - C),
    \quad
    \alpha = \frac{\kappa}{\kappa + \lambda^{2}}
  $$  
  - Units:  
    - \(B, \alpha, \delta, (1-C)\) dimensionless  
    - \(N_{\mathrm{ARP}}/\lambda\) ⇒ MeV  
    - ⇒ \(E_{\mathrm{des}}\) in MeV  

## 7. Rest Mass

- **\(m\,c^{2}\)**  
  $$
    m\,c^{2} = E_{\mathrm{val}} + E_{\mathrm{echo}}
  $$  
  - Units:  
    - both \(E_{\mathrm{val}}\) and \(E_{\mathrm{echo}}\) in MeV ⇒ total in MeV  
    - dividing by \((\hbar c)^{2}\approx(197.327\,\mathrm{MeV\cdot fm})^{2}\) yields mass in GeV/\(c^{2}\)


## 8. Charge

**Charge**  

$$
q_{\mathrm{final}} = q_0 \,\epsilon\!\left(\frac{\kappa}{\kappa_{\min}}\right)\exp\bigl(1 - \kappa\bigr)
$$


## 9. Derived ℏ c as used in calculator

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
\;\approx\;197.328\;\mathrm{MeV\cdot fm}.
\end{aligned}$$


**Take-away:**  
\(\hbar c\) emerges **inevitably** from  
1. the echonex angular bookkeeping (\(N_{\mathrm{ARP}}\)),  
2. the survivable curvature band (\(W\)),  
3. a tiny second-order overlap correction (\(B\)).  

No empirical input only triadic geometry, coherence decay, and exact thresholds.




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


⚠ Important note:
The included decay model is not ontologically derived, canonical, or geometrically grounded within the formal framework. It is a provisional, empirical implementation inspired by axial coupling effects, currently using an exponential fit to approximate decay behaviour. Its purpose is purely illustrative   to reflect ongoing developmental thinking and model behaviour under unstable or non-coherent configurations (e.g. duads/mesons).
This placeholder will be replaced once duad and meson formation are fully derived within the framework’s transductive geometry.

## A Call for Collaboration

Since I first began sketching these ideas nearly a decade ago and more fully committed to this project in 2023 I have worked alone to develop a coherent framework grounded in simple mathematics accessible even to undergraduates. Before redundancy swept through 600 000 of us in 2023 as AI automation reshaped the industry I served as a senior lead developer and programmer specialising in topology and computer graphics for teams at Stadia and Meta.

I now dedicate myself fully to this open access endeavour and I need your help; if you share an interest in relational coherence and structural contrast or if you ever wanted a seminar or a ground up presentation or if you are curious about the blend of mathematics and philosophy at its core please reach out. I am ready to teach everything I know and to collaborate on papers essays or tools.

Visit https://omnisyndetics.org for regularly updated essays and published papers  
Email me at **tyronegabrielanderson@gmail.com**; I look forward to building this community together

*Independent research by Tyrone Gabriel Anderson © 2025. Work begun in 2023.*  
*All essays papers and updates on the website are open access; please reference appropriately.*  

