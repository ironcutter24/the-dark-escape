using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerController : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    [SerializeField]
    float moveSpeed = 10f;

    bool isMoving = false;

    void Update()
    {
        if (!isMoving) return;
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void InitAnimation()
    {
        anim.SetBool("IsMoving", true);
        isMoving = true;
    }

    public void StopMoving()
    {
        anim.SetBool("IsMoving", false);
        isMoving = false;
    }
}
