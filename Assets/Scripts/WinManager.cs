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

        cam.transform.DOMoveY(0f, 3f)
            .SetEase(Ease.OutSine)
            .OnComplete(() => OnEndOfAnimation());
    }

    private void Update()
    {
        if (!endOfAnimation) return;

        if (Input.anyKeyDown)
        {
            GameManager.Instance.LoadMainScene();
        }
    }

    void OnEndOfAnimation()
    {
        endOfAnimation = true;
    }
}
