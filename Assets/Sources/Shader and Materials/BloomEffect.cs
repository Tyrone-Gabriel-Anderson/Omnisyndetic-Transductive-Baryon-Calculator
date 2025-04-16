using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class BloomEffect : MonoBehaviour
{
    public Shader bloomShader;
    private Material bloomMat;

    [Range(0f, 2f)] public float threshold = 1.0f;
    [Range(0f, 5f)] public float intensity = 1.2f;
    [Range(0.5f, 5f)] public float blurSize = 1.0f;

    void OnEnable()
    {
        if (bloomShader == null)
            bloomShader = Shader.Find("Hidden/Transductive/Bloom");

        if (bloomShader != null)
            bloomMat = new Material(bloomShader);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (bloomMat == null)
        {
            Graphics.Blit(src, dest);
            return;
        }

        bloomMat.SetFloat("_Threshold", threshold);
        bloomMat.SetFloat("_Intensity", intensity);
        bloomMat.SetFloat("_BlurSize", blurSize);

        RenderTexture temp = RenderTexture.GetTemporary(src.width / 2, src.height / 2, 0);
        Graphics.Blit(src, temp, bloomMat);
        Graphics.Blit(temp, dest);
        RenderTexture.ReleaseTemporary(temp);
    }
}
