using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    [Header("Character")]
    [SerializeField]
    float moveSpeed = 6f;
    [SerializeField, Range(0f, 10f)]
    float sightDistance = 2f;
    [SerializeField, Range(0f, 4f)]
    float earingDistance = 1f;
    [SerializeField, Range(0f, 2f)]
    float killDistance = .5f;
    [SerializeField]
    LayerMask sightMask;

    [Header("Navigation")]
    [SerializeField]
    float wpDistance = .4f;

    [SerializeField]
    List<Transform> waypoints = new List<Transform>();

    int current = 0;
    bool canWalk = false;
    bool invertedPatrol = false;

    Animator anim;
    Rigidbody2D rb;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        Controller2D.Instance.OnHide += PlayerHides;
        Controller2D.Instance.OnShow += PlayerShows;

        anim.SetBool("IsMoving", true);
    }

    void PlayerHides()
    {
        if (CanSeePlayer())
            knowsPlayerPosition = true;
    }

    void PlayerShows()
    {
        knowsPlayerPosition = false;
    }

    bool knowsPlayerPosition = false;
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

            if (CanSeePlayer() || knowsPlayerPosition)
            {
                if (IsOnPlayerPath(waypoints[KeepInRange(current + (invertedPatrol ? 1 : -1))].position))
                {
                    invertedPatrol = !invertedPatrol;
                    ProgressCurrentIndex();
                }

                if (Vector2.Distance(rb.position, Controller2D.Pos) < Vector2.Distance(rb.position, waypoints[current].position))
                {
                    move = (Controller2D.Pos - rb.position).normalized;
                }
            }

            if (!Mathf.Approximately(move.x, 0f))
            {
                transform.localScale = new Vector3(move.x > 0f ? -1f : 1f, 1f, 1f);
            }

            rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, waypoints[current].position) < wpDistance)
            {
                ProgressCurrentIndex();
            }
        }

        if (!Controller2D.Instance.IsHidden && Vector2.Distance(rb.position, Controller2D.Pos) < killDistance)
        {
            if (!GameOver.Instance.State)
                Controller2D.Instance.Death();
        }
    }

    bool IsOnPlayerPath(Vector2 pos)
    {
        return Vector2.Distance(rb.position, pos) > Vector2.Distance(Controller2D.Pos, pos);
    }

    void ProgressCurrentIndex()
    {
        current = KeepInRange(current + (invertedPatrol ? -1 : +1));
    }

    int KeepInRange(int index)
    {
        if (index < 0)
            return index + waypoints.Count;
        else if (index >= waypoints.Count)
            return index - waypoints.Count;

        return index;
    }

    bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Controller2D.Pos - rb.position, Mathf.Infinity, sightMask);
        if (hit.rigidbody.gameObject.CompareTag("Player"))
        {
            var playerDistance = Vector2.Distance(rb.position, Controller2D.Pos);
            if ((playerDistance < sightDistance && Controller2D.Instance.LanternState) ||
                (playerDistance < earingDistance))
            {
                return !Controller2D.Instance.IsHidden;
            }
        }

        return false;
    }

    public void EnableMovement() { canWalk = true; }
    public void DisableMovement() { canWalk = false; }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, sightDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, earingDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, killDistance);

        if (CanSeePlayer())
        {
            Gizmos.color = Color.green;

            if (knowsPlayerPosition)
                Gizmos.color = Color.yellow;

            Gizmos.DrawLine(rb.position, Controller2D.Pos);
        }
    }
}
