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

    [SerializeField]
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
        GameOver.Instance.SetTo(true);
    }
}
