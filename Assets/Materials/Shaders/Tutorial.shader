Shader "Custom/Tutorial/Beginner/ Flat Color" {
	Properties 
	{
		_Color ("Color", Color) = (1.0,1.0,1.0,1.0);

	}

	SubShader {

		Pass {
			CGPROGRAM

			// pragmas

			// user-defined variables
			uniform float4 _Color;


			// base input strcuts
			struct vertexInput {
				float4 vertex : POSITION;
			};

			struct vertexOutput {
				float4 pos : SV_POSITION;
			};

			// vertex function
			vertexOutput vert(vertexInput v){
				vertexOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

				UNITY_MATRIX_MVP xyzw;
				v.vertex xyzw;
				UNITY_MATRIX_MVP

				return o;
			}

			// fragment functions

			ENDCG
		}

	}
}
