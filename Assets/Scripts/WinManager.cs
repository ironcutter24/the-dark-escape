using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    Camera cam;

    bool endOfAnimation = false;

    void Start()
    {
        cam = Camera.main;
        cam.transform.position = new Vector3(0, -50, 0);

        AudioManager.SetOn(FMODParameter.WinState);

        cam.transform.DOMoveY(0f, 5f)
            .SetEase(Ease.OutSine)
            .OnComplete(() => OnEndOfAnimation());
    }

    void Update()
    {
        if (!endOfAnimation) return;

        if (Input.anyKeyDown)
        {
            AudioManager.SetOff(FMODParameter.WinState);
            GameManager.Instance.LoadMainScene();
        }
    }

    void OnEndOfAnimation()
    {
        endOfAnimation = true;
    }
}
