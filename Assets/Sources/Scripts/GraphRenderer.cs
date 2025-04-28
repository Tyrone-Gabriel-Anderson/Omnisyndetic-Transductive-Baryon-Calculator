using UnityEngine;
using System;
using System.Collections.Generic;

public static class GraphRenderer
{
    public class GraphSettings
    {
        public int resolution = 512;
        public float xMin = 0f, xMax = 100f;
        public float yMin = 0f, yMax = 1f;

        // Soft cream background
        public Color backgroundColor = new Color(0.98f, 0.97f, 0.94f);

        // Gentle pastel grey for axes
        public Color axisColor = new Color(0.5f, 0.5f, 0.5f);

        // Light pastel lavender for grid lines
        public Color gridColor = new Color(0.85f, 0.80f, 0.95f);

        public int axisThickness = 2;
        public int gridDivisions = 10;

        public string xLabel = "Centrality (%)";
        public string yLabel = "Azimuthal Width Δϕ (rad)";
    }



    public class CurveData
    {
        public Func<float, float> function;
        public Color color = Color.black;
        public int thickness = 2;

        public CurveData(Func<float, float> fn, Color col, int thick = 2)
        {
            function = fn;
            color = col;
            thickness = thick;
        }
    }

    public static void RenderCurves(RenderTexture target, List<CurveData> curves, GraphSettings settings)
    {
        int margin = 96;
        int graphSize = settings.resolution;
        int width = graphSize + margin;
        int height = graphSize + margin;

        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;

        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++) pixels[i] = settings.backgroundColor;
        texture.SetPixels(pixels);

        float xRange = settings.xMax - settings.xMin;
        float yRange = settings.yMax - settings.yMin;
        float xStep = xRange / settings.gridDivisions;
        float yStep = yRange / settings.gridDivisions;

        // Draw gridlines and axis tick labels
        for (int i = 0; i <= settings.gridDivisions; i++)
        {
            // X-axis ticks and vertical gridlines
            float xVal = settings.xMin + i * xStep;
            int x = Mathf.RoundToInt(i / (float)settings.gridDivisions * graphSize) + margin;
            for (int yy = margin; yy < height; yy++)
            {
                for (int t = -1; t <= 1; t++)
                {
                    int gx = Mathf.Clamp(x + t, margin, width - 1);
                    texture.SetPixel(gx, yy, settings.gridColor);
                }
            }
            DrawText(texture, x - 16, 6, Mathf.Round(xVal).ToString(), settings.axisColor, 2);

            // Y-axis ticks and horizontal gridlines
            float yVal = settings.yMin + i * yStep;
            int y = Mathf.RoundToInt(i / (float)settings.gridDivisions * graphSize) + margin;
            for (int xx = margin; xx < width; xx++)
            {
                for (int t = -1; t <= 1; t++)
                {
                    int gy = Mathf.Clamp(y + t, margin, height - 1);
                    texture.SetPixel(xx, gy, settings.gridColor);
                }
            }
            DrawText(texture, 6, y - 8, yVal.ToString("0.000"), settings.axisColor, 2);
        }

        // Axes
        int axisX = Mathf.RoundToInt(Mathf.InverseLerp(settings.xMin, settings.xMax, 0f) * graphSize) + margin;
        int axisY = Mathf.RoundToInt(Mathf.InverseLerp(settings.yMin, settings.yMax, 0f) * graphSize) + margin;

        for (int i = margin; i < height; i++)
            for (int t = -settings.axisThickness / 2; t <= settings.axisThickness / 2; t++)
                if (axisX + t >= margin && axisX + t < width)
                    texture.SetPixel(axisX + t, i, settings.axisColor);

        for (int i = margin; i < width; i++)
            for (int t = -settings.axisThickness / 2; t <= settings.axisThickness / 2; t++)
                if (axisY + t >= margin && axisY + t < height)
                    texture.SetPixel(i, axisY + t, settings.axisColor);

        // Plot curves
        foreach (var curve in curves)
        {
            for (int i = 0; i < graphSize; i++)
            {
                float t = i / (float)(graphSize - 1);
                float xVal = Mathf.Lerp(settings.xMin, settings.xMax, t);
                float yVal = curve.function(xVal);

                int xPix = i + margin;
                int yPix = Mathf.RoundToInt(Mathf.InverseLerp(settings.yMin, settings.yMax, yVal) * (graphSize - 1)) + margin;

                for (int dx = -curve.thickness / 2; dx <= curve.thickness / 2; dx++)
                {
                    for (int dy = -curve.thickness / 2; dy <= curve.thickness / 2; dy++)
                    {
                        int px = Mathf.Clamp(xPix + dx, margin, width - 1);
                        int py = Mathf.Clamp(yPix + dy, margin, height - 1);
                        texture.SetPixel(px, py, curve.color);
                    }
                }
            }
        }

