using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    Vector2 offset;

    void LateUpdate()
    {
        Vector3 newPos = target.transform.position;
        newPos.x += offset.x;
        newPos.y += offset.y;
        newPos.z = transform.position.z;
        transform.position = newPos;
    }
}
