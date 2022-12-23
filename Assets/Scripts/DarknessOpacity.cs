using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessOpacity : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer darkness;

    [SerializeField]
    SpriteRenderer lanternFade;

    [SerializeField]
    SpriteMask lanternMask;

    [Space]

    [SerializeField]
    Color lightOnColor = new Color(0f, 0f, 0f, .9f);

    [SerializeField]
    Color lightOffColor = new Color(0f, 0f, 0f, .98f);

    OldCinemaEffect cinemaEffect;

    void Start()
    {
        darkness.gameObject.SetActive(true);
        cinemaEffect = Camera.main.GetComponent<OldCinemaEffect>();

        Controller2D.Instance.OnLightStateChange += OnLightStateChange;
    }

    void OnLightStateChange(bool state)
    {
        var color = state ? lightOnColor : lightOffColor;
        darkness.color = color;
        //lanternFade.color = color;
        lanternMask.enabled = state;
        cinemaEffect.enabled = isFading = !state;

        if (!state)
            StartCoroutine(_FadeToBlack());
    }

    bool isFading = false;
    IEnumerator _FadeToBlack()
    {
        const float min = 76f;
        const float max = 93f;

        cinemaEffect.VignetteStrange = min;

        var tenseSound = FMODUnity.RuntimeManager.CreateInstance("event:/TenseSound");
        tenseSound.setParameterByName("Tension", 0f);
        tenseSound.start();

        while (cinemaEffect.VignetteStrange < max)
        {
            yield return null;

            if (!isFading)
            {
                tenseSound.setParameterByName("Tension", 0f);
                yield break;
            }

            cinemaEffect.VignetteStrange += 1.8f * Time.deltaTime;
            tenseSound.setParameterByName("Tension", Utility.Math.UMath.Normalize(cinemaEffect.VignetteStrange, min, max));
        }

        tenseSound.setParameterByName("Tension", 0f);
        GameOver.Instance.SetTo(true);
    }
}
