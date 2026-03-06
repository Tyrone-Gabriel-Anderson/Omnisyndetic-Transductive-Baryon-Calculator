Omnisyndetic Calculator Prefab (UI shell)

What this is
- A cleaned-up UI prefab (Canvas + input/output widgets) prepared for the Omnisyndetic baryon calculator.
- This prefab is UI-only: it does NOT contain script references, because Unity script references require .meta GUIDs.

What you get on screen
- Sample count slider: SLD_Samples
- Lambda label: TXT_LambdaLabel
- Main triad frame placeholder: Frame_TriadMain
- Output text placeholders:
  - TXT_OutputReport
  - TXT_Diagnostics
- Systems root object: Systems
- Canvas renamed: Canvas_Omnisyndetic

How to make it functional (2 minutes)
1) Drag PF_OmnisyndeticCalculator into the scene (or this prefab if you rename it).
2) Add components:
   - OmnisyndeticBaryonCalculator to Systems (or a child)
   - RealTimeTriadVisualizer to Systems (or a child)
   - TransductiveCoherenceParticles to Systems (optional)
3) In the inspector, assign references by name:
   - Samples slider -> SLD_Samples
   - Output report text -> TXT_OutputReport
   - Diagnostic text -> TXT_Diagnostics
   - Any lambda label / UI text -> TXT_LambdaLabel
   - Triad visual target -> Frame_TriadMain (or its RawImage/Renderer as your visualiser expects)

If you upload the .meta files for the three scripts, I can return a prefab with all script references wired automatically.
