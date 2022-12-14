using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    OldCinemaEffect oldCinemaEffect;

    void Start()
    {
        oldCinemaEffect = GetComponent<OldCinemaEffect>();
    }

    public void EnableCinemaFX(bool state)
    {
        oldCinemaEffect.enabled = state;
    }

    public float GrainStrange
    {
        get { return oldCinemaEffect.GrainStrange; }
        set { oldCinemaEffect.GrainStrange = Mathf.Lerp(0f, 1f, Mathf.Clamp(value, 0f, 1f)); }
    }

    public float VignetteStrange
    {
        get { return oldCinemaEffect.VignetteStrange; }
        set { oldCinemaEffect.VignetteStrange = Mathf.Lerp(1f, 100f, Mathf.Clamp(value, 0f, 1f)); }
    }

    public float JitterStrange
    {
        get { return oldCinemaEffect.JitterStrange; }
        set { oldCinemaEffect.JitterStrange = Mathf.Lerp(0f, .01f, Mathf.Clamp(value, 0f, 1f)); }
    }
}
