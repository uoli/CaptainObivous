﻿Shader "Custom/HueShift" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("NoiseTex", 2D) = "white" {}
        _HueShift("HueShift", Float ) = 0
        _Sat("Saturation", Float) = 1
        _Val("Value", Float) = 1
    }
    SubShader {
 
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType" = "Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
 
            #include "UnityCG.cginc"
 
            float3 shift_col(float3 RGB, float3 shift)
            {
            float3 RESULT = float3(RGB);
            float VSU = shift.z*shift.y*cos(shift.x*3.14159265/180);
                float VSW = shift.z*shift.y*sin(shift.x*3.14159265/180);
               
                RESULT.x = (.299*shift.z+.701*VSU+.168*VSW)*RGB.x
                        + (.587*shift.z-.587*VSU+.330*VSW)*RGB.y
                        + (.114*shift.z-.114*VSU-.497*VSW)*RGB.z;
               
                RESULT.y = (.299*shift.z-.299*VSU-.328*VSW)*RGB.x
                        + (.587*shift.z+.413*VSU+.035*VSW)*RGB.y
                        + (.114*shift.z-.114*VSU+.292*VSW)*RGB.z;
               
                RESULT.z = (.299*shift.z-.3*VSU+1.25*VSW)*RGB.x
                        + (.587*shift.z-.588*VSU-1.05*VSW)*RGB.y
                        + (.114*shift.z+.886*VSU-.203*VSW)*RGB.z;
               
            return (RESULT);
            }
 
            struct v2f {
                float4  pos : SV_POSITION;
                float2  uv : TEXCOORD0;
            };
 
            float4 _MainTex_ST;
 
            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }
 
            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float _HueShift;
            float _Sat;
            float _Val;
 
            half4 frag(v2f i) : COLOR
            {
                half4 col = tex2D(_MainTex, i.uv);
                half3 noise = tex2D(_NoiseTex, i.uv);
                
                //float modShift = noise.x * _HueShift;
                
                float modShift = (_HueShift + noise.x * 360) % 360;
                float satShift = _Sat;
                float vslShift = _Val;
                
                //float modShift = 0;
                //float satShift = _Sat + ((noise.y * _HueShift/360));
                //float vslShift = _Val * (noise.z * _HueShift/360) % 0.5;
                
                
          
                float3 shift = float3(modShift, satShift, _Val);
                half4 final = half4( half3(shift_col(col, shift)), col.a);
                
                //half4 final = half4( noise, 1);
                
                
                return final;
            }
            ENDCG
        }
    }
    Fallback "Particles/Alpha Blended"
}