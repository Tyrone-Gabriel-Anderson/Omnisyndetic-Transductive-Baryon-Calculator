Shader "Custom/ElectricFieldPlasma"
{
    Properties
    {
        _MainTex ("Flow Noise Texture", 2D) = "white" {}
        _ElectricGradient ("Electric Gradient", 2D) = "white" {}

        _StartColor ("Start Color", Color) = (1, 0.5, 1, 1)
        _EndColor ("End Color", Color) = (0.3, 1.0, 1, 1)
        _BlendWithGradient ("Gradient Blend Amount", Range(0,1)) = 0.5

        _FlowSpeed ("Flow Speed", Range(0.1, 10)) = 2
        _GlowIntensity ("Glow Intensity", Range(0, 5)) = 2.5
        _NoiseIntensity ("Noise Strength", Range(0, 2)) = 1.0
        _FresnelPower ("Fresnel Edge Power", Range(0.1, 8)) = 2
        _AlphaCoherence ("Min Coherence Cutoff", Range(0,1)) = 0.1
        _Tiling ("Tiling", Vector) = (4, 4, 0, 0)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend One One
        ZWrite Off
        Cull Back
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _ElectricGradient;

            float4 _StartColor;
            float4 _EndColor;
            float _BlendWithGradient;

            float4 _Tiling;
            float _FlowSpeed;
            float _GlowIntensity;
            float _NoiseIntensity;
            float _FresnelPower;
            float _AlphaCoherence;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };

            v2f vert (appdata v)
            {
                v2f o;
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.pos = UnityObjectToClipPos(v.vertex);

                o.uv = v.uv * _Tiling.xy;
                o.uv.y += _Time.y * _FlowSpeed;

                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize(_WorldSpaceCameraPos - worldPos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float fresnel = pow(1.0 - saturate(dot(i.viewDir, normalize(i.worldNormal))), _FresnelPower);
                float noise = tex2D(_MainTex, i.uv * 1.3).r;
                float flicker = lerp(0.5, 1.5, noise * _NoiseIntensity);

                // Calculate base gradient position (UV scroll gives movement)
                float gradientCoord = frac(i.uv.y + _Time.y * 0.3);
                float4 bandColor = tex2D(_ElectricGradient, float2(gradientCoord, 0.5));

                // Custom colour blend (horizontal mix between start and end)
                float4 userColor = lerp(_StartColor, _EndColor, gradientCoord);

                // Blend custom colour with electric gradient
                float4 finalColor = lerp(userColor, bandColor, _BlendWithGradient);

                // Glow and alpha
                float glow = fresnel * flicker * _GlowIntensity;
                float alpha = saturate(glow - _AlphaCoherence);

                return float4(finalColor.rgb * glow, alpha);
            }
            ENDCG
        }
    }

    FallBack "Unlit/Transparent"
}
