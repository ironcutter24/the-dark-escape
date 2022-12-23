using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    CrawlerController crawler;

    [SerializeField]
    VFXManager VFX;

    [SerializeField]
    float stopTime = 1.8f;

    EventInstance creatureAttack;

    void Start()
    {
        //FMODUnity.RuntimeManager.PlayOneShot("event:/CreatureSteps");

        crawler.InitAnimation();
        Invoke("StopCrawler", stopTime);
        Invoke("FadeOut", stopTime + .1f);
        Invoke("PlayCreatureSFX", stopTime - .4f);
    }

    private void OnDestroy()
    {
        creatureAttack.release();
    }

    void StopCrawler()
    {
        crawler.StopMoving();
    }

    void PlayCreatureSFX()
    {
        creatureAttack = FMODUnity.RuntimeManager.CreateInstance("event:/CreatureAttack");
        creatureAttack.start();
    }

    void FadeOut()
    {
        GameOver.Instance.SetTo(true);
    }
}
