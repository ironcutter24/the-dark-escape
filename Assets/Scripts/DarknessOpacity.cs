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

    Coroutine fadeCoroutine;

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

        cinemaEffect.enabled = !state;

        if (state && fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        else
        {
            fadeCoroutine = StartCoroutine(_FadeToBlack());
        }
    }

    IEnumerator _FadeToBlack()
    {
        cinemaEffect.VignetteStrange = 76f;

        while (cinemaEffect.VignetteStrange < 93f)
        {
            yield return null;
            cinemaEffect.VignetteStrange += 1.8f * Time.deltaTime;

            // Update fmod
        }

        Controller2D.Instance.Death();
    }
}
