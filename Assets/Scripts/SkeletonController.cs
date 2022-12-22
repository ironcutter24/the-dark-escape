using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    [Header("Character")]
    [SerializeField]
    float moveSpeed = 6f;

    [Header("Navigation")]
    [SerializeField]
    float wpDistance = .4f;

    [SerializeField]
    List<Transform> waypoints = new List<Transform>();

    int current = 0;
    bool canWalk = false;

    Animator anim;
    Rigidbody2D rb;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        anim.SetBool("IsMoving", true);
    }

    void FixedUpdate()
    {
        anim.SetBool("IsMoving", canWalk);

        if (waypoints.Count == 0 || !anim.GetBool("IsMoving"))
        {
            anim.SetBool("IsMoving", false);
            return;
        }

        if (anim.GetBool("IsMoving"))
        {
            Vector2 move = waypoints[current].position - transform.position;
            move.Normalize();

            if (!Mathf.Approximately(move.x, 0f))
            {
                transform.localScale = new Vector3(move.x > 0f ? -1f : 1f, 1f, 1f);
            }

            rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, waypoints[current].position) < wpDistance)
            {
                current++;

                if (current >= waypoints.Count)
                    current -= waypoints.Count;
            }
        }
    }

    public void EnableMovement() { canWalk = true; }
    public void DisableMovement() { canWalk = false; }
}
