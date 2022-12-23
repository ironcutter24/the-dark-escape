using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Patterns;
using TMPro;

public class GameOver : Singleton<GameOver>
{
    [SerializeField]
    TextMeshProUGUI text;

    VFXManager VFX;

    public bool State { get; private set; }

    bool endOfAnimation = false;

    void Start()
    {
        VFX = GetComponent<VFXManager>();
    }

    private void Update()
    {
        if (State && endOfAnimation)
        {
            if (Input.anyKeyDown)
            {
                GameManager.Instance.LoadMainScene();
                endOfAnimation = false;
                SetTo(false);
            }
        }
    }

    public void SetTo(bool state)
    {
        State = state;

        ResetGFX();

        text.gameObject.SetActive(State);
        VFX.EnableCinemaFX(State);

        

        if (State)
        {
            AudioManager.SetOn(FMODParameter.CryVol);
            AudioManager.SetOn(FMODParameter.StrangeMusicVol);

            DOTween.To(() => VFX.GrainStrange, x => VFX.GrainStrange = x, 1f, 1.4f);
            DOTween.To(() => VFX.VignetteStrange, x => VFX.VignetteStrange = x, .9f, .6f)
                .OnComplete(() => ShowText());
        }

        void ShowText()
        {
            text.DOColor(GetWithAlpha(.9f, text.color), 1.8f)
                .OnComplete(() => endOfAnimation = true);
        }
    }

    void ResetGFX()
    {
        AudioManager.SetOff(FMODParameter.CryVol);
        AudioManager.SetOff(FMODParameter.StrangeMusicVol);
        AudioManager.SetOff(FMODParameter.WinState);

        VFX.GrainStrange = 0f;
        VFX.VignetteStrange = 0f;
        text.color = GetWithAlpha(0f, text.color);
    }

    Color GetWithAlpha(float alpha, Color c)
    {
        return new Color(c.r, c.b, c.g, alpha);
    }
}
