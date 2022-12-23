using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Patterns;

public class AudioManager : Singleton<AudioManager>
{
    public FMODUnity.EventReference ambientEvent;

    private FMOD.Studio.EventInstance ambient;

    void Start()
    {
        ambient = FMODUnity.RuntimeManager.CreateInstance(ambientEvent);
        ambient.start();
    }

    public static void SetAmbientParameter(float val, FMODParameter px)
    {
        Instance.ambient.setParameterByName(px.ToString(), val);
    }

    public static void SetOn(FMODParameter px)
    {
        Instance.ambient.setParameterByName(px.ToString(), 1f);
    }

    public static void SetOff(FMODParameter px)
    {
        Instance.ambient.setParameterByName(px.ToString(), 0f);
    }

    public static void PlayHitScare()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HitScare");
    }

}
    public enum FMODParameter { CryVol, StrangeMusicVol, WinState }
