using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBox : MonoBehaviour
{
    [Range(0.01f, 0.1f)]
    public float rotateSpeed = 0.01f;

    [Range(0.01f, 0.1f)]
    public float exposureSpeed = 0.01f;

    public float exposureMin = 1.0f;
    public float exposureMax = 4.0f;

    private float exposureTime = 0.0f;

    public Material skybox;
    void Start()
    {
        RenderSettings.skybox = skybox;
    }

    void Update()
    {
        float rot = Mathf.Repeat(skybox.GetFloat("_Rotation") + rotateSpeed, 360f);
        skybox.SetFloat("_Rotation", rot);

        exposureTime += exposureSpeed;

        float exposure = exposureMin + Mathf.Sin(exposureTime *Mathf.Deg2Rad * exposureMax); ;
        skybox.SetFloat("_Exposure",exposure);
    }
}
