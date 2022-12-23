using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Patterns;

public class Controller2D : Singleton<Controller2D>
{
    [SerializeField]
    Animator anim;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float moveSpeed = 6f;

    [SerializeField, ReadOnly]
    bool isUnderCover = false;

    public bool LanternState { get; private set; } = true;

#if UNITY_EDITOR
    [SerializeField, ReadOnly]
    bool isHidden = false;
#endif

    public bool IsHidden => isUnderCover && !LanternState;

    Vector2 move = Vector2.zero;

    public event System.Action<bool> OnLightStateChange;


    public static Vector2 Pos => Instance.transform.position;

    public event System.Action OnHide;
    public event System.Action OnShow;
    bool wasHidden = false;
    void Update()
    {
        if (GameOver.Instance.State) return;

        if (IsHidden && !wasHidden)
        {
            OnHide();
        }
        if (!IsHidden && wasHidden)
        {
            OnShow();
        }
        wasHidden = IsHidden;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Interact
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLantern();
        }

#if UNITY_EDITOR
        isHidden = IsHidden;
#endif

        move = new Vector2(
            GetAxis(KeyCode.A, KeyCode.D),
            GetAxis(KeyCode.S, KeyCode.W)
            ).normalized;

        move.y *= .8f;
    }

    void FixedUpdate()
    {
        if (GameOver.Instance.State) return;

        if (!Mathf.Approximately(move.x, 0f))
        {
            transform.localScale = new Vector3(move.x < 0f ? -1f : 1f, 1f, 1f);
        }

        if (Mathf.Approximately(move.magnitude, 0f))
        {
            if (stepsCoroutine != null)
            {
                StopCoroutine(stepsCoroutine);
                stepsCoroutine = null;
            }
        }
        else
        if (stepsCoroutine == null)
            stepsCoroutine = StartCoroutine(_StepsSounds());

        rb.MovePosition(rb.position + move * GetMoveSpeed() * Time.deltaTime);

        anim.SetBool("IsWalking", move.magnitude > 0f);
    }

    Coroutine stepsCoroutine;
    IEnumerator _StepsSounds()
    {
        const float stepDuration = .666f;

        while (true)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Footsteps");
            yield return new WaitForSeconds(stepDuration);
        }
    }

    float GetMoveSpeed()
    {
        float speed = moveSpeed;

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.LeftShift))
            speed = moveSpeed * 3f;

        if (Input.GetKey(KeyCode.G))
            Death();
#endif

        return speed;
    }

    void ToggleLantern()
    {
        LanternState = !LanternState;
        OnLightStateChange(LanternState);

        if (LanternState)
            FMODUnity.RuntimeManager.PlayOneShot("event:/LanternLit");
        else
            FMODUnity.RuntimeManager.PlayOneShot("event:/LanternBlown");
    }

    public void SetCover(bool state)
    {
        isUnderCover = state;
    }

    public void Death()
    {
        Debug.Log("Game Over");

        if (stepsCoroutine != null)
        {
            StopCoroutine(stepsCoroutine);
            stepsCoroutine = null;
        }

        GameManager.Instance.LoadDeathScene();
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