        // Draw axis labels
        DrawText(texture, width / 2 - 80, 0, settings.xLabel, settings.axisColor, 2);
        DrawText(texture, 8, height / 2, settings.yLabel, settings.axisColor, 2, vertical: true);

        texture.Apply();
        Graphics.Blit(texture, target);
    }

    /// <summary>
    /// Crude fixed-size block letter drawing (scales with scale factor).
    /// </summary>
    private static void DrawText(Texture2D tex, int x, int y, string text, Color color, int scale = 1, bool vertical = false)
    {
        foreach (char c in text)
        {
            if (char.IsDigit(c) || c == '.' || c == '-' || char.IsLetter(c) || c == ' ' || c == '(' || c == ')' || c == 'Δ' || c == '%')
            {
                DrawChar(tex, x, y, c, color, scale);
                if (vertical) y += 9 * scale;
                else x += 8 * scale;
            }
        }
    }

    private static void DrawChar(Texture2D tex, int x, int y, char c, Color color, int scale = 1)
    {
        int[,] pattern = GetCharPattern(c);
        int w = pattern.GetLength(0);
        int h = pattern.GetLength(1);

        for (int dx = 0; dx < w; dx++)
        {
            for (int dy = 0; dy < h; dy++)
            {
                if (pattern[dx, dy] == 1)
                {
                    for (int sx = 0; sx < scale; sx++)
                    {
                        for (int sy = 0; sy < scale; sy++)
                        {
                            tex.SetPixel(x + dx * scale + sx, y + (h - dy) * scale + sy, color);
                        }
                    }
                }
            }
        }
    }

    private static int[,] GetCharPattern(char c)
    {
        switch (char.ToUpper(c))
        {
            case 'A': return new int[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 1, 1, 1 }, { 1, 0, 1 }, { 1, 0, 1 } };
            case 'C': return new int[,] { { 0, 1, 1 }, { 1, 0, 0 }, { 1, 0, 0 }, { 1, 0, 0 }, { 0, 1, 1 } };
            case 'D': return new int[,] { { 1, 1, 0 }, { 1, 0, 1 }, { 1, 0, 1 }, { 1, 0, 1 }, { 1, 1, 0 } };
            case 'E': return new int[,] { { 1, 1, 1 }, { 1, 0, 0 }, { 1, 1, 0 }, { 1, 0, 0 }, { 1, 1, 1 } };
            case 'H': return new int[,] { { 1, 0, 1 }, { 1, 0, 1 }, { 1, 1, 1 }, { 1, 0, 1 }, { 1, 0, 1 } };
            case 'I': return new int[,] { { 1, 1, 1 }, { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 }, { 1, 1, 1 } };
            case 'L': return new int[,] { { 1, 0, 0 }, { 1, 0, 0 }, { 1, 0, 0 }, { 1, 0, 0 }, { 1, 1, 1 } };
            case 'M': return new int[,] { { 1, 0, 1 }, { 1, 1, 1 }, { 1, 0, 1 }, { 1, 0, 1 }, { 1, 0, 1 } };
            case 'N': return new int[,] { { 1, 0, 1 }, { 1, 1, 1 }, { 1, 1, 1 }, { 1, 0, 1 }, { 1, 0, 1 } };
            case 'O': return new int[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 1, 0, 1 }, { 1, 0, 1 }, { 0, 1, 0 } };
            case 'P': return new int[,] { { 1, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 }, { 1, 0, 0 }, { 1, 0, 0 } };
            case 'R': return new int[,] { { 1, 1, 0 }, { 1, 0, 1 }, { 1, 1, 0 }, { 1, 0, 1 }, { 1, 0, 1 } };
            case 'S': return new int[,] { { 0, 1, 1 }, { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 }, { 1, 1, 0 } };
            case 'T': return new int[,] { { 1, 1, 1 }, { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 } };
            case 'U': return new int[,] { { 1, 0, 1 }, { 1, 0, 1 }, { 1, 0, 1 }, { 1, 0, 1 }, { 0, 1, 0 } };
            case '%': return new int[,] { { 1, 0, 1 }, { 0, 0, 1 }, { 0, 1, 0 }, { 1, 0, 0 }, { 1, 0, 1 } };
            case '(': return new int[,] { { 0, 1 }, { 1, 0 }, { 1, 0 }, { 1, 0 }, { 0, 1 } };
            case ')': return new int[,] { { 1, 0 }, { 0, 1 }, { 0, 1 }, { 0, 1 }, { 1, 0 } };
            case 'Δ': return new int[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 1, 1, 1 }, { 1, 0, 1 }, { 1, 0, 1 } };
            case '-': return new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 1, 1, 1 }, { 0, 0, 0 }, { 0, 0, 0 } };
            case '.': return new int[,] { { 0 }, { 0 }, { 0 }, { 0 }, { 1 } };
            case ' ': return new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            default: return GetCharPattern('0'); // fallback to '0' pattern
        }
    }
}
