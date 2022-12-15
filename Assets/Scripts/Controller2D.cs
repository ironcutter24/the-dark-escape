using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float moveSpeed = 6f;

    Vector2 move = Vector2.zero;

    void Start()
    {

    }

    void Update()
    {
        move = new Vector2(
            GetAxis(KeyCode.A, KeyCode.D),
            GetAxis(KeyCode.S, KeyCode.W)
            ).normalized;

        move.y *= .8f;
    }

    void FixedUpdate()
    {
        if (!Mathf.Approximately(move.x, 0f))
        {
            transform.localScale = new Vector3(move.x < 0f ? -1f : 1f, 1f, 1f);
        }

        rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);

        anim.SetBool("IsWalking", move.magnitude > 0f);
    }

    float GetAxis(KeyCode min, KeyCode max)
    {
        return -IsPressed(min) + IsPressed(max);

        float IsPressed(KeyCode key)
        {
            return Input.GetKey(key) ? 1f : 0f;
        }
    }
}
