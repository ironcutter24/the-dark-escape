using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Controller2D>();

        bool isUnderCover = player != null && player.transform.position.y > transform.position.y;
        player.SetCover(isUnderCover);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Controller2D>();

        if (player != null)
        {
            player.SetCover(false);
        }
    }
}
