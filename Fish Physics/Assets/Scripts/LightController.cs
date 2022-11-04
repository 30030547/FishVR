using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class LightController : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D explosionLight;
    public float explosionLightIntensity;

    void Start()
    {
        DOVirtual.Float(0, explosionLightIntensity, .05f, ChangeLight).OnComplete(() => DOVirtual.Float(explosionLightIntensity, 0, .1f, ChangeLight));
    }

    void ChangeLight(float x)
    {
        explosionLight.intensity = x;
    }
}
