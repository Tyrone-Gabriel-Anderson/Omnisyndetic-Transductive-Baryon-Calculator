Shader "Hidden/Transductive/Bloom"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Threshold ("Bloom Threshold", Range(0, 2)) = 1.0
        _Intensity ("Bloom Intensity", Range(0, 5)) = 1.2
        _BlurSize ("Blur Size", Range(0.5, 5.0)) = 1.0
    }

    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _Threshold;
            float _Intensity;
            float _BlurSize;

            float4 frag(v2f_img i) : COLOR
            {
                float3 col = tex2D(_MainTex, i.uv).rgb;

                // Bright-pass filter
                float luminance = dot(col, float3(0.299, 0.587, 0.114));
                float3 bright = max(col - _Threshold, 0.0);

                return float4(bright * _Intensity, 1.0);
            }
            ENDCG
        }
    }
    FallBack Off
}
