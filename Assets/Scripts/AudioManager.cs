using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public FMODUnity.EventReference ambientEvent;

    private FMOD.Studio.EventInstance ambient;

    [SerializeField]
    [Range(0f, 1f)]
    private float CryVol, StrangeMusicVol;

    void Start()
    {
        ambient = FMODUnity.RuntimeManager.CreateInstance(ambientEvent);
        ambient.start();
    }

#if UNITY_EDITOR
    void Update()
    {
        ambient.setParameterByName("CryVol", CryVol);
        ambient.setParameterByName("StrangeMusicVol", StrangeMusicVol);
    }
#endif

    void SetAmbientParameter(float val, FMODParameter px)
    {
        ambient.setParameterByName(px.ToString(), val);
    }

    public enum FMODParameter { CryVol, StrangeMusicVol }
}
