using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    CrawlerController crawler;

    [SerializeField]
    VFXManager VFX;

    float stopTime = 2.2f;

    void Start()
    {
        crawler.InitAnimation();
        Invoke("StopCrawler", stopTime);
        Invoke("FadeOut", stopTime + .1f);
    }

    void StopCrawler()
    {
        crawler.StopMoving();
    }

    void FadeOut()
    {
        VFX.EnableCinemaFX(true);
        DOTween.To(() => VFX.GrainStrange, x => VFX.GrainStrange = x, 1f, 1.4f);
        DOTween.To(() => VFX.VignetteStrange, x => VFX.VignetteStrange = x, .9f, .6f);
    }
}
