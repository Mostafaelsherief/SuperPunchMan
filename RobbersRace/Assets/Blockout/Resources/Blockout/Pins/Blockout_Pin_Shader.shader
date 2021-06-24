// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1340,x:33252,y:32579,varname:node_1340,prsc:2|emission-5612-OUT;n:type:ShaderForge.SFN_Color,id:9537,x:31837,y:32341,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3249493,c2:0.2352941,c3:1,c4:1;n:type:ShaderForge.SFN_VertexColor,id:5752,x:32063,y:32954,varname:node_5752,prsc:2;n:type:ShaderForge.SFN_Fresnel,id:3621,x:32063,y:32751,varname:node_3621,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6894,x:32160,y:32574,varname:node_6894,prsc:2|A-9537-RGB,B-698-OUT;n:type:ShaderForge.SFN_Lerp,id:1255,x:32402,y:32606,varname:node_1255,prsc:2|A-6894-OUT,B-9537-RGB,T-3621-OUT;n:type:ShaderForge.SFN_Vector1,id:698,x:31920,y:32600,varname:node_698,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Lerp,id:5612,x:32983,y:32719,varname:node_5612,prsc:2|A-1255-OUT,B-7351-OUT,T-5752-R;n:type:ShaderForge.SFN_Multiply,id:6214,x:32602,y:32740,varname:node_6214,prsc:2|A-1255-OUT,B-2764-OUT;n:type:ShaderForge.SFN_Vector1,id:2764,x:32342,y:32823,varname:node_2764,prsc:2,v1:3;n:type:ShaderForge.SFN_Clamp01,id:7351,x:32771,y:32750,varname:node_7351,prsc:2|IN-6214-OUT;proporder:9537;pass:END;sub:END;*/

Shader "Blockout/Blockout_Pin_Shader" {
    Properties {
        _Color ("Color", Color) = (0.3249493,0.2352941,1,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 node_1255 = lerp((_Color.rgb*0.8),_Color.rgb,(1.0-max(0,dot(normalDirection, viewDirection))));
                float3 emissive = lerp(node_1255,saturate((node_1255*3.0)),i.vertexColor.r);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
