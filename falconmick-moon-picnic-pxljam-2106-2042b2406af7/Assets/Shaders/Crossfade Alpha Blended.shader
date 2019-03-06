// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Crossfade Alpha Blended" {
	Properties{
		_Fade("Fade", Range(0, 1)) = 0
		_TexA("Texture A", 2D) = "white" {}
	_TexB("Texture B", 2D) = "white" {}
	}
		//Single pass for programmable pipelines
		SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off
		ColorMask RGB
		Blend One OneMinusSrcAlpha
		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_fog_exp2
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

		struct appdata_tiny {
		float4 vertex : POSITION;
		float4 texcoord : TEXCOORD0;
		float4 texcoord1 : TEXCOORD1;
	};

	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
		float2 uv2 : TEXCOORD1;
	};

	uniform float4  _TexA_ST,
		_TexB_ST;

	v2f vert(appdata_tiny v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord,_TexA);
		o.uv2 = TRANSFORM_TEX(v.texcoord1,_TexB);
		return o;
	}

	uniform float _Fade;
	uniform sampler2D   _TexA,
		_TexB;

	fixed4 frag(v2f i) : COLOR
	{
		half4   tA = tex2D(_TexA, i.uv),
		tB = tex2D(_TexB, i.uv2);
	fixed3 sum = lerp(tA.rgb * tA.a, tB.rgb * tB.a, _Fade);
	fixed alpha = lerp(tA.a, tB.a, _Fade);
	return fixed4(sum, alpha);
	}
		ENDCG
	}
	}
		// ---- Single texture cards
		SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off
		Pass{
		ColorMask A
		SetTexture[_TexA]{
		constantColor(0, 0, 0,[_Fade])
		combine texture * one - constant
	}
	}
		Pass{
		BindChannels{
		Bind "Vertex", vertex
		Bind "Texcoord1", texcoord0
	}
		ColorMask A
		Blend One One
		SetTexture[_TexB]{
		constantColor(0, 0, 0,[_Fade])
		combine texture * constant
	}
	}
		Pass{
		ColorMask RGB
		Blend Zero OneMinusDstAlpha
	}
		Pass{
		ColorMask RGB
		Blend SrcAlpha One
		SetTexture[_TexA]{
		constantColor([_Fade],[_Fade],[_Fade], 0)
		combine texture * one - constant
	}
	}
		Pass{
		BindChannels{
		Bind "Vertex", vertex
		Bind "Texcoord1", texcoord0
	}
		ColorMask RGB
		Blend SrcAlpha One
		SetTexture[_TexB]{
		constantColor([_Fade],[_Fade],[_Fade], 1)
		combine texture * constant
	}
	}
	}
}