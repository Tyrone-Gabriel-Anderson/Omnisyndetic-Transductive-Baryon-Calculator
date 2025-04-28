using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/// <summary>
/// DEBUG VERSION: Confirms graph renders correctly onto RenderTexture.
/// Attach to a GameObject in a scene with a Canvas and RawImage.
/// </summary>
public class GraphCoherenceEnvelope : MonoBehaviour
{
    [Header("References")]
    public RenderTexture renderTarget;
    public RawImage outputImage;

    private void Start()
    {
        if (!CheckSetup()) return;

        outputImage.texture = renderTarget;

        Debug.Log("Rendering sigmoid envelope...");
        RenderSigmoidEnvelope();
    }

    private bool CheckSetup()
    {
        if (renderTarget == null)
        {
            Debug.LogError("RenderTexture not assigned.");
            return false;
        }

        if (outputImage == null)
        {
            Debug.LogError("RawImage UI element not assigned.");
            return false;
        }

        if (!renderTarget.IsCreated())
        {
            renderTarget.Create();
            Debug.Log("RenderTexture created at runtime.");
        }

        return true;
    }

    private void RenderSigmoidEnvelope()
    {
        var settings = new GraphRenderer.GraphSettings
        {
            resolution = 1024,
            xMin = 0,
            xMax = 100,
            yMin = 0.25f,
            yMax = 1.05f,
            axisThickness = 2,
            gridDivisions = 10,
            backgroundColor = Color.black,
            axisColor = Color.white,
            gridColor = new Color(0.6f, 0.6f, 0.6f),
            xLabel = "Centrality (%)",
            yLabel = "Azimuthal Width Δϕ (rad)"
        };

        var curves = new List<GraphRenderer.CurveData>
        {
            new GraphRenderer.CurveData(AwaySidePhi, Color.red, 2),
            new GraphRenderer.CurveData(NearSidePhi, Color.yellow, 2)
        };

        GraphRenderer.RenderCurves(renderTarget, curves, settings);
        Debug.Log("Render complete.");
    }

    private float Sigmoid(float x, float midpoint, float width)
    {
        return 1f / (1f + Mathf.Exp(-(x - midpoint) / width));
    }

    private float AwaySidePhi(float centrality)
    {
        return 0.52f + (1.0f - 0.52f) * Sigmoid(centrality, 40f, 15f);
    }

    private float NearSidePhi(float centrality)
    {
        return 0.28f + (0.33f - 0.28f) * Sigmoid(centrality, 50f, 30f);
    }
}
