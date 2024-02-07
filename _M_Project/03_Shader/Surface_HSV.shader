// 色相だけでなく、光度や明度まで変更できるようにする Shader

Shader "Custom/Surface_HSV"
{
    Properties
    {
        // 原色
        _Color ("Color", Color) = (1,1,1,1)
        // テクスチャ
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        // 光度：デフォ
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        // 光沢：デフォ
        _Metallic ("Metallic", Range(0,1)) = 0.0

        // 色調
        _Hue ("Hue", Float) = 0
        // 飽和
        _Sat("Saturation", Float) = 1
        // 受け取り変数
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


        // プロパティーの宣言
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

        /* 編集 */
        // RGB -> HSV 変換
        float3 rgb2hsv(float3 rgb)
        {
            // 返す彩度
            float3 hsv;

            // RGBの値で最大値の変数：比較関数 max を利用
            float maxValue = max(rgb.x, max(rgb.y, rgb.z));
            // RGBの値で最大値の変数：比較関数 min を利用
            float minValue = min(rgb.x, min(rgb.y, rgb.z));

            // 最大値と最小値の差
            float delta = maxValue - minValue;

            // V (明度)：一番強い色を V値 とする
            hsv.z = maxValue;

            // S (彩度)：最大値と最小値の差を正規化して求める
            if (maxValue != 0.0f) { hsv.y = delta / maxValue; }
            else { hsv.y = 0.0f; }

            // H (色相)：RGBの内で最大値と最小値の差から求める
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


        // HSV -> RGB 変換
        float3 hsv2rgb(float3 hsv)
        {
            float3 rgb;

            if (hsv.y == 0) 
            {
                // S（彩度）が0と等しいならば無色もしくは灰色
                rgb.r = rgb.x = rgb.y = hsv.z;
            }
            else 
            {
                // 色環のH（色相）の位置とS（彩度）、V（明度）からRGB値を算出する
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
            // RGB -> HSV 変換
            float3 hsv = rgb2hsv(rgb);

            // HSV操作
            hsv.x += shift.x;
            if (1.0f <= hsv.x)
            {
                hsv.x -= 1.0f;
            }
            hsv.y *= shift.y;
            hsv.z *= shift.z;

            // HSV -> RGB 変換
            return hsv2rgb(hsv);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

            // カラーを変更する処理
            half3 shift = half3(_Hue, _Sat, _Val);

            // テクスチャに明度などを加算
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
