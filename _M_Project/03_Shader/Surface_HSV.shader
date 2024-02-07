// �F�������łȂ��A���x�▾�x�܂ŕύX�ł���悤�ɂ��� Shader

Shader "Custom/Surface_HSV"
{
    Properties
    {
        // ���F
        _Color ("Color", Color) = (1,1,1,1)
        // �e�N�X�`��
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        // ���x�F�f�t�H
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        // ����F�f�t�H
        _Metallic ("Metallic", Range(0,1)) = 0.0

        // �F��
        _Hue ("Hue", Float) = 0
        // �O�a
        _Sat("Saturation", Float) = 1
        // �󂯎��ϐ�
        _Val("Value", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0


        // �v���p�e�B�[�̐錾
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        half _Hue, _Sat, _Val;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        /* �ҏW */
        // RGB -> HSV �ϊ�
        float3 rgb2hsv(float3 rgb)
        {
            // �Ԃ��ʓx
            float3 hsv;

            // RGB�̒l�ōő�l�̕ϐ��F��r�֐� max �𗘗p
            float maxValue = max(rgb.x, max(rgb.y, rgb.z));
            // RGB�̒l�ōő�l�̕ϐ��F��r�֐� min �𗘗p
            float minValue = min(rgb.x, min(rgb.y, rgb.z));

            // �ő�l�ƍŏ��l�̍�
            float delta = maxValue - minValue;

            // V (���x)�F��ԋ����F�� V�l �Ƃ���
            hsv.z = maxValue;

            // S (�ʓx)�F�ő�l�ƍŏ��l�̍��𐳋K�����ċ��߂�
            if (maxValue != 0.0f) { hsv.y = delta / maxValue; }
            else { hsv.y = 0.0f; }

            // H (�F��)�FRGB�̓��ōő�l�ƍŏ��l�̍����狁�߂�
            if (hsv.y > 0.0f)
            {
                if (rgb.x == maxValue)
                {
                    hsv.x = (rgb.y - rgb.z) / delta;
                }
                else if (rgb.y == maxValue)
                {
                    hsv.x = 2 + (rgb.y - rgb.x) / delta;
                }
                else
                {
                    hsv.x = 4 + (rgb.x - rgb.y) / delta;
                }
            }

            return hsv;
        }


        // HSV -> RGB �ϊ�
        float3 hsv2rgb(float3 hsv)
        {
            float3 rgb;

            if (hsv.y == 0) 
            {
                // S�i�ʓx�j��0�Ɠ������Ȃ�Ζ��F�������͊D�F
                rgb.r = rgb.x = rgb.y = hsv.z;
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
                    rgb.x = hsv.z;
                    rgb.y = cc;
                    rgb.z = aa;
                }
                else if (i < 2)
                {
                    rgb.x = bb;
                    rgb.y = hsv.z;
                    rgb.z = aa;
                }
                else if (i < 3)
                {
                    rgb.x = aa;
                    rgb.y = hsv.z;
                    rgb.z = cc;
                }
                else if (i < 4)
                {
                    rgb.x = aa;
                    rgb.y = bb;
                    rgb.z = hsv.z;
                }
                else if (i < 5) 
                {
                    rgb.x = cc;
                    rgb.y = aa;
                    rgb.z = hsv.z;
                }
                else 
                {
                    rgb.x = hsv.z;
                    rgb.y = aa;
                    rgb.z = bb;
                }
            }
            return rgb;
        }

        float3 shift_col(float3 rgb, half3 shift)
        {
            // RGB -> HSV �ϊ�
            float3 hsv = rgb2hsv(rgb);

            // HSV����
            hsv.x += shift.x;
            if (1.0f <= hsv.x)
            {
                hsv.x -= 1.0f;
            }
            hsv.y *= shift.y;
            hsv.z *= shift.z;

            // HSV -> RGB �ϊ�
            return hsv2rgb(hsv);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

            // �J���[��ύX���鏈��
            half3 shift = half3(_Hue, _Sat, _Val);

            // �e�N�X�`���ɖ��x�Ȃǂ����Z
            fixed4 shiftColor = fixed4(shift_col(c.xyz, shift), c.a);

            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = shiftColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
