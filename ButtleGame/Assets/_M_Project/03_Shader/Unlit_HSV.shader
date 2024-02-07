Shader "Unlit/Unlit_HSV"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}

        _Hue("Hue", Float) = 0
        _Sat("Saturation", Float) = 1
        _Val("Value", Float) = 1

        _ShadowSeparate("ShadowSeparate", Float) = 1

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _Color;
            half _Hue, _Sat, _Val;

            // �ȈՃg�D�[���V�F�[�f�B���O
            float _ShadowSeparate;

            // RGB->HSV�ϊ�
            float3 rgb2hsv(float3 rgb)
            {
                float3 hsv;

                // RGB�̎O�̒l�ōő�̂���
                float maxValue = max(rgb.r, max(rgb.g, rgb.b));
                // RGB�̎O�̒l�ōŏ��̂���
                float minValue = min(rgb.r, min(rgb.g, rgb.b));
                // �ő�l�ƍŏ��l�̍�
                float delta = maxValue - minValue;

                // V�i���x�j
                // ��ԋ����F��V�l�ɂ���
                hsv.z = maxValue;

                // S�i�ʓx�j
                // �ő�l�ƍŏ��l�̍��𐳋K�����ċ��߂�
                if (maxValue != 0.0)
                {
                    hsv.y = delta / maxValue;
                }
                else
                {
                    hsv.y = 0.0;
                }

                // H�i�F���j
                // RGB�̂����ő�l�ƍŏ��l�̍����狁�߂�
                if (hsv.y > 0.0)
                {
                    if (rgb.r == maxValue)
                    {
                        hsv.x = (rgb.g - rgb.b) / delta;
                    }
                    else if (rgb.g == maxValue)
                    {
                        hsv.x = 2 + (rgb.b - rgb.r) / delta;
                    }
                    else
                    {
                        hsv.x = 4 + (rgb.r - rgb.g) / delta;
                    }

                    hsv.x /= 6.0;

                    if (hsv.x < 0) { hsv.x += 1.0; }
                }

                return hsv;
            }

            // HSV->RGB�ϊ�
            float3 hsv2rgb(float3 hsv)
            {
                float3 rgb;

                if (hsv.y == 0)
                {
                    // S�i�ʓx�j��0�Ɠ������Ȃ�Ζ��F�������͊D�F
                    rgb.r = rgb.g = rgb.b = hsv.z;
                }
                else
                {
                    // �F��H�i�F���j�̈ʒu��S�i�ʓx�j�AV�i���x�j����RGB�l���Z�o����
                    hsv.x *= 6.0;
                    float i = floor(hsv.x);
                    float f = hsv.x - i;
                    float aa = hsv.z * (1 - hsv.y);
                    float bb = hsv.z * (1 - (hsv.y * f));
                    float cc = hsv.z * (1 - (hsv.y * (1 - f)));

                    if (i < 1)
                    {
                        rgb.r = hsv.z;
                        rgb.g = cc;
                        rgb.b = aa;
                    }
                    else if (i < 2)
                    {
                        rgb.r = bb;
                        rgb.g = hsv.z;
                        rgb.b = aa;
                    }
                    else if (i < 3)
                    {
                        rgb.r = aa;
                        rgb.g = hsv.z;
                        rgb.b = cc;
                    }
                    else if (i < 4)
                    {
                        rgb.r = aa;
                        rgb.g = bb;
                        rgb.b = hsv.z;
                    }
                    else if (i < 5)
                    {
                        rgb.r = cc;
                        rgb.g = aa;
                        rgb.b = hsv.z;
                    }
                    else
                    {
                        rgb.r = hsv.z;
                        rgb.g = aa;
                        rgb.b = bb;
                    }
                }
                return rgb;
            }

            float3 shift_col(float3 rgb, half3 shift)
            {
                // RGB->HSV�ϊ�
                float3 hsv = rgb2hsv(rgb);

                // HSV����
                hsv.x += shift.x;
                if (1.0 <= hsv.x)
                {
                    hsv.x -= 1.0;
                }
                hsv.y *= shift.y;
                hsv.z *= shift.z;

                // HSV->RGB�ϊ�
                return hsv2rgb(hsv);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // �e�N�X�`���T���v�����O
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;

                // �g�D�[���V�F�[�_�[�̉e���쐬����
                // HSV��V�l(���x)�ŉe��ݒ肷��
                // ���C�g�̊p�x�ƃI�u�W�F�N�g�̖@���̋����̍ő�l���v�Z
                half nl = max(0, dot(i.normal, _WorldSpaceLightPos0.xyz));

                // �e�ƂȂ镔���������Őݒ肷��
                if (_ShadowSeparate == 0) 
                {
                    nl = 0.9f;
                }
                else if (_ShadowSeparate == 1)
                {
                    if (nl <= 0.01f) { nl = 0.1f; }     // �m�C�Y����
                    else { nl = 1.0f; }                 // 2���e
                }
                else if (_ShadowSeparate == 2)
                {
                    if (nl <= 0.01f) { nl = 0.1f; }     // �m�C�Y����
                    else if (nl <= 0.2f) { nl = 0.2f; } // 1���e
                    else { nl = 1.0f; }                 // 2���e
                }
                else
                {
                    nl = 0.0f;
                }

                // HSV �ϊ�
                half3 shift = half3(_Hue, _Sat, _Val);
                fixed4 shiftColor = fixed4(shift_col(col.rgb, shift), col.a);

                // HSV �K�� + �g�D�[���V���h�E�̓K��
                col.xyz = shiftColor.xyz * nl;
                col.a = shiftColor.a;



                return col;
            }
            ENDCG
        }
    }
}
