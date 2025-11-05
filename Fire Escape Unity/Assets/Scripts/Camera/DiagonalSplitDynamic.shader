Shader "Custom/DiagonalSplitDynamic"
{
    Properties
    {
        _TexA ("Camera A", 2D) = "white" {}
        _TexB ("Camera B", 2D) = "black" {}
        _Split ("Split Position", Range(0,1)) = 0.5
        _Blend ("Blend Width", Range(0,0.2)) = 0.02
        _DynamicAngle ("Dynamic Rotation (Radians)", Range(-3.14,3.14)) = 0
        _BarWidth ("Black Bar Width", Range(0,0.05)) = 0.01
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _TexA;
            sampler2D _TexB;
            float _Split;
            float _Blend;
            float _DynamicAngle;
            float _BarWidth;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample both camera render textures
                fixed4 colA = tex2D(_TexA, i.uv);
                fixed4 colB = tex2D(_TexB, i.uv);

                // --- DYNAMIC ROTATION ---
                float s = sin(_DynamicAngle);
                float c = cos(_DynamicAngle);

                float2 centeredUV = i.uv - 0.5;
                float2 rotatedUV = float2(
                    centeredUV.x * c - centeredUV.y * s,
                    centeredUV.x * s + centeredUV.y * c
                ) + 0.5;

                // --- SPLIT & BLEND ---
                float edge = rotatedUV.x - _Split;
                float blend = smoothstep(-_Blend, _Blend, edge);

                // --- BLACK BAR ---
                float bar = smoothstep(-_BarWidth, 0.0, edge) - smoothstep(0.0, _BarWidth, edge);

                // --- COMBINE ---
                fixed4 finalColor = lerp(colA, colB, blend);
                finalColor = lerp(finalColor, float4(0,0,0,1), bar);

                return finalColor;
            }
            ENDCG
        }
    }
}
