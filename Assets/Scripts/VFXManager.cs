using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VFXManager : MonoBehaviour
{
    OldCinemaEffect oldCinemaEffect;

    private void Start()
    {
        oldCinemaEffect = Camera.main.GetComponent<OldCinemaEffect>();
    }

    private void OnLevelWasLoaded(int level)
    {
        Start();
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
