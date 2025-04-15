Shader "Unlit/MagnifyingGlass"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		[IntRange] _StencilRef ("Stencil reference value", Range(0, 255)) = 0
    }
    SubShader
    {
        LOD 100
		Tags {"RenderType"="Transparent"}

		Pass
        {
			Tags { "LightMode" = "ForwardBase" "Queue"="Transparent+6"}
            Blend SrcAlpha OneMinusSrcAlpha
			//ZWrite Off
			
			CGPROGRAM

            #pragma vertex vert alpha
            #pragma fragment frag alpha

            #include "UnityCG.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float4 screenPos: TEXCOORD1;
            };

            fixed4 _Color;
			float2 _Offset;
			sampler2D _GlobalRenderTexture;

            v2f vert (appdata v){
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
				o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float2 offset = i.screenPos.xy / i.screenPos.w;
				offset = 2.0f * offset - 1.0f;
				float2 transformedOffset = 2.0f * _Offset - 1.0f;
				float r = length(offset - transformedOffset);
				float distortion = 1.0f -2.5f * r * r;
				offset *= distortion;
				offset = (offset + 1.0f) * 0.5f;
				float insideCircle = distance(i.uv, float2(0.5f, 0.5f));
				if (insideCircle >= 0.5f){
					return fixed4(0,0,0,0);
				}
				else{
					fixed4 col = tex2D(_GlobalRenderTexture, offset);
					return col;
				}
            }
            ENDCG
        }
    }
	Fallback "Diffuse"
}
