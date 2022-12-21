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

    void Start()
    {
        darkness.gameObject.SetActive(true);

        Controller2D.Instance.OnLightStateChange += OnLightStateChange;
    }

    void OnLightStateChange(bool state)
    {


        var color = state ? lightOnColor : lightOffColor;
        darkness.color = color;
        lanternFade.color = color;
        lanternMask.enabled = state;

        Camera.main.GetComponent<OldCinemaEffect>().enabled = !state;
    }
}
